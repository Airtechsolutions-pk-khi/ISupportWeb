using ISupportWeb.Models.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISupportWeb.Models.Service
{
    public class shopService : baseService
    {
        shopBLL _service;
        serviceBLL _serviceS;

        public shopService()
        {
            _service = new shopBLL();
            _serviceS = new serviceBLL();
        }

        public List<shopBLL> GetAll(string Category)
        {
            try
            {
                return _service.GetAll(Category);
            }
            catch (Exception ex)
            {
                return new List<shopBLL>();
            }
        }
        public List<shopBLL> BestServices()
        {
            try
            {
                return _service.BestServices();
            }
            catch (Exception ex)
            {
                return new List<shopBLL>();
            }
        }
        public serviceBLL GetAllService(int ServiceID)
        {
            try
            {
                return _serviceS.GetAll(ServiceID);
            }
            catch (Exception ex)
            {
                return new serviceBLL();
            }
        }
    }
}