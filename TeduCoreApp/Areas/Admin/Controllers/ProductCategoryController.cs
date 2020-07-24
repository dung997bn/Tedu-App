using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.Product;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    public class ProductCategoryController : BaseController
    {
        IProductCategoryService _productCategory;

        public ProductCategoryController(IProductCategoryService productCategory)
        {
            _productCategory = productCategory;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region AJAX API
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<ProductCategoryViewModel> productCategories;
            productCategories = await _productCategory.GetAll();
            return new OkObjectResult(productCategories);
        }
        #endregion
    }
}
