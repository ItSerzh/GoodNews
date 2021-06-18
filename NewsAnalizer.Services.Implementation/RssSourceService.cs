using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewsAnalizer.Core.DataTransferObjects;
using NewsAnalizer.Core.Services.Interfaces;
using NewsAnalizer.Dal.Repositories.Implementation;
using NewsAnalizer.Dal.Repositories.Interfaces;
using NewsAnalizer.DAL.Core;
using NewsAnalizer.DAL.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalizer.Services.Implementation
{
    public class RssSourceService : IRssSourceService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public RssSourceService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<RssSourceDto>> GetRssSources(Guid? id = null)
        {
            if(id == null)
            {
                return await _unitOfWork.RssSource.FindBy(s => true)
                    .Select(rs => _mapper.Map<RssSourceDto>(rs))
                    .ToListAsync();

            }
            else
            {
                return await _unitOfWork.RssSource.FindBy(s => s.Id == id.Value)
                    .Select(rs => _mapper.Map<RssSourceDto>(rs))?
                    .ToListAsync();
            }
        }
    }
}
