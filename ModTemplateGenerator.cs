using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cheesenaf
{
    public class ModTemplateGenerator
    {
        public class Splashes
        {
            public string[] splashtext { get; set; } = new string[1]
            {
                "You have to edit splash.json in the texts folder!"
            };
        }
        public class BBG()
        {
            public string Name { get; set; } = "Modded";
            public List<string> BBGDialogue { get; set; } = new List<string>
            {
                    "This is the introductory line", "This is the first line of text after the username input", "This is the line of dialogue that prompts the yes/no choice", "You chose answer 1. The game proceeds.",
                    "This is the first line after the scene change", "This is the first line after this scene's player dialogue", "This line features disgust from pizza usually", "Second disgust dialogue", "This is the final dialogue before the gameplay transition", "You chose answer 2. The game ends.", "This is the welcome back message, when the BBG already has the player name on starting"
            };
            public List<string> PlayerDialogue { get; set; } = new List<string>
                {
                    "Option 1", "Option 2", "First dialogue", "Second dialogue", "Third dialogue", "Final dialogue.\nTake me to cheesenaf."
                };
            public Color Color { get; set; } = new Color(100, 149, 237, 255);
            public bool OverlayExpression { get; set; } = true;
        }
        public class Pack()
        {
            public string Description { get; set; } = "A blank template for you to edit!";
            public string GameVersion { get; set; } = Game1.Version;
            public string ModVersion { get; set; } = "0.0.0";
        }

        public static void CreateTemplate(Game1 game1)
        {
            string path = "Mods" + Path.DirectorySeparatorChar + "Mod Template";
            int index = 1;
            while (Directory.Exists(path))
            {
                path = "Mods" + Path.DirectorySeparatorChar + "Mod Template (" + index + ")";
                index++;
            }
            Directory.CreateDirectory(path);
            path += Path.DirectorySeparatorChar;
            //Pack general
            string file = JsonSerializer.Serialize<Pack>(new Pack());
            File.WriteAllText(path + "pack.json", file);
            //Text mods
            Directory.CreateDirectory(path + "text");
            file = JsonSerializer.Serialize<Splashes>(new Splashes());
            File.WriteAllText(path + "text" + Path.DirectorySeparatorChar + "splash.json", file);
            //Texture mods
            Directory.CreateDirectory(path + "textures");
            //Audio mods
            Directory.CreateDirectory(path + "audio");
            //BBG Mods
            Texture2D texture = game1.Content.Load<Texture2D>("menus/PizzaPoster");
            FileStream stream;
            stream = File.OpenWrite(path + "temp.png");
            texture.SaveAsPng(stream, texture.Width, texture.Height);
            stream.Dispose();
            Directory.CreateDirectory(path + "bbgs");
            Directory.CreateDirectory(path + "bbgs" + Path.DirectorySeparatorChar + "Syowen");
            Directory.CreateDirectory(path + "bbgs" + Path.DirectorySeparatorChar + "Mocha");
            Directory.CreateDirectory(path + "bbgs" + Path.DirectorySeparatorChar + "Brett");
            Directory.CreateDirectory(path + "bbgs" + Path.DirectorySeparatorChar + "Alan");
            Directory.CreateDirectory(path + "bbgs" + Path.DirectorySeparatorChar + "Berry");
            Directory.CreateDirectory(path + "bbgs" + Path.DirectorySeparatorChar + "Custom");
            foreach (string dir in Directory.GetDirectories(path + "bbgs"))
            {
                file = JsonSerializer.Serialize<BBG>(new BBG());
                File.WriteAllText(dir + Path.DirectorySeparatorChar + "details.json", file);
                File.Copy(path + "temp.png", dir + Path.DirectorySeparatorChar + "base.png");
                File.Copy(path + "temp.png", dir + Path.DirectorySeparatorChar + "happy.png");
                File.Copy(path + "temp.png", dir + Path.DirectorySeparatorChar + "neutral.png");
                File.Copy(path + "temp.png", dir + Path.DirectorySeparatorChar + "unhappy.png");
                File.Copy(path + "temp.png", dir + Path.DirectorySeparatorChar + "select.png");
            }
            File.Delete(path + Path.DirectorySeparatorChar + "temp.png");
            //Help documents
            file = "[FILE LAYOUT]\r\n/mods\r\n--modmusic.wav\r\n----/YourModFolder\r\n------icon.png\r\n------pack.json\r\n--------/audio\r\n----------simtitle.wav\r\n----------simgame.wav\r\n--------/BBGs\r\n------------/BBGName\r\n----------------base.png\r\n----------------details.json\r\n----------------happy.png\r\n----------------neutral.png\r\n----------------unhappy.png\r\n----------------a.wav\r\n----------------e.wav\r\n----------------i.wav\r\n----------------o.wav\r\n----------------u.wav\r\n--------/text\r\n----------splash.json\r\n--------/textures\r\n----------simtitle.png\r\n----------simp1.png\r\n----------simp2.png\r\n\r\n[TEXT MODS]\r\n\r\nSplash Texts\r\n- {0} is the Player name. Defaults to \"player\" if none is set in savedata\r\n- {1} is one of the BBG names, secret character excluded\r\n- {2} is how many splash texts there are in total.\r\n- {3} is how many splash texts there are in total, minus one.\r\n- §C is the countdown splash text\r\n- §R will reverse the text during the draw call\r\n- §W will allow the Wumbo audio to play when pressing W.\r\n\r\n[BBG MODS]\r\nMissing items will prevent loading and will insead load Syowen.\r\n\r\nDialogue\r\n- {0} is the Player name.\r\n- \\n will allow you to create an extra line. Mostly useful for player dialogue, extra lines are automatically made in BBG dialogue.\r\n- If you have more or less dialogue written than in the game normally, it will be replaced with the respective BBG's dialoge, or Syowen if the mod doesnt replace a bbg.\r\n\r\nOverlay Expression\r\nThis sets whether or not base.png is rendered below happy.png, neutral.png, or unhappy.png. Useful if you want a custom welcome back dialogue portrait.\r\n\r\nVoice clips\r\na.wav, i.wav, u.wav, e.wav, and o.wav are optional, and if missing will be replaced by the replaced BBG's voice, or Alan's voice if the mod does not replace an existing BBG.\r\n\r\n[INDICATORS]\r\n\r\nOn the modloader screen, each modpack will have a set of letters that will be colored if they have certain types of mod. This may not always be accurate as in most cases simply having the directory for a type of mod may trigger the game's detection. As a mod creator, please do not leave any directories that go unused to ensure less player confusion.\r\n\r\nT - Text mod\r\nEnabled when there's any modification to the splash texts on the BBGSim title.\r\n\r\nB - BBG Mod\r\nEnabled when a BBG is replaced, or a BBG is added.\r\n\r\nI - Image mod\r\nEnabled when any background images are replaced.\r\n\r\nA - Audio mod\r\nEnabled when any music is replaced.\r\n\r\n[OTHER]\r\n\r\nAny file over 15 mb will not be loaded. Usually this limit will not be reached. Otherwise, images and sounds of any dimension/length will load.";
            File.WriteAllText(path + "ModdingGuide.txt", file);
        }
    }
}
