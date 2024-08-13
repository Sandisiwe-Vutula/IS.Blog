using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS.Domain.Models
{
    public class Settings
    {
        public int SettingsID { get; set; }
        public string Theme { get; set; } = String.Empty;
        public string Language { get; set; } = String.Empty;
        public bool Notifications { get; set; }
        public int UserID { get; set; }
        public User User { get; set; } = new User();
    }
}
