using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS.Domain.Models
{
    public class Request
    {
        public int RequestID { get; set; }
        public int UserID { get; set; }
        ///public User User { get; set; }
        /// <summary>
        /// long RequestedUserID { get; set; }
        /// </summary>
        //public User RequestedUser { get; set; }
        public bool IsAccepted { get; set; }
    }
}
