using ISupportWeb.Models;
using ISupportWeb.Models.BLL;
using ISupportWeb.Models.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Linq;
using static ISupportWeb.Models.BLL.itemBLL;

namespace ISupportWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            ViewBag.Banner = new bannerBLL().GetBanner("Home");
            var itemData = new itemBLL().GetAll();
            ViewBag.itemList = itemData.Take(40).ToList();             
            ViewBag.TrendingItem = itemData.OfType<itemBLL>().OrderByDescending(x => x.DisplayOrder).Where(x => x.IsTrending == 1).OrderBy(c => Guid.NewGuid()).Take(8).ToList();
            ViewBag.IsOffer = itemData.OfType<itemBLL>().OrderByDescending(c => c.CreationDate).Take(8).ToList();

            var serviceCatData = new serviceCatBLL().GetServiceCat();
            ViewBag.serviceCat = serviceCatData;

            return View();
        }

        public IActionResult Privacy()
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
    }
}