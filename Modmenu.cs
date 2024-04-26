using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using static Cheesenaf.Modmenu;

namespace Cheesenaf
{
    internal class Modmenu
    {
        public class Bubble
        {
            public Vector2 pos;
            public Rectangle rect;
            public float scale;
            public float sinOffset;
            public Texture2D texture;
            public int id;
        }
        public class Modpack
        {
            public string Name { get; set; } = "Somehow, there was an error.";
            public string DisplayName { get; set; } = "Somehow, there was an error.";
            public string Description { get; set; } = "No description set.";
            public Texture2D Icon { get; set; } = null;
            public bool ImageMod { get; set; } = false;
            public bool AudioMod { get; set; } = false;
            public bool TextMod { get; set; } = false;
            public bool BBGSimMod { get; set; } = false;
            public bool CheesenafMod { get; set; } = false;
            public bool AddedContent { get; set; } = false;
            public bool ReplacedContent { get; set; } = false;
            public string GameVersion { get; set; } = "unknown";
            public string ModVersion { get; set; } = "?.?.?";
        }
        public class InfoFile
        {
            public string Description { get; set; }
            public string GameVersion { get; set; }
            public string ModVersion { get; set; }
        }
        public Game1 Game1;
        public List<Modpack> disabledModpacks;
        public List<Modpack> modpackBuffer;
        public List<Modpack> enabledModpacks;
        public float LeftScroll;
        public float LeftScrollGoal;
        public float LeftScrollLerp;
        public float LeftScrollLimit;
        public float RightScroll;
        public float RightScrollGoal;
        public float RightScrollLerp;
        public float RightScrollLimit;
        public SoundEffectInstance music;
        public Song builtInMusic;
        public bool confirmScreen;
        public bool operationScreen;

        public float bubbleDelay;
        public Bubble[] bubbles;
        public Texture2D bubbleTexture;
        public Texture2D bg;
        float time;

        Rectangle confirm = new Rectangle(1150, 235, 250, 100);
        Rectangle cancel = new Rectangle(1150, 365, 250, 100);
        Rectangle disableAll = new Rectangle(1150, 495, 250, 100);
        Rectangle menuOnBoot = new Rectangle(1100, 775, 300, 100);

        FileSystemWatcher watcher;
        float buffer;

        public void LoadContent(ContentManager Content)
        {
            bubbleTexture = Content.Load<Texture2D>("menus/bubble");
            bg = Content.Load<Texture2D>("menus/modmenugradient");
            bubbles = new Bubble[35];

            disabledModpacks = new List<Modpack>();
            modpackBuffer = new List<Modpack>();
            enabledModpacks = new List<Modpack>();
            RefreshMods(Content);
        }

        public void Initialize(Game1 game1)
        {
            Game1 = game1;
            Game1.ClearColor = new Color(0, 0, 41);
            LeftScroll = 0;
            LeftScrollGoal = 0;
            game1.Window.Title = "Cheesenaf/BBGSim Mod Menu";
            watcher = new FileSystemWatcher();
            watcher.Path = "Mods";
            watcher.EnableRaisingEvents = true;
            watcher.IncludeSubdirectories = true;
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.FileName;
            watcher.Changed += RefreshMods;
            watcher.Created += RefreshMods;
            watcher.Renamed += RefreshMods;
            watcher.Deleted += RefreshMods;
        }

        private void RefreshMods(object sender, FileSystemEventArgs e)
        {
            buffer = 0.25f;
        }

