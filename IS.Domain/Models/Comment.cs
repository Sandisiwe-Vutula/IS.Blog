using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS.Domain.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public int PostID { get; set; }
        public int UserID { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public bool IsApproved { get; set; } // Indicates whether the comment is approved by the moderator
        public int Likes { get; set; } // Number of likes received for the comment
        public int Dislikes { get; set; } // Number of dislikes received for the comment
        public Post Post { get; set; }
       // public ICollection<Reply> Replies { get; set; } // All the replies to this comment
    }

}
