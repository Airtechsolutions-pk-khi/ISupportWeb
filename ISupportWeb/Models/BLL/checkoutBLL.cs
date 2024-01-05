using ISupportWeb.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using WebAPICode.Helpers;

namespace ISupportWeb.Models.BLL
{
    public class checkoutBLL1
    {
        public string? CustomerName { get; set; }
    }
        public class checkoutBLL
    {
        public int? OrderID { get; set; }
        public string? TransactionNo { get; set; }
        public string? OrderNo { get; set; }
        public int? CustomerID { get; set; } 
        public string? Customer { get; set; }
        public int? ItemsQty { get; set; } 
        public int OrderDetailType { get; set; } 
        public double? ItemsTotal { get; set; } 
        public double? ProductTaxPercent { get; set; } 
        public double? ServiceTaxPercent { get; set; } 
        public double? ServiceDiscountPercent { get; set; } 
        public double? ItemDiscountPercent { get; set; } 
        public int? ServicesQty { get; set; } 
        public double? ServicesTotal { get; set; } 
        public double? Discount { get; set; } 
        public double? TotalDiscount { get; set; } 
        public double? TotalTax { get; set; } 
        public double? ServiceDiscount { get; set; } 
        public double? ItemDiscount { get; set; } 
        public double? AmountTotal { get; set; } 
        public double? CostTotal { get; set; } 
        public double? BookingCharges { get; set; } 
        public double? DeliveryCharges { get; set; } 
        public double? Tax { get; set; } 
        public double? GrandTotal { get; set; } 
        public double? ProductTax { get; set; } 
        public double? ServiceTax { get; set; } 
        public double? TotalProductAmount { get; set; } 
        public double? TotalServiceAmount { get; set; } 
        public DateTime? OrderDate { get; set; } = DateTime.UtcNow.AddMinutes(180);
        public int? StatusID { get; set; } 
        public DateTime? CreationDate { get; set; } = DateTime.UtcNow.AddMinutes(180);
        public DateTime? UpdatedDate { get; set; } = DateTime.UtcNow.AddMinutes(180);
        public int? UpdatedBy { get; set; } 
        
       
        /*Cust Order Info*/
        
            public int OrderInfoID { get; set; } 
             
            public int? PaymentMethodID { get; set; } 
            public string? PaymentMethod { get; set; }
            public string? CustomerName { get; set; }
            public string? Email { get; set; }
            public string? ContactNo { get; set; }
            public string? Longitude { get; set; }
            public string? Latitude { get; set; }
            public DateTime? DeliveryDate { get; set; } = DateTime.UtcNow.AddMinutes(180);
            //public TimeSpan? DeliveryTime { get; set; }
            public string? Address { get; set; }
            public string? NearestPlace { get; set; }
            public string? City { get; set; }
            public string? Country { get; set; }
            public string? PostalCode { get; set; }
            public string? PaymentStatus { get; set; }
            public string? CardType { get; set; }
            public string? CardNo { get; set; }
            public string? CVC { get; set; }
            public string? RefNo { get; set; }
            public string? ExpiryDate { get; set; }
        public string? OrderDetailString { get; set; }
        public List<OrderDetails> OrderDetail = new List<OrderDetails>();

        /*Order Details*/
        public class OrderDetails
        {
            public int OrderDetailID { get; set; } 
            public int? OrderID { get; set; } 
            public int? ItemID { get; set; } 
            public int? DealID { get; set; } 
            public string? Item { get; set; }
            public string? ItemImage { get; set; }
            public int? ServiceID { get; set; } 
            public string? Service { get; set; }
            public string? ServiceImage { get; set; }
            public string? Problem { get; set; }
            public string[]? ImageForGuidance { get; set; }
            public string[]? ServiceImagesForGuidance { get; set; }
            
            public string? Type { get; set; }
            public int? Quantity { get; set; } 
            public double? Price { get; set; } 
            public double? Cost { get; set; } 
            public double? Discount { get; set; } 
            public double? RefundAmount { get; set; } 
            public double? RefundQty { get; set; } 
            public string ServiceTime { get; set; }
            public string ServiceDate { get; set; }
            public int? StatusID { get; set; } 
            public DateTime? CreationDate { get; set; } = DateTime.UtcNow.AddMinutes(180);
            public DateTime? UpdatedDate { get; set; } = DateTime.UtcNow.AddMinutes(180);
            public int? UpdatedBy { get; set; } 
        }
        /*Order Details*/
        
        public static DataTable? _dt;
        
