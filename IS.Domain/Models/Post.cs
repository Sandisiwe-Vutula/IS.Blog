using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS.Domain.Models
{
    public class Post
    {
        public int PostID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public int Likes { get; set; }
        public int ViewCount { get; set; } // Number of times the post has been viewed
        public bool IsPublished { get; set; } // Indicates whether the post is published or in draft
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Category> Categories { get; set; } // Categories associated with the post
    }
}
