using ISupportWeb.Models.BLL;
using ISupportWeb.Models.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 

namespace ISupportWeb.Controllers
{
    public class AccountController : Controller
    {

        // GET: Account
        [HttpGet]
        public IActionResult Login_Register(int id = 0)
        {            
            ViewBag.Banner = new bannerBLL().GetBanner("Home");
            //Session["LoginRoute"] = id;
            HttpContext.Session.SetInt32("LoginRoute", id);
            return View();
        }
        [HttpPost]
        public IActionResult Login_Register(loginBLL service)
        {
            if (service.Contact != null)
            {
                service.Register();
                HttpContext.Session.SetString("LoginNote", "Login Now");
                return RedirectToAction("Login_Register", "Account");
            }
            else
            {
                service = service.login();
                HttpContext.Session.SetString("LoginNote", "");
                HttpContext.Session.SetInt32("CustomerID", service.CustomerID);
                HttpContext.Session.SetString("CustomerEmail", service.Email);
                HttpContext.Session.SetString("CustomerContactNo", service.Contact);
                HttpContext.Session.SetString("CustomerName", service.UserName);
                //HttpContext.Session.SetInt32("IsVerified", service.IsVerified ? 1 : 0);
                if (HttpContext.Session.GetInt32("CustomerEmail") != 0)
                {
                    // Check if CustomerEmail session variable is not null
                    if (HttpContext.Session.GetString("CustomerEmail") != null)
                    {
                        HttpContext.Session.SetString("LoginNote", "Successfully Login");

                        // Check the value of LoginRoute session variable
                        if (HttpContext.Session.GetInt32("LoginRoute") == 1)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return RedirectToAction("Checkout", "Order");
                        }
                    }

                    HttpContext.Session.SetString("LoginNote", "User is not verified");
                    return RedirectToAction("Login_Register", "Account");
                }
                else
                {
                    HttpContext.Session.SetString("CustomerName", null);
                    HttpContext.Session.SetString("LoginNote", "Invalid Email or Password");
                    return RedirectToAction("Login_Register", "Account");
                }
            }

        }
        public IActionResult Logout()
        {
            // Clear session variables
            HttpContext.Session.SetString("LoginNote", "");
            HttpContext.Session.SetInt32("CustomerID", 0);
            HttpContext.Session.SetString("CustomerEmail", "");
            HttpContext.Session.SetString("CustomerContactNo", "");
            HttpContext.Session.SetString("CustomerName", "");
            //HttpContext.Session.SetInt32("IsVerified", null);
            HttpContext.Session.SetInt32("LoginRoute", 0);

            return RedirectToAction("Index", "Home");
        }

    }
}