        public int InsertOrder(checkoutBLL obj)
        {

            try
            {
                SqlParameter[] p = new SqlParameter[29];
                p[0] = new SqlParameter("@TransactionNo", obj.TransactionNo);
                p[1] = new SqlParameter("@OrderNo", obj.OrderNo);
                p[2] = new SqlParameter("@CustomerID", obj.CustomerID);
                p[3] = new SqlParameter("@ItemsQty", obj.ItemsQty);
                p[4] = new SqlParameter("@ItemsTotal", obj.ItemsTotal);
                p[5] = new SqlParameter("@ServicesQty", obj.ServicesQty);
                p[6] = new SqlParameter("@ServicesTotal", obj.ServicesTotal);
                p[7] = new SqlParameter("@ServiceDiscount", obj.ServiceDiscount);
                p[8] = new SqlParameter("@ItemDiscount", obj.ItemDiscount);
                p[9] = new SqlParameter("@AmountTotal", obj.AmountTotal);
                p[10] = new SqlParameter("@CostTotal", obj.CostTotal);
                p[11] = new SqlParameter("@BookingCharges", obj.BookingCharges);
                if (obj.ItemsTotal > 10)
                {
                    p[12] = new SqlParameter("@DeliveryCharges", 0);
                }
                else
                {
                    p[12] = new SqlParameter("@DeliveryCharges", obj.DeliveryCharges);
                }
                p[13] = new SqlParameter("@Tax", obj.Tax);
                p[14] = new SqlParameter("@GrandTotal", obj.GrandTotal);
                p[15] = new SqlParameter("@OrderDate", obj.OrderDate);
                p[16] = new SqlParameter("@ProductTax", obj.ProductTax);
                p[17] = new SqlParameter("@ServiceTax", obj.ServiceTax);
                p[18] = new SqlParameter("@TotalProductAmount", obj.TotalProductAmount);
                p[19] = new SqlParameter("@TotalServiceAmount", obj.TotalServiceAmount);
                p[20] = new SqlParameter("@StatusID", obj.StatusID);
                p[21] = new SqlParameter("@CreatedDate", obj.CreationDate);
                p[22] = new SqlParameter("@ProductTaxPercent", obj.ProductTaxPercent);
                p[23] = new SqlParameter("@ServiceTaxPercent", obj.ServiceTaxPercent);
                p[24] = new SqlParameter("@ServiceDiscountPercent", obj.ServiceDiscountPercent);
                p[25] = new SqlParameter("@ItemDiscountPercent", obj.ItemDiscountPercent);
                p[26] = new SqlParameter("@Discount", obj.Discount);
                p[27] = new SqlParameter("@TotalTax", obj.TotalTax);
                p[28] = new SqlParameter("@TotalDiscount", obj.TotalDiscount);

                obj.OrderID = int.Parse((new DBHelper().GetDatasetFromSP)("sp_InsertOrderMaster_API", p).Tables[0].Rows[0][0].ToString());

                foreach (var odt in obj.OrderDetail)
                {
                    var od = new OrderDetails();
                    if (obj.OrderDetail.Count != 0)
                    {
                        SqlParameter[] q = new SqlParameter[16];

                        q[0] = new SqlParameter("@OrderID", obj.OrderID);
                        q[1] = new SqlParameter("@ItemID", odt.ItemID);
                        q[2] = new SqlParameter("@ServiceID", odt.ServiceID);
                        q[3] = new SqlParameter("@DealID", odt.DealID);
                        q[4] = new SqlParameter("@Type", odt.Type);
                        q[5] = new SqlParameter("@Quantity", odt.Quantity);
                        q[6] = new SqlParameter("@Price", odt.Price);
                        q[7] = new SqlParameter("@Cost", odt.Cost);
                        q[8] = new SqlParameter("@Discount", odt.Discount);
                        q[9] = new SqlParameter("@RefundAmount", odt.RefundAmount);
                        q[10] = new SqlParameter("@RefundQty", odt.RefundQty);
                        q[11] = new SqlParameter("@ServiceDate", odt.ServiceDate);
                        q[12] = new SqlParameter("@ServiceTime", odt.ServiceTime);
                        q[13] = new SqlParameter("@StatusID", 1);
                        q[14] = new SqlParameter("@CreationDate", obj.CreationDate);
                        q[15] = new SqlParameter("@Problem", odt.Problem);

                        od.OrderDetailID = int.Parse((new DBHelper().GetDatasetFromSP)("sp_InsertOrderDetail_API", q).Tables[0].Rows[0][0].ToString());
                    }
                   
                }
                //var oi = new OrderInfoBLL();
                SqlParameter[] r = new SqlParameter[21];

                r[0] = new SqlParameter("@OrderID", obj.OrderID);
                r[1] = new SqlParameter("@PaymentMethodID", obj.PaymentMethodID);
                r[2] = new SqlParameter("@CustomerName", obj.CustomerName);
                r[3] = new SqlParameter("@Email", obj.Email);
                r[4] = new SqlParameter("@ContactNo", obj.ContactNo);
                r[5] = new SqlParameter("@DeliveryDate", obj.DeliveryDate);
                r[6] = new SqlParameter("@DeliveryTime", "");
                r[7] = new SqlParameter("@Address", obj.Address);
                r[8] = new SqlParameter("@NearestPlace", obj.NearestPlace);
                r[9] = new SqlParameter("@City", obj.City);
                r[10] = new SqlParameter("@Country", obj.Country);                
                r[11] = new SqlParameter("@PostalCode", obj.PostalCode);
                r[12] = new SqlParameter("@PaymentStatus", obj.PaymentStatus);
                r[13] = new SqlParameter("@CardType", obj.CardType);
                r[14] = new SqlParameter("@CardNo", obj.CardNo);
                r[15] = new SqlParameter("@CVC", obj.CVC);
                r[16] = new SqlParameter("@RefNo", obj.RefNo);
                r[17] = new SqlParameter("@ExpiryDate", obj.ExpiryDate);
                r[18] = new SqlParameter("@StatusID", 1);
                r[19] = new SqlParameter("@Latitude", obj.Latitude);
                r[20] = new SqlParameter("@longitude", obj.Longitude);

                obj.OrderID = int.Parse((new DBHelper().GetDatasetFromSP)("sp_InsertOrderInfo_API", r).Tables[0].Rows[0][0].ToString());

               
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 1;
        }
        public int OrderUpdate(int OrderID, int StatusID)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@OrderID", OrderID);
                p[1] = new SqlParameter("@StatusID", StatusID);
                rtn = (new DBHelper().ExecuteNonQueryReturn)("sp_OrderReject", p);

                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}