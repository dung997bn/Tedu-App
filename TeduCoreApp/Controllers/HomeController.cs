using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Models;

namespace TeduCoreApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IProductService _productService;
        private IProductCategoryService _productCategoryService;
        private IBlogService _blogService;
        private ICommonService _commonService;

        public HomeController(ILogger<HomeController> logger, IProductService productService,
            IProductCategoryService productCategoryService, IBlogService blogService, ICommonService commonService)
        {
            _logger = logger;
            _productService = productService;
            _productCategoryService = productCategoryService;
            _blogService = blogService;
            _commonService = commonService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["BodyClass"] = "cms-index-index cms-home-page";
            var homeVm = new HomeViewModel();
            homeVm.HomeCategories = await _productCategoryService.GetAll();
            homeVm.HotProducts = await _productService.GetHotProduct(3);
            homeVm.TopSellProducts = await _productService.GetLastest(7);
            homeVm.FeatureProducts = await _productService.GetHotProduct(4);
            homeVm.LastestBlogs = await _blogService.GetLastest(5);
            homeVm.HomeSlides = await _commonService.GetSlides("top");
            return View(homeVm);
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
    }
}
