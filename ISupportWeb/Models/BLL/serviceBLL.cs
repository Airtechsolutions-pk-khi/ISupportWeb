using ISupportWeb.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WebAPICode.Helpers;


namespace ISupportWeb.Models.BLL
{
    public class serviceBLL
    {

        public int ServiceID { get; set; }

        public int? ServiceCategoryID { get; set; }

        public string Name { get; set; }

        public string ArabicName { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string AlternateImage { get; set; }

        public decimal? Cost { get; set; }

        public decimal? Price { get; set; }

        public decimal? DiscountedPrice { get; set; }

        public string SKU { get; set; }

        public string Barcode { get; set; }

        public string Type { get; set; }

        public int? DisplayOrder { get; set; }

        public int? StatusID { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public string Termsandcondition { get; set; }


        public static DataTable? _dt;
        public static DataSet? _ds;

         

        public List<itemBLL> GetAll()
        {
            try
            {
                var lst = new List<itemBLL>();
                SqlParameter[] p = new SqlParameter[0];

                _dt = (new DBHelper().GetTableFromSP)("sp_getItemList_Web", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<itemBLL>>();
                        //lst = _dt.DataTableToList<itemBLL>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<itemBLL> GetSelecteditems()
        {
            try
            {
                var lst = new List<itemBLL>();
                _dt = (new DBHelper().GetTableFromSP)("sp_itemListselected");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<itemBLL>>();
                        //lst = _dt.DataTableToList<itemBLL>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<itemBLL> GetAllFeatured()
        {
            try
            {
                var lst = new List<itemBLL>();
                _dt = (new DBHelper().GetTableFromSP)("sp_Featureditems");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst= JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<itemBLL>>();
                        //lst = _dt.DataTableToList<itemBLL>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<itemBLL> GetAllPopular(int? cityID)
        {
            try
            {
                var lst = new List<itemBLL>();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@CityID", cityID);
                _dt = (new DBHelper().GetTableFromSP)("sp_PopularProducts_V2",p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<itemBLL>>();
                        //lst = _dt.DataTableToList<itemBLL>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<itemBLL> GetAllValentineDay()
        {
            try
            {
                var lst = new List<itemBLL>();
                _dt = (new DBHelper().GetTableFromSP)("sp_ValentineDaySpecial");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<itemBLL>>();
                        //lst = _dt.DataTableToList<itemBLL>();
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