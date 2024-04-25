using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using System;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;

namespace Cheesenaf
{
    internal class BBGSim
    {
        Game1 Game1;
        public class BBG()
        {
            public string Name { get; set; }
            public List<string> BBGDialogue { get; set; }
            public List<string> PlayerDialogue { get; set; }
            public Color Color { get; set; }
            public bool OverlayExpression { get; set; }
        }
        BBG Syowen;
        BBG Mocha;
        BBG Brett;
        BBG Alan;
        BBG Berry;
        BBG Custom;
        string name;
        List<string> bbgDialogue;
        List<string> playerDialogue;
        string display = "";
        string buffer = "";
        string userInput;
        int delay = 1; //In frames
        int currentDelay;

        int dialoguePart = -1;

        int[] textcoords = new int[2] {360,900};
        const float wraplimit = 1000f;

        int choicesPulledUp;
        bool inChoice;

        float[] scale = new float[3];
        float[] scaleGoal = new float[3];
        bool[] scaleDown = new bool[3];
        int scaleIndex;

        int selection;
        int currentSelectionCount;

        Texture2D[] bbgs;
        Texture2D[] bgs;
        SoundEffect[] voices;
        int emotion = 1;
        bool emotionOverlay;
        Color nametagColor;
        Song song;

        Texture2D dialogueUI;
        Texture2D enterSign;
        float enterAlpha;
        Rectangle[] UIRects = new Rectangle[4];

