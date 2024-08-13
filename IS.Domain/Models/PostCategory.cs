using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS.Domain.Models
{
    public class PostCategory
    {
        public int PostCategoryID { get; set; } // Primary key
        public int PostID { get; set; }
        public int CategoryID { get; set; }
        public Post Post { get; set; }
        public Category Category { get; set; }
    }
}
