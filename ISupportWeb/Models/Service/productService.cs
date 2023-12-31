﻿using ISupportWeb.Models.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISupportWeb.Models.Service
{
    public class productService : baseService
    {
        productBLL _service;
        serviceBLL _serviceS;
        public productService()
        {
            _service = new productBLL();
            _serviceS = new serviceBLL();
        }

        public productBLL GetAll(int ItemID)
        {
            try
            {
                return _service.GetAll(ItemID);
            }
            catch (Exception ex)
            {
                return new productBLL();
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