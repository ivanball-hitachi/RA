using System;
using System.Collections.Generic;
using System.Linq;

namespace Generators.Model
{
    internal class ClassToGenerate : IEquatable<ClassToGenerate?>
    {
        public ClassToGenerate(string namespaceName,
            string className,
            IEnumerable<PropertyDeclaration> propertyDeclarations)
        {
            NamespaceName = namespaceName;
            ClassName = className;
            PropertyDeclarations = propertyDeclarations;
        }

        public string NamespaceName { get; }
        public string ClassName { get; }
        public IEnumerable<PropertyDeclaration> PropertyDeclarations { get; }

        public override bool Equals(object? obj)
        {
            return Equals(obj as ClassToGenerate);
        }

        public bool Equals(ClassToGenerate? other)
        {
            return other is not null &&
                   NamespaceName == other.NamespaceName &&
                   ClassName == other.ClassName &&
                   PropertyDeclarations.SequenceEqual(other.PropertyDeclarations);
        }

        public override int GetHashCode()
        {
            int hashCode = -39551721;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(NamespaceName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ClassName);
            return hashCode;
        }
    }
}
