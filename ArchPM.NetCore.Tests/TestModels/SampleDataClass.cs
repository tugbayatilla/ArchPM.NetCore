using System;
using System.Collections.Generic;
// ReSharper disable All

namespace ArchPM.NetCore.Tests.TestModels
{
    internal class SampleDataClass
    {
        public List<SampleDataSimpleSubClass> SampleDataSimpleSubClassList { get; set; }
        public IList<SampleDataSimpleSubClass> SampleDataSimpleSubClassIListInterface { get; set; }
        public ICollection<SampleDataSimpleSubClass> SampleDataSimpleSubClassICollectionInterface { get; set; }
        public IEnumerable<SampleDataSimpleSubClass> SampleDataSimpleSubClassIEnumerableInterface { get; set; }

        #region Classes

        public SampleDataSimpleSubClass SampleDataSimpleSubClass { get; set; }
        public SampleDataInheritedSubClass SampleDataInheritedSubClass { get; set; }
        public SampleDataPrimitiveConstructorSubClass SampleDataPrimitiveConstructorSubClass { get; set; }
        public SampleDataPrivateConstructorSubClass SampleDataPrivateConstructorSubClass { get; set; } 
        
        #endregion

        #region Primitives / Value Types

        public int IntProperty { get; set; }
        public int? IntPropertyNullable { get; set; }
        public short Int16Property { get; set; }
        public short? Int16PropertyNullable { get; set; }
        public long Int64Property { get; set; }
        public long? Int64PropertyNullable { get; set; }
        public uint UIntProperty { get; set; }
        public uint? UIntPropertyNullable { get; set; }
        public ushort UInt16Property { get; set; }
        public ushort? UInt16PropertyNullable { get; set; }
        public ulong UInt64Property { get; set; }
        public ulong? UInt64PropertyNullable { get; set; }
        public decimal DecimalProperty { get; set; }
        public decimal? DecimalPropertyNullable { get; set; }
        public float FloatProperty { get; set; }
        public float? FloatPropertyNullable { get; set; }
        public double DoubleProperty { get; set; }
        public double? DoublePropertyNullable { get; set; }
        public float SingleProperty { get; set; }
        public float? SinglePropertyNullable { get; set; }
        public byte ByteProperty { get; set; }
        public byte? BytePropertyNullable { get; set; }
        public DateTime DateTimeProperty { get; set; }
        public DateTime? DateTimePropertyNullable { get; set; }
        public bool BooleanProperty { get; set; }
        public bool? BooleanPropertyNullable { get; set; }
        public string StringProperty { get; set; } = "";
        public string StringPropertyNullable { get; set; } = null;
        public Guid GuidProperty { get; set; }
        public Guid GuidPropertyNullable { get; set; } = Guid.Empty;

        #endregion
    }

    internal class SampleDataSimpleSubClass
    {
        public int IntProperty { get; set; }
        public string StringProperty { get; set; }
        // ReSharper disable once UnusedMember.Global
        // ReSharper disable once UnassignedGetOnlyAutoProperty
        public string NoSetterProperty { get; }
    }

    internal class SampleDataInheritedSubClass : SampleDataSimpleSubClass
    {
        public DateTime DateTimeProperty { get; set; }
    }

    internal class SampleDataPrimitiveConstructorSubClass 
    {
        public SampleDataPrimitiveConstructorSubClass(DateTime dateTime)
        {
            DateTimeProperty = dateTime;
        }

        public DateTime DateTimeProperty { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class SampleDataPrivateConstructorSubClass
    {
        private SampleDataPrivateConstructorSubClass()
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class SampleDataClassForEquality
    {
        public int IntValue { get; set; }

        public override bool Equals(object obj)
        {
            return IntValue == (obj as SampleDataClassForEquality)?.IntValue;
        }

        protected bool Equals(SampleDataClassForEquality other)
        {
            return IntValue == other.IntValue;
        }

        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return IntValue.GetHashCode();
        }
    }

    internal class SampleDataWithDictionary
    {
        public Dictionary<object,object> DictionaryProperty { get; set; }
        public Dictionary<string,int> DictionaryStringIntProperty { get; set; }
    }
    

}
