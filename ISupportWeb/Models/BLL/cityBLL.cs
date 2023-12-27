﻿using ISupportWeb.Models;
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
   
    public class cityBLL
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
        public string StatusID { get; set; }
     

        public static DataTable _dt;
        public static DataSet _ds;
        public List<cityBLL> GetAll()
        {
            try
            {
                var lst = new List<cityBLL>();
                SqlParameter[] p = new SqlParameter[0];
                //p[0] = new SqlParameter("@CityID", cityID);
                _dt = (new DBHelper().GetTableFromSP)("sp_GetAllCities", p);
                
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = _dt.DataTableToList<cityBLL>();
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