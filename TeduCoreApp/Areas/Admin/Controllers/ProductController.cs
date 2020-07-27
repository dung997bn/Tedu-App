using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.Product;
using TeduCoreApp.Ultilities.Dtos;
using TeduCoreApp.Ultilities.Helpers;

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


        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _productService.GetById(id);

            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult SaveEntity(ProductViewModel productVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                productVm.SeoAlias = TextHelper.ToUnsignString(productVm.Name);
                if (productVm.Id == 0)
                {
                    _productService.Add(productVm);
                }
                else
                {
                    _productService.Update(productVm);
                }
                _productService.Save();
                return new OkObjectResult(productVm);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                _productService.Delete(id);
                _productService.Save();

                return new OkObjectResult(id);
            }
        }
        #endregion
    }
}
