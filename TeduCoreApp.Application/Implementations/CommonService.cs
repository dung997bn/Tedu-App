using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.Common;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.IRepositories;
using TeduCoreApp.Infrastructure.Interfaces;
using TeduCoreApp.Ultilities.Constants;

namespace TeduCoreApp.Application.Implementations
{
    public class CommonService : ICommonService
    {
        IFooterRepository _footerRepository;
        ISystemConfigRepository _systemConfigRepository;
        IUnitOfWork _unitOfWork;
        ISlideRepository _slideRepository;
        private readonly IMapper _mapper;

        public CommonService(IFooterRepository footerRepository,
            ISystemConfigRepository systemConfigRepository, IUnitOfWork unitOfWork,
            ISlideRepository slideRepository, IMapper mapper)
        {
            _footerRepository = footerRepository;
            _systemConfigRepository = systemConfigRepository;
            _unitOfWork = unitOfWork;
            _slideRepository = slideRepository;
            _mapper = mapper;
        }

        public FooterViewModel GetFooter()
        {
            return _mapper.Map<Footer, FooterViewModel>(_footerRepository.FindSingle(x => x.Id ==
            CommonConstants.DefaultFooterId));
        }

        public Task<List<SlideViewModel>> GetSlides(string groupAlias)
        {
            return _mapper.ProjectTo<SlideViewModel>(_slideRepository.FindAll(x => x.Status && x.GroupAlias == groupAlias)).ToListAsync();
        }

        public SystemConfigViewModel GetSystemConfig(string code)
        {
            return _mapper.Map<SystemConfig, SystemConfigViewModel>(_systemConfigRepository.FindSingle(x => x.Id == code));
        }
    }
}
