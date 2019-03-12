using System.Collections.Generic;

namespace ArchPM.NetCore.Tests.ClassExtensionSampleClassStructure
{
    public class AddressSOM
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
}