using System.Collections.Generic;

namespace ArchPM.NetCore.Tests.ClassExtensionSampleClassStructure
{
    public class OrderInvoicePropertiesSOM
    {
        public string PostofficeBox { get; set; }
        public string ZipPostofficeBox { get; set; }
        public string ShipToIdBRG { get; set; }
        public string ShipToIdECS { get; set; }

        // ax internal id for address entry, commented out
        //public decimal ShipToId { get; set; }
        public string FinalDestination { get; set; }

        public string UnloadingPoint { get; set; }
        public bool HolidayDelivery { get; set; }
        public InheritedClass Forwarding { get; set; }

        public AddressSOM Address { get; set; }
        public string LanguageId { get; set; }

        public List<ClassHavingNoArguments> CommunicationEntries { get; set; }

        public FinancialPropertiesSOM Fiscal { get; set; }
    }
}