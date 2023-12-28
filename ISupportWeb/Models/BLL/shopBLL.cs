using ISupportWeb.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WebAPICode.Helpers;
//using static java.util.Locale;

namespace ISupportWeb.Models.BLL
{
    public class shopBLL
    {
        public int ServiceID { get; set; }
        public string Name { get; set; }
        public string ArabicName { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public string Cost { get; set; }
        public double? Price { get; set; }
        public double? DiscountedPrice { get; set; }                
        public string Image { get; set; }        
        public int? StatusID { get; set; }
        public int? DisplayOrder { get; set; }
        public bool? IsFeatured { get; set; }
        public int? StockQty { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? Row_Counter { get; set; }
        

        public static DataTable _dt;
        public static DataSet _ds;
        public List<shopBLL> GetAll(string Category)
        {
            try
            {
                var lst = new List<shopBLL>();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@Category", Category);
                _dt = (new DBHelper().GetTableFromSP)("sp_GetServiceShopList_Web", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<shopBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<shopBLL> BestServices()
        {
            try
            {
                var lst = new List<shopBLL>();
                SqlParameter[] p = new SqlParameter[0];
                
                _dt = (new DBHelper().GetTableFromSP)("sp_GetBestServices_Web", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<shopBLL>>(); 
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}