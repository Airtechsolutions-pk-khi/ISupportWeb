using Microsoft.AspNetCore.Mvc;

namespace ISupportWeb.Controllers
{
    public class Shared : Controller
    {
        public IActionResult Header()
        {
            return PartialView("Header");
        }
        public IActionResult MobileNavigation()
        {
            return PartialView("MobileNavigation");
        }
    }
}
