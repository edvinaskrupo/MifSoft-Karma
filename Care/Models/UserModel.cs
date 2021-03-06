using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Care.Models
{
    public class UserModel
    {
        [Required]
        [Key]
        public int UserId { get; set; }
        [Required]
        [DisplayName("Email address")]
        public string EmailAddress { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string PasswordSalt { get; set; }

    }
}
