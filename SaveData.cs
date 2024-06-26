﻿namespace Cheesenaf
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
        public bool[] splashesSeen { get; set; } = new bool[200];
        public bool SkipModMenu {  get; set; }
        public bool EnableDebugTogglewithTildeKey { get; set; }
        public string[] enabledMods { get; set; }
    }
}