        SoundEffect[] sounds;
        SoundEffectInstance soundInstance;
        SoundEffectInstance boxZooms;
        public void LoadContent(ContentManager Content)
        {
            bbgs = new Texture2D[4];
            voices = new SoundEffect[5];
            string path;
            switch (Game1.saveData.Bbg)
            {
                case 0:
                    foreach (string mod in Game1.Modpacks)
                    {
                        if (Directory.Exists("Mods" + Path.DirectorySeparatorChar + mod + Path.DirectorySeparatorChar + "bbgs" + Path.DirectorySeparatorChar + "Syowen"))
                        {
                            path = "Mods" + Path.DirectorySeparatorChar + mod + Path.DirectorySeparatorChar + "bbgs" + Path.DirectorySeparatorChar + "Syowen" + Path.DirectorySeparatorChar;
                            try
                            {
                                FileInfo fi = new FileInfo(path + "base.png");
                                if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                    bbgs[0] = Texture2D.FromFile(Game1.GraphicsDevice, path + "base.png");
                                else
                                    bbgs[0] = Content.Load<Texture2D>("BBGSim/syowen");
                                fi = new FileInfo(path + "happy.png");
                                if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                    bbgs[1] = Texture2D.FromFile(Game1.GraphicsDevice, path + "happy.png");
                                else
                                    bbgs[1] = Content.Load<Texture2D>("BBGSim/syowenhappy");
                                fi = new FileInfo(path + "neutral.png");
                                if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                    bbgs[2] = Texture2D.FromFile(Game1.GraphicsDevice, path + "neutral.png");
                                else
                                    bbgs[2] = Content.Load<Texture2D>("BBGSim/syowenneutral");
                                fi = new FileInfo(path + "unhappy.png");
                                if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                    bbgs[3] = Texture2D.FromFile(Game1.GraphicsDevice, path + "unhappy.png");
                                else
                                    bbgs[3] = Content.Load<Texture2D>("BBGSim/syowenunhappy");
                                try
                                {
                                    fi = new FileInfo(path + "a.wav");
                                    if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                        voices[0] = SoundEffect.FromFile(path + "a.wav");
                                    fi = new FileInfo(path + "i.wav");
                                    if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                        voices[1] = SoundEffect.FromFile(path + "i.wav");
                                    fi = new FileInfo(path + "u.wav");
                                    if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                        voices[2] = SoundEffect.FromFile(path + "u.wav");
                                    fi = new FileInfo(path + "e.wav");
                                    if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                        voices[3] = SoundEffect.FromFile(path + "e.wav");
                                    fi = new FileInfo(path + "o.wav");
                                    if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                        voices[4] = SoundEffect.FromFile(path + "o.wav");
                                }
                                catch
                                {
                                    voices[0] = Content.Load<SoundEffect>("Audio/Voices/sb_a");
                                    voices[1] = Content.Load<SoundEffect>("Audio/Voices/sb_i");
                                    voices[2] = Content.Load<SoundEffect>("Audio/Voices/sb_u");
                                    voices[3] = Content.Load<SoundEffect>("Audio/Voices/sb_e");
                                    voices[4] = Content.Load<SoundEffect>("Audio/Voices/sb_o");
                                }
                                string json = File.ReadAllText(path + "details.json");
                                Custom = JsonSerializer.Deserialize<BBG>(json);
                                if (Custom.BBGDialogue.Count == Syowen.BBGDialogue.Count)
                                    bbgDialogue = Custom.BBGDialogue;
                                else
                                    bbgDialogue = Syowen.BBGDialogue;
                                if (Custom.PlayerDialogue.Count == Syowen.PlayerDialogue.Count)
                                    playerDialogue = Custom.PlayerDialogue;
                                else
                                    playerDialogue = Syowen.PlayerDialogue;
                                name = Custom.Name;
                                nametagColor = Custom.Color;
                                emotionOverlay = Custom.OverlayExpression;
                                break;
                            }
                            catch
                            {
                                bbgs[0] = Content.Load<Texture2D>("BBGSim/syowen");
                                bbgs[1] = Content.Load<Texture2D>("BBGSim/syowenhappy");
                                bbgs[2] = Content.Load<Texture2D>("BBGSim/syowenneutral");
                                bbgs[3] = Content.Load<Texture2D>("BBGSim/syowenunhappy");
                                voices[0] = Content.Load<SoundEffect>("Audio/Voices/sb_a");
                                voices[1] = Content.Load<SoundEffect>("Audio/Voices/sb_i");
                                voices[2] = Content.Load<SoundEffect>("Audio/Voices/sb_u");
                                voices[3] = Content.Load<SoundEffect>("Audio/Voices/sb_e");
                                voices[4] = Content.Load<SoundEffect>("Audio/Voices/sb_o");
                                bbgDialogue = Syowen.BBGDialogue;
                                playerDialogue = Syowen.PlayerDialogue;
                                name = Syowen.Name;
                                nametagColor = Syowen.Color;
                                emotionOverlay = Syowen.OverlayExpression;
                            }
                        }
                        else
                        {
                            bbgs[0] = Content.Load<Texture2D>("BBGSim/syowen");
                            bbgs[1] = Content.Load<Texture2D>("BBGSim/syowenhappy");
                            bbgs[2] = Content.Load<Texture2D>("BBGSim/syowenneutral");
                            bbgs[3] = Content.Load<Texture2D>("BBGSim/syowenunhappy");
                            voices[0] = Content.Load<SoundEffect>("Audio/Voices/sb_a");
                            voices[1] = Content.Load<SoundEffect>("Audio/Voices/sb_i");
                            voices[2] = Content.Load<SoundEffect>("Audio/Voices/sb_u");
                            voices[3] = Content.Load<SoundEffect>("Audio/Voices/sb_e");
                            voices[4] = Content.Load<SoundEffect>("Audio/Voices/sb_o");
                            bbgDialogue = Syowen.BBGDialogue;
                            playerDialogue = Syowen.PlayerDialogue;
                            name = Syowen.Name;
                            nametagColor = Syowen.Color;
                            emotionOverlay = Syowen.OverlayExpression;
                        }
                    }
                    break;
                case 1:
                    foreach (string mod in Game1.Modpacks)
                    {
                        if (Directory.Exists("Mods" + Path.DirectorySeparatorChar + mod + Path.DirectorySeparatorChar + "bbgs" + Path.DirectorySeparatorChar + "Mocha"))
                        {
                            path = "Mods" + Path.DirectorySeparatorChar + mod + Path.DirectorySeparatorChar + "bbgs" + Path.DirectorySeparatorChar + "Mocha" + Path.DirectorySeparatorChar;
                            try
                            {
                                FileInfo fi = new FileInfo(path + "base.png");
                                if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                    bbgs[0] = Texture2D.FromFile(Game1.GraphicsDevice, path + "base.png");
                                else
                                    bbgs[0] = Content.Load<Texture2D>("BBGSim/mocha");
                                fi = new FileInfo(path + "happy.png");
                                if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                    bbgs[1] = Texture2D.FromFile(Game1.GraphicsDevice, path + "happy.png");
                                else
                                    bbgs[1] = Content.Load<Texture2D>("BBGSim/mochahappy");
                                fi = new FileInfo(path + "neutral.png");
                                if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                    bbgs[2] = Texture2D.FromFile(Game1.GraphicsDevice, path + "neutral.png");
                                else
                                    bbgs[2] = Content.Load<Texture2D>("BBGSim/mochaneutral");
                                fi = new FileInfo(path + "unhappy.png");
                                if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                    bbgs[3] = Texture2D.FromFile(Game1.GraphicsDevice, path + "unhappy.png");
                                else
                                    bbgs[3] = Content.Load<Texture2D>("BBGSim/mochaunhappy");

                                try
                                {
                                    fi = new FileInfo(path + "a.wav");
                                    if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                        voices[0] = SoundEffect.FromFile(path + "a.wav");
                                    fi = new FileInfo(path + "i.wav");
                                    if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                        voices[1] = SoundEffect.FromFile(path + "i.wav");
                                    fi = new FileInfo(path + "u.wav");
                                    if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                        voices[2] = SoundEffect.FromFile(path + "u.wav");
                                    fi = new FileInfo(path + "e.wav");
                                    if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                        voices[3] = SoundEffect.FromFile(path + "e.wav");
                                    fi = new FileInfo(path + "o.wav");
                                    if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                        voices[4] = SoundEffect.FromFile(path + "o.wav");
                                }
                                catch
                                {
                                    voices[0] = Content.Load<SoundEffect>("Audio/Voices/mm_a");
                                    voices[1] = Content.Load<SoundEffect>("Audio/Voices/mm_i");
                                    voices[2] = Content.Load<SoundEffect>("Audio/Voices/mm_u");
                                    voices[3] = Content.Load<SoundEffect>("Audio/Voices/mm_e");
                                    voices[4] = Content.Load<SoundEffect>("Audio/Voices/mm_o");
                                }
                                string json = File.ReadAllText(path + "details.json");
                                Custom = JsonSerializer.Deserialize<BBG>(json);
                                if (Custom.BBGDialogue.Count == Mocha.BBGDialogue.Count)
                                    bbgDialogue = Custom.BBGDialogue;
                                else
                                    bbgDialogue = Mocha.BBGDialogue;
                                if (Custom.PlayerDialogue.Count == Mocha.PlayerDialogue.Count)
                                    playerDialogue = Custom.PlayerDialogue;
                                else
                                    playerDialogue = Mocha.PlayerDialogue;
                                name = Custom.Name;
                                nametagColor = Custom.Color;
                                emotionOverlay = Custom.OverlayExpression;
                                break;
                            }
                            catch
                            {
                                bbgs[0] = Content.Load<Texture2D>("BBGSim/mocha");
                                bbgs[1] = Content.Load<Texture2D>("BBGSim/mochahappy");
                                bbgs[2] = Content.Load<Texture2D>("BBGSim/mochaneutral");
                                bbgs[3] = Content.Load<Texture2D>("BBGSim/mochaunhappy");
                                voices[0] = Content.Load<SoundEffect>("Audio/Voices/mm_a");
                                voices[1] = Content.Load<SoundEffect>("Audio/Voices/mm_i");
                                voices[2] = Content.Load<SoundEffect>("Audio/Voices/mm_u");
                                voices[3] = Content.Load<SoundEffect>("Audio/Voices/mm_e");
                                voices[4] = Content.Load<SoundEffect>("Audio/Voices/mm_o");
                                bbgDialogue = Mocha.BBGDialogue;
                                playerDialogue = Mocha.PlayerDialogue;
                                name = Mocha.Name;
                                nametagColor = Mocha.Color;
                                emotionOverlay = Mocha.OverlayExpression;
                            }
                        }
                        else
                        {
                            bbgs[0] = Content.Load<Texture2D>("BBGSim/mocha");
                            bbgs[1] = Content.Load<Texture2D>("BBGSim/mochahappy");
                            bbgs[2] = Content.Load<Texture2D>("BBGSim/mochaneutral");
                            bbgs[3] = Content.Load<Texture2D>("BBGSim/mochaunhappy");
                            voices[0] = Content.Load<SoundEffect>("Audio/Voices/mm_a");
                            voices[1] = Content.Load<SoundEffect>("Audio/Voices/mm_i");
                            voices[2] = Content.Load<SoundEffect>("Audio/Voices/mm_u");
                            voices[3] = Content.Load<SoundEffect>("Audio/Voices/mm_e");
                            voices[4] = Content.Load<SoundEffect>("Audio/Voices/mm_o");
                            bbgDialogue = Mocha.BBGDialogue;
                            playerDialogue = Mocha.PlayerDialogue;
                            name = Mocha.Name;
                            nametagColor = Mocha.Color;
                            emotionOverlay = Mocha.OverlayExpression;
                        }
                    }
                    break;
                case 2:
                    foreach (string mod in Game1.Modpacks)
                    {
                        if (Directory.Exists("Mods" + Path.DirectorySeparatorChar + mod + Path.DirectorySeparatorChar + "bbgs" + Path.DirectorySeparatorChar + "Brett"))
                        {
                            path = "Mods" + Path.DirectorySeparatorChar + mod + Path.DirectorySeparatorChar + "bbgs" + Path.DirectorySeparatorChar + "Brett" + Path.DirectorySeparatorChar;
                            try
                            {
                                FileInfo fi = new FileInfo(path + "base.png");
                                if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                    bbgs[0] = Texture2D.FromFile(Game1.GraphicsDevice, path + "base.png");
                                else
                                    bbgs[0] = Content.Load<Texture2D>("BBGSim/brett");
                                fi = new FileInfo(path + "happy.png");
                                if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                    bbgs[1] = Texture2D.FromFile(Game1.GraphicsDevice, path + "happy.png");
                                else
                                    bbgs[1] = Content.Load<Texture2D>("BBGSim/bretthappy");
                                fi = new FileInfo(path + "neutral.png");
                                if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                    bbgs[2] = Texture2D.FromFile(Game1.GraphicsDevice, path + "neutral.png");
                                else
                                    bbgs[2] = Content.Load<Texture2D>("BBGSim/brettneutral");
                                fi = new FileInfo(path + "unhappy.png");
                                if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                    bbgs[3] = Texture2D.FromFile(Game1.GraphicsDevice, path + "unhappy.png");
                                else
                                    bbgs[3] = Content.Load<Texture2D>("BBGSim/brettunhappy");

                                try
                                {
                                    fi = new FileInfo(path + "a.wav");
                                    if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                        voices[0] = SoundEffect.FromFile(path + "a.wav");
                                    fi = new FileInfo(path + "i.wav");
                                    if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                        voices[1] = SoundEffect.FromFile(path + "i.wav");
                                    fi = new FileInfo(path + "u.wav");
                                    if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                        voices[2] = SoundEffect.FromFile(path + "u.wav");
                                    fi = new FileInfo(path + "e.wav");
                                    if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                        voices[3] = SoundEffect.FromFile(path + "e.wav");
                                    fi = new FileInfo(path + "o.wav");
                                    if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                        voices[4] = SoundEffect.FromFile(path + "o.wav");
                                }
                                catch
                                {
                                    voices[0] = Content.Load<SoundEffect>("Audio/Voices/gb_a");
                                    voices[1] = Content.Load<SoundEffect>("Audio/Voices/gb_i");
                                    voices[2] = Content.Load<SoundEffect>("Audio/Voices/gb_u");
                                    voices[3] = Content.Load<SoundEffect>("Audio/Voices/gb_e");
                                    voices[4] = Content.Load<SoundEffect>("Audio/Voices/gb_o");
                                }
                                string json = File.ReadAllText(path + "details.json");
                                Custom = JsonSerializer.Deserialize<BBG>(json);
                                if (Custom.BBGDialogue.Count == Brett.BBGDialogue.Count)
                                    bbgDialogue = Custom.BBGDialogue;
                                else
                                    bbgDialogue = Brett.BBGDialogue;
                                if (Custom.PlayerDialogue.Count == Brett.PlayerDialogue.Count)
                                    playerDialogue = Custom.PlayerDialogue;
                                else
                                    playerDialogue = Brett.PlayerDialogue;
                                name = Custom.Name;
                                nametagColor = Custom.Color;
                                emotionOverlay = Custom.OverlayExpression;
                                break;
                            }
                            catch
                            {
                                bbgs[0] = Content.Load<Texture2D>("BBGSim/brett");
                                bbgs[1] = Content.Load<Texture2D>("BBGSim/bretthappy");
                                bbgs[2] = Content.Load<Texture2D>("BBGSim/brettneutral");
                                bbgs[3] = Content.Load<Texture2D>("BBGSim/brettunhappy");
                                voices[0] = Content.Load<SoundEffect>("Audio/Voices/gb_a");
                                voices[1] = Content.Load<SoundEffect>("Audio/Voices/gb_i");
                                voices[2] = Content.Load<SoundEffect>("Audio/Voices/gb_u");
                                voices[3] = Content.Load<SoundEffect>("Audio/Voices/gb_e");
                                voices[4] = Content.Load<SoundEffect>("Audio/Voices/gb_o");
                                bbgDialogue = Brett.BBGDialogue;
                                playerDialogue = Brett.PlayerDialogue;
                                name = Brett.Name;
                                nametagColor = Brett.Color;
                                emotionOverlay = Brett.OverlayExpression;
                            }
                        }
                        else
                        {
                            bbgs[0] = Content.Load<Texture2D>("BBGSim/brett");
                            bbgs[1] = Content.Load<Texture2D>("BBGSim/bretthappy");
                            bbgs[2] = Content.Load<Texture2D>("BBGSim/brettneutral");
                            bbgs[3] = Content.Load<Texture2D>("BBGSim/brettunhappy");
                            voices[0] = Content.Load<SoundEffect>("Audio/Voices/gb_a");
                            voices[1] = Content.Load<SoundEffect>("Audio/Voices/gb_i");
                            voices[2] = Content.Load<SoundEffect>("Audio/Voices/gb_u");
                            voices[3] = Content.Load<SoundEffect>("Audio/Voices/gb_e");
                            voices[4] = Content.Load<SoundEffect>("Audio/Voices/gb_o");
                            bbgDialogue = Brett.BBGDialogue;
                            playerDialogue = Brett.PlayerDialogue;
                            name = Brett.Name;
                            nametagColor = Brett.Color;
                            emotionOverlay = Brett.OverlayExpression;
                        }
                    }
                    break;
                case 3:
                    foreach (string mod in Game1.Modpacks)
                    {
                        if (Directory.Exists("Mods" + Path.DirectorySeparatorChar + mod + Path.DirectorySeparatorChar + "bbgs" + Path.DirectorySeparatorChar + "Alan"))
                        {
                            path = "Mods" + Path.DirectorySeparatorChar + mod + Path.DirectorySeparatorChar + "bbgs" + Path.DirectorySeparatorChar + "Alan" + Path.DirectorySeparatorChar;
                            try
                            {
                                FileInfo fi = new FileInfo(path + "base.png");
                                if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                    bbgs[0] = Texture2D.FromFile(Game1.GraphicsDevice, path + "base.png");
                                else
                                    bbgs[0] = Content.Load<Texture2D>("BBGSim/alan");
                                fi = new FileInfo(path + "happy.png");
                                if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                    bbgs[1] = Texture2D.FromFile(Game1.GraphicsDevice, path + "happy.png");
                                else
                                    bbgs[1] = Content.Load<Texture2D>("BBGSim/alanhappy");
                                fi = new FileInfo(path + "neutral.png");
                                if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                    bbgs[2] = Texture2D.FromFile(Game1.GraphicsDevice, path + "neutral.png");
                                else
                                    bbgs[2] = Content.Load<Texture2D>("BBGSim/alanneutral");
                                fi = new FileInfo(path + "unhappy.png");
                                if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                    bbgs[3] = Texture2D.FromFile(Game1.GraphicsDevice, path + "unhappy.png");
                                else
                                    bbgs[3] = Content.Load<Texture2D>("BBGSim/alanunhappy");

                                try
                                {
                                    fi = new FileInfo(path + "a.wav");
                                    if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                        voices[0] = SoundEffect.FromFile(path + "a.wav");
                                    fi = new FileInfo(path + "i.wav");
                                    if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                        voices[1] = SoundEffect.FromFile(path + "i.wav");
                                    fi = new FileInfo(path + "u.wav");
                                    if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                        voices[2] = SoundEffect.FromFile(path + "u.wav");
                                    fi = new FileInfo(path + "e.wav");
                                    if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                        voices[3] = SoundEffect.FromFile(path + "e.wav");
                                    fi = new FileInfo(path + "o.wav");
                                    if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                        voices[4] = SoundEffect.FromFile(path + "o.wav");
                                }
                                catch
                                {
                                    voices[0] = Content.Load<SoundEffect>("Audio/Voices/sawyer-01");
                                    voices[1] = Content.Load<SoundEffect>("Audio/Voices/sawyer-02");
                                    voices[2] = Content.Load<SoundEffect>("Audio/Voices/sawyer-03");
                                    voices[3] = Content.Load<SoundEffect>("Audio/Voices/sawyer-04");
                                    voices[4] = Content.Load<SoundEffect>("Audio/Voices/sawyer-05");
                                }
                                string json = File.ReadAllText(path + "details.json");
                                Custom = JsonSerializer.Deserialize<BBG>(json);
                                if (Custom.BBGDialogue.Count == Alan.BBGDialogue.Count)
                                    bbgDialogue = Custom.BBGDialogue;
                                else
                                    bbgDialogue = Alan.BBGDialogue;
                                if (Custom.PlayerDialogue.Count == Alan.PlayerDialogue.Count)
                                    playerDialogue = Custom.PlayerDialogue;
                                else
                                    playerDialogue = Alan.PlayerDialogue;
                                name = Custom.Name;
                                nametagColor = Custom.Color;
                                emotionOverlay = Custom.OverlayExpression;
                                break;
                            }
                            catch
                            {
                                bbgs[0] = Content.Load<Texture2D>("BBGSim/alan");
                                bbgs[1] = Content.Load<Texture2D>("BBGSim/alanhappy");
                                bbgs[2] = Content.Load<Texture2D>("BBGSim/alanneutral");
                                bbgs[3] = Content.Load<Texture2D>("BBGSim/alanunhappy");
                                voices[0] = Content.Load<SoundEffect>("Audio/Voices/sawyer-01");
                                voices[1] = Content.Load<SoundEffect>("Audio/Voices/sawyer-02");
                                voices[2] = Content.Load<SoundEffect>("Audio/Voices/sawyer-03");
                                voices[3] = Content.Load<SoundEffect>("Audio/Voices/sawyer-04");
                                voices[4] = Content.Load<SoundEffect>("Audio/Voices/sawyer-05");
                                bbgDialogue = Alan.BBGDialogue;
                                playerDialogue = Alan.PlayerDialogue;
                                name = Alan.Name;
                                nametagColor = Alan.Color;
                                emotionOverlay = Alan.OverlayExpression;
                            }
                        }
                        else
                        {
                            bbgs[0] = Content.Load<Texture2D>("BBGSim/alan");
                            bbgs[1] = Content.Load<Texture2D>("BBGSim/alanhappy");
                            bbgs[2] = Content.Load<Texture2D>("BBGSim/alanneutral");
                            bbgs[3] = Content.Load<Texture2D>("BBGSim/alanunhappy");
                            voices[0] = Content.Load<SoundEffect>("Audio/Voices/sawyer-01");
                            voices[1] = Content.Load<SoundEffect>("Audio/Voices/sawyer-02");
                            voices[2] = Content.Load<SoundEffect>("Audio/Voices/sawyer-03");
                            voices[3] = Content.Load<SoundEffect>("Audio/Voices/sawyer-04");
                            voices[4] = Content.Load<SoundEffect>("Audio/Voices/sawyer-05");
                            bbgDialogue = Alan.BBGDialogue;
                            playerDialogue = Alan.PlayerDialogue;
                            name = Alan.Name;
                            nametagColor = Alan.Color;
                            emotionOverlay = Alan.OverlayExpression;
                        }
                    }
                    break;
                case 4:
                    foreach (string mod in Game1.Modpacks)
                    {
                        if (Directory.Exists("Mods" + Path.DirectorySeparatorChar + mod + Path.DirectorySeparatorChar + "bbgs" + Path.DirectorySeparatorChar + "Berry"))
                        {
                            path = "Mods" + Path.DirectorySeparatorChar + mod + Path.DirectorySeparatorChar + "bbgs" + Path.DirectorySeparatorChar + "Berry" + Path.DirectorySeparatorChar;
                            try
                            {
                                FileInfo fi = new FileInfo(path + "base.png");
                                if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                    bbgs[0] = Texture2D.FromFile(Game1.GraphicsDevice, path + "base.png");
                                else
                                    bbgs[0] = Content.Load<Texture2D>("BBGSim/berry");
                                fi = new FileInfo(path + "happy.png");
                                if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                    bbgs[1] = Texture2D.FromFile(Game1.GraphicsDevice, path + "happy.png");
                                else
                                    bbgs[1] = Content.Load<Texture2D>("BBGSim/berryhappy");
                                fi = new FileInfo(path + "neutral.png");
                                if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                    bbgs[2] = Texture2D.FromFile(Game1.GraphicsDevice, path + "neutral.png");
                                else
                                    bbgs[2] = Content.Load<Texture2D>("BBGSim/berryneutral");
                                fi = new FileInfo(path + "unhappy.png");
                                if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                    bbgs[3] = Texture2D.FromFile(Game1.GraphicsDevice, path + "unhappy.png");
                                else
                                    bbgs[3] = Content.Load<Texture2D>("BBGSim/berryunhappy");

                                try
                                {
                                    fi = new FileInfo(path + "a.wav");
                                    if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                        voices[0] = SoundEffect.FromFile(path + "a.wav");
                                    fi = new FileInfo(path + "i.wav");
                                    if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                        voices[1] = SoundEffect.FromFile(path + "i.wav");
                                    fi = new FileInfo(path + "u.wav");
                                    if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                        voices[2] = SoundEffect.FromFile(path + "u.wav");
                                    fi = new FileInfo(path + "e.wav");
                                    if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                        voices[3] = SoundEffect.FromFile(path + "e.wav");
                                    fi = new FileInfo(path + "o.wav");
                                    if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                        voices[4] = SoundEffect.FromFile(path + "o.wav");
                                }
                                catch
                                {
                                    voices[0] = Content.Load<SoundEffect>("Audio/Voices/sawyer-01");
                                    voices[1] = Content.Load<SoundEffect>("Audio/Voices/sawyer-02");
                                    voices[2] = Content.Load<SoundEffect>("Audio/Voices/sawyer-03");
                                    voices[3] = Content.Load<SoundEffect>("Audio/Voices/sawyer-04");
                                    voices[4] = Content.Load<SoundEffect>("Audio/Voices/sawyer-05");
                                }
                                string json = File.ReadAllText(path + "details.json");
                                Custom = JsonSerializer.Deserialize<BBG>(json);
                                if (Custom.BBGDialogue.Count == Berry.BBGDialogue.Count)
                                    bbgDialogue = Custom.BBGDialogue;
                                else
                                    bbgDialogue = Berry.BBGDialogue;
                                if (Custom.PlayerDialogue.Count == Berry.PlayerDialogue.Count)
                                    playerDialogue = Custom.PlayerDialogue;
                                else
                                    playerDialogue = Berry.PlayerDialogue;
                                name = Custom.Name;
                                nametagColor = Custom.Color;
                                emotionOverlay = Custom.OverlayExpression;
                                break;
                            }
                            catch
                            {
                                bbgs[0] = Content.Load<Texture2D>("BBGSim/berry");
                                bbgs[1] = Content.Load<Texture2D>("BBGSim/berryhappy");
                                bbgs[2] = Content.Load<Texture2D>("BBGSim/berryneutral");
                                bbgs[3] = Content.Load<Texture2D>("BBGSim/berryunhappy");
                                voices[0] = Content.Load<SoundEffect>("Audio/Voices/sawyer-01");
                                voices[1] = Content.Load<SoundEffect>("Audio/Voices/sawyer-02");
                                voices[2] = Content.Load<SoundEffect>("Audio/Voices/sawyer-03");
                                voices[3] = Content.Load<SoundEffect>("Audio/Voices/sawyer-04");
                                voices[4] = Content.Load<SoundEffect>("Audio/Voices/sawyer-05");
                                bbgDialogue = Berry.BBGDialogue;
                                playerDialogue = Berry.PlayerDialogue;
                                name = Berry.Name;
                                nametagColor = Berry.Color;
                                emotionOverlay = Berry.OverlayExpression;
                            }
                        }
                        else
                        {
                            bbgs[0] = Content.Load<Texture2D>("BBGSim/berry");
                            bbgs[1] = Content.Load<Texture2D>("BBGSim/berryhappy");
                            bbgs[2] = Content.Load<Texture2D>("BBGSim/berryneutral");
                            bbgs[3] = Content.Load<Texture2D>("BBGSim/berryunhappy");
                            voices[0] = Content.Load<SoundEffect>("Audio/Voices/sawyer-01");
                            voices[1] = Content.Load<SoundEffect>("Audio/Voices/sawyer-02");
                            voices[2] = Content.Load<SoundEffect>("Audio/Voices/sawyer-03");
                            voices[3] = Content.Load<SoundEffect>("Audio/Voices/sawyer-04");
                            voices[4] = Content.Load<SoundEffect>("Audio/Voices/sawyer-05");
                            bbgDialogue = Berry.BBGDialogue;
                            playerDialogue = Berry.PlayerDialogue;
                            name = Berry.Name;
                            nametagColor = Berry.Color;
                            emotionOverlay = Berry.OverlayExpression;
                        }
                    }
                    break;
                default:
                    path = Game1.BBGs[Game1.saveData.Bbg - 5] + Path.DirectorySeparatorChar;
                    try
                    {
                        FileInfo fi = new FileInfo(path + "base.png");
                        if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                            bbgs[0] = Texture2D.FromFile(Game1.GraphicsDevice, path + "base.png");
                        else
                            bbgs[0] = Content.Load<Texture2D>("BBGSim/syowen");
                        fi = new FileInfo(path + "happy.png");
                        if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                            bbgs[1] = Texture2D.FromFile(Game1.GraphicsDevice, path + "happy.png");
                        else
                            bbgs[1] = Content.Load<Texture2D>("BBGSim/syowenhappy");
                        fi = new FileInfo(path + "neutral.png");
                        if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                            bbgs[2] = Texture2D.FromFile(Game1.GraphicsDevice, path + "neutral.png");
                        else
                            bbgs[2] = Content.Load<Texture2D>("BBGSim/syowenneutral");
                        fi = new FileInfo(path + "unhappy.png");
                        if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                            bbgs[3] = Texture2D.FromFile(Game1.GraphicsDevice, path + "unhappy.png");
                        else
                            bbgs[3] = Content.Load<Texture2D>("BBGSim/syowenunhappy");

                        try
                        {
                            fi = new FileInfo(path + "a.wav");
                            if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                voices[0] = SoundEffect.FromFile(path + "a.wav");
                            fi = new FileInfo(path + "i.wav");
                            if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                voices[1] = SoundEffect.FromFile(path + "i.wav");
                            fi = new FileInfo(path + "u.wav");
                            if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                voices[2] = SoundEffect.FromFile(path + "u.wav");
                            fi = new FileInfo(path + "e.wav");
                            if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                voices[3] = SoundEffect.FromFile(path + "e.wav");
                            fi = new FileInfo(path + "o.wav");
                            if (fi.Length / 1024 / 1024 <= 15) //15 mb limit
                                voices[4] = SoundEffect.FromFile(path + "o.wav");
                        }
                        catch
                        {
                            voices[0] = Content.Load<SoundEffect>("Audio/Voices/sawyer-01");
                            voices[1] = Content.Load<SoundEffect>("Audio/Voices/sawyer-02");
                            voices[2] = Content.Load<SoundEffect>("Audio/Voices/sawyer-03");
                            voices[3] = Content.Load<SoundEffect>("Audio/Voices/sawyer-04");
                            voices[4] = Content.Load<SoundEffect>("Audio/Voices/sawyer-05");
                        }
                        string json = File.ReadAllText(path + "details.json");
                        Custom = JsonSerializer.Deserialize<BBG>(json);
                        if (Custom.BBGDialogue.Count == Syowen.BBGDialogue.Count)
                            bbgDialogue = Custom.BBGDialogue;
                        else
                            bbgDialogue = Syowen.BBGDialogue;
                        if (Custom.PlayerDialogue.Count == Syowen.PlayerDialogue.Count)
                            playerDialogue = Custom.PlayerDialogue;
                        else
                            playerDialogue = Syowen.PlayerDialogue;
                        name = Custom.Name;
                        nametagColor = Custom.Color;
                        emotionOverlay = Custom.OverlayExpression;
                        break;
                    }
                    catch
                    {
                        Game1.saveData.Bbg = 0;
                        Game1.Save(Game1.saveData);
                        bbgs[0] = Content.Load<Texture2D>("BBGSim/syowen");
                        bbgs[1] = Content.Load<Texture2D>("BBGSim/syowenhappy");
                        bbgs[2] = Content.Load<Texture2D>("BBGSim/syowenneutral");
                        bbgs[3] = Content.Load<Texture2D>("BBGSim/syowenunhappy");
                        voices[0] = Content.Load<SoundEffect>("Audio/Voices/sb_a");
                        voices[1] = Content.Load<SoundEffect>("Audio/Voices/sb_i");
                        voices[2] = Content.Load<SoundEffect>("Audio/Voices/sb_u");
                        voices[3] = Content.Load<SoundEffect>("Audio/Voices/sb_e");
                        voices[4] = Content.Load<SoundEffect>("Audio/Voices/sb_o");
                        bbgDialogue = Syowen.BBGDialogue;
                        playerDialogue = Syowen.PlayerDialogue;
                        name = Syowen.Name;
                        nametagColor = Syowen.Color;
                        emotionOverlay = Syowen.OverlayExpression;
                    }
                    break;
            }
            sounds = new SoundEffect[6];
            sounds[0] = Content.Load<SoundEffect>("BBGSim/bbgtext");
            sounds[1] = Content.Load<SoundEffect>("BBGSim/bbgenter");
            sounds[2] = Content.Load<SoundEffect>("BBGSim/bbgbackspace");
            sounds[3] = Content.Load<SoundEffect>("BBGSim/bbgopenbox");
            sounds[4] = Content.Load<SoundEffect>("BBGSim/bbgclosebox");
            sounds[5] = Content.Load<SoundEffect>("BBGSim/bbgadvance");
            soundInstance = sounds[0].CreateInstance();
            boxZooms = sounds[0].CreateInstance();
            dialogueUI = Content.Load<Texture2D>("BBGSim/bbgdialogue");
            enterSign = Content.Load<Texture2D>("BBGSim/entersymbol");
            bgs = [Content.Load<Texture2D>("BBGSim/Cherry_Blossom_Tree"), Content.Load<Texture2D>("BBGSim/Lanterns")];
            UIRects[0] = new Rectangle(0,0,1592,604); //Textbox
            UIRects[1] = new Rectangle(107,671,824,300); //Nametag
            UIRects[2] = new Rectangle(1047,637,543,524); //Choicesbox
            UIRects[3] = new Rectangle(16,1236,1563,291); //Inputbox

            song = Content.Load<Song>("BBGSim/simp1");
            MediaPlayer.Play(song);

            boxZooms.Stop();
            boxZooms = sounds[3].CreateInstance();
            boxZooms.Play();
            ReReadString();
        }