        public void Update(GameTime gameTime)
        {
            if (operationScreen)
            {
                ModTemplateGenerator.CreateTemplate(Game1);
                operationScreen = false;
            }
            if (buffer > 0)
            {
                buffer -= Game1.delta;
            }
            if (buffer < 0 || Game1.GetKeyDown(Keys.F12))
            {
                RefreshMods(Game1.Content);
                buffer = 0;
            }
            time += Game1.delta;
            if (bubbleDelay <= 0)
            {
                Random rng = new Random();
                bubbleDelay = rng.Next(0, 100) / 100f;
                Bubble bubble = new Bubble();
                bubble.pos = new Vector2(rng.Next(32, 1888), 1144);
                bubble.texture = bubbleTexture;
                bubble.sinOffset = rng.Next(0, 1000) / 100f;
                bubble.scale = rng.Next(90, 110) / 100f;
                int index = 0;
                while (index < bubbles.Length)
                {
                    if (bubbles[index] == null)
                    {
                        bubble.id = index;
                        bubbles[index] = bubble;
                        break;
                    }
                    index++;
                }
            } else
            {
                bubbleDelay -= Game1.delta;
            }

            if (Math.Abs((Game1.mouseThisFrame.Position - Game1.mouseLastFrame.Position).X) > 100 && Game1.mouseLastFrame.Position.X > 0)
            {
                Random rng = new Random();
                Bubble bubble = new Bubble();
                bubble.pos = new Vector2(Game1.MouseX, Game1.MouseY);
                bubble.texture = bubbleTexture;
                bubble.sinOffset = rng.Next(0, 1000) / 100f;
                bubble.scale = rng.Next(10, 50) / 100f;
                int index = 0;
                while (index < bubbles.Length)
                {
                    if (bubbles[index] == null)
                    {
                        bubble.id = index;
                        bubbles[index] = bubble;
                        break;
                    }
                    index++;
                }
            }
            foreach (var bubble in bubbles)
            {
                if (bubble != null)
                {
                    bubble.pos.Y -= Game1.delta * 100 / bubble.scale;
                    bubble.pos.X += MathF.Sin(bubble.sinOffset + (time * 2));
                    if (bubble.pos.Y < -64)
                    {
                        bubbles[bubble.id] = null;
                        return;
                    }
                    bubble.rect = new Rectangle((int)bubble.pos.X - 32, (int)bubble.pos.Y - 32, 64, 64);
                    if (bubble.rect.Contains(Game1.MousePos) && bubble.scale > 0.5f)
                    {
                        Random rng = new Random();
                        for (int i = 0; i < rng.Next(4, 8); i++)
                        {
                            Bubble minibubble = new Bubble();
                            minibubble.pos = new Vector2(Game1.MouseX + rng.Next(-32, 32), Game1.MouseY + rng.Next(-32, 32));
                            minibubble.texture = bubbleTexture;
                            minibubble.sinOffset = rng.Next(0, 1000) / 100f;
                            minibubble.scale = rng.Next(20, 50) / 100f;
                            int index = 0;
                            while (index < bubbles.Length)
                            {
                                if (bubbles[index] == null)
                                {
                                    minibubble.id = index;
                                    bubbles[index] = minibubble;
                                    break;
                                }
                                index++;
                            }
                        }
                        bubbles[bubble.id] = null;
                        return;
                    }
                }
            }
            if (Game1.IsActive)
            {
                if (!confirmScreen)
                {
                    if (Game1.GetKeyDown(Keys.F10))
                    {
                        Game1.OpenURL("Mods");
                    }
                    if (Game1.GetKeyDown(Keys.F8))
                    {
                        operationScreen = true;
                    }
                    if (Game1.MouseX <= 960)
                    {
                        LeftScrollGoal += Game1.GetScrollWheel(150);
                        LeftScrollGoal = Math.Clamp(LeftScrollGoal, -LeftScrollLimit, 0);
                    }
                    else
                    {
                        RightScrollGoal += Game1.GetScrollWheel(150);
                        RightScrollGoal = Math.Clamp(RightScrollGoal, -RightScrollLimit, 0);
                    }

                    if (Math.Round(LeftScroll, 1) != Math.Round(LeftScrollGoal, 1))
                    {
                        LeftScrollLerp += Game1.delta;
                        LeftScrollLerp = Math.Clamp(LeftScrollLerp, 0, 1);
                        LeftScroll = MathHelper.Lerp(LeftScroll, LeftScrollGoal, LeftScrollLerp);
                    }
                    else
                    {
                        LeftScrollLerp = 0;
                    }
                    if (Math.Round(RightScroll, 1) != Math.Round(RightScrollGoal, 1))
                    {
                        RightScrollLerp += Game1.delta;
                        RightScrollLerp = Math.Clamp(RightScrollLerp, 0, 1);
                        RightScroll = MathHelper.Lerp(RightScroll, RightScrollGoal, RightScrollLerp);
                    }
                    else
                    {
                        RightScrollLerp = 0;
                    }

                    int yOffset = 0;
                    int count = 0;
                    foreach (var modpack in disabledModpacks)
                    {
                        Rectangle clickBox = new Rectangle(50, 100 + yOffset + (int)Math.Round(LeftScroll), 828, 128);
                        if (clickBox.Contains(Game1.MouseX, Game1.MouseY) && Game1.GetMouseDown())
                        {
                            enabledModpacks.Add(modpack);
                            Game1.Modpacks.Add(modpack.Name);
                            disabledModpacks.Remove(modpack);
                            return;
                        }
                        yOffset += 150;
                        count++;
                    }
                    LeftScrollLimit = 0;
                    if (count > 6)
                        LeftScrollLimit = (count - 6) * 150;
                    yOffset = 0;
                    count = 0;
                    foreach (var modpack in enabledModpacks)
                    {
                        Rectangle clickBox = new Rectangle(1050, 100 + yOffset + (int)Math.Round(RightScroll), 828, 128);
                        if (clickBox.Contains(Game1.MouseX, Game1.MouseY) && Game1.GetMouseDown())
                        {
                            enabledModpacks.Remove(modpack);
                            Game1.Modpacks.Remove(modpack.Name);
                            disabledModpacks.Add(modpack);
                            disabledModpacks = disabledModpacks.OrderBy(x => x.Name).ToList();
                            return;
                        }
                        yOffset += 150;
                        count++;

                    }
                    RightScrollLimit = 0;
                    if (count > 6)
                        RightScrollLimit = (count - 6) * 150;

                    if (Game1.GetKeyDown(Keys.Enter))
                    {
                        confirmScreen = true;
                    }
                }
                else
                {
                    if (confirm.Contains(Game1.MouseX, Game1.MouseY) && Game1.GetMouseDown())
                    {
                        Game1.saveData.enabledMods = Game1.Modpacks.ToArray();

                        if (Game1.saveData.enabledMods != null)
                        {
                            Game1.BBGs = new List<string>();
                            foreach (string pack in Game1.saveData.enabledMods)
                            {
                                if (Directory.Exists("Mods" + Path.DirectorySeparatorChar + pack))
                                {
                                    foreach (string bbg in Directory.GetDirectories("Mods" + Path.DirectorySeparatorChar + pack + Path.DirectorySeparatorChar + "bbgs"))
                                    {
                                        if (bbg != "Syowen" && bbg != "Mocha" && bbg != "Brett" && bbg != "Alan" && bbg != "Berry")
                                        {
                                            Game1.BBGs.Add(bbg);
                                        }
                                    }
                                }
                            }
                        }
                        Game1.Save(Game1.saveData);
                        if (music != null)
                            music.Stop();

                        if (Game1.saveData.Bbg > 4 + Game1.BBGs.Count)
                        {
                            Game1.saveData.Bbg = 0;
                        }

                        if (Game1.saveData.AltTitle)
                        {
                            Game1.ChangeScene(2);
                        }
                        else
                        {
                            Game1.ChangeScene(0);
                        }
                    }
                    if (cancel.Contains(Game1.MouseX, Game1.MouseY) && Game1.GetMouseDown())
                    {
                        confirmScreen = false;
                    }
                    if (disableAll.Contains(Game1.MouseX, Game1.MouseY) && Game1.GetMouseDown())
                    {
                        Game1.saveData.enabledMods = null;
                        Game1.Modpacks = new List<string>();
                        Game1.BBGs = new List<string>();
                        Game1.Save(Game1.saveData);
                        if (music != null)
                            music.Stop();

                        if (Game1.saveData.Bbg > 4 + Game1.BBGs.Count)
                        {
                            Game1.saveData.Bbg = 0;
                        }

                        if (Game1.saveData.AltTitle)
                        {
                            Game1.ChangeScene(2);
                        }
                        else
                        {
                            Game1.ChangeScene(0);
                        }
                    }
                    if (menuOnBoot.Contains(Game1.MouseX, Game1.MouseY) && Game1.GetMouseDown())
                    {
                        Game1.saveData.SkipModMenu = !Game1.saveData.SkipModMenu;
                    }
                }
            }
        }

