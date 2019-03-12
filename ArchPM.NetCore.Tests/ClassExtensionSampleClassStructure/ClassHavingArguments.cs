namespace ArchPM.NetCore.Tests.ClassExtensionSampleClassStructure
{
    public class ClassHavingArguments
    {
        public ClassHavingArguments(string type, string value)
        {
            this.Type = type;
            this.Value = value;
        }

        public string Type { get; set; }

        public string Value { get; set; }

        public int Priority { get; set; } = 0;
    }
}
