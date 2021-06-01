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

        public RssSourceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<RssSourceDto>> AddRange(IEnumerable<RssSourceDto> rssSourceDto)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddRssSource(RssSourceDto rssSourceDto)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(RssSourceDto rssSourceDto)
        {
            throw new NotImplementedException();
        }

        public Task<int> Edit(RssSourceDto rssSourceDto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RssSourceDto>> FindRssSource()
        {
            throw new NotImplementedException();
        }

        public Task<RssSourceDto> GetRssSourceById(Guid? id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RssSourceDto>> GetRssSources(Guid? id = null)
        {
            if(id == null)
            {
                return await _unitOfWork.RssSource.FindBy(s => true)
                    .Select(rs => new RssSourceDto
                    {
                        Id = rs.Id,
                        Name = rs.Name,
                        Url = rs.Url
                    })
                    .ToListAsync();

            }
            else
            {
                return await _unitOfWork.RssSource.FindBy(s => s.Id == id.Value)
                    .Select(rs => new RssSourceDto
                    {
                        Id = rs.Id,
                        Name = rs.Name,
                        Url = rs.Url
                    })?
                    .ToListAsync();
            }
        }
    }
}
