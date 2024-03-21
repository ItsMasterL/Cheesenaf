using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cheesenaf
{
    public class SaveData
    {
        public bool altTitle { get; set; }
        public int Night { get; set; }
        public bool CustomUnlocked { get; set; }
        public bool SixUnlocked { get; set; }
        public int bbg { get; set; } //Syowen: 0 Mocha: 1 Brett: 2 Alan: 3
        public string Username { get; set; }
        public bool FullScreen { get; set; }
    }
}
