using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.System;
using TeduCoreApp.Extensions;
using TeduCoreApp.Ultilities.Constants;

namespace TeduCoreApp.Areas.Admin.Components
{
    [ViewComponent(Name = "SideBar")]
    public class SlideBarViewComponent :ViewComponent
    {
        private IFunctionService _functionService;

        public SlideBarViewComponent(IFunctionService functionService)
        {
            _functionService = functionService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var roles = ((ClaimsPrincipal)User).GetSpecificClaim("Roles");
            List<FunctionViewModel> functions;
            if (roles.Split(";").Contains(CommonConstants.AdminRole))
            {
                functions = await _functionService.GetAll(String.Empty);
            }
            else
            {
                //TODO: Get by permission
                functions = await _functionService.GetAll(String.Empty);
            }
            return View(functions);
        }
    }
}