        public void Draw(SpriteBatch _spriteBatch, GameTime gameTime, SpriteFont defaultfont, Texture2D debugbox)
        {
            _spriteBatch.Draw(bg, new Vector2(0, 0), Color.White);
            foreach (var bubble in bubbles)
            {
                if (bubble != null)
                {
                    _spriteBatch.Draw(bubble.texture ?? debugbox, bubble.pos, new Rectangle(0, 0, 64, 64), Color.White * 0.5f, 0, new Vector2(32, 32), bubble.scale, SpriteEffects.None, 0);
                    if (Game1.isDebugMode)
                    {
                        _spriteBatch.Draw(debugbox, bubble.rect, new Color(139, 51, 255, 20));
                    }
                }
            }
            int yOffset = 0;
            foreach (var modpack in disabledModpacks)
            {
                Rectangle clickBox = new Rectangle(50, 100 + yOffset + (int)Math.Round(LeftScroll), 828, 128);
                if (clickBox.Contains(Game1.MouseX, Game1.MouseY) && !confirmScreen && Game1.IsActive)
                {
                    _spriteBatch.Draw(debugbox, new Rectangle(40, 90 + yOffset + (int)Math.Round(LeftScroll), 828, 150), Color.White * 0.1f);
                }
                _spriteBatch.Draw(debugbox, new Rectangle(50, 100 + yOffset + (int)Math.Round(LeftScroll), 128, 128), Color.White * 0.1f);
                _spriteBatch.Draw(modpack.Icon ?? debugbox, new Rectangle(50, 100 + yOffset + (int)Math.Round(LeftScroll), 128, 128), Color.White);

                _spriteBatch.DrawString(defaultfont, modpack.DisplayName, new Vector2(200, 100 + yOffset + LeftScroll), Color.White);
                _spriteBatch.DrawString(Game1.PixelFont, modpack.Description, new Vector2(200, 130 + yOffset + LeftScroll), Color.White);
                _spriteBatch.DrawString(Game1.PixelFont, "v" + modpack.ModVersion + " for game version " + modpack.GameVersion, new Vector2(200, 190 + yOffset + LeftScroll), modpack.GameVersion == Game1.Version ? Color.Gray : new Color(255, 85, 85));
                _spriteBatch.DrawString(Game1.PixelFont, "T", new Vector2(200, 210 + yOffset + LeftScroll), modpack.TextMod ? Color.Cyan : Color.Black);
                _spriteBatch.DrawString(Game1.PixelFont, "B", new Vector2(215, 210 + yOffset + LeftScroll), modpack.BBGSimMod ? Color.Magenta : Color.Black);
                _spriteBatch.DrawString(Game1.PixelFont, "I", new Vector2(230, 210 + yOffset + LeftScroll), modpack.ImageMod ? Color.Lime : Color.Black);
                _spriteBatch.DrawString(Game1.PixelFont, "A", new Vector2(245, 210 + yOffset + LeftScroll), modpack.AudioMod ? Color.Gold : Color.Black);
                yOffset += 150;

            }
            yOffset = 0;
            foreach (var modpack in enabledModpacks)
            {
                Rectangle clickBox = new Rectangle(1050, 100 + yOffset + (int)Math.Round(RightScroll), 828, 128);
                if (clickBox.Contains(Game1.MouseX, Game1.MouseY) && !confirmScreen && Game1.IsActive)
                {
                    _spriteBatch.Draw(debugbox, new Rectangle(1040, 90 + yOffset + (int)Math.Round(RightScroll), 828, 150), Color.White * 0.1f);
                }
                _spriteBatch.Draw(debugbox, new Rectangle(1050, 100 + yOffset + (int)Math.Round(RightScroll), 128, 128), Color.White * 0.1f);
                _spriteBatch.Draw(modpack.Icon ?? debugbox, new Rectangle(1050, 100 + yOffset + (int)Math.Round(RightScroll), 128, 128), Color.White);

                _spriteBatch.DrawString(defaultfont, modpack.DisplayName, new Vector2(1200, 100 + yOffset + RightScroll), Color.White);
                _spriteBatch.DrawString(Game1.PixelFont, modpack.Description, new Vector2(1200, 130 + yOffset + RightScroll), Color.White);
                _spriteBatch.DrawString(Game1.PixelFont, "v" + modpack.ModVersion + " for game version " + modpack.GameVersion, new Vector2(1200, 190 + yOffset + RightScroll), modpack.GameVersion == Game1.Version ? Color.Gray : new Color(255, 85, 85));
                _spriteBatch.DrawString(Game1.PixelFont, "T", new Vector2(1200, 210 + yOffset + LeftScroll), modpack.TextMod ? Color.Cyan : Color.Black);
                _spriteBatch.DrawString(Game1.PixelFont, "B", new Vector2(1215, 210 + yOffset + LeftScroll), modpack.BBGSimMod ? Color.Magenta : Color.Black);
                _spriteBatch.DrawString(Game1.PixelFont, "I", new Vector2(1230, 210 + yOffset + LeftScroll), modpack.ImageMod ? Color.Lime : Color.Black);
                _spriteBatch.DrawString(Game1.PixelFont, "A", new Vector2(1245, 210 + yOffset + LeftScroll), modpack.AudioMod ? Color.Gold : Color.Black);
                yOffset += 150;
            }
            _spriteBatch.Draw(bg, new Rectangle(0, 0, 1920, 80), new Rectangle(0, 0, 1920, 80), Color.White);
            _spriteBatch.DrawString(defaultfont, "Cheesenaf/BBGSim Mod Menu", new Vector2(0, 0), Color.White);
            _spriteBatch.DrawString(defaultfont, "Press F10 to open mod folder", new Vector2(695, 0), Color.White);
            _spriteBatch.DrawString(defaultfont, "Press F12 to refresh mod list", new Vector2(685, 30), Color.White);
            _spriteBatch.DrawString(defaultfont, "Press F8 to create a blank mod", new Vector2(680, 60), Color.White);
            _spriteBatch.DrawString(defaultfont, "Game version " + Game1.Version, new Vector2(1590, 0), Color.White);
            _spriteBatch.DrawString(defaultfont, "Click to enable a mod", new Vector2(250, 40), Color.White);
            _spriteBatch.DrawString(Game1.PixelFont, "Mods are applied in selected order", new Vector2(275, 65), Color.White);
            _spriteBatch.DrawString(defaultfont, "Click to disable a mod", new Vector2(1250, 40), Color.White);
            _spriteBatch.DrawString(Game1.PixelFont, "Otherwise, press enter when ready", new Vector2(1275, 65), Color.White);

            if (confirmScreen)
            {
                _spriteBatch.Draw(debugbox, new Rectangle(0, 0, 1920, 1080), Color.Black * 0.5f);
                _spriteBatch.Draw(bg, new Rectangle(420, 180, 1080, 720), new Rectangle(0, 0, 1920, 1080), Color.White);
                _spriteBatch.DrawString(defaultfont, "Load these mods?", new Vector2(440, 200), Color.White);
                yOffset = 0;
                int count = 0;
                int count2 = 0;
                foreach (string item in Game1.Modpacks)
                {
                    if (count < 20)
                    {
                        string text = item;
                        while (Game1.PixelFont.MeasureString(text).X > 650)
                        {
                            text = text.Replace("...", string.Empty);
                            text = text.Remove(text.Length - 1);
                            text += "...";
                        }
                        _spriteBatch.DrawString(Game1.PixelFont, text, new Vector2(480, 235 + yOffset), Color.White);
                        yOffset += 30;
                        count++;
                    }
                    count2++;
                }
                if (count >= 20) _spriteBatch.DrawString(Game1.PixelFont, "...and " + (count2 - count) + " more", new Vector2(480, 235 + yOffset), Color.Gray);
                if (confirm.Contains(Game1.MouseX, Game1.MouseY))
                {
                    _spriteBatch.Draw(debugbox, confirm, Color.White * 0.5f);
                }
                else
                {
                    _spriteBatch.Draw(debugbox, confirm, Color.White * 0.1f);
                }
                _spriteBatch.DrawString(defaultfont, "Load", new Vector2(confirm.X + 85, confirm.Y + 35), Color.White);
                if (cancel.Contains(Game1.MouseX, Game1.MouseY))
                {
                    _spriteBatch.Draw(debugbox, cancel, Color.White * 0.5f);
                }
                else
                {
                    _spriteBatch.Draw(debugbox, cancel, Color.White * 0.1f);
                }
                _spriteBatch.DrawString(defaultfont, "Back", new Vector2(cancel.X + 85, cancel.Y + 35), Color.White);
                if (disableAll.Contains(Game1.MouseX, Game1.MouseY))
                {
                    _spriteBatch.Draw(debugbox, disableAll, Color.White * 0.5f);
                }
                else
                {
                    _spriteBatch.Draw(debugbox, disableAll, Color.White * 0.1f);
                }
                _spriteBatch.DrawString(defaultfont, "Disable mods\n  and load", new Vector2(disableAll.X + 15, disableAll.Y + 15), Color.White);
                if (menuOnBoot.Contains(Game1.MouseX, Game1.MouseY))
                {
                    _spriteBatch.Draw(debugbox, menuOnBoot, (Game1.saveData.SkipModMenu ? Color.Red : Color.Green) * 0.5f);
                }
                else
                {
                    _spriteBatch.Draw(debugbox, menuOnBoot, (Game1.saveData.SkipModMenu ? Color.Red : Color.Green) * 0.1f);
                }
                _spriteBatch.DrawString(defaultfont, "Mod menu on load:\n" + (Game1.saveData.SkipModMenu ? "    Disabled" : "    Enabled"), new Vector2(menuOnBoot.X + 5, menuOnBoot.Y + 15), Color.White);
                _spriteBatch.DrawString(Game1.PixelFont, "Press F12 on any title screen to access the mod menu.", new Vector2(menuOnBoot.X - 220, menuOnBoot.Y + 100), Color.White);
            }
            if (operationScreen)
            {
                _spriteBatch.Draw(debugbox, new Rectangle(0, 0, 1920, 1080), Color.Black * 0.5f);
                _spriteBatch.Draw(bg, new Rectangle(420, 180, 1080, 720), new Rectangle(0, 0, 1920, 1080), Color.White);
                _spriteBatch.DrawString(defaultfont, "Generating modpack...\nDepending on your write speed,\nthis may take some time.", new Vector2(440, 200), Color.White);
            }
        }