        public void Initialize(Game1 game1)
        {
            Game1 = game1;
            Game1.ClearColor = Color.LightPink;

            Syowen = new BBG()
            {
                Name = "Syowen",
                BBGDialogue = new List<string>
                {
                    "Oh, hey. I'm Syowen. What's your name?", "Huh. {0}, huh?", "You seem pretty cool, want to check out the star festival tonight?", "Awesome, see you there bbg",
                    "Oh hey, you made it!", "Oh cool, thanks!", "Pizza? Erm.. well...", "Just, bad memories with that.", "Yeah, the pizza.", "Oh, okay. See you later.", "Welcome back, {0}..."
                },
                PlayerDialogue = new List<string>
                {
                    "Sure!", "Nah", "I got you some\n food!", "What's wrong?", "With... Pizza?", "What happened?"
                },
                Color = new Color(185, 56, 255),
                OverlayExpression = true,
            };
            Mocha = new BBG()
            {
                Name = "Mocha",
                BBGDialogue = new List<string>
                {
                "Heyo! I'm Mocha, and you areeee...?", "You said {0}? Cool, nice to meet you!", "Hey, theres this cool star festival thing going on, wanna check it out?", "Alright, see you there!",
                "Glad you made it!", "Ooh! You're so thoughtful, thank you!", "Pizza...? With.. stuffed crust..?", "...Don't worry about it, just... bad memories...", "mhm...", "Oh... Alright. bye guys", "Welcome back, {0}..."
                },
                PlayerDialogue = new List<string>
                {
                    "Sounds good!", "No I don't", "I got you some\n food!", "What's wrong?", "From... Pizza?", "What happened?"
                },
                Color = new Color(20, 148, 222),
                OverlayExpression = true,
            };
            Brett = new BBG()
            {
                Name = "Brett",
                BBGDialogue = new List<string>
                {
                "Yo, what up... Why are you wearing a nametag?", "Dude, your name is {0}? That's awesome", "Yo they got the star festival going on tonight, who out here marioing they galaxy",
                "That's whats up", "Yo what up", "thanks dude", "Pizza? Really? why pizza", "Pizza is mid. I had bad stuff happen around it before", "yeah", "That's lame", "Welcome back, {0}..."
                },
                PlayerDialogue = new List<string>
                {
                    "Dude I am", "Not me lol", "I got you some\n food!", "What's wrong?", "Bad stuff?", "What happened?"
                },
                Color = new Color(20, 148, 222),
                OverlayExpression = true,
            };
            Alan = new BBG()
            {
                Name = "Alan",
                BBGDialogue = new List<string>
                {
                "Hey there! What's your name?", "Nice to meet you, {0}, I'm Alan!", "Hey theres a star festival thing tonight, you should be there tonight!", "Alright, sounds good!",
                "Oh hey, you're here!", "Awesome, thanks!", "Ohhh. Pizza...", "Yeeeaaah, no I dont have good memories with pizza.", "Yyyyyeahh.", "Oh. okay bye", "Welcome back, {0}..."
                },
                PlayerDialogue = new List<string>
                {
                    "Okay", "No", "I got you some\n food!", "What's wrong?", "You don't?", "What happened?"
                },
                Color = new Color(8, 140, 37),
                OverlayExpression = true,
            };
            Berry = new BBG()
            {
                Name = "Berry",
                BBGDialogue = new List<string>
                {
                "Hi! My name is Berry, what's yours?", "Nice to meet you, {0}!", "Hey theres a star festival thing tonight, you should come with me there!", "Great! See you there, {0}!",
                "About time you made it!", "Awe, how thoughtful of you!", "...\niiit's a slice of pizza, yaayyyyy...", "I'm sorry, I just, really haven't had a good experience with pizza, especially with the stuffed crust...", "Yeah, no, none at all.", "Alright, your loss.", "Welcome back, {0}..."
                },
                PlayerDialogue = new List<string>
                {
                    "Sure!", "No thanks", "I got you some\n food!", "What's wrong?", "Really?", "What happened?"
                },
                Color = new Color(255, 143, 186),
                OverlayExpression = true,
            };

            Game1.Window.TextInput += processTextInput;
            scaleGoal[0] = 1f;
            if (Game1.saveData.Username != null)
            {
                dialoguePart = 10;
                emotion = -1;
            }
        }
        public static string WrapText(SpriteFont font, string text, float maxLineWidth)
        {
            text = string.Format(text, Game1.saveData.Username);
            string[] words = text.Split(' ');
            StringBuilder sb = new StringBuilder();
            float lineWidth = 0f;
            float spaceWidth = font.MeasureString(" ").X;

            foreach (string word in words)
            {
                Vector2 size = font.MeasureString(word);

                if (lineWidth + size.X < maxLineWidth)
                {
                    sb.Append(word + " ");
                    lineWidth += size.X + spaceWidth;
                }
                else
                {
                    if (size.X > maxLineWidth)
                    {
                        if (sb.ToString() == "")
                        {
                            sb.Append(WrapText(font, word.Insert(word.Length / 2, " ") + " ", maxLineWidth));
                        }
                        else
                        {
                            sb.Append("\n" + WrapText(font, word.Insert(word.Length / 2, " ") + " ", maxLineWidth));
                        }
                    }
                    else
                    {
                        sb.Append("\n" + word + " ");
                        lineWidth = size.X + spaceWidth;
                    }
                }
            }

            return sb.ToString();
        }

