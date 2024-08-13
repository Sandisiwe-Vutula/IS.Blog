using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS.Domain.Models
{
    public class Followers
    {
        [Key]
        public int FollowerID { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public DateTime FollowedDate { get; set; } // Date when the follower started following the user
        public bool IsBlocked { get; set; } // Indicates whether the follower is blocked by the user
    }
}
