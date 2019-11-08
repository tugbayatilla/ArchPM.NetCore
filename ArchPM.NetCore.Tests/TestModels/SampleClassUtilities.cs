// ReSharper disable All
namespace ArchPM.NetCore.Tests.TestModels
{
    /// <summary>
    /// 
    /// </summary>
    public class SampleClassUtilities
    {
        public const string ConstantName = nameof(ConstantName);
        public const float ConstantFloat = 3.14f;

        public int? IntNullable { get; set; } = 1;

        public string Name2 { get; set; } = nameof(Name2);
        public string Name3 { get; } = nameof(Name3);
        public string Name4 { set; private get; } = nameof(Name4);
        public string Name5 { set { value = nameof(Name5); } }
        internal string Name6 { get; set; } = nameof(Name6);
    }
}
