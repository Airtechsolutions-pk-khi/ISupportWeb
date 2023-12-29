using ISupportWeb.Models.BLL;
using ISupportWeb.Models.Service;
using Microsoft.AspNetCore.Mvc;

namespace ISupportWeb.Controllers
{
    public class ShopServiceController : Controller
    {
        shopService _service;
        filterService filterService;

        public ShopServiceController()
        {
            _service = new shopService();
            filterService = new filterService();

        }
        public IActionResult Shop(string Category = "", string ServiceCategoryIDs = "", string SubCategoryIDs = "", string ColorIDs = "", string MinPrice = "", string MaxPrice = "", string Searchtext = "", int SortID = 0)
        {
            
            ViewBag.BestService = new shopService().BestServices();
            var serviceCatData = new serviceCatBLL().GetServiceCat();
            ViewBag.serviceCat = serviceCatData;
            ViewBag.Banner = new bannerBLL().GetBanner("Home");
            TempData["Category"] = Category;
            TempData["ServiceCategoryIDs"] = ServiceCategoryIDs;
            TempData["SubCategoryIDs"] = SubCategoryIDs;
            TempData["ColorIDs"] = ColorIDs;
            TempData["MinPrice"] = MinPrice;
            TempData["MaxPrice"] = MaxPrice;
            TempData["Searchtext"] = Searchtext;
            TempData["SortID"] = SortID.ToString();
            return View();
        }
        public ActionResult Services(List<filterBLL> Services)
        {
            ViewBag.Message = "";
            if (Services != null)
            {
                ViewBag.shopList = Services;
                if (ViewBag.shopList.Count < 1)
                {
                    ViewBag.Message = "No Service Found";
                }
                return PartialView("AllServices");
            }
            else
            {
                if (TempData.Count > 1)
                {
                    if (TempData["CategoryIDs"].ToString() != "" ||
                    TempData["SubCategoryIDs"].ToString() != "" ||
                    TempData["ColorIDs"].ToString() != "" ||
                    TempData["MinPrice"].ToString() != "" ||
                    TempData["MaxPrice"].ToString() != "" ||
                    TempData["Searchtext"].ToString() != "" ||
                    TempData["SortID"].ToString() != "5")
                    {
                        filterBLL data = new filterBLL();
                        data.Category = TempData["CategoryIDs"].ToString();
                        data.SubCategory = TempData["SubCategoryIDs"].ToString();
                        data.Color = TempData["ColorIDs"].ToString();
                        data.MinPrice = TempData["MinPrice"].ToString();
                        data.MaxPrice = TempData["MaxPrice"].ToString();
                        data.Searchtxt = TempData["Searchtext"].ToString();
                        data.SortID = Convert.ToInt32(TempData["SortID"].ToString());
                        if (data.MinPrice == "" || data.MaxPrice == "")
                        {
                            data.MinPrice = "BHD0";
                            data.MaxPrice = "BHD50000";
                        }
                       
                        ViewBag.shopList = filterService.GetAll(data);
                        if (ViewBag.shopList.Count < 1)
                        {
                            ViewBag.Message = "No Service Found";
                        }
                    }
                }
                else
                {
                    /*string category = "";
                    if (TempData["Category"] != null)
                    {
                        category = TempData["Category"].ToString();
                    }
                    ViewBag.shopList = _service.GetAll(category);*/
                    ViewBag.shopList = "";
                    ViewBag.Message = "No Service Found";

                }

                return PartialView("AllServices");
            }
           
        }
        public JsonResult Filter(filterBLL data)
        {            
            ViewBag.shopList = filterService.GetAll(data);
            return Json(new { data = ViewBag.shopList });            
        }
    }
}
