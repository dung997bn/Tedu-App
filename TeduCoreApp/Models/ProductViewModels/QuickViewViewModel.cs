using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Application.ViewModels.Bill;
using TeduCoreApp.Application.ViewModels.Common;
using TeduCoreApp.Application.ViewModels.Product;

namespace TeduCoreApp.Models.ProductViewModels
{
    public class QuickViewViewModel
    {
        public ProductViewModel Product { get; set; }

        public List<ProductImageViewModel> ProductImages { set; get; }

        public List<ColorViewModel> Colors { set; get; }

        public List<SizeViewModel> Sizes { set; get; }

        public bool Available { set; get; }
    }
}
