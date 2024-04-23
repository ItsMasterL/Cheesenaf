using Microsoft.Xna.Framework;
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
            public Color Color { get; set; } = Color.CornflowerBlue;
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
            //BBG Mods
            Directory.CreateDirectory(path + "bbgs");
            Directory.CreateDirectory(path + "bbgs" + Path.DirectorySeparatorChar + "Syowen");
            Directory.CreateDirectory(path + "bbgs" + Path.DirectorySeparatorChar + "Mocha");
            Directory.CreateDirectory(path + "bbgs" + Path.DirectorySeparatorChar + "Brett");
            Directory.CreateDirectory(path + "bbgs" + Path.DirectorySeparatorChar + "Alan");
            Directory.CreateDirectory(path + "bbgs" + Path.DirectorySeparatorChar + "Berry");
            Directory.CreateDirectory(path + "bbgs" + Path.DirectorySeparatorChar + "Custom");
            Texture2D texture = game1.Content.Load<Texture2D>("menus/PizzaPoster");
            FileStream stream;
            stream = File.OpenWrite(path + Path.DirectorySeparatorChar + "temp.png");
            texture.SaveAsPng(stream, texture.Width, texture.Height);
            stream.Dispose();
            foreach (string dir in Directory.GetDirectories(path + "bbgs"))
            {
                file = JsonSerializer.Serialize<BBG>(new BBG());
                File.WriteAllText(dir + Path.DirectorySeparatorChar + "details.json", file);
                File.Copy(path + Path.DirectorySeparatorChar + "temp.png", dir + Path.DirectorySeparatorChar + "base.png");
                File.Copy(path + Path.DirectorySeparatorChar + "temp.png", dir + Path.DirectorySeparatorChar + "happy.png");
                File.Copy(path + Path.DirectorySeparatorChar + "temp.png", dir + Path.DirectorySeparatorChar + "neutral.png");
                File.Copy(path + Path.DirectorySeparatorChar + "temp.png", dir + Path.DirectorySeparatorChar + "unhappy.png");
                File.Copy(path + Path.DirectorySeparatorChar + "temp.png", dir + Path.DirectorySeparatorChar + "select.png");
            }
            File.Delete(path + Path.DirectorySeparatorChar + "temp.png");
            //Help documents
            file = "[FILE LAYOUT]\r\n/mods\r\n--modmusic.wav\r\n----/YourModFolder\r\n------icon.png\r\n------pack.json\r\n--------/audio\r\n--------/BBGs\r\n------------/BBGName" +
                "\r\n----------------base.png\r\n----------------details.json\r\n----------------happy.png\r\n----------------neutral.png\r\n----------------unhappy.png\r\n--------/text" +
                "\r\n----------splash.json\r\n--------/textures\r\n\r\n[TEXT MODS]\r\n\r\nSplash Texts\r\n- {0} is the Player name. Defaults to \"player\" if none is set in savedata" +
                "\r\n- {1} is one of the BBG names, secret character excluded\r\n- {2} is how many splash texts there are in total.\r\n- {3} is how many splash texts there are in total," +
                " minus one.\r\n- §C is the countdown splash text\r\n- §R will reverse the text during the draw call\r\n- §W will allow the Wumbo audio to play when pressing W.\r\n\r\n" +
                "[BBG MODS]\r\n\r\nDialogue\r\n- {0} is the Player name.\r\n- \\n will allow you to create an extra line. Mostly useful for player dialogue, extra lines are automatically" +
                " made in BBG dialogue.\r\n\r\nOverlay Expression\r\nThis sets whether or not base.png is rendered below happy.png, neutral.png, or unhappy.png. Useful if you want a custom" +
                " welcome back dialogue portrait.";
            File.WriteAllText(path + "ModdingGuide.txt", file);
        }
    }
}
