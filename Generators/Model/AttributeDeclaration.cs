    using System;
using System.Collections.Generic;

namespace Generators.Model
{
    internal class AttributeDeclaration : IEquatable<AttributeDeclaration?>
    {
        public AttributeDeclaration(string name, string propertyName, string dataType)
        {
            Name = name;
            PropertyName = propertyName;
            DataType = dataType;
        }

        public string Name { get; }
        public string PropertyName { get; }
        public string DataType { get; }

        public override bool Equals(object? obj)
        {
            return Equals(obj as AttributeDeclaration);
        }

        public bool Equals(AttributeDeclaration? other)
        {
            return other is not null &&
                   Name == other.Name &&
                   PropertyName == other.PropertyName &&
                   DataType == other.DataType;
        }

        public override int GetHashCode()
        {
            int hashCode = -1688666378;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PropertyName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DataType);
            return hashCode;
        }
    }
}
