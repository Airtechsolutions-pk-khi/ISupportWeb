using ISupportWeb.Models.BLL;
using ISupportWeb.Models.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using System.Net;
using Newtonsoft.Json.Linq;
using static ISupportWeb.Models.BLL.myorderBLL;
using static ISupportWeb.Models.BLL.checkoutBLL;


namespace ISupportWeb.Controllers
{
    public class OrderController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public OrderController(ILogger<HomeController> logger, IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }
        public IActionResult Cart()
        {
            ViewBag.Banner = new bannerBLL().GetBanner("Home");
            return View();
        }
        public IActionResult Checkout(int? id = -1)
        {             
            ViewBag.Banner = new bannerBLL().GetBanner("Home");
            return View();
            //int? CustomerID = id;
            //if (CustomerID == 0)
            //{

            //    //Session["CustomerID"] = 0;
            //    int? retrievedCustomerID = int.TryParse(HttpContext.Session.GetString("CustomerID"), out int parsedCustomerID) ? parsedCustomerID : (int?)null;
            //    //HttpContext.Session.GetInt32(CustomerID) = 0;
            //    return View();
            //}
            //else
            //{
            //    if (HttpContext.Session.GetString("CustomerID") != null &&
            //Convert.ToInt32(HttpContext.Session.GetString("CustomerID")) != 0)
            //    {
            //        return View();
            //    }
            //    else
            //    {
            //        return RedirectToAction("Login_Register", "Account");
            //    }
            //}

        }
        public IActionResult OrderComplete(int OrderID = 0, string OrderNo = "")
        {

            checkoutBLL check = new checkoutBLL();
            if (OrderNo == "Reject" || OrderID == 0)
            {
                ViewBag.OrderNo = "Reject";
                //check.OrderUpdate(OrderID, 103);//Rejected 
            }
            else
            {
                var data = new myorderBLL().GetDetails(OrderID);

                if (data.PaymentMethodID == 1)
                {
                    //check.OrderUpdate(OrderID, 101);//In progress
                }
                string ToEmail, SubJect, cc, Bcc;
                ToEmail = data.Email;
                SubJect = "Your order on ISupport - " + data.OrderNo;

                string webRootPath = System.IO.Path.Combine(_hostingEnvironment.ContentRootPath, "Template", "emailpattern.txt");
                string BodyEmail = System.IO.File.ReadAllText(webRootPath);

                string webRootPathA = System.IO.Path.Combine(_hostingEnvironment.ContentRootPath, "Template", "emailpattern-admin.txt");
                string BodyEmailadmin = System.IO.File.ReadAllText(webRootPathA);

                //string webRootPath = _hostingEnvironment.WebRootPath;
                //string BodyEmail = System.IO.File.ReadAllText(System.IO.Path.Combine(webRootPath, "Template", "emailpattern.txt"));
                //string BodyEmailadmin = System.IO.File.ReadAllText(System.IO.Path.Combine(webRootPath, "Template", "emailpattern-admin.txt"));

                string items = "";
                
                string ServiceTimeSlot = "";

                //string BodyEmail = System.IO.File.ReadAllText(Server.MapPath("~/Template") + "\\" + "emailpattern.txt");
                //string BodyEmailadmin = System.IO.File.ReadAllText(Server.MapPath("~/Template") + "\\" + "emailpattern-admin.txt");

                foreach (var item in data.OrderDetail)
                {
                    if (item.Type == "Item")
                    {
                       
                        items += "<table border = '0' cellpadding = '0' cellspacing = '0' align = 'center' width = '100%' role = 'module' data - type = 'columns' style = 'padding:0px 0px 0px 5px;' bgcolor = '#FFFFFF'>"
                    + "<tbody>"
                    + "<tr role = 'module-content'>"
                    + "<td height = '100%' valign = 'top'>"
                    + "<table class='column' width='137' style='width:137px; border-spacing:0; border-collapse:collapse; margin:0px 0px 0px 0px;' cellpadding='0' cellspacing='0' align='left' border='0' bgcolor=''>"
                    + "<tbody>"
                    + "<tr>"
                    + "<td style = 'padding:0px;margin:0px;border-spacing:0;'><table class='wrapper' role='module' data-type='image' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='239f10b7-5807-4e0b-8f01-f2b8d25ec9d7'>"
                    + "<tbody>"
                    + "<tr>"
                    + "<td style = 'font-size:6px; line-height:10px; padding:0px 0px 0px 0px;' valign='top' align='left'>"
                    //+ "<img src = '" + System.Configuration.ConfigurationManager.AppSettings["Imageapi"].ToString() + item.ItemImage + "' class='max-width' border='0' style='display:block;width: 108px;height: 108px;object-fit: contain;' alt='' >"
                    + "</td>"
                    + "</tr>"
                    + "</tbody>"
                    + "</table></td>"
                    + "</tr>"
                    + "</tbody>"
                    + "</table>"
                    + "<table class='column' style='display: contents; border-spacing:0; border-collapse:collapse; margin:0px 0px 0px 0px;' cellpadding='0' cellspacing='0' align='left' border='0' bgcolor=''>"
                    + "<tbody>"
                    + "<tr>"
                    + "<td style = 'padding:0px;margin:0px;border-spacing:0;' ><table class='module' role='module' data-type='text' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='f404b7dc-487b-443c-bd6f-131ccde745e2'>"
                    + "<tbody>"
                    + "<tr>"
                    + "<td style = 'padding: 0px 0px 0px 23px; line-height:22px; text-align:inherit;' height='100%' valign='top' bgcolor='' role='module-content'><div>"
                    + "<h3 style = 'margin-bottom: 2px;'> Product </h3>"
                    + "<div style = 'font-family: inherit; text-align: inherit'> Name : " + item.ItemName + "</div>"
                    + "<div style = 'font-family: inherit; text-align: inherit'> Qty : " + data.ItemsQty + "</div>"
                    + "<div style = 'font-family: inherit; padding-bottom:12px; text-align: inherit'><span style='color: #a43a93'> Price : BHD" + item.Price + "</span></div>"
                    + "<div></div></div></td>"
                    + "</tr>"
                    + "</tbody>"
                    + "</table>"
                    + "</td>"
                    + "</tr>"
                    + "</tbody>"
                    + "</table>"
                    + "</td>"
                    + "</tr>"
                    + "</tbody>"
                    + "</table>";
                    }
                    if (item.Type == "Service")
                    {
                       
                        items += "<table border = '0' cellpadding = '0' cellspacing = '0' align = 'center' width = '100%' role = 'module' data - type = 'columns' style = 'padding:0px 0px 0px 5px;' bgcolor = '#FFFFFF'>"
                    + "<tbody>"
                    + "<tr role = 'module-content'>"
                    + "<td height = '100%' valign = 'top'>"
                    + "<table class='column' width='137' style='width:137px; border-spacing:0; border-collapse:collapse; margin:0px 0px 0px 0px;' cellpadding='0' cellspacing='0' align='left' border='0' bgcolor=''>"
                    + "<tbody>"
                    + "<tr>"
                    + "<td style = 'padding:0px;margin:0px;border-spacing:0;'><table class='wrapper' role='module' data-type='image' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='239f10b7-5807-4e0b-8f01-f2b8d25ec9d7'>"
                    + "<tbody>"
                    + "<tr>"
                    + "<td style = 'font-size:6px; line-height:10px; padding:0px 0px 0px 0px;' valign='top' align='left'>"
                    //+ "<img src = '" + System.Configuration.ConfigurationManager.AppSettings["Imageapi"].ToString() + item.ItemImage + "' class='max-width' border='0' style='display:block;width: 108px;height: 108px;object-fit: contain;' alt='' >"
                    + "</td>"
                    + "</tr>"
                    + "</tbody>"
                    + "</table></td>"
                    + "</tr>"
                    + "</tbody>"
                    + "</table>"
                    + "<table class='column' style='display: contents; border-spacing:0; border-collapse:collapse; margin:0px 0px 0px 0px;' cellpadding='0' cellspacing='0' align='left' border='0' bgcolor=''>"
                    + "<tbody>"
                    + "<tr>"
                    + "<td style = 'padding:0px;margin:0px;border-spacing:0;' ><table class='module' role='module' data-type='text' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='f404b7dc-487b-443c-bd6f-131ccde745e2'>"
                    + "<tbody>"
                    + "<tr>"
                    + "<td style = 'padding:0px 0px 0px 23px; line-height:22px; text-align:inherit;' height='100%' valign='top' bgcolor='' role='module-content'><div>"
                    + "<h3 style = 'margin-bottom: 2px;'> Service </h3>"
                    + "<div style = 'font-family: inherit; text-align: inherit'> Name : " + item.ServiceName + "</div>"
                    + "<div style = 'font-family: inherit; text-align: inherit'> Service Date : " + item.ServiceDate + "</div>"
                    + "<div style = 'font-family: inherit; text-align: inherit'> Service Time : " + item.ServiceTime + "</div>"
                    + "<div style = 'font-family: inherit; text-align: inherit'> Qty : " + data.ServicesQty + "</div>"
                    + "<div style = 'font-family: inherit; text-align: inherit'> Problem/Issue : " + item.Problem + "</div>"
                    + "<div style = 'font-family: inherit; text-align: inherit'><span style='color: #a43a93'> Starting Price : BHD" + item.Price + "</span></div>"
                    + "<div></div></div></td>"
                    + "</tr>"
                    + "</tbody>"
                    + "</table>"
                    + "</td>"
                    + "</tr>"
                    + "</tbody>"
                    + "</table>"
                    + "</td>"
                    + "</tr>"
                    + "</tbody>"
                    + "</table>";
                    }


                }
                BodyEmail = BodyEmail.Replace("#ReceiverName#", data.CustomerName);
                BodyEmail = BodyEmail.Replace("#OrderNo#", data.OrderNo);
                BodyEmail = BodyEmail.Replace("#items#", items.ToString());


                BodyEmailadmin = BodyEmailadmin.Replace("#ReceiverName#", data.CustomerName);
                BodyEmailadmin = BodyEmailadmin.Replace("#OrderNo#", data.OrderNo);
                BodyEmailadmin = BodyEmailadmin.Replace("#items#", items.ToString());

                DateTime dateTime = DateTime.UtcNow.AddMinutes(180);
                BodyEmail = BodyEmail.Replace("#Customer#", data.CustomerName.ToString());
                BodyEmail = BodyEmail.Replace("#OrderDate#", dateTime.ToString("dd/MMM/yyyy"));
                BodyEmail = BodyEmail.Replace("#Address#", data.NearestPlace.ToString());

                BodyEmailadmin = BodyEmailadmin.Replace("#Customer#", data.CustomerName.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#CustomerContact#", data.ContactNo.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#OrderDate#", dateTime.ToString("dd/MMM/yyyy"));
                BodyEmailadmin = BodyEmailadmin.Replace("#Address#", data.NearestPlace.ToString());
                string PaymentType = "";
                string PaymentMethod = "";
                if (data.PaymentMethodID != 1)
                {
                    PaymentType = "Benefit Pay";
                    PaymentMethod = "Benefit Pay";

                }
                else
                {
                    PaymentType = "Cash On Delivery";
                    PaymentMethod = "Cash On Delivery";
                }
                BodyEmail = BodyEmail.Replace("#PaymentType#", PaymentType.ToString());
                BodyEmail = BodyEmail.Replace("#PaymentMethod#", PaymentMethod.ToString());
                BodyEmail = BodyEmail.Replace("#ItemsTotal#", data.ItemsTotal.ToString());
                BodyEmail = BodyEmail.Replace("#ServiceTotal#", data.ServicesTotal.ToString());
                BodyEmail = BodyEmail.Replace("#TotalDiscount#", data.TotalDiscount.ToString());
                BodyEmail = BodyEmail.Replace("#SubTotal#", data.AmountTotal.ToString());
                BodyEmail = BodyEmail.Replace("#ItemDiscount#", data.ItemDiscount.ToString() == null ? "0.00" : data.ItemDiscount.ToString());
                BodyEmail = BodyEmail.Replace("#ServiceDiscount#", data.ServiceDiscount.ToString() == null ? "0.00" : data.ServiceDiscount.ToString());
                BodyEmail = BodyEmail.Replace("#Tax#", data.Tax.ToString() == null ? "0.00" : data.Tax.ToString());
                BodyEmail = BodyEmail.Replace("#ProductTax#", data.ProductTax.ToString() == null ? "0.00" : data.ProductTax.ToString());
                BodyEmail = BodyEmail.Replace("#ServiceTax#", data.ServiceTax.ToString() == null ? "0.00" : data.ServiceTax.ToString());


                if (data.ItemsTotal > 10)
                {
                    BodyEmail = BodyEmail.Replace("#DeliveryAmount#", "0.00").ToString();
                }
                else
                {
                    BodyEmail = BodyEmail.Replace("#DeliveryAmount#", data.DeliveryCharges.ToString() == null ? "0.00" : data.DeliveryCharges.ToString());
                }


                BodyEmail = BodyEmail.Replace("#GrandTotal#", data.GrandTotal.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#ItemsTotal#", data.ItemsTotal.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#TotalDiscount#", data.TotalDiscount.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#ServiceTotal#", data.ServicesTotal.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#PaymentType#", PaymentType.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#PaymentMethod#", PaymentMethod.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#SubTotal#", data.AmountTotal.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#ItemDiscount#", data.ItemDiscount.ToString() == null ? "0.00" : data.ItemDiscount.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#ServiceDiscount#", data.ServiceDiscount.ToString() == null ? "0.00" : data.ServiceDiscount.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#Tax#", data.Tax.ToString() == null ? "0.00" : data.Tax.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#ProductTax#", data.ProductTax.ToString() == null ? "0.00" : data.ProductTax.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#ServiceTax#", data.ServiceTax.ToString() == null ? "0.00" : data.ServiceTax.ToString());

                if (data.ItemsTotal > 10)
                {
                    BodyEmailadmin = BodyEmailadmin.Replace("#DeliveryAmount#", "0.00").ToString();
                }
                else
                {
                    BodyEmailadmin = BodyEmailadmin.Replace("#DeliveryAmount#", data.DeliveryCharges.ToString() == null ? "0.00" : data.DeliveryCharges.ToString());
                }   
                
                BodyEmailadmin = BodyEmailadmin.Replace("#GrandTotal#", data.GrandTotal.ToString());
                cc = "";
                //Bcc = ConfigurationManager.AppSettings["From"].ToString();
                Bcc = _configuration["From"].ToString();
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(ToEmail);

                    mail.From = new MailAddress(_configuration["From"].ToString());
                    mail.Subject = SubJect;
                    string Body = BodyEmail;
                    mail.Body = Body;
                    mail.IsBodyHtml = true;

                    SmtpClient smtp = new SmtpClient();
                    smtp.UseDefaultCredentials = false;
                    smtp.Port = int.Parse(_configuration["SmtpPort"].ToString());
                    smtp.Host = _configuration["SmtpServer"].ToString(); //Or Your SMTP Server Address
                    smtp.Credentials = new System.Net.NetworkCredential
                         (_configuration["From"].ToString(), _configuration["Password"].ToString());
                    //Or your Smtp Email ID and Password
                    smtp.EnableSsl = false;

                    smtp.Send(mail);
                    ViewBag.Status = "Order Invoice will be sent to your Email.";
                }
                 
                catch (Exception ex)
                {
                    ViewBag.Status = "";
                }
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(_configuration["From"].ToString());
                    mail.From = new MailAddress(_configuration["From"].ToString());
                    mail.Subject = "NEW ORDER | " + SubJect;
                    string Body = BodyEmailadmin;
                    mail.Body = Body;
                    mail.IsBodyHtml = true;

                    SmtpClient smtp = new SmtpClient();
                    smtp.UseDefaultCredentials = false;
                    smtp.Port = int.Parse(_configuration["SmtpPort"].ToString());
                    smtp.Host = _configuration["SmtpServer"].ToString(); //Or Your SMTP Server Address
                    smtp.Credentials = new System.Net.NetworkCredential
                         (_configuration["From"].ToString(), _configuration["Password"].ToString());
                    //Or your Smtp Email ID and Password
                    smtp.EnableSsl = false;

                    smtp.Send(mail);
                }
                
                catch (Exception)
                {
                }
                ViewBag.OrderNo = data.OrderNo;
            }

            return View();
        }

         
        public JsonResult PunchOrder(checkoutBLL data)
        {
            try
            {

                var currDate = DateTime.UtcNow.AddMinutes(180);
                Random random = new Random();
                data.TransactionNo = "IT-" + random.Next(10, 10000) + random.Next(1, 1000);
                data.OrderNo = data.OrderNo = "IT-" + currDate.Date.ToString("ddMMyy") + "-" + random.Next(1, 100) + random.Next(1, 100);
                data.OrderDate = DateTime.UtcNow.AddMinutes(180);
                data.CreationDate = DateTime.UtcNow.AddMinutes(180);
                data.StatusID = 101;

                checkoutBLL _service = new checkoutBLL();
                //orderdetails
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(JArray.Parse(data.OrderDetailString));
                JArray jsonResponse = JArray.Parse(json);
                data.OrderDetail = jsonResponse.ToObject<List<checkoutBLL.OrderDetails>>();

                int rtn = _service.InsertOrder(data);

                return Json(new { data = rtn });
            }
            catch (Exception ex)
            {
                return Json(new { dawta = 0 });
            }
        }
        //Coupon
        public IActionResult Coupon(string coupon)
        {
            couponBLL couponBLL = new couponBLL();
            ViewBag.Coupon = couponBLL.Get(coupon);

            return Json(new { data = ViewBag.Coupon });
        }
    }
}
