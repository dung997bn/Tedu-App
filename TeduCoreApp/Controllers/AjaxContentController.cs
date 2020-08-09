using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Extensions;
using TeduCoreApp.Ultilities.Constants;

namespace TeduCoreApp.Controllers
{
    public class AjaxContentController : Controller
    {
        public IActionResult HeaderCart()
        {
            return ViewComponent("HeaderCart");
        }

        [HttpPost]
        public IActionResult SetIdProduct(int productId)
        {
            var session = HttpContext.Session.GetString(CommonConstants.IdProduct);
            HttpContext.Session.Set(CommonConstants.IdProduct, productId);
            return new OkObjectResult(productId);
        }

        public IActionResult QuickReView()
        {
            return ViewComponent("QuickReview");
        }

    }
}
