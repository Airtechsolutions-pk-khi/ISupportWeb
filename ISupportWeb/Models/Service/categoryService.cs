﻿using ISupportWeb.Models.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ISupportWeb.Models.Service
{
    public class categoryService : baseService
    {
        categoryBLL _service;
        public categoryService()
        {
            _service = new categoryBLL();
        }

        public List<categoryBLL> GetAll()
        {
            try
            {
                return _service.GetAll();
            }
            catch (Exception ex)
            {
                return new List<categoryBLL>();
            }
        }

    }
}