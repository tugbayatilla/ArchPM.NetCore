using System;

namespace ArchPM.NetCore.Tests.ClassExtensionSampleClassStructure
{
    public class OrderPaymentSOM
    {
        // ISO 4217
        public string CurrencyCode { get; set; }

        // Zahlungsbedingungen
        public string PaymentConditions { get; set; }

        // CashDisc:Skontobedingugen
        public string CashDiscount { get; set; }

        // Zahlungs und Skontobedingungen
        public string MerlinPaymentId { get; set; }

        public string TaxGroup { get; set; }

        // customer are grouped, within a group the same prices are calculated for the same products
        public string PriceGroupId { get; set; }

        public string VatNumber { get; set; }

        // Zuschlag
        public string MarkupGroup { get; set; }

        //endgültiger kauf/Gutschrift
        public string TransactionCode { get; set; }

        public bool IsPaidOnline { get; set; }

        public string PricesFromCustomerReference { get; set; }

        public DateTime PurchaseOrderReceiptDate { get; set; }
    }
}