using ISupportWeb.Models.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ISupportWeb.Models.Service
{
    public class cityService : baseService
    {
        cityBLL _service;
        public cityService()
        {
            _service = new cityBLL();
        }

        public List<cityBLL> GetAll()
        {
            try
            {
                return _service.GetAll();
            }
            catch (Exception ex)
            {
                return new List<cityBLL>();
            }
        }

    }
}