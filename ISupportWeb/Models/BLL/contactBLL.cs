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
    public class contactBLL
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Message { get; set; }

    }
    public partial class Technician
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string CPRNumber { get; set; }
        public string Address { get; set; }
    }
}