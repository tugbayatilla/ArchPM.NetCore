using System;
using System.Collections.Generic;

namespace ArchPM.NetCore.Tests.ClassExtensionSampleClassStructure
{
    public class PositionComparer : IEqualityComparer<PositionSOM>
    {
        public bool Equals(PositionSOM x, PositionSOM y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(PositionSOM obj)
        {
            return obj.QMAFASTSALESPOSNR.GetHashCode();
        }
    }


    public class PositionSOM : IEquatable<PositionSOM>
    {
        public PositionSOM()
        {
            DrawingTableList = new List<DrawingTableItemSOM>();
            Payment = new PositionPaymentSOM();
        }

        // Relations

        public List<DrawingTableItemSOM> DrawingTableList { get; set; }

        // Fields
        public string OrderId { get; set; }

        //public DateTime? FixDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public DateTime ReceiptDateRequested { get; set; }

        public DateTime ReceiptDateConfirmed { get; set; }

        public PositionPaymentSOM Payment { get; set; }

        public PositionDeliverySOM Delivery { get; set; }

        public string ProductId { get; set; }

        public int PiecesRemain { get; set; }

        public string SalesStatus { get; set; }

        public decimal QuantityOrdered { get; set; }

        public decimal QuantityToDeliverNow { get; set; }

        public decimal QuantityRemainSalesPhysical { get; set; }

        public decimal QuantityRemainSalesFinancial { get; set; }

        public string Unit { get; set; }

        public string UnitText { get; set; }

        public string Dimension { get; set; }

        public string Dimension2 { get; set; }

        public string Dimension3 { get; set; }

        public decimal PositionNumber { get; set; }

        public decimal TotalWeight { get; set; }

        public decimal WeightPerUnit { get; set; }

        public string DepartmentId { get; set; }

        public string EXTERNALITEMID { get; set; }
        public decimal LINEPERCENT { get; set; }
        public decimal LINEAMOUNT { get; set; }
        public string INVENTTRANSID { get; set; }
        public decimal SALESQTY { get; set; }
        public decimal SALESMARKUP { get; set; }
        public string SALESTYPE { get; set; }
        public decimal REMAININVENTPHYSICAL { get; set; }

