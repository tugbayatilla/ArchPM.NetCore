using System;

namespace ArchPM.NetCore.Tests.ClassExtensionSampleClassStructure
{
    public class OrderDeliveryPropertiesSOM
    {
        public DateTime ReceiptDateRequested { get; set; }

        public DateTime ReceiptDateConfirmed { get; set; }

        public DateTime ShippingDateRequested { get; set; }

        public DateTime ShippingDateConfirmed { get; set; }

        public string DeliveryTerm { get; set; }

        public string DeliveryTermDescription { get; set; }

        public string DeliveryMode { get; set; }

        public bool IsJustInTime { get; set; }

        public string Carrier { get; set; }

        public string CarrierDescription { get; set; }

        public string FinalDestination { get; set; }

        public string UnloadingPoint { get; set; }

        public bool IsHolidayDelivery { get; set; }

        public bool IsSpeedService { get; set; }

        // ???
        public decimal IGUDLVDATEUP { get; set; }
    }
}