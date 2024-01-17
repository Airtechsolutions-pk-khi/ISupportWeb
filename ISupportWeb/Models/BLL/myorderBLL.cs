using ISupportWeb.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WebAPICode.Helpers;

namespace ISupportWeb.Models.BLL
{
    public class myorderBLL
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

        public string? ServiceTime { get; set; }
        public string? ServiceDate { get; set; }
        public string? Problem { get; set; }
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
            public string? ItemName { get; set; }
            public string? ServiceName { get; set; }
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
            public string? ServiceTime { get; set; }
            public string? ServiceDate { get; set; }
            public int? StatusID { get; set; }
            public DateTime? CreationDate { get; set; } = DateTime.UtcNow.AddMinutes(180);
            public DateTime? UpdatedDate { get; set; } = DateTime.UtcNow.AddMinutes(180);
            public int? UpdatedBy { get; set; }
        }
         
        public static DataTable _dt;
        public static DataSet _ds;
        //public List<myorderBLL> GetAll(int CustomerID)
        //{
        //    try
        //    {
        //        var lst = new List<myorderBLL>();
        //        SqlParameter[] p = new SqlParameter[1];
        //        p[0] = new SqlParameter("@CustomerID", CustomerID);
        //        _ds = (new DBHelper().GetDatasetFromSP)("sp_GetMyOrders",p);
        //        if (_ds != null)
        //        {
        //            if (_ds.Tables.Count > 0)
        //            {
        //                //lst = _dt.DataTableToList<myorderBLL>();
        //                lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_ds.Tables[0])).ToObject<List<myorderBLL>>().ToList();
        //            }
        //        }
        //        return lst;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        public myorderBLL GetDetails(int? OrderID)
        {
            try
            {
                var obj = new myorderBLL();
                List<OrderDetails> lstIM = new List<OrderDetails>();
                
                SqlParameter[] p = new SqlParameter[1];

                p[0] = new SqlParameter("@OrderID", OrderID);
                _ds = (new DBHelper().GetDatasetFromSP)("sp_GetMyOrderDetails_Web", p);
                if (_ds != null)
                {
                    if (_ds.Tables.Count > 0)
                    {
                        if (_ds.Tables[0] != null)
                        {
                            obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_ds.Tables[0])).ToObject<List<myorderBLL>>().FirstOrDefault();
                        }
                        if (_ds.Tables[1] != null)
                        {
                            lstIM = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_ds.Tables[1])).ToObject<List<OrderDetails>>();
                        }
                         
                        obj.OrderDetail = lstIM;
                         
                    }
                }
                return obj;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}