        public string TAXITEMGROUP { get; set; }
        public decimal UNDERDELIVERYPCT { get; set; }
        public decimal OVERDELIVERYPCT { get; set; }
        public string INVENTREFTRANSID { get; set; }
        public decimal INVENTREFTYPE { get; set; }
        public string INVENTREFID { get; set; }
        public string ITEMBOMID { get; set; }
        public string INVENTDIMID { get; set; }
        public string BLANKETREFTRANSID { get; set; }
        public string IGSSTANDARDIGUSORDER { get; set; }
        public decimal EBCLENGTH { get; set; }
        public decimal EBCNBRTOSUPPLY { get; set; }
        public decimal EBCTOTLENGTH { get; set; }
        public bool EBCSKIPATP { get; set; }
        public bool IGULEADINGITEM { get; set; }
        public string IGSPRIORITYSTR { get; set; }
        public decimal QMABLINDDISCOUNT { get; set; }
        public string QMAFASTSALESPOSNR { get; set; }
        public decimal IGUNUMBER { get; set; }
        public decimal IGULINK { get; set; }
        public decimal IGUMERLININDEX { get; set; }
        public bool IGUDAYDELIVERY { get; set; }
        public decimal IGSSTANDARDPRICE { get; set; }
        public string IGULEADINGSERIAL { get; set; }
        public decimal IGUDRYLINC5 { get; set; }
        public decimal IGUDRYLINC6 { get; set; }
        public string IGUCUSTOMERIDENTNUMBER { get; set; }
        public bool IGUSPECIALITEM { get; set; }
        public decimal IGSLASTSALESPRICE { get; set; }
        public string IGSTECHBLOCK { get; set; }
        public string IGUDRAWING { get; set; }
        public bool IGUCLARIFICATION { get; set; }
        public string HSOBLANKETORDERTYPE { get; set; }
        public string IGUBLOCKID { get; set; }
        public DateTime IGSFIXEDDELIVERYDATE { get; set; }
        public string IGUINVOICEDETAILST { get; set; }
        public bool IGUMNA { get; set; }
        public string IGUASSEMBLYINSTRUCTIONSEKS { get; set; }
        public decimal IGUSTARTINGAT { get; set; }
        public bool IGSEBCLEANORDERBLOCK { get; set; }
        public string IGUSETUPCOSTITEMASSIGNMENT { get; set; }
        public decimal IGULINENUMINT { get; set; }
        public bool IGUSKIPSALESVALUEUPDATE { get; set; }
        public string IGUF2NUM { get; set; }
        public string IGSSALESDESCRIPTIONFIELD2 { get; set; }
        public string IGSSALESDESCRIPTIONFIELD1 { get; set; }
        public bool IGSISLONGTRAVEL { get; set; }
        public bool IGUPRICEBREAK { get; set; }
        public decimal OPENLINEAMOUNT { get; set; }
        public decimal UNITMERLIN { get; set; }
        public string LANGTEXT { get; set; }
        public string MerlinPosIndex { get; set; }
        public decimal FROMPOS_ADD { get; set; }
        public decimal TOPOS_ADD { get; set; }
        public bool GROUPACROSS_ADD { get; set; }
        public bool LONGWAY_ADD { get; set; }
        public string INVENTREFTRANSID_ADD { get; set; }
        public string IGUQUOTATIONID_ADD { get; set; }
        public string IGSITEMIDORIG_ADD { get; set; }
        public decimal IGSBATCHSIZE_ADD { get; set; }
        public decimal IGSDEMANDY_ADD { get; set; }
        public DateTime IGSDATEOFFIRSTCALL_ADD { get; set; }
        public DateTime IGSACTUALRECEIPTDATECONFIRMED_ADD { get; set; }
        public string PARENTREFTRANSID_ADD { get; set; }
        public decimal SUPPITEMTABLEREFRECID_ADD { get; set; }
        public string IGSLOTIDSUBSIDIARY_ADD { get; set; }
        public string DIFFDEPARTMENTID_ADD { get; set; }
        public string IGSDEPARTMENTID_ADD { get; set; }
        public bool IGUINSERTINSAMEGROUP_ADD { get; set; }

        public bool Equals(PositionSOM other)
        {
            return string.Equals(QMAFASTSALESPOSNR,
                other.QMAFASTSALESPOSNR,
                StringComparison.InvariantCultureIgnoreCase
            );
        }

        /// <summary>
        ///     Returns the price per unit including discounts.
        /// </summary>
        /// <returns></returns>
        public virtual decimal GetNewPricePerUnit(bool round = false)
        {
            var newPrice = Payment.SalesPrice;
            if (QMABLINDDISCOUNT != 0 || LINEPERCENT != 0)
            {
                // Blind discount is intended to be round.
                newPrice = Math.Round(newPrice * ((100 - QMABLINDDISCOUNT) / 100), 2);

                newPrice = newPrice * ((100 - LINEPERCENT) / 100);
                newPrice = newPrice < 0 ? 0 : newPrice;
            }

            return round ? Math.Round(newPrice, 2) : newPrice;
        }

        /// <summary>
        ///     Returns the total price including discounts.
        /// </summary>
        /// <returns></returns>
        public virtual decimal GetTotalPrice(bool round = false)
        {
            var totalPrice = EBCNBRTOSUPPLY * EBCLENGTH * GetNewPricePerUnit(false);

            return round ? Math.Round(totalPrice, 2) : totalPrice;
        }

        /// <summary>
        ///     Returns the unit text depending on the id in <see cref="Unit" />.
        /// </summary>
        /// <returns></returns>
        public virtual string GetQuantityUnit()
        {
            switch (Unit)
            {
                case "1":
                    return "M";
                case "2":
                    return "Lnk";
                case "4":
                    return "Set";
                case "3":
                case "5":
                default:
                    return "Pcs";
            }
        }

        public virtual bool IsCompleteDelivered()
        {
            return QuantityRemainSalesPhysical == 0;
        }

        public virtual bool IsBefore(PositionSOM beforePosition)
        {
            return IGULINENUMINT < beforePosition.IGULINENUMINT;
        }

        public virtual bool IsAfter(PositionSOM beforePosition)
        {
            return IGULINENUMINT > beforePosition.IGULINENUMINT;
        }
    }
}