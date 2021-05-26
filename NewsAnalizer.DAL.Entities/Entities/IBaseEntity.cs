using System;
using System.Collections.Generic;
using System.Text;

namespace NewsAnalizer.DAL.Core.Entities
{
    public interface IBaseEntity
    {
        public Guid Id { get; set; }
    }
}
