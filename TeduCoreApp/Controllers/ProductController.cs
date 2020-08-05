using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Models.ProductViewModels;

namespace TeduCoreApp.Controllers
{
    public class ProductController : Controller
    {
        IProductService _productService;
        IProductCategoryService _productCategoryService;
        IConfiguration _configuration;
        public ProductController(IProductService productService, IConfiguration configuration,
            IProductCategoryService productCategoryService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
            _configuration = configuration;
        }
        [Route("products.html")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("{alias}-c.{id}.html")]
        public async Task<IActionResult> Catalog(int id, int? pageSize, string sortBy, int page = 1)
        {
            var catalog = new CatalogViewModel();
            ViewData["BodyClass"] = "shop_grid_full_width_page";
            if (pageSize == null)
                pageSize = _configuration.GetValue<int>("PageSize");

            catalog.PageSize = pageSize;
            catalog.SortType = sortBy;
            catalog.Data = await _productService.GetAllPaging(id, string.Empty, page, pageSize.Value);
            switch (sortBy)
            {
                case "lastest":
                    catalog.Data.Results = catalog.Data.Results.OrderByDescending(x => x.DateCreated).ToList();
                    break;
                case "price":
                    catalog.Data.Results = catalog.Data.Results.OrderByDescending(x => x.Price).ToList(); ;
                    break;
                case "name":
                    catalog.Data.Results = catalog.Data.Results.OrderByDescending(x => x.Name).ToList(); ;
                    break;
                default:
                    catalog.Data.Results = catalog.Data.Results;
                    break;
            }
            catalog.Category = await _productCategoryService.GetById(id);
            return View(catalog);
        }



        [Route("search.html")]
        public async Task<IActionResult> Search(string keyword, int? pageSize, string sortBy, int page = 1)
        {
            var catalog = new SearchResultViewModel();
            ViewData["BodyClass"] = "shop_grid_full_width_page";
            if (pageSize == null)
                pageSize = _configuration.GetValue<int>("PageSize");

            catalog.PageSize = pageSize;
            catalog.SortType = sortBy;
            catalog.Data = await _productService.GetAllPaging(null, keyword, page, pageSize.Value);
            switch (sortBy)
            {
                case "lastest":
                    catalog.Data.Results = catalog.Data.Results.OrderByDescending(x => x.DateCreated).ToList();
                    break;
                case "price":
                    catalog.Data.Results = catalog.Data.Results.OrderByDescending(x => x.Price).ToList(); ;
                    break;
                case "name":
                    catalog.Data.Results = catalog.Data.Results.OrderByDescending(x => x.Name).ToList(); ;
                    break;
                default:
                    catalog.Data.Results = catalog.Data.Results;
                    break;
            }
            catalog.Keyword = keyword;
            return View(catalog);
        }

        [Route("{alias}-p.{id}.html", Name = "ProductDetail")]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ViewData["BodyClass"] = "product-page";
            var model = new DetailViewModel();
            model.Product = _productService.GetById(id);
            model.Category = await _productCategoryService.GetById(model.Product.CategoryId);
            model.RelatedProducts = await _productService.GetRelatedProducts(id, 9);
            model.UpsellProducts = await _productService.GetUpsellProducts(6);
            model.ProductImages = await _productService.GetImages(id);
            model.Tags = await _productService.GetProductTags(id);
            return View(model);
        }
    }


}
