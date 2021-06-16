using System;
using System.Collections.Generic;
using System.Text;

namespace NewsAnalizer.DAL.Core.Entities
{
    public class Role : IBaseEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
