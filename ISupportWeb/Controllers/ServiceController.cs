using ISupportWeb.Models.BLL;
using ISupportWeb.Models.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ISupportWeb.Controllers
{
    public class ServiceController : Controller
    {
        
        shopService _service;
        filterService filterService;

        public ServiceController()
        {            
            _service = new shopService();
            filterService = new filterService();

        }
        public IActionResult ServiceDetails(int ServiceID)
        {
            ViewBag.Banner = new bannerBLL().GetBanner("Home");
            ViewBag.ServiceDetails = _service.GetAllService(ServiceID);
            return View(_service.GetAllService(ServiceID));
        }
        public IActionResult Shop(string Category = "", string ServiceCategoryIDs = "", string SubCategoryIDs = "", string ColorIDs = "", string MinPrice = "", string MaxPrice = "", string Searchtext = "", int SortID = 0)
        {
            
            ViewBag.BestService = new shopService().BestServices();
            var serviceCatData = new serviceCatBLL().GetServiceCat();
            ViewBag.serviceCat = serviceCatData;
            ViewBag.Banner = new bannerBLL().GetBanner("Home");
            TempData["Category"] = Category;
            TempData["ServiceCategoryIDs"] = ServiceCategoryIDs;
            //TempData["SubCategoryIDs"] = SubCategoryIDs;
            TempData["ColorIDs"] = ColorIDs;
            TempData["MinPrice"] = MinPrice;
            TempData["MaxPrice"] = MaxPrice;
            TempData["Searchtext"] = Searchtext;
            TempData["SortID"] = SortID.ToString();
            return View();
        }
        public IActionResult Services(List<filterBLL> Services)
        {
            ViewBag.Message = "";
            if (Services.Count > 0)
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
                    if (TempData["CategoryIDs"]?.ToString() != "" ||
                    TempData["ColorIDs"]?.ToString() != "" ||
                    TempData["MinPrice"]?.ToString() != "" ||
                    TempData["MaxPrice"]?.ToString() != "" ||
                    TempData["Searchtext"]?.ToString() != "" ||
                    TempData["SortID"]?.ToString() != "5")
                    {
                        filterBLL data = new filterBLL();
                        data.Category = TempData["CategoryIDs"]?.ToString();
                        //data.SubCategory = TempData["SubCategoryIDs"].ToString();
                        data.Color = TempData["ColorIDs"]?.ToString();
                        data.MinPrice = TempData["MinPrice"]?.ToString();
                        data.MaxPrice = TempData["MaxPrice"]?.ToString();
                        data.Searchtxt = TempData["Searchtext"]?.ToString();
                        data.SortID = Convert.ToInt32(TempData["SortID"]?.ToString());
                        if (data.MinPrice == "" || data.MaxPrice == "" || data.MinPrice == null || data.MaxPrice == null)
                        {
                            data.MinPrice = "BHD0.000";
                            data.MaxPrice = "BHD20.00";
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
                     
                    ViewBag.shopList = "";
                    ViewBag.Message = "No Service Found";

                }

                return PartialView("AllServices");
            }
           
        }
        public JsonResult Filter([FromBody] filterBLL data)
        {
            
            ViewBag.shopList = filterService.GetAll(data);
            return Json(new { data = ViewBag.shopList });            
            //return new JsonResult(Ok(ViewBag.shopList));

        }
        public IActionResult FilterServices([FromBody]List<filterBLL> Services)
        {
            ViewBag.Message = "";
            if (Services.Count > 0)
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
                    if (TempData["CategoryIDs"]?.ToString() != "" ||
                    //TempData["ColorIDs"]?.ToString() != "" ||
                    //TempData["MinPrice"]?.ToString() != "" ||
                    //TempData["MaxPrice"]?.ToString() != "" ||
                    TempData["Searchtext"]?.ToString() != "" ||
                    TempData["SortID"]?.ToString() != "5")
                    {
                        filterBLL data = new filterBLL();
                        data.Category = TempData["CategoryIDs"]?.ToString();
                        //data.SubCategory = TempData["SubCategoryIDs"].ToString();
                        //data.Color = TempData["ColorIDs"]?.ToString();
                        //data.MinPrice = TempData["MinPrice"]?.ToString();
                        //data.MaxPrice = TempData["MaxPrice"]?.ToString();
                        data.Searchtxt = TempData["Searchtext"]?.ToString();
                        data.SortID = Convert.ToInt32(TempData["SortID"]?.ToString());
                        //if (data.MinPrice == "" || data.MaxPrice == "")
                        //{
                        //    data.MinPrice = "BHD0";
                        //    data.MaxPrice = "BHD50000";
                        //}

                        ViewBag.shopList = filterService.GetAll(data);
                        if (ViewBag.shopList.Count < 1)
                        {
                            ViewBag.Message = "No Service Found";
                        }
                    }
                }
                else
                {

                    ViewBag.shopList = "";
                    ViewBag.Message = "No Service Found";

                }

                return PartialView("AllServices");
            }

        }
    }
}
