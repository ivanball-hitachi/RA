using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Text;

namespace Generators;

internal record struct PropertyDeclaration
{
    public string Name { get; set; }
    public string DataType { get; set; }
}

[Generator]
public class DTOClassGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var classes = context.SyntaxProvider.CreateSyntaxProvider(
            predicate: static (node, _) => IsSyntaxTarget(node),
            transform: static (ctx, _) => GetSemanticTarget(ctx))
            .Where(static (target) => target is not null);

        context.RegisterSourceOutput(classes,
            static (ctx, source) => Execute(ctx, source!));

        context.RegisterPostInitializationOutput(
            static (ctx) => PostInitializationOutput(ctx));
    }

    private static bool IsSyntaxTarget(SyntaxNode node)
    {
        return node is ClassDeclarationSyntax classDeclarationSyntax
            && classDeclarationSyntax.AttributeLists.Count > 0;
    }

    private static AttributeSyntax? GetAttribute(SyntaxList<AttributeListSyntax> attributeLists, string attributeName)
    {
        foreach (var attributeListSyntax in attributeLists)
        {
            foreach (var attributeSyntax in attributeListSyntax.Attributes)
            {
                var attributeSyntaxName = attributeSyntax.Name.ToString();

                if (attributeSyntaxName == attributeName
                    || attributeSyntaxName == $"{attributeName}Attribute")
                {
                    return attributeSyntax;
                }
            }
        }

        return null;
    }
    private static string? GetAttributeArgument(AttributeArgumentListSyntax? argumentList, string attributeArgumentName)
    {
        if (argumentList == null) return null;

        foreach (var attributeArgumentSyntax in argumentList.Arguments)
        {
            var attributeArgumentSyntaxName = attributeArgumentSyntax?.NameEquals?.Name.Identifier.Text;

            if (attributeArgumentSyntaxName == attributeArgumentName)
            {
                return ((Microsoft.CodeAnalysis.CSharp.Syntax.LiteralExpressionSyntax)attributeArgumentSyntax?.Expression!).Token.ValueText;
            }
        }

        return null;
    }

    private static ClassDeclarationSyntax? GetSemanticTarget(GeneratorSyntaxContext context)
    {
        var classDeclarationSyntax = (ClassDeclarationSyntax)context.Node;

        if (GetAttribute(classDeclarationSyntax.AttributeLists, "GenerateDTOClass") is not null) return classDeclarationSyntax;

        return null;
    }

    private static void PostInitializationOutput(IncrementalGeneratorPostInitializationContext context)
    {
        context.AddSource("Generators.GenerateDTOClassAttribute.g.cs",
            @"namespace Generators
{
    internal class GenerateDTOClassAttribute : System.Attribute { }

    internal class ExcludeFromCodeGeneration : System.Attribute { }

    internal class GenerateInDTOClassAttribute : System.Attribute
    {
        public GenerateInDTOClassAttribute(string propertyName = """", string dataType = ""string"")
        {
            this.PropertyName = propertyName;
            this.DataType = dataType;
        }

        public string PropertyName { get; set; } = """";
        public string DataType { get; set; } = ""string"";
    }
}");
    }

    private static void Execute(SourceProductionContext context, ClassDeclarationSyntax classDeclarationSyntax)
    {
        if (classDeclarationSyntax.Parent
            is BaseNamespaceDeclarationSyntax namespaceDeclarationSyntax)
        {
            var namespaceName = namespaceDeclarationSyntax.Name.ToString();
            var className = classDeclarationSyntax.Identifier.Text;
            var dtoClassName = $"{className}DTO";
            var fileName = $"{dtoClassName}.g.cs";
            var propertiesForBaseDTOClass = new List<PropertyDeclaration>();
            var propertiesForDTOClass = new List<PropertyDeclaration>();

            foreach (var memberDeclarationSyntax in classDeclarationSyntax.Members)
            {
                if (memberDeclarationSyntax
                    is PropertyDeclarationSyntax propertyDeclarationSyntax
                    && propertyDeclarationSyntax.Modifiers.Any(SyntaxKind.PublicKeyword))
                {
                    if (GetAttribute(propertyDeclarationSyntax.AttributeLists, "ExcludeFromCodeGeneration") is null)
                    {
                        var generateInDTOClassAttribute = GetAttribute(propertyDeclarationSyntax.AttributeLists, "GenerateInDTOClass");

                        if (generateInDTOClassAttribute is null)
                        {
                            propertiesForBaseDTOClass.Add(new PropertyDeclaration
                            {
                                Name = propertyDeclarationSyntax.Identifier.Text.Trim(),
                                DataType = propertyDeclarationSyntax.Type.GetText().ToString().Trim()
                            });
                        }
                        else
                        {
                            var propertyNameArgumentValue = GetAttributeArgument(generateInDTOClassAttribute.ArgumentList, "PropertyName");
                            var dataTypeArgumentValue = GetAttributeArgument(generateInDTOClassAttribute.ArgumentList, "DataType");

                            if (!string.IsNullOrEmpty(propertyNameArgumentValue) && !string.IsNullOrEmpty(dataTypeArgumentValue))
                            propertiesForDTOClass.Add(new PropertyDeclaration
                            {
                                Name = propertyNameArgumentValue!,
                                DataType = dataTypeArgumentValue!
                            });
                        }
                    }
                }
            }

            var stringBuilder = new StringBuilder();
            stringBuilder.Append($@"using Domain.Common;

namespace {namespaceName}.DTO
{{
    public partial class {className}BaseDTO : AuditableDTO
    {{
");
            foreach (var propertyDeclaration in propertiesForBaseDTOClass)
            {
                if (propertyDeclaration.DataType.Contains("?")) stringBuilder.Append($@"#nullable enable
");
                stringBuilder.Append($@"        public {propertyDeclaration.DataType} {propertyDeclaration.Name} {{ get; set; }} = default!;
");
                if (propertyDeclaration.DataType.Contains("?")) stringBuilder.Append($@"#nullable restore
");
            }
            stringBuilder.Append($@"    }}

    public partial class {className}DTO : {className}BaseDTO, IBaseEntity<int>
    {{
        public int Id {{ get; set; }}
");
            foreach (var propertyDeclaration in propertiesForDTOClass)
            {
                if (propertyDeclaration.DataType.Contains("?")) stringBuilder.Append($@"#nullable enable
");
                stringBuilder.Append($@"        public {propertyDeclaration.DataType} {propertyDeclaration.Name} {{ get; set; }}
");
                if (propertyDeclaration.DataType.Contains("?")) stringBuilder.Append($@"#nullable restore
");
            }
            stringBuilder.Append($@"    }}

    public partial class {className}ForCreationDTO : {className}BaseDTO
    {{
    }}

    public partial class {className}ForUpdateDTO : {className}BaseDTO, IBaseEntity<int>
    {{
        public int Id {{ get; set; }}
    }}
}}");

            context.AddSource(fileName, stringBuilder.ToString());
        }
    }
}
