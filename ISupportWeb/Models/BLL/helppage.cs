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
    public class HelpPageBLL
    {
        public int? TicketID { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public string TicketNo { get; set; }
        public int? TicketStatus { get; set; }
        public int? StatusID { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}