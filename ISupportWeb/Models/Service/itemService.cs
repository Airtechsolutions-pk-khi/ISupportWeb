using ISupportWeb.Models.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISupportWeb.Models.Service
{
    public class itemService : baseService
    {
        itemBLL _service;
        public itemService()
        {
            _service = new itemBLL();
        }

        public List<itemBLL> GetAll()
        {
            try
            {
                return _service.GetAll();
            }
            catch (Exception ex)
            {
                return new List<itemBLL>();
            }
        }
        public List<itemBLL> GetSelecteditems()
        {
            try
            {
                return _service.GetSelecteditems();
            }
            catch (Exception ex)
            {
                return new List<itemBLL>();
            }
        }
        public List<itemBLL> GetAllFeatured()
        {
            try
            {
                return _service.GetAllFeatured();
            }
            catch (Exception ex)
            {
                return new List<itemBLL>();
            }
        }

        public List<itemBLL> GetAllPopular(int? cityID)
        {
            try
            {
                return _service.GetAllPopular(cityID);
            }
            catch (Exception ex)
            {
                return new List<itemBLL>();
            }
        }

        public List<itemBLL> GetAllValentineDay()
        {
            try
            {
                return _service.GetAllValentineDay();
            }
            catch (Exception ex)
            {
                return new List<itemBLL>();
            }
        }

    }
}