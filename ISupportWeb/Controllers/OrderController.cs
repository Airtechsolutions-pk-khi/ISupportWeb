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
            int? CustomerID = id;
            if (CustomerID == 0)
            {

                //Session["CustomerID"] = 0;
                int? retrievedCustomerID = int.TryParse(HttpContext.Session.GetString("CustomerID"), out int parsedCustomerID) ? parsedCustomerID : (int?)null;
                //HttpContext.Session.GetInt32(CustomerID) = 0;
                return View();
            }
            else
            {
                if (HttpContext.Session.GetString("CustomerID") != null &&
            Convert.ToInt32(HttpContext.Session.GetString("CustomerID")) != 0)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login_Register", "Account");
                }
            }

        }
        public IActionResult OrderComplete(int OrderID = 0, string OrderNo = "")
        {
            var cityData = new cityService().GetAll();
            ViewBag.City = cityData;
            checkoutBLL check = new checkoutBLL();
            if (OrderNo == "Reject" || OrderID == 0)
            {
                ViewBag.OrderNo = "Reject";
                //check.OrderUpdate(OrderID, 103);//Rejected 
            }
            else
            {
                var data = new myorderBLL().GetDetails(OrderID);
                if (data.PaymentMethodTitle == "DebitCreditCard")
                {
                    check.OrderUpdate(OrderID, 101);//In progress
                }
                string ToEmail, SubJect, cc, Bcc;
                ToEmail = data.SenderEmail;
                SubJect = "Your order on KarachiFlora - " + data.OrderNo;

                string webRootPath = _hostingEnvironment.WebRootPath;
                string BodyEmail = System.IO.File.ReadAllText(System.IO.Path.Combine(webRootPath, "Template", "emailpattern.txt"));
                string BodyEmailadmin = System.IO.File.ReadAllText(System.IO.Path.Combine(webRootPath, "Template", "emailpattern-admin.txt"));

                //string BodyEmail = System.IO.File.ReadAllText(Server.MapPath("~/Template") + "\\" + "emailpattern.txt");
                //string BodyEmailadmin = System.IO.File.ReadAllText(Server.MapPath("~/Template") + "\\" + "emailpattern-admin.txt");
                string items = "";
                foreach (var item in data.OrderDetail)
                {
                    try
                    {
                        items += "<table border = '0' cellpadding = '0' cellspacing = '0' align = 'center' width = '100%' role = 'module' data - type = 'columns' style = 'padding:20px 20px 20px 30px;' bgcolor = '#FFFFFF'>"
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
                        //+ "<img src = '" + Configuration["ImageBaseUrl"].ToString() + item.ItemImage + "' class='max-width' border='0' style='display:block;width: 108px;height: 108px;object-fit: contain;' alt='' >"
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
                        + "<td style = 'padding:18px 0px 18px 0px; line-height:22px; text-align:inherit;' height='100%' valign='top' bgcolor='' role='module-content'><div>"
                        + "<div style = 'font-family: inherit; text-align: inherit'> " + item.ItemTitle + "</div>"
                        + "<div style = 'font-family: inherit; text-align: inherit'> Notes: " + item.ProductNote + "</div>"
                        + "<div style = 'font-family: inherit; text-align: inherit'> Qty: " + item.Quantity + "</div>"
                        + "<div style = 'font-family: inherit; text-align: inherit'><span style='color: #006782'>RS " + item.Price + "</span></div>"
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
                    catch (Exception ex)
                    {
                    }
                }
                string gifts = "";
                if (data.GiftDetail.Count > 0)
                {
                    foreach (var item in data.GiftDetail)
                    {
                        try
                        {
                            gifts += "<table border = '0' cellpadding = '0' cellspacing = '0' align = 'center' width = '100%' role = 'module' data - type = 'columns' style = 'padding:20px 20px 20px 30px;' bgcolor = '#FFFFFF'>"
                            + "<tbody>"
                            + "<tr role = 'module-content'>"
                            + "<td height = '100%' valign = 'top'>"
                            + "<table class='column' width='137' style='width:137px; border-spacing:0; border-collapse:collapse; margin:0px 0px 0px 0px;' cellpadding='0' cellspacing='0' align='left' border='0' bgcolor=''>"
                            + "<tbody>"
                            + "<tr>"
                            + "<td style = 'padding:0px;margin:0px;border-spacing:0;' ><table class='wrapper' role='module' data-type='image' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='239f10b7-5807-4e0b-8f01-f2b8d25ec9d7'>"
                            + "<tbody>"
                            + "<tr>"
                            + "<td style = 'font-size:6px; line-height:10px; padding:0px 0px 0px 0px;' valign='top' align='left'>"
                            //+ "<img src = '" + System.Configuration.ConfigurationManager.AppSettings["Image"].ToString() + item.GiftImage + "' class='max-width' border='0' style='display:block;width: 108px;height: 108px;object-fit: contain;' alt='' >"
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
                            + "<td style = 'padding:18px 0px 18px 0px; line-height:22px; text-align:inherit;' height='100%' valign='top' bgcolor='' role='module-content'><div>"
                            + "<div style = 'font-family: inherit; text-align: inherit'> " + item.GiftTitle + "</div>"
                            + "<div style = 'font-family: inherit; text-align: inherit'><span style='color: #006782'>RS " + item.Price + "</span></div>"
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
                        catch (Exception ex)
                        {
                        }
                    }
                    gifts += "<table class='module' role='module' data-type='divider' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' datad-muid='f7373f10-9ba4-4ca7-9a2e-1a2ba700deb9.1'>"
                    + "<tbody>"
                    + "<tr>"
                    + "<td style='padding:20px 30px 0px 30px;' role='module-content' height='100%' valign='top' >"
                    + "<table border='0' cellpadding='0' cellspacing='0' align='center' width='100%' height='3px' style='line-height:3px; font-size:3px;'>"
                    + "<tbody>"
                    + "<tr>"
                    + "<td style='padding:0px 0px 3px 0px;background-color: #ffcc00'></td>"
                    + "</tr>"
                    + "</tbody>"
                    + "</table>"
                    + "</td>"
                    + "</tr>"
                    + "</tbody>"
                    + "</table>";
                }
                BodyEmail = BodyEmail.Replace("#ReceiverName#", data.CustomerName.ToString());
                BodyEmail = BodyEmail.Replace("#ReceiverContact#", data.ContactNo.ToString());
                BodyEmail = BodyEmail.Replace("#OrderNo#", data.OrderNo.ToString());
                BodyEmail = BodyEmail.Replace("#items#", items.ToString());
                BodyEmail = BodyEmail.Replace("#gifts#", gifts.ToString());

                BodyEmailadmin = BodyEmailadmin.Replace("#ReceiverName#", data.CustomerName.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#ReceiverContact#", data.ContactNo.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#OrderNo#", data.OrderNo.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#items#", items.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#gifts#", gifts.ToString());
                DateTime dateTime = DateTime.UtcNow.AddMinutes(180);
                BodyEmail = BodyEmail.Replace("#Customer#", data.SenderName.ToString());
                BodyEmail = BodyEmail.Replace("#CustomerAddress#", data.SenderAddress.ToString());
                //BodyEmail = BodyEmail.Replace("#CustomerContact#", data.SenderName.ToString());
                BodyEmail = BodyEmail.Replace("#SelectedTime#", data.SelectedTime.ToString());
                BodyEmail = BodyEmail.Replace("#DeliveryDate#", data.DeliveryDate.ToString("dd/MMM/yyyy"));
                BodyEmail = BodyEmail.Replace("#OrderDate#", dateTime.ToString("dd/MMM/yyyy"));
                BodyEmail = BodyEmail.Replace("#Address#", data.Address.ToString());

                BodyEmailadmin = BodyEmailadmin.Replace("#Customer#", data.SenderName.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#CustomerAddress#", data.SenderAddress.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#CustomerContact#", data.SenderContact.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#SelectedTime#", data.SelectedTime.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#DeliveryDate#", data.DeliveryDate.ToString("dd/MMM/yyyy"));
                BodyEmailadmin = BodyEmailadmin.Replace("#OrderDate#", dateTime.ToString("dd/MMM/yyyy"));
                BodyEmailadmin = BodyEmailadmin.Replace("#Address#", data.Address.ToString());

                //string PaymentType = "";
                //PaymentType = data.PaymentMethodID == 3 ? "Paypal" : data.PaymentMethodID == 5 ? "Easypaisa" : data.PaymentMethodID == 3 ? "Banktransfer" : "Cash on delivery";
                //BodyEmail = BodyEmail.Replace("#PaymentType#", PaymentType.ToString());

                BodyEmail = BodyEmail.Replace("#Description#", data.CardNotes.ToString());
                BodyEmail = BodyEmail.Replace("#PaymentMethod#", data.PaymentMethodTitle.ToString());
                BodyEmail = BodyEmail.Replace("#TotalItems#", data.TotalItems.ToString());
                BodyEmail = BodyEmail.Replace("#SubTotal#", data.AmountTotal.ToString());
                BodyEmail = BodyEmail.Replace("#Tax#", data.Tax.ToString());
                BodyEmail = BodyEmail.Replace("#DeliveryAmount#", data.DeliveryAmount.ToString());
                BodyEmail = BodyEmail.Replace("#GrandTotal#", data.GrandTotal.ToString());

                //BodyEmailadmin = BodyEmailadmin.Replace("#PaymentType#", PaymentType.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#Description#", data.CardNotes.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#PaymentMethod#", data.PaymentMethodTitle.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#TotalItems#", data.TotalItems.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#SubTotal#", data.AmountTotal.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#Tax#", data.Tax.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#DeliveryAmount#", data.DeliveryAmount.ToString());
                BodyEmailadmin = BodyEmailadmin.Replace("#GrandTotal#", data.GrandTotal.ToString());
                cc = "";
                Bcc = _configuration["EmailSettings:From"].ToString();
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(ToEmail);

                    mail.From = new MailAddress(_configuration["EmailSettings:From"]);
                    mail.Subject = SubJect;
                    string Body = BodyEmail;
                    mail.Body = Body;
                    mail.IsBodyHtml = true;

                    SmtpClient smtp = new SmtpClient();
                    smtp.UseDefaultCredentials = false;
                    smtp.Port = int.Parse(_configuration["EmailSettings:SmtpPort"]);
                    smtp.Host = _configuration["EmailSettings:SmtpServer"];
                    smtp.Credentials = new System.Net.NetworkCredential
                 (_configuration["EmailSettings:From"], _configuration["EmailSettings:Password"]);
                    smtp.EnableSsl = true;

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
                    mail.To.Add(_configuration["EmailSettings:From"].ToString());

                    mail.From = new MailAddress(_configuration["EmailSettings:From"].ToString());
                    mail.Subject = "NEW ORDER | " + SubJect;
                    string Body = BodyEmailadmin;
                    mail.Body = Body;
                    mail.IsBodyHtml = true;

                    SmtpClient smtp = new SmtpClient();
                    smtp.UseDefaultCredentials = false;
                    using (SmtpClient smtp1 = new SmtpClient(_configuration["EmailSettings:SmtpServer"], int.Parse(_configuration["EmailSettings:SmtpPort"])))
                    {
                        smtp1.UseDefaultCredentials = false;
                        smtp1.Credentials = new NetworkCredential(_configuration["EmailSettings:From"], _configuration["EmailSettings:Password"]);
                        smtp1.EnableSsl = true;

                        smtp1.Send(mail);
                    }

                    smtp.Send(mail);
                }
                catch (Exception)
                {
                }
                ViewBag.OrderNo = data.OrderNo;
            }

            return View();
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