        public void RefreshMods(ContentManager Content)
        {
            if (disabledModpacks.Count > 0)
            {
                foreach (var modpack in disabledModpacks)
                {
                    if (modpack.Icon != null)
                        modpack.Icon.Dispose();
                }
            }
            if (enabledModpacks.Count > 0)
            {
                foreach(var modpack in disabledModpacks)
                {
                    if (modpack.Icon != null)
                        modpack.Icon.Dispose();
                }
            }
            disabledModpacks.Clear();
            enabledModpacks.Clear();
            modpackBuffer.Clear();
            foreach (string dir in Directory.GetDirectories("Mods"))
            {
                Trace.WriteLine(dir);
            }
            Trace.WriteLine(Directory.GetDirectories("Mods").Length);
            if (Directory.GetDirectories("Mods").Length > 0)
            {
                foreach (string dir in Directory.GetDirectories("Mods"))
                {
                    Modpack modpack = new Modpack();
                    try
                    {
                        modpack.Name = dir.Replace("Mods" + Path.DirectorySeparatorChar, string.Empty);
                        modpack.DisplayName = modpack.Name;
                        while (Game1.defaultfont.MeasureString(modpack.DisplayName).X > 650)
                        {
                            modpack.DisplayName = modpack.DisplayName.Replace("...", string.Empty);
                            modpack.DisplayName = modpack.DisplayName.Remove(modpack.DisplayName.Length - 1);
                            modpack.DisplayName += "...";
                        }
                    }
                    catch { }
                    if (File.Exists(dir + Path.DirectorySeparatorChar + "pack.json"))
                    {
                        try
                        {
                            string file = File.ReadAllText(dir + Path.DirectorySeparatorChar + "pack.json");
                            InfoFile info = JsonSerializer.Deserialize<InfoFile>(file);
                            char[] chars = info.Description.ToCharArray();
                            string[] desc = new string[3];
                            int index = 0;
                            foreach (char c in chars)
                            {
                                desc[index] += c;
                                if (Game1.PixelFont.MeasureString(desc[index]).X >= 650)
                                {
                                    if (index < 2)
                                    {
                                        index++;
                                    }
                                    else
                                    {
                                        desc[index] += "...";
                                        break;
                                    }
                                }
                            }

                            modpack.Description = desc[0] + "\n" + desc[1] + "\n" + desc[2];

                            char[] buffer = info.ModVersion.ToCharArray();
                            modpack.ModVersion = string.Empty;
                            index = 0;
                            foreach (char c in buffer)
                            {
                                if (c != '0' || c != '1' || c != '2' || c != '3' || c != '4' || c != '5' || c != '6' || c != '7' || c != '8' || c != '9' || c != '.')
                                {
                                    buffer[index] = '?';
                                }
                                modpack.ModVersion += c;
                                index++;
                            }
                            buffer = info.GameVersion.ToCharArray();
                            modpack.GameVersion = string.Empty;
                            index = 0;
                            foreach (char c in buffer)
                            {
                                if (c != '0' || c != '1' || c != '2' || c != '3' || c != '4' || c != '5' || c != '6' || c != '7' || c != '8' || c != '9' || c != '.')
                                {
                                    buffer[index] = '?';
                                }
                                modpack.GameVersion += c;
                                index++;
                            }
                        }
                        catch
                        {
                            modpack.Description = "Unable to load description.";
                        }
                    }
                    if (File.Exists(dir + Path.DirectorySeparatorChar + "icon.png"))
                    {
                        FileInfo fi = new FileInfo(dir + Path.DirectorySeparatorChar + "icon.png");
                        if (fi.Length / 1024 / 1024 <= 50) //50 mb limit
                            modpack.Icon = Texture2D.FromFile(Game1.GraphicsDevice, dir + Path.DirectorySeparatorChar + "icon.png");
                    }
                    if (File.Exists(dir + Path.DirectorySeparatorChar + "text" + Path.DirectorySeparatorChar + "splash.json"))
                    {
                        modpack.TextMod = true;
                    }
                    if (Directory.Exists(dir + Path.DirectorySeparatorChar + "bbgs"))
                    {
                        modpack.BBGSimMod = true;
                    }
                    if (Directory.Exists(dir + Path.DirectorySeparatorChar + "textures"))
                    {
                        modpack.ImageMod = true;
                    }
                    if (Directory.Exists(dir + Path.DirectorySeparatorChar + "audio"))
                    {
                        modpack.AudioMod = true;
                    }
                    if (Game1.saveData.enabledMods != null)
                    {
                        if (Game1.saveData.enabledMods.Contains<string>(modpack.Name))
                        {
                            if (modpack != null)
                            {
                                modpackBuffer.Add(modpack);
                            }
                        }
                        else
                        {
                            disabledModpacks.Add(modpack);
                        }
                    }
                    else
                    {
                        disabledModpacks.Add(modpack);
                    }
                }
                if (modpackBuffer.Count > 0)
                {
                    Game1.Modpacks = new List<string>();
                    foreach (string entry in Game1.saveData.enabledMods)
                    {
                        enabledModpacks.Add(modpackBuffer.Find(x => x.Name == entry));
                        Game1.Modpacks.Add(entry);
                    }
                }
                modpackBuffer.Clear();
            }
            if (File.Exists("Mods" + Path.DirectorySeparatorChar + "modmusic.wav"))
            {
                if (music == null)
                {
                    try
                    {
                        music = SoundEffect.FromFile("Mods" + Path.DirectorySeparatorChar + "modmusic.wav").CreateInstance();
                        music.IsLooped = true;
                        if (music.State != SoundState.Playing)
                            music.Play();
                        if (MediaPlayer.State == MediaState.Playing) MediaPlayer.Stop();
                    }
                    catch
                    {
                        music = Content.Load<SoundEffect>("Audio/error").CreateInstance();
                        music.IsLooped = false;
                        music.Play();

                        builtInMusic = Content.Load<Song>("Audio/modmusic");
                        MediaPlayer.IsRepeating = true;
                        if (MediaPlayer.State != MediaState.Playing) MediaPlayer.Play(builtInMusic);
                    }
                }
            }
            else
            {
                if (music != null)
                {
                    if (music.State == SoundState.Playing) music.Stop();
                    music = null;
                }
                builtInMusic = Content.Load<Song>("Audio/modmusic");
                MediaPlayer.IsRepeating = true;
                if (MediaPlayer.State != MediaState.Playing) MediaPlayer.Play(builtInMusic);
            }
            if (disabledModpacks.Count > 6)
                LeftScrollLimit = (disabledModpacks.Count - 6) * 150;
        }
    }
}
