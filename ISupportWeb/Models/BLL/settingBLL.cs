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
 
    public class settingBLL
    {
        public int SettingID { get; set; }
        public double? DeliveryCharges { get; set; }
        public double? ServiceCharges { get; set; }
        public double? OtherCharges { get; set; }
        public double? TaxPercentage { get; set; }
        public double? MinimumOrderValue { get; set; }
        public double? ServiceBookingCharges { get; set; }
        public double? ConsultancyCharges { get; set; }
        public string? Currency { get; set; }
        public bool? COD { get; set; }
        public bool? PayPal { get; set; }
        public bool? CardOnDelivery { get; set; }
        public bool? BenefitPay { get; set; }
        public int? StatusID { get; set; }
        public int? CompanyID { get; set; }
        public double? DiscountPercentage { get; set; }
        public bool? IsAppUpdate { get; set; }
        
         
         
        public static DataTable _dt;
        public static DataSet _ds;
        public settingBLL GetSettings()
        {
            try
            {
                var obj = new settingBLL();
                SqlParameter[] p = new SqlParameter[0];
                _ds = (new DBHelper().GetDatasetFromSP)("sp_GetSettings", p);
                if (_ds != null)
                {
                    if (_ds.Tables.Count > 0)
                    {
                        if (_ds.Tables[0] != null)
                        {
                            obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_ds.Tables[0])).ToObject<List<settingBLL>>().FirstOrDefault();
                        }
                        
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