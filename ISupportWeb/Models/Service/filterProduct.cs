using ISupportWeb.Models.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ISupportWeb.Models.Service
{
    public class filterProduct : baseService
    {
        filterBLL _service;
        public filterProduct()
        {
            _service = new filterBLL();
        }

        public List<filterBLL> GetAll(filterBLL filter)
        {
            try
            {
                return _service.GetAll(filter);
            }
            catch (Exception ex)
            {
                return new List<filterBLL>();
            }
        }
        public List<filterBLL> GetAllProduct(filterBLL filter)
        {
            try
            {
                return _service.GetAllProduct(filter);
            }
            catch (Exception ex)
            {
                return new List<filterBLL>();
            }
        }
    }
}