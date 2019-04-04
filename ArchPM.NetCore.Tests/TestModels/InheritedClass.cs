using System.Collections.Generic;

namespace ArchPM.NetCore.Tests.TestModels
{
    public class InheritedClass : BaseClass
    {
        public bool IsForwarding { get; set; }
        public string Addressing { get; set; }
    }

    public class BaseClass
    {
        public string Name { get; set; }

        public List<string> AdditionalLines { get; set; } = new List<string>();

        public string State { get; set; }

        public string County { get; set; }

        public string CountryIsoAlpha2Code { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public string Street { get; set; }
    }

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

    public class ClassHavingNoArguments
    {
        public string Type { get; set; }

        public string Value { get; set; }

        public int Priority { get; set; } = 0;
    }

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