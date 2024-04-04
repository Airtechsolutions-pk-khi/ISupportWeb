//using ISupportWeb.Models.BLL;
//using ISupportWeb.Models.Service;
//using Microsoft.AspNetCore.Mvc;

//namespace ISupportWeb.Controllers
//{
//    public class ShopProductController : Controller
//    {
//        shopProduct _service;
//        filterProduct filterProduct;

//        public ShopProductController()
//        {
//            _service = new shopProduct();
//            filterProduct = new filterProduct();

//        }
//        public IActionResult Shop(string Category = "", string ProductIDs = "", string SubCategoryIDs = "", string ColorIDs = "", string MinPrice = "", string MaxPrice = "", string Searchtext = "", int SortID = 0)
//        {

//            ViewBag.BestProduct = new shopProduct().BestProducts();
//            ViewBag.itemCat = new itemCatBLL().GetItemCat();
//            //ViewBag.itemCat = itemCatData;
//            ViewBag.Banner = new bannerBLL().GetBanner("Home");
//            TempData["Category"] = Category;
//            TempData["ProductIDs"] = ProductIDs;
//            //TempData["SubCategoryIDs"] = SubCategoryIDs;
//            TempData["ColorIDs"] = ColorIDs;
//            TempData["MinPrice"] = MinPrice;
//            TempData["MaxPrice"] = MaxPrice;
//            TempData["Searchtext"] = Searchtext;
//            TempData["SortID"] = SortID.ToString();
//            return View();
//        }
//        public IActionResult Products(List<filterBLL> Products)
//        {
//            ViewBag.Message = "";
//            if (Products.Count > 0)
//            {
//                ViewBag.shopList = Products;
//                if (ViewBag.shopList.Count < 1)
//                {
//                    ViewBag.Message = "No Product Found";
//                }
//                return PartialView("AllProducts");
//            }
//            else
//            {
//                if (TempData.Count > 1)
//                {
//                    if (TempData["CategoryIDs"]?.ToString() != "" ||
//                    TempData["ColorIDs"]?.ToString() != "" ||
//                    TempData["MinPrice"]?.ToString() != "" ||
//                    TempData["MaxPrice"]?.ToString() != "" ||
//                    TempData["Searchtext"]?.ToString() != "" ||
//                    TempData["SortID"]?.ToString() != "5")
//                    {
//                        filterBLL data = new filterBLL();
//                        data.Category = TempData["CategoryIDs"]?.ToString();
//                        //data.SubCategory = TempData["SubCategoryIDs"].ToString();
//                        data.Color = TempData["ColorIDs"]?.ToString();
//                        data.MinPrice = TempData["MinPrice"]?.ToString();
//                        data.MaxPrice = TempData["MaxPrice"]?.ToString();
//                        data.Searchtxt = TempData["Searchtext"]?.ToString();
//                        data.SortID = Convert.ToInt32(TempData["SortID"]?.ToString());
//                        if (data.MinPrice == "" || data.MaxPrice == "")
//                        {
//                            data.MinPrice = "BHD0";
//                            data.MaxPrice = "BHD50000";
//                        }

//                        ViewBag.shopList = filterProduct.GetAllProduct(data);
//                        if (ViewBag.shopList.Count < 1)
//                        {
//                            ViewBag.Message = "No Product Found";
//                        }
//                    }
//                }
//                else
//                {
//                    /*string category = "";
//                    if (TempData["Category"] != null)
//                    {
//                        category = TempData["Category"].ToString();
//                    }
//                    ViewBag.shopList = _service.GetAll(category);*/
//                    ViewBag.shopList = "";
//                    ViewBag.Message = "No Product Found";

//                }

//                return PartialView("AllProducts");
//            }

//        }

//        public JsonResult Filter([FromBody] filterBLL data)
//        {
//            ViewBag.shopList = filterProduct.GetAllProduct(data);
//            return Json(new { data = ViewBag.shopList });
//        }
//        //public JsonResult Filter(filterBLL data)
//        //{
//        //    ViewBag.shopList = filterProduct.GetAll(data);
//        //    return Json(new { data = ViewBag.shopList });
//        //}

//        public IActionResult FilterProducts([FromBody]List<filterBLL> Products)
//        {
//            ViewBag.Message = "";
//            if (Products.Count > 0)
//            {
//                ViewBag.shopList = Products;
//                if (ViewBag.shopList.Count < 1)
//                {
//                    ViewBag.Message = "No Product Found";
//                }
//                return PartialView("AllProducts");
//            }
//            else
//            {
//                if (TempData.Count > 1)
//                {
//                    if (TempData["CategoryIDs"]?.ToString() != "" ||
//                    TempData["ColorIDs"]?.ToString() != "" ||
//                    TempData["MinPrice"]?.ToString() != "" ||
//                    TempData["MaxPrice"]?.ToString() != "" ||
//                    TempData["Searchtext"]?.ToString() != "" ||
//                    TempData["SortID"]?.ToString() != "5")
//                    {
//                        filterBLL data = new filterBLL();
//                        data.Category = TempData["CategoryIDs"]?.ToString();
//                        //data.SubCategory = TempData["SubCategoryIDs"].ToString();
//                        data.Color = TempData["ColorIDs"]?.ToString();
//                        data.MinPrice = TempData["MinPrice"]?.ToString();
//                        data.MaxPrice = TempData["MaxPrice"]?.ToString();
//                        data.Searchtxt = TempData["Searchtext"]?.ToString();
//                        data.SortID = Convert.ToInt32(TempData["SortID"]?.ToString());
//                        if (data.MinPrice == "" || data.MaxPrice == "")
//                        {
//                            data.MinPrice = "BHD0";
//                            data.MaxPrice = "BHD50000";
//                        }

//                        ViewBag.shopList = filterProduct.GetAllProduct(data);
//                        if (ViewBag.shopList.Count < 1)
//                        {
//                            ViewBag.Message = "No Product Found";
//                        }
//                    }
//                }
//                else
//                {
//                    /*string category = "";
//                    if (TempData["Category"] != null)
//                    {
//                        category = TempData["Category"].ToString();
//                    }
//                    ViewBag.shopList = _service.GetAll(category);*/
//                    ViewBag.shopList = "";
//                    ViewBag.Message = "No Product Found";

//                }

//                return PartialView("AllProducts");
//            }

//        }
//    }
//}
