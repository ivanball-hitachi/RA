using System;
using System.Collections.Generic;
using System.Linq;

namespace Generators.Model
{
    internal class PropertyDeclaration : IEquatable<PropertyDeclaration?>
    {
        public PropertyDeclaration(string name, string dataType, IEnumerable<AttributeDeclaration>? attributes = null)
        {
            Name = name;
            DataType = dataType;
            Attributes = attributes;
        }

        public string Name { get; }
        public string DataType { get; }
        public IEnumerable<AttributeDeclaration>? Attributes { get; }

        public override bool Equals(object? obj)
        {
            return Equals(obj as PropertyDeclaration);
        }

        public bool Equals(PropertyDeclaration? other)
        {
            return other is not null &&
                   Name == other.Name &&
                   DataType == other.DataType &&
                   Attributes.SequenceEqual(other.Attributes);
        }

        public override int GetHashCode()
        {
            int hashCode = -1540434333;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DataType);
            return hashCode;
        }
    }
}
