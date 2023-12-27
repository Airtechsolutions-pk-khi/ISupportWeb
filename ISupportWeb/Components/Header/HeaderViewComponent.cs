using ISupportWeb.Models.BLL;
using Microsoft.AspNetCore.Mvc;

namespace ISupportWeb.Components.Header
{
    public class HeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var getServiceCat = new navigationBLL().GetServiceCategory();
            // Your logic to retrieve data for the header
            return View(getServiceCat);
            //return View(); // Assuming there's a corresponding Razor view named Default.cshtml in the Views/Shared/Components/Header folder
        }
    }
}
