using ISupportWeb.Models.BLL;
using Microsoft.AspNetCore.Mvc;

namespace ISupportWeb.Controllers
{
    public class SharedController : Controller
    {
        public IActionResult Header()
        {
            return PartialView("Header", new navigationBLL().GetServiceCategory());
        }
        public IActionResult MobileNavigation()
        {
            return PartialView("MobileNavigation", new navigationBLL().GetServiceCategory());
        }
    }
}
