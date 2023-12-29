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
    public class productShopBLL
    {
        public int ItemID { get; set; }
        public int? CategoryID { get; set; }
        public string Name { get; set; }
        public string ArabicName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string AlternateImage { get; set; }
        public double? Cost { get; set; }
        public double? Price { get; set; }
        public double? DiscountedPrice { get; set; }
        public double? Rating { get; set; }
        public string SKU { get; set; }
        public string Barcode { get; set; }
        public string Type { get; set; }
        public int? DisplayOrder { get; set; }
        public int? Inventory { get; set; }
        public bool? InStock { get; set; }
        public int? IsTrending { get; set; }
        public int? IsOffer { get; set; }
        public int? IsBestSeller { get; set; }
        public int? StatusID { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }


        public static DataTable _dt;
        public static DataSet _ds;
        public List<productShopBLL> GetAll(string Category)
        {
            try
            {
                var lst = new List<productShopBLL>();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@Category", Category);
                _dt = (new DBHelper().GetTableFromSP)("sp_GetProductShopList_Web", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<productShopBLL>>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<productShopBLL> BestProducts()
        {
            try
            {
                var lst = new List<productShopBLL>();
                SqlParameter[] p = new SqlParameter[0];
                
                _dt = (new DBHelper().GetTableFromSP)("sp_GetBestProduct_Web", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<productShopBLL>>(); 
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