using NUnit.Framework;

namespace ArchPM.NetCore.Tests.TestModels
{
    internal class SampleArrayPropertyClass
    {
        public string[] StringArray { get; set; }
        public byte[] ByteArray { get; set; }
        public int[] IntArray { get; set; }
        public bool[] BoolArray { get; set; }
        public SampleArrayPropertyEnum[] EnumArray { get; set; }
        public SampleDataClass[] SampleDataClassArray { get; set; }
        public SampleRecursiveListClass.A Recursive_A { get; set; }
    }

    internal enum SampleArrayPropertyEnum
    {
        Item1,
        Item2
    }
}
