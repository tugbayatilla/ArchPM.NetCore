using System;
using System.Collections.Generic;

namespace ArchPM.NetCore.Tests.TestModels
{
    public class SampleDataClass
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
        public Int16 Int16Property { get; set; }
        public Int16? Int16PropertyNullable { get; set; }
        public Int64 Int64Property { get; set; }
        public Int64? Int64PropertyNullable { get; set; }
        public uint UIntProperty { get; set; }
        public uint? UIntPropertyNullable { get; set; }
        public UInt16 UInt16Property { get; set; }
        public UInt16? UInt16PropertyNullable { get; set; }
        public UInt64 UInt64Property { get; set; }
        public UInt64? UInt64PropertyNullable { get; set; }
        public decimal DecimalProperty { get; set; }
        public decimal? DecimalPropertyNullable { get; set; }
        public float FloatProperty { get; set; }
        public float? FloatPropertyNullable { get; set; }
        public double DoubleProperty { get; set; }
        public double? DoublePropertyNullable { get; set; }
        public Single SingleProperty { get; set; }
        public Single? SinglePropertyNullable { get; set; }
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

    public class SampleDataSimpleSubClass
    {
        public int IntProperty { get; set; }
        public string StringProperty { get; set; }
        public string NoSetterProperty { get; }
    }

    public class SampleDataInheritedSubClass : SampleDataSimpleSubClass
    {
        public DateTime DateTimeProperty { get; set; }
    }

    public class SampleDataPrimitiveConstructorSubClass 
    {
        public SampleDataPrimitiveConstructorSubClass(DateTime dateTime)
        {
            this.DateTimeProperty = dateTime;
        }

        public DateTime DateTimeProperty { get; set; }
    }

    public class SampleDataPrivateConstructorSubClass
    {
        private SampleDataPrivateConstructorSubClass()
        {
        }

        public DateTime DateTimeProperty { get; set; }
    }

    

}
