using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeduCoreApp.Application.ViewModels.Common;

namespace TeduCoreApp.Application.Interfaces
{
    public interface ICommonService
    {
        FooterViewModel GetFooter();
        Task<List<SlideViewModel>> GetSlides(string groupAlias);
        SystemConfigViewModel GetSystemConfig(string code);
    }
}
