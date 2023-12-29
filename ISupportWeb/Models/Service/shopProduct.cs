using ISupportWeb.Models.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISupportWeb.Models.Service
{
    public class shopProduct : baseService
    {
        productShopBLL _service;
        public shopProduct()
        {
            _service = new productShopBLL();
        }

        public List<productShopBLL> GetAll(string Category)
        {
            try
            {
                return _service.GetAll(Category);
            }
            catch (Exception ex)
            {
                return new List<productShopBLL>();
            }
        }
        public List<productShopBLL> BestProducts()
        {
            try
            {
                return _service.BestProducts();
            }
            catch (Exception ex)
            {
                return new List<productShopBLL>();
            }
        }
    }
}