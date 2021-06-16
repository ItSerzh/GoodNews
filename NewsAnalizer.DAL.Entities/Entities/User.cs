using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NewsAnalizer.DAL.Core.Entities
{
    public class User : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public Guid? RoleId { get; set; }
        public Role Role { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
