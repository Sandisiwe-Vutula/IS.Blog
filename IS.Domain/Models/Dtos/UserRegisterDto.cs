using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS.Domain.Models.Dtos
{
    public class UserRegisterDto
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;
        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Your password must atleast be 6 characters long")]
        public string Password { get; set; } = string.Empty;
        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Your password must atleast be 6 characters long")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