        public void ReReadString()
        {
            display = string.Empty;
            if (dialoguePart >= 0)
            {
                buffer = WrapText(Game1.BBGFont, bbgDialogue[dialoguePart], wraplimit);
            }
            else
            {
                buffer = WrapText(Game1.BBGFont, bbgDialogue[0], wraplimit);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (Game1.GetKeyUp(Keys.Escape))
            {
                Game1.ChangeScene(0);
            }
            if (currentDelay == 0)
            {
                if (display.Length < buffer.Length)
                {
                    char character = buffer.ToCharArray()[display.Length];
                    display += character;
                    if (Char.ToLower(character) == 'a')
                        voices[0].Play();
                    if (Char.ToLower(character) == 'i')
                        voices[1].Play();
                    if (Char.ToLower(character) == 'u')
                        voices[2].Play();
                    if (Char.ToLower(character) == 'e')
                        voices[3].Play();
                    if (Char.ToLower(character) == 'o')
                        voices[4].Play();
                }
                currentDelay = delay;
            }
            else currentDelay--;

            if (scale[0] == 0 && dialoguePart == 8)
            {
                Game1.sneakyLoad = true;
                Game1.saveData.AltTitle = true;
                Game1.Save(Game1.saveData);
                Game1.ChangeScene(3);
            }
            scaleIndex = 0;
            while (scaleIndex <= 2)
            {
                if (scaleDown[scaleIndex])
                {
                    if (scale[scaleIndex] > scaleGoal[scaleIndex])
                    {
                        scale[scaleIndex] -= Game1.delta * 4;
                    }
                    else
                    {
                        scale[scaleIndex] = scaleGoal[scaleIndex];
                    }
                }
                else
                {
                    if (scale[scaleIndex] < scaleGoal[scaleIndex])
                    {
                        scale[scaleIndex] += Game1.delta * 4;
                    }
                    else
                    {
                        scale[scaleIndex] = scaleGoal[scaleIndex];
                    }
                }
                scaleIndex++;
            }

            if (inChoice)
            {
                if (Game1.GetKeyDown(Keys.Up) && selection > 0)
                {
                    soundInstance.Stop();
                    soundInstance = sounds[0].CreateInstance();
                    soundInstance.Play();
                    selection--;
                }
                if (Game1.GetKeyDown(Keys.Down) && selection < currentSelectionCount - 1)
                {
                    selection++;
                    soundInstance.Stop();
                    soundInstance = sounds[0].CreateInstance();
                    soundInstance.Play();
                }
            }

            if (display.Length == buffer.Length)
            {
                enterAlpha += Game1.delta;
            }

            if (Game1.GetKeyDown(Keys.Enter) && display.Length == buffer.Length)
            {
                enterAlpha = 0;
                switch (dialoguePart)
                {
                    case -1:
                        scale[1] = 0;
                        scaleGoal[1] = 1f;
                        dialoguePart++;
                        boxZooms.Stop();
                        boxZooms = sounds[3].CreateInstance();
                        boxZooms.Play();
                        break;
                    case 0:
                        if (userInput != string.Empty && userInput != null)
                        {
                            Game1.saveData.Username = userInput;
                            Game1.Save(Game1.saveData);
                            dialoguePart++;
                            ReReadString();
                            emotion = 0;
                            soundInstance.Stop();
                            soundInstance = sounds[1].CreateInstance();
                            soundInstance.Play();
                            boxZooms.Stop();
                            boxZooms = sounds[4].CreateInstance();
                            boxZooms.Play();
                            scaleDown[1] = true;
                            scale[1] = 1f;
                            scaleGoal[1] = 0;
                        }
                        break;
                    case 1:
                        emotion = 1;
                        dialoguePart++;
                        ReReadString();
                        soundInstance.Stop();
                        soundInstance = sounds[5].CreateInstance();
                        soundInstance.Play();
                        break;
                    case 2:
                        if (inChoice && selection == 0)
                        {
                            emotion = 0;
                            inChoice = false;
                            choicesPulledUp += 2;
                            dialoguePart++;
                            ReReadString();
                            soundInstance.Stop();
                            soundInstance = sounds[5].CreateInstance();
                            soundInstance.Play();
                            scaleGoal[2] = 0;
                            scaleDown[2] = true;
                            boxZooms.Stop();
                            boxZooms = sounds[4].CreateInstance();
                            boxZooms.Play();
                        }
                        else if (inChoice && selection == 1)
                        {
                            emotion = 2;
                            inChoice = false;
                            choicesPulledUp += 2;
                            dialoguePart = 9;
                            ReReadString();
                            soundInstance.Stop();
                            soundInstance = sounds[5].CreateInstance();
                            soundInstance.Play();
                            scaleGoal[2] = 0;
                            scaleDown[2] = true;
                            boxZooms.Stop();
                            boxZooms = sounds[4].CreateInstance();
                            boxZooms.Play();
                        }
                        else
                        {
                            inChoice = true;
                            currentSelectionCount = 2;
                            scaleGoal[2] = 1;
                            boxZooms.Stop();
                            boxZooms = sounds[3].CreateInstance();
                            boxZooms.Play();
                        }
                        break;
                    case 3:
                        Game1.ClearColor = Color.Navy;
                        dialoguePart++;
                        ReReadString();
                        soundInstance.Stop();
                        soundInstance = sounds[5].CreateInstance();
                        soundInstance.Play();
                        break;
                    case 4:
                        if (inChoice && selection == 0)
                        {
                            emotion = 1;
                            inChoice = false;
                            choicesPulledUp++;
                            dialoguePart++;
                            ReReadString();
                            soundInstance.Stop();
                            soundInstance = sounds[5].CreateInstance();
                            soundInstance.Play();
                            scaleGoal[2] = 0;
                            scaleDown[2] = true;
                            boxZooms.Stop();
                            boxZooms = sounds[4].CreateInstance();
                            boxZooms.Play();
                        }
                        else
                        {
                            inChoice = true;
                            currentSelectionCount = 1;
                            scaleDown[2] = false;
                            scaleGoal[2] = 1;
                            boxZooms.Stop();
                            boxZooms = sounds[3].CreateInstance();
                            boxZooms.Play();
                        }
                        break;
                    case 5:
                        emotion = 2;
                        dialoguePart++;
                        ReReadString();
                        soundInstance.Stop();
                        soundInstance = sounds[5].CreateInstance();
                        soundInstance.Play();
                        break;
                    case 6:
                        if (inChoice && selection == 0)
                        {
                            inChoice = false;
                            choicesPulledUp++;
                            dialoguePart++;
                            ReReadString();
                            soundInstance.Stop();
                            soundInstance = sounds[5].CreateInstance();
                            soundInstance.Play();
                            scaleGoal[2] = 0;
                            scaleDown[2] = true;
                            boxZooms.Stop();
                            boxZooms = sounds[4].CreateInstance();
                            boxZooms.Play();
                        }
                        else
                        {
                            inChoice = true;
                            currentSelectionCount = 1;
                            scaleDown[2] = false;
                            scaleGoal[2] = 1;
                            boxZooms.Stop();
                            boxZooms = sounds[3].CreateInstance();
                            boxZooms.Play();
                        }
                        break;
                    case 7:
                        if (inChoice && selection == 0)
                        {
                            inChoice = false;
                            choicesPulledUp++;
                            dialoguePart++;
                            ReReadString();
                            soundInstance.Stop();
                            soundInstance = sounds[5].CreateInstance();
                            soundInstance.Play();
                            scaleGoal[2] = 0;
                            scaleDown[2] = true;
                            boxZooms.Stop();
                            boxZooms = sounds[4].CreateInstance();
                            boxZooms.Play();
                        }
                        else
                        {
                            inChoice = true;
                            currentSelectionCount = 1;
                            scaleDown[2] = false;
                            scaleGoal[2] = 1;
                            boxZooms.Stop();
                            boxZooms = sounds[3].CreateInstance();
                            boxZooms.Play();
                        }
                        break;
                    case 8:
                        if (inChoice && selection == 0)
                        {
                            inChoice = false;
                            choicesPulledUp++;
                            soundInstance.Stop();
                            soundInstance = sounds[5].CreateInstance();
                            soundInstance.Play();
                            scaleGoal[2] = 0;
                            scaleDown[2] = true;
                            scaleGoal[0] = 0;
                            scaleDown[0] = true;
                            boxZooms.Stop();
                            boxZooms = sounds[4].CreateInstance();
                            boxZooms.Play();
                        }
                        else
                        {
                            inChoice = true;
                            currentSelectionCount = 1;
                            scaleDown[2] = false;
                            scaleGoal[2] = 1;
                            boxZooms.Stop();
                            boxZooms = sounds[3].CreateInstance();
                            boxZooms.Play();
                        }
                        break;
                    case 9: //refusal
                        Game1.Exit();
                        break;
                    case 10: //Welcome back
                        emotion = 1;
                        dialoguePart = 2;
                        ReReadString();
                        soundInstance.Stop();
                        soundInstance = sounds[5].CreateInstance();
                        soundInstance.Play();
                        break;
                }
            }
        }

        private void processTextInput(object sender, TextInputEventArgs e)
        {
            if (dialoguePart == 0)
            {
                if (e.Key == Keys.Back)
                {
                    if (userInput != string.Empty)
                    {
                        userInput = userInput.Remove(userInput.Length - 1);
                        soundInstance.Stop();
                        soundInstance = sounds[2].CreateInstance();
                        soundInstance.Play();
                    }
                }
                else if (e.Key != Keys.Enter)
                {
                    if (userInput != null)
                    {
                        if (Game1.BBGFont.MeasureString(userInput).X < 600)
                        {
                            userInput += (e.Character);
                            soundInstance.Stop();
                            soundInstance = sounds[0].CreateInstance();
                            soundInstance.Play();
                        }
                    }
                    else
                    {
                        userInput += (e.Character);
                        soundInstance.Stop();
                        soundInstance = sounds[0].CreateInstance();
                        soundInstance.Play();
                    }
                }
            }
        }

        public void Draw(SpriteBatch _spriteBatch, GameTime gameTime, SpriteFont defaultfont, Texture2D debugbox)
        {
            if (Game1.ClearColor == Color.Navy)
            {
                _spriteBatch.Draw(bgs[1], new Vector2(-100, 0), new Rectangle(0, 0, 3000, 1500), Color.White, 0, new Vector2(0, 0), 0.72f, SpriteEffects.None, 0);
            }
            else
            {
                _spriteBatch.Draw(bgs[0], new Vector2(-100, 0), new Rectangle(0, 0, 3000, 1500), Color.White, 0, new Vector2(0, 0), 0.72f, SpriteEffects.FlipHorizontally, 0);
            }
            if (bbgs[0] != null)
            {
                if (emotionOverlay)
                    _spriteBatch.Draw(bbgs[0], new Rectangle(400, 50, 1040, 1040), new Rectangle(0, 0, bbgs[0].Width, bbgs[0].Height), Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0);
                _spriteBatch.Draw(bbgs[emotion + 1], new Rectangle(400, 50, 1040, 1040), new Rectangle(0, 0, bbgs[emotion + 1].Width, bbgs[emotion + 1].Height), Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0);
            }
            _spriteBatch.Draw(dialogueUI, new Vector2(900, 1000), UIRects[0], Color.White, 0, new Vector2(UIRects[0].Width / 2, UIRects[0].Height / 2), 0.75f * scale[0], SpriteEffects.None, 0);
            _spriteBatch.Draw(dialogueUI, new Vector2(900, 1000), UIRects[1], nametagColor, -0.35f, new Vector2(1400, 1100), 0.35f * scale[0], SpriteEffects.None, 0);
                _spriteBatch.DrawString(defaultfont, name, new Vector2(942, 997), Color.White, -0.35f, new Vector2(300, 235), 1.65f * scale[0], SpriteEffects.None, 0);
            //Input
            _spriteBatch.Draw(dialogueUI, new Vector2(900, 100), UIRects[3], Color.White, 0, new Vector2(UIRects[3].Width / 2, UIRects[3].Height / 2), 0.5f * scale[1], SpriteEffects.None, 0);
            _spriteBatch.DrawString(defaultfont, "Enter your name:", new Vector2(900, 100), Color.Black, 0, new Vector2(550, 70), scale[1] / 1.75f, SpriteEffects.None, 0);
            if (userInput != null)
                _spriteBatch.DrawString(defaultfont, userInput, new Vector2(900, 100), Color.Black, 0, new Vector2(310, 20), scale[1], SpriteEffects.None, 0);
            //Dialogue
            _spriteBatch.DrawString(defaultfont, display, new Vector2(textcoords[0], textcoords[1]), Color.Black);
            _spriteBatch.Draw(enterSign, new Vector2(1400, 1020), nametagColor * Math.Abs((float)Math.Sin(enterAlpha)));
            //Choices
            _spriteBatch.Draw(dialogueUI, new Vector2(1500, 850), UIRects[2], Color.White, 0, new Vector2(UIRects[2].Width / 2, UIRects[2].Height / 2), 0.75f * scale[2], SpriteEffects.None, 0);
            if (scale[2] == 1 && inChoice)
            {
                _spriteBatch.DrawString(defaultfont, (selection == 0 ? "> " : "") + playerDialogue[choicesPulledUp], new Vector2(1400, 775), Color.Chocolate, 0, new Vector2(50, 0), scale[2], SpriteEffects.None, 0);
                if (currentSelectionCount >= 2)
                    _spriteBatch.DrawString(defaultfont, (selection == 1 ? "> " : "") + playerDialogue[choicesPulledUp + 1], new Vector2(1500, 900), Color.Chocolate, 0, new Vector2(50, 0), scale[2], SpriteEffects.None, 0);
            }
        }
    }
}
