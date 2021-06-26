using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NewsAnalizer.DAL.Core.Entities
{
    public class RefreshToken : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime ExpiresUtc { get; set; }

        [Required]
        public string Token { get; set; }

        public DateTime CreationDate { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
