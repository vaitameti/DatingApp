using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required] //this verifies that a username is not empty
        public string username { get; set; }
        [Required]
        [StringLength(8,MinimumLength =4)]
        public string password { get; set; }
    }
}