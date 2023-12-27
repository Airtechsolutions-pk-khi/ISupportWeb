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
    
    public class navigationBLL
    {
        public int ServiceCategoryID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string ArabicName { get; set; }
        public int StatusID { get; set; }

        public static DataTable _dt;
        public static DataSet _ds;
        
       

        public List<navigationBLL> GetServiceCategory()
        {
            try
            {
                var ServiceCategories = new List<navigationBLL>();
                
                SqlParameter[] p = new SqlParameter[0];
                _dt = (new DBHelper().GetTableFromSP)("sp_ServiceNavigation_Web", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        ServiceCategories = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<navigationBLL>>();
                    }
                }
                
                return ServiceCategories;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
    }

}