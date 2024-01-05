using ISupportWeb.Models.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ISupportWeb.Models.Service
{
    public class filterService : baseService
    {
        filterBLL _service;
        public filterService()
        {
            _service = new filterBLL();
        }

        public List<filterBLL> GetAll(filterBLL filter)
        {
            try
            {
                var a = _service.GetAll(filter);
                return a;
            }
            catch (Exception ex)
            {
                return new List<filterBLL>();
            }
        }

    }
}