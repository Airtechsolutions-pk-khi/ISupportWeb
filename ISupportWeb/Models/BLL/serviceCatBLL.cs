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
    public class serviceCatBLL
    {
        public int ServiceCategoryID { get; set; }
        public int? CategoryID { get; set; }

        public int? CompanyID { get; set; }
        public int? ItemID { get; set; }

        public string Name { get; set; }

        public string ArabicName { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public int? DisplayOrder { get; set; }

        public int? StatusID { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }
        public int Row_Counter { get; set; }

        public static DataTable _dt;
        public static DataSet _ds;
        public List<serviceCatBLL> GetServiceCat()
        {
            try
            {
                var lst = new List<serviceCatBLL>();
                _dt = (new DBHelper().GetTableFromSP)("sp_GetServiceCategoryList_Web");
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = _dt.DataTableToList<serviceCatBLL>();
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