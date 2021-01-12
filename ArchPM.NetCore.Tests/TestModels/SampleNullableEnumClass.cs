using System;
using System.Collections.Generic;
using System.Text;

namespace ArchPM.NetCore.Tests.TestModels
{
    public class SampleNullableEnumClass
    {
        public SampleNullableEnum? SampleNullableEnum { get; set; }
    }

    public enum SampleNullableEnum
    {
        OnlyItem = 1,
        OnlyItem2 = 2,
        
    }
}
