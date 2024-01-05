using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebAPICode.Helpers;

namespace ISupportWeb.Models.BLL
{
    public class filterBLL
    {
        public string? Category { get; set; } = "";
        public string? SubCategory { get; set; } = "";
        public string? Color { get; set; } = "";
        public string? MinPrice { get; set; } = "";
        public string? MaxPrice { get; set; } = "";
        public string? Searchtxt { get; set; } = "";
        public int SortID { get; set; } = 1;
        public int ServiceID { get; set; } = 0;
        public int ItemID { get; set; } = 0;
        public string? Name { get; set; } = "";
        public string? ArabicName { get; set; } = "";
        public string? SKU { get; set; } = "";
        public string? Description { get; set; } = "";
        public double? Cost { get; set; } = 0;
        public double? Price { get; set; } = 0;
        public double? DiscountedPrice { get; set; } = 0;
        public string? Barcode { get; set; } = "";       
        public string? Image { get; set; } = "";
        public string? Type { get; set; } = "";
        public string? Termsandcondition { get; set; } = "";
        public int? StatusID { get; set; } = 1;
        public int? DisplayOrder { get; set; } = 0;

        public DateTime? CreationDate { get; set; }= DateTime.UtcNow.AddMinutes(180);
        //public DateTime? UpdatedDate { get; set; } = DateTime.UtcNow.AddMinutes(180);
        //public int? UpdatedBy { get; set; } = 0;

        public static DataTable _dt;
        public static DataSet _ds;
        public List<filterBLL> GetAll(filterBLL data)
        {
            var lst = new List<filterBLL>();
            try
            {                
                SqlParameter[] p = new SqlParameter[6];
                p[0] = new SqlParameter("@Category", data.Category == "" ? null : data.Category);
                p[1] = new SqlParameter("@Color", data.Color == "" ? null : data.Color);
                //p[2] = new SqlParameter("@SubCategory", data.SubCategory == "" ? null : data.SubCategory);
                p[2] = new SqlParameter("@MinPrice", float.Parse(data.MinPrice.Replace("BHD.","").Replace("BHD", "")));
                p[3] = new SqlParameter("@MaxPrice", float.Parse(data.MaxPrice.Replace("BHD.", "").Replace("BHD", "")));
                p[4] = new SqlParameter("@Searchtxt", data.Searchtxt == "" ? null : data.Searchtxt);
                p[5] = new SqlParameter("@SortID", data.SortID);
                
                _ds = (new DBHelper().GetDatasetFromSP)("sp_filterService_Web", p);
                if (_ds != null)
                {   
                    if (_ds.Tables.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_ds.Tables[0])).ToObject<List<filterBLL>>();
                    }
                }
                if (data.SortID == 2)
                {
                    lst = lst.OrderByDescending(x => x.Name).ToList();
                }
                else if (data.SortID == 3)
                {
                    lst = lst.OrderBy(x => x.Price).ToList();
                }
                else if (data.SortID == 4)
                {
                    lst = lst.OrderByDescending(x => x.Price).ToList();
                }
                else if (data.SortID == 1)
                {
                    lst = lst.OrderBy(x => x.Name).ToList();
                }
                else
                {

                }
                return lst;
            }
            
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<filterBLL> GetAllProduct(filterBLL data)
        {
            try
            {
                var lst = new List<filterBLL>();
                SqlParameter[] p = new SqlParameter[6];
                p[0] = new SqlParameter("@Category", data.Category == "" ? null : data.Category);
                p[1] = new SqlParameter("@Color", data.Color == "" ? null : data.Color);
                //p[2] = new SqlParameter("@SubCategory", data.SubCategory == "" ? null : data.SubCategory);
                p[2] = new SqlParameter("@MinPrice", float.Parse(data.MinPrice.Replace("BHD.","").Replace("BHD", "")));
                p[3] = new SqlParameter("@MaxPrice", float.Parse(data.MaxPrice.Replace("BHD.", "").Replace("BHD", "")));
                p[4] = new SqlParameter("@Searchtxt", data.Searchtxt == "" ? null : data.Searchtxt);
                p[5] = new SqlParameter("@SortID", data.SortID);

                _ds = (new DBHelper().GetDatasetFromSP)("sp_filterProduct_Web", p);
                if (_ds != null)
                {
                    if (_ds.Tables.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_ds.Tables[0])).ToObject<List<filterBLL>>();
                    }
                }
                if (data.SortID == 2)
                {
                    lst = lst.OrderByDescending(x => x.Name).ToList();
                }
                else if (data.SortID == 3)
                {
                    lst = lst.OrderBy(x => x.Price).ToList();
                }
                else if (data.SortID == 4)
                {
                    lst = lst.OrderByDescending(x => x.Price).ToList();
                }
                else if (data.SortID == 1)
                {
                    lst = lst.OrderBy(x => x.Name).ToList();
                }
                else
                {

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