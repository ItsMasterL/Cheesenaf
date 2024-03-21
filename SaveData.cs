using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cheesenaf
{
    public class SaveData
    {
        public bool AltTitle { get; set; }
        public int Night { get; set; }
        public bool CustomUnlocked { get; set; }
        public bool SixUnlocked { get; set; }
        public int Bbg { get; set; } //Syowen: 0 Mocha: 1 Brett: 2 Alan: 3
        public string Username { get; set; }
        public bool FullScreen { get; set; }
        public bool UnlockedSecret { get; set; }
    }
}
