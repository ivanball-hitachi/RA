using Generators.Model;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generators;

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
            static (ctx, source) => Execute(ctx, source));

        context.RegisterPostInitializationOutput(
            static (ctx) => PostInitializationOutput(ctx));
    }

    private static bool IsSyntaxTarget(SyntaxNode node)
    {
        return node is ClassDeclarationSyntax classDeclarationSyntax
            && classDeclarationSyntax.AttributeLists.Count > 0;
    }

    private static ClassToGenerate? GetSemanticTarget(GeneratorSyntaxContext context)
    {
        var classDeclarationSyntax = (ClassDeclarationSyntax)context.Node;
        var classSymbol = context.SemanticModel.GetDeclaredSymbol(classDeclarationSyntax);
        var attributeSymbol = context.SemanticModel.Compilation.GetTypeByMetadataName(
            "Generators.GenerateDTOClassAttribute");

        //Using the Semantic model
        if (classSymbol is not null && attributeSymbol is not null)
        {
            foreach (var attributeData in classSymbol.GetAttributes())
            {
                if (attributeSymbol.Equals(attributeData.AttributeClass,
                    SymbolEqualityComparer.Default))
                {
                    var namespaceName = classSymbol.ContainingNamespace.ToDisplayString();
                    var className = classSymbol.Name;
                    var propertyDeclarations = new List<PropertyDeclaration>();

                    foreach (var memberSymbol in classSymbol.GetMembers())
                    {
                        if (memberSymbol.Kind == SymbolKind.Property
                            && memberSymbol.DeclaredAccessibility == Accessibility.Public)
                        {
                            var attributes = memberSymbol.GetAttributes();

                            if (!attributes.Any(attr => 
                                attr.AttributeClass!.Name.Equals("ExcludeFromCodeGeneration") ||
                                attr.AttributeClass!.Name.Equals("ExcludeFromCodeGenerationAttribute")))
                            {
                                var typeSymbol = (memberSymbol as IPropertySymbol)!.Type;

                                var attributeDeclarations = new List<AttributeDeclaration>();

                                foreach (var attribute in attributes) 
                                {
                                    var name = attribute.AttributeClass!.Name.EndsWith("Attribute")? 
                                        attribute.AttributeClass!.Name.Replace("Attribute", ""): 
                                        attribute.AttributeClass!.Name;
                                    var propertyName = attribute.NamedArguments.FirstOrDefault(arg => arg.Key.Equals("PropertyName")).Value.Value!.ToString();
                                    var dataType = attribute.NamedArguments.FirstOrDefault(arg => arg.Key.Equals("DataType")).Value.Value!.ToString();
                                    attributeDeclarations.Add(new AttributeDeclaration(name, propertyName, dataType));
                                }

                                propertyDeclarations.Add(new PropertyDeclaration(memberSymbol.Name, typeSymbol.ToString(), attributeDeclarations));
                            }
                        }
                    }

                    return new ClassToGenerate(namespaceName, className, propertyDeclarations);
                }
            }
        }

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

    private static Dictionary<string, int> _countPerFileName = new();

    private static void Execute(SourceProductionContext context, ClassToGenerate? classToGenerate)
    {
        if (classToGenerate is null)
        {
            return;
        }

        var namespaceName = classToGenerate.NamespaceName;
        var className = classToGenerate.ClassName;
        var dtoClassName = $"{className}DTO";
        var fileName = $"{dtoClassName}.g.cs";
        var propertiesForBaseDTOClass = new List<PropertyDeclaration>();
        var propertiesForDTOClass = new List<PropertyDeclaration>();

        if (_countPerFileName.ContainsKey(fileName))
        {
            _countPerFileName[fileName]++;
        }
        else
        {
            _countPerFileName.Add(fileName, 1);
        }

        foreach (var propertyDeclaration in classToGenerate.PropertyDeclarations!)
        {
            var generateInDTOClassAttribute = propertyDeclaration.Attributes.FirstOrDefault(attr => attr.Name.Equals("GenerateInDTOClass"));

            if (generateInDTOClassAttribute is null)
            {
                propertiesForBaseDTOClass.Add(new PropertyDeclaration(propertyDeclaration.Name, propertyDeclaration.DataType));
            }
            else
            {
                propertiesForDTOClass.Add(new PropertyDeclaration(generateInDTOClassAttribute.PropertyName, generateInDTOClassAttribute.DataType));
            }
        }

        var stringBuilder = new StringBuilder();
        stringBuilder.Append($@"// Generation count: {_countPerFileName[fileName]}
using Domain.Common;

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
