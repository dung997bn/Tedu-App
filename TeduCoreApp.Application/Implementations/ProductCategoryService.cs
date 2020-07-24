using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.Product;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.IRepositories;
using TeduCoreApp.Infrastructure.Interfaces;

namespace TeduCoreApp.Application.Implementations
{
    public class ProductCategoryService : IProductCategoryService
    {
        private IProductCategoryRepository _productCategoryRepository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productCategoryRepository = productCategoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ProductCategoryViewModel Add(ProductCategoryViewModel productCategoryVm)
        {
            var productCategory = Mapper.Map<ProductCategoryViewModel, ProductCategory>(productCategoryVm);
            _productCategoryRepository.Add(productCategory);
            return productCategoryVm;

        }

        public void Delete(int id)
        {
            _productCategoryRepository.Remove(id);
        }

        public Task<List<ProductCategoryViewModel>> GetAll()
        {
            var productCategories = _productCategoryRepository.FindAll().OrderBy(x => x.ParentId);
            return _mapper.ProjectTo<ProductCategoryViewModel>(productCategories).ToListAsync();
        }

        public Task<List<ProductCategoryViewModel>> GetAll(string keyword)
        {
            var productCategories = _productCategoryRepository.FindAll();
            if (!string.IsNullOrEmpty(keyword))
                return _mapper.ProjectTo<ProductCategoryViewModel>(productCategories.Where(x => x.Name.Contains(keyword)
                || x.Description.Contains(keyword))
                    .OrderBy(x => x.ParentId)).ToListAsync();

            else
                return _mapper.ProjectTo<ProductCategoryViewModel>(productCategories
                    .OrderBy(x => x.ParentId)).ToListAsync();
        }

        public Task<List<ProductCategoryViewModel>> GetAllByParentId(int parentId)
        {
            var productCategories = _productCategoryRepository.FindAll(x => x.Status == Status.Active && x.ParentId == parentId);
            return _mapper.ProjectTo<ProductCategoryViewModel>(productCategories
                          .OrderBy(x => x.ParentId)).ToListAsync();
        }

        public Task<ProductCategoryViewModel> GetById(int id)
        {
            return Task.FromResult(_mapper.Map<ProductCategory, ProductCategoryViewModel>(_productCategoryRepository.FindById(id)));

        }

        public Task<List<ProductCategoryViewModel>> GetHomeCategories(int top)
        {
            var query = _productCategoryRepository
                .FindAll(x => x.HomeFlag == true, c => c.Products)
                  .OrderBy(x => x.HomeOrder)
                  .Take(top);;

            var categories = query;
            foreach (var category in categories)
            {
                //category.Products = _productRepository
                //    .FindAll(x => x.HotFlag == true && x.CategoryId == category.Id)
                //    .OrderByDescending(x => x.DateCreated)
                //    .Take(5)
                //    .ProjectTo<ProductViewModel>().ToList();
            }
            return _mapper.ProjectTo<ProductCategoryViewModel>(categories.OrderBy(x => x.ParentId)).ToListAsync();
        }

        public void ReOrder(int sourceId, int targetId)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProductCategoryViewModel productCategoryVm)
        {
            throw new NotImplementedException();
        }

        public void UpdateParentId(int sourceId, int targetId, Dictionary<int, int> items)
        {
            throw new NotImplementedException();
        }
    }
}
