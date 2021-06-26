using System;
using System.Collections.Generic;
using System.Text;

namespace NewsAnalyzer.DAL.Core.Entities
{
    public interface IBaseEntity
    {
        public Guid Id { get; set; }
    }
}
