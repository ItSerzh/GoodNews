using System;
using System.Collections.Generic;
using System.Text;

namespace NewsAnalizer.Core.DataTransferObjects
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
