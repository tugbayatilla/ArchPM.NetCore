namespace ArchPM.NetCore.Tests.ClassExtensionSampleClassStructure
{
    public class ClassHavingAnotherClassAsProperty
    {
        public InheritedClass Address { get; set; }

        public string Id { get; set; }

        public string Fax { get; set; }

        public string Phone { get; set; }

        public string VatNumber { get; set; }

        public string CustomerGroup { get; set; }

        public string CustomerReference { get; set; }

        // IGUCUSTALTORDERACCOUNT
        public string AlternativeCustomerId { get; set; }
    }
}