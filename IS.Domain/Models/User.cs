using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS.Domain.Models
{
    public class User : IdentityUser
    {
        [Key]
        public int UserID { get; set; }
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; }= string.Empty;
        public string LastName { get; set; }= string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;
        public string Role { get; set; }= string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Bio { get; set; }= string.Empty;
        public string Website { get; set; } = string.Empty;
        public string Location { get; set; }=string.Empty;
        public DateTime LastLoginTime { get; set; }
        public bool IsOnline { get; set; }
        public ICollection<Followers> Followers { get; set; }
        public ICollection<Followers> FollowedUsers { get; set; }
    }
}
