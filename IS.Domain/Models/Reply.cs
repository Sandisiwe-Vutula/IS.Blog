using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS.Domain.Models
{
    public class Reply
    {
        public int ReplyID { get; set; }
        public int CommentID { get; set; } // The ID of the comment this reply is associated with
        public string Content { get; set; }=string.Empty;
        public DateTime CreatedDate { get; set; } // The date and time the reply was created
        public DateTime? UpdatedDate { get; set; } // The date and time the reply was last updated (nullable for replies that haven't been edited)
        public bool IsEdited { get; set; } // Indicates whether the reply has been edited
        public int Likes { get; set; } // Number of likes received for the reply
        public int Dislikes { get; set; } // Number of dislikes received for the reply
        public bool IsDeleted { get; set; } // Indicates whether the reply has been deleted (soft deleted)
        //public Comment Comment { get; set; } // The comment this reply is associated with
    }

}
