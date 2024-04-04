using ISupportWeb.Models;
using ISupportWeb.Models.BLL;
using ISupportWeb.Models.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Linq;
using static ISupportWeb.Models.BLL.itemBLL;
using Microsoft.AspNetCore.Hosting.Server;
using System.Net.Mail;
using System.Configuration;
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using WebAPICode.Helpers;

namespace ISupportWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;
        TicketBLL repo;
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
            repo = new TicketBLL();
        }
        public IActionResult Index(contactBLL obj)
        {
            //ViewBag.Banner = new bannerBLL().GetBanner("WebsiteHome");
            var itemData = new itemBLL().GetAll();
            ViewBag.itemList = itemData.Take(40).ToList();

            ViewBag.TrendingItem = itemData.OfType<itemBLL>().OrderByDescending(x => x.DisplayOrder).Where(x => x.Name == "test").OrderBy(c => Guid.NewGuid()).Take(8).ToList();
            ViewBag.IsOffer = itemData.OfType<itemBLL>().OrderByDescending(c => c.CreationDate).Take(8).ToList();

            var serviceCatData = new serviceCatBLL().GetServiceCat();
            ViewBag.serviceCat = serviceCatData;

            var popularServiceData = new serviceBLL().GetPopularServices();
            ViewBag.popularServiceData = popularServiceData.Take(14);
            ViewBag.Banner = new bannerBLL().GetBanner("WebsiteHome");

            return View();
        }
        [HttpPost]
        public IActionResult SendNewsletterEmail([FromBody] string email)
        {
            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    Subscribe(email);
                    return Ok(new { success = true, message = "Email sent successfully." });
                }
                else
                {
                    return BadRequest("Invalid email address.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public void Subscribe(string email)
        {
            string ToEmail, SubJect, cc, Bcc;
            cc = "";
            Bcc = "";
            ToEmail = _configuration["From"].ToString();
            SubJect = "New Subscribtion at ISupport";
            string webRootPath = System.IO.Path.Combine(_hostingEnvironment.ContentRootPath, "Template", "newsletter.txt");
            string BodyEmail = System.IO.File.ReadAllText(webRootPath);

            BodyEmail = BodyEmail.Replace("#email#", email.ToString());
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(_configuration["From"].ToString());
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
            }
            catch (Exception ex)
            {
                ViewBag.Status = "";
            }
        }
        [HttpPost]
        public IActionResult EmailConsultant([FromBody] HelpPageBLL obj)
        {
            try
            {
                if (obj != null)
                {
                    var tcno = obj.TicketNo;
                    Random random = new Random();
                    if (obj.Type == "Consultancy")
                    {
                        obj.TicketNo = "CN-" + random.Next(10, 10000) + random.Next(1, 1000);
                        Insert(obj);
                    }
                    if (obj.Type == "Submit Request")
                    {
                        obj.TicketNo = "RQ-" + random.Next(10, 10000) + random.Next(1, 1000);
                        Insert(obj);
                    }
                    SendEmailtoCust(obj, obj.TicketNo, obj.Type, obj.Title);
                    return Ok(new { success = true, message = "Email sent successfully.", ticketNo = obj.TicketNo });
                }
                else
                {
                    return BadRequest("Invalid email address.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public int Insert(HelpPageBLL obj)
        {
            try
            {
                obj.StatusID = 1;
                obj.TicketStatus = 101;
                obj.CreatedOn = DateTime.Now;
                obj.CreatedBy = "Website";
                SqlParameter[] p = new SqlParameter[10];
                p[0] = new SqlParameter("@Title", obj.Title);
                p[1] = new SqlParameter("@Message", obj.Message);
                p[2] = new SqlParameter("@Mobile", obj.Mobile);
                p[3] = new SqlParameter("@Email", obj.Email);
                p[4] = new SqlParameter("@Type", obj.Type);
                p[5] = new SqlParameter("@TicketNo", obj.TicketNo);
                p[6] = new SqlParameter("@StatusID", obj.StatusID);
                p[7] = new SqlParameter("@TicketStatus", obj.TicketStatus);
                p[8] = new SqlParameter("@CreatedOn", obj.CreatedOn);
                p[9] = new SqlParameter("@CreatedBy", obj.CreatedBy);
                obj.TicketID = int.Parse((new DBHelper().GetDatasetFromSP)("sp_InsertTicket_Web", p).Tables[0].Rows[0][0].ToString());
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public void SendEmailtoCust(HelpPageBLL obj, string TicketNo, string TicketType, string Title)
        {
            //var data = DBContext.Tickets.Where(y => y.CustomerName == obj.).FirstOrDefault();
            string ToEmail, SubJect, cc, Bcc;
            ToEmail = obj.Email;
            string mobile = obj.Mobile;
            DateTime dt = DateTime.Now;

            SubJect = "Your Booking # - " + TicketNo;

            string webRootPath = System.IO.Path.Combine(_hostingEnvironment.ContentRootPath, "Template", "ticketemailpattern.txt");
            string BodyEmail = System.IO.File.ReadAllText(webRootPath);

            string webRootPathA = System.IO.Path.Combine(_hostingEnvironment.ContentRootPath, "Template", "emailticket-admin.txt");
            string BodyEmailadmin = System.IO.File.ReadAllText(webRootPathA);

            //string BodyEmail = System.IO.Path.Combine(_hostingEnvironment.ContentRootPath, "Template", "ticketemailpattern.txt");
            //string BodyEmailadmin = System.IO.Path.Combine(_hostingEnvironment.ContentRootPath, "Template", "emailticket-admin.txt");

            BodyEmail = BodyEmail.Replace("#TicketNo#", TicketNo.ToString());
            BodyEmail = BodyEmail.Replace("#Date#", dt.ToString());
            BodyEmail = BodyEmail.Replace("#TicketType#", obj.Type.ToString());
            BodyEmail = BodyEmail.Replace("#Customer#", obj.Name.ToString());
            BodyEmail = BodyEmail.Replace("#Title#", obj.Title.ToString());
            BodyEmail = BodyEmail.Replace("#Description#", obj.Message.ToString());
            //BodyEmail = BodyEmail.Replace("#TimeSlots#", obj.Timeslots.ToString());

            //Admin
            BodyEmailadmin = BodyEmailadmin.Replace("#TicketNo#", TicketNo.ToString());
            BodyEmailadmin = BodyEmailadmin.Replace("#Date#", dt.ToString());
            BodyEmailadmin = BodyEmailadmin.Replace("#TicketType#", obj.Type.ToString());
            BodyEmailadmin = BodyEmailadmin.Replace("#Customer#", obj.Name.ToString());
            BodyEmailadmin = BodyEmailadmin.Replace("#Mobile#", obj.Mobile.ToString());
            BodyEmailadmin = BodyEmailadmin.Replace("#Email#", obj.Email.ToString());
            BodyEmailadmin = BodyEmailadmin.Replace("#Title#", obj.Title.ToString());
            BodyEmailadmin = BodyEmailadmin.Replace("#Description#", obj.Message.ToString());

            if (mobile != null)
            {

            }
            else
            {
                BodyEmailadmin = BodyEmailadmin.Replace("#CustContact#", "N/A");
            }

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
                ViewBag.Status = "Your Ticket has been Generated";
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
                mail.Subject = "NEW Ticket Request | " + SubJect;
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
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Terms()
        {
            return View();
        }
        public IActionResult Faqs()
        {
            return View();
        }
        public IActionResult Return()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult About()
        {
            // ViewBag.Banner = new bannerBLL().GetBanner("About");
            return View();
        }
        [HttpGet]
        public IActionResult Contact()
        {
            // ViewBag.Banner = new bannerBLL().GetBanner("Contact");
            return View();
        }
        [HttpPost]
        public IActionResult Contact(contactBLL obj)
        {
            ViewBag.Contact = "";
            string ToEmail, SubJect, cc, Bcc;
            cc = "";
            Bcc = "";
            ToEmail = _configuration["From"].ToString();
            SubJect = "New Query From Customer";
            string webRootPath = System.IO.Path.Combine(_hostingEnvironment.ContentRootPath, "Template", "contact.txt");
            string BodyEmail = System.IO.File.ReadAllText(webRootPath);
            DateTime dateTime = DateTime.UtcNow.Date;
            BodyEmail = BodyEmail.Replace("#Date#", dateTime.ToString("dd/MMM/yyyy"))
            .Replace("#Name#", obj.Name.ToString())
            .Replace("#Email#", obj.Email.ToString())
            .Replace("#Contact#", obj.Phone.ToString())
            .Replace("#Subject#", obj.Subject.ToString())
            .Replace("#Message#", obj.Message.ToString());
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
                ViewBag.Contact = "Your Query is received. Our support department contact you soon.";
            }
            catch (Exception ex)
            {
                ViewBag.Contact = "";
            }
            return View();
        }
        public ActionResult GetSetting()
        {
            return Json(new settingBLL().GetSettings());
        }
        [HttpPost]
        public IActionResult SendEmailTechnician([FromBody] Technician obj)
        {
            try
            {
                if (obj != null)
                {
                    EmailTechnician(obj);
                    return Ok(new { success = true, message = "Email sent successfully." });
                }
                else
                {
                    return BadRequest("Invalid email address.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public void EmailTechnician(Technician obj)
        {
            string ToEmail, SubJect, cc, Bcc;
            ToEmail = obj.Email;
            string mobile = obj.Mobile;
            DateTime dt = DateTime.Now;

            string webRootPathA = System.IO.Path.Combine(_hostingEnvironment.ContentRootPath, "Template", "technicianemail-admin.txt");
            string BodyEmailadmin = System.IO.File.ReadAllText(webRootPathA);
            //string BodyEmailadmin = System.IO.Path.Combine(_hostingEnvironment.ContentRootPath, "Template",  "technicianemail-admin.txt");
            //Admin
            BodyEmailadmin = BodyEmailadmin.Replace("#Date#", dt.ToString());
            BodyEmailadmin = BodyEmailadmin.Replace("#Name#", obj.Name.ToString());
            BodyEmailadmin = BodyEmailadmin.Replace("#Mobile#", obj.Mobile.ToString());
            BodyEmailadmin = BodyEmailadmin.Replace("#Email#", obj.Email.ToString());
            BodyEmailadmin = BodyEmailadmin.Replace("#Address#", obj.Address.ToString());
            BodyEmailadmin = BodyEmailadmin.Replace("#CPRNumber#", obj.CPRNumber.ToString());

            if (mobile != null)
            {

            }
            else
            {
                BodyEmailadmin = BodyEmailadmin.Replace("#CustContact#", "N/A");
            }

            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(_configuration["From"].ToString());

                mail.From = new MailAddress(_configuration["From"].ToString());
                mail.Subject = "Dear Admin you have a New Registered Technician";
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
        }
    }
}