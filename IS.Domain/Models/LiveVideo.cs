using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS.Domain.Models
{
    public class LiveVideo
    {
        public int LiveVideoID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int UserID { get; set; }
        public string StreamUrl { get; set; } // URL for the video stream
        public bool IsLive { get; set; } // Indicates whether the video is currently live
        public int ViewersCount { get; set; } // Number of viewers during the live session
        public ICollection<Comment> Comments { get; set; } // Comments related to the live video
        public User User { get; set; }
    }


}
