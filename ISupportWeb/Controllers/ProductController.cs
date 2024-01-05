using ISupportWeb.Models.BLL;
using ISupportWeb.Models.Service;
using Microsoft.AspNetCore.Mvc;

namespace ISupportWeb.Controllers
{
    public class ProductController : Controller
    {
        productService _service;
        public ProductController()
        {
            _service = new productService();

        }
        public IActionResult ProductDetails(int ItemID)
        {
            ViewBag.Banner = new bannerBLL().GetBanner("Home");
            ViewBag.ProductDetails = _service.GetAll(ItemID);
            return View(_service.GetAll(ItemID));
        }
        public IActionResult ServiceDetails(int ServiceID)
        {
            ViewBag.Banner = new bannerBLL().GetBanner("Home");
            ViewBag.ServiceDetails = _service.GetAllService(ServiceID);
            return View(_service.GetAllService(ServiceID));
        }
        public IActionResult Wishlist()
        {
           
            ViewBag.Banner = new bannerBLL().GetBanner("Home");
            return View();
        }
    }
}
