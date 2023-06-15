using System;
using System.Collections.Generic;

namespace ArchPM.NetCore.Extensions
{
    [Serializable]
    public class EntityPropertyInfo
    {
        public string Name { get; internal set; }
        public object Value { get; internal set; }
        public string ValueTypeName { get; internal set; }
        public Type ValueType { get; internal set; }
        public bool IsNullable { get; internal set; }
        public bool IsPrimitive { get; internal set; }
        public bool IsClass { get; internal set; }
        public bool IsEnum { get; internal set; }
        public bool IsList { get; internal set; }
        public bool IsArray { get; internal set; }
        public IEnumerable<Attribute> Attributes { get; internal set; }

        public override string ToString()
        {
            return $"{Name} [{ValueTypeName}] [{(IsNullable ? "IsNullable" : "")}]";
        }
    }
}
