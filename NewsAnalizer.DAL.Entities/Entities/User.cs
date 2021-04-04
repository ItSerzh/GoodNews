using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NewsAnalizer.DAL.Core.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime NewsDate { get; set; }
        public DateTime DateCollect { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
