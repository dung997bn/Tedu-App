using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.Product;
using TeduCoreApp.Authorization;
using TeduCoreApp.Ultilities.Helpers;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    public class ProductCategoryController : BaseController
    {
        IProductCategoryService _productCategory;
        private readonly IAuthorizationService _authorizationService;

        public ProductCategoryController(IProductCategoryService productCategory, IAuthorizationService authorizationService)
        {
            _productCategory = productCategory;
            _authorizationService = authorizationService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _authorizationService.AuthorizeAsync(User, "PRODUCT_CATEGORY", Operations.Read);
            if (result.Succeeded == false)
                return new RedirectResult("/Admin/Login/Index");
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
        [HttpPost]
        public IActionResult UpdateParentId(int sourceId, int targetId, Dictionary<int, int> items)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                if (sourceId == targetId)
                {
                    return new BadRequestResult();
                }
                else
                {
                    _productCategory.UpdateParentId(sourceId, targetId, items);
                    _productCategory.Save();
                    return new OkResult();
                }
            }
        }
        [HttpPost]
        public IActionResult ReOrder(int sourceId, int targetId)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                if (sourceId == targetId)
                {
                    return new BadRequestResult();
                }
                else
                {
                    _productCategory.ReOrder(sourceId, targetId);
                    _productCategory.Save();
                    return new OkResult();
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            ProductCategoryViewModel model = null;
            model = await _productCategory.GetById(id);

            return new ObjectResult(model);
        }
        [HttpPost]
        public IActionResult SaveEntity(ProductCategoryViewModel productVm)
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
                    _productCategory.Add(productVm);
                }
                else
                {
                    _productCategory.Update(productVm);
                }
                _productCategory.Save();
                return new OkObjectResult(productVm);

            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new BadRequestResult();
            }
            else
            {
                _productCategory.Delete(id);
                _productCategory.Save();
                return new OkObjectResult(id);
            }
        }
        #endregion
    }
}
