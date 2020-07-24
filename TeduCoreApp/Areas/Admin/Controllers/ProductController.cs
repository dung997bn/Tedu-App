using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.Product;
using TeduCoreApp.Ultilities.Dtos;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {

        IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region AJAX API
        [HttpGet]
        //[Route("getall")]
        public async Task<IActionResult> GetAll()
        {
            List<ProductViewModel> products;
            products = await _productService.GetAll();
            return new OkObjectResult(products);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaging(int? categoryId, string keyword, int page, int pageSize)
        {
            PagedResult<ProductViewModel> products;
            products = await _productService.GetAllPaging(categoryId, keyword, page, pageSize);
            return new OkObjectResult(products);
        }
        #endregion
    }
}
