using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Extensions;
using TeduCoreApp.Models.ProductViewModels;
using TeduCoreApp.Ultilities.Constants;

namespace TeduCoreApp.Controllers.Components
{
    [ViewComponent(Name = "QuickReview")]
    public class QuickReviewViewComponent : ViewComponent
    {
        private readonly IProductService _productService;
        IBillService _billService;

        public QuickReviewViewComponent(IProductService productService, IBillService billService)
        {
            _productService = productService;
            _billService = billService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var session = HttpContext.Session.GetString(CommonConstants.IdProduct);
            var model = new QuickViewViewModel();
            if (session != null)
            {
                try
                {
                    var productId = JsonConvert.DeserializeObject<int>(session);

                    model.Product = _productService.GetById(productId);
                    model.ProductImages = await _productService.GetImages(productId);
                    model.Colors = await _billService.GetColors();
                    model.Sizes = await _billService.GetSizes();
                    return View(model);
                }
                catch (Exception)
                {
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }
    }
}
