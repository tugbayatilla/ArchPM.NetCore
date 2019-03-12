using System;

namespace ArchPM.NetCore.Tests.ClassExtensionSampleClassStructure
{
    public class PositionDeliverySOM
    {
        public DateTime ReceiptDateRequested { get; set; }

        public DateTime ReceiptDateConfirmed { get; set; }

        public DateTime ShippingDateRequested { get; set; }

        public DateTime ShippingDateConfirmed { get; set; }

        public string ConfirmedShippingWeek { get; set; }
    }
}