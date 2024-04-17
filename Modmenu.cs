using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cheesenaf
{
    internal class Modmenu
    {
        public class Modpack
        {
            public string Name { get; set; } = "Somehow, there was an error.";
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
            public string ModVersion { get; set; } = "unknown";
            public bool enabled { get; set; } = false;
        }
        public class InfoFile
        {
            public string Description { get; set; }
            public string GameVersion { get; set; }
            public string ModVersion { get; set; }
        }
        public Game1 Game1;
        public List<Modpack> modpacks;
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
        public void LoadContent(ContentManager Content)
        {
            modpacks = new List<Modpack>();
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
                    }
                    catch { }
                    if (File.Exists(dir + Path.DirectorySeparatorChar + "pack.json"))
                    {
                        try
                        {
                            string file = File.ReadAllText(dir + Path.DirectorySeparatorChar + "pack.json");
                            InfoFile info = JsonSerializer.Deserialize<InfoFile>(file);
                            modpack.Description = info.Description;
                            modpack.ModVersion = info.ModVersion;
                            modpack.GameVersion = info.GameVersion;
                        }
                        catch
                        {
                            modpack.Description = "Unable to load description.";
                        }
                    }
                    if (File.Exists(dir + Path.DirectorySeparatorChar + "icon.png"))
                    {
                        modpack.Icon = Texture2D.FromFile(Game1.GraphicsDevice, dir + Path.DirectorySeparatorChar + "icon.png");
                    }
                    modpacks.Add(modpack);
                }
            }
            if (File.Exists("Mods" + Path.DirectorySeparatorChar + "modmusic.wac"))
            {
                music = SoundEffect.FromFile("Mods" + Path.DirectorySeparatorChar + "modmusic.wav").CreateInstance();
                music.IsLooped = true;
                music.Play();
            }
            else
            {
                builtInMusic = Content.Load<Song>("Audio/modmusic");
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(builtInMusic);
            }
            if (modpacks.Count > 6)
                LeftScrollLimit = (modpacks.Count - 6) * 150;
        }

        public void Initialize(Game1 game1)
        {
            Game1 = game1;
            Game1.ClearColor = new Color(0, 0, 41);
            LeftScroll = 0;
            LeftScrollGoal = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (Game1.MouseX <= 960)
            {
                LeftScrollGoal += Game1.GetScrollWheel(150);
                LeftScrollGoal = Math.Clamp(LeftScrollGoal, -LeftScrollLimit, 0);
            } else
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
            foreach (var modpack in modpacks)
            {
                if (!modpack.enabled)
                {
                    Rectangle clickBox = new Rectangle(50, 100 + yOffset + (int)Math.Round(LeftScroll), 828, 128);
                    if (clickBox.Contains(Game1.MouseX, Game1.MouseY) && Game1.GetMouseDown())
                    {
                        modpack.enabled = true;
                        Game1.Modpacks.Add(modpack.Name);
                    }
                    yOffset += 150;
                    count++;
                }
            }
            LeftScrollLimit = 0;
            if (count > 6)
                LeftScrollLimit = (count - 6) * 150;
            yOffset = 0;
            count = 0;
            foreach (var modpack in modpacks)
            {
                if (modpack.enabled)
                {
                    Rectangle clickBox = new Rectangle(1050, 100 + yOffset + (int)Math.Round(RightScroll), 828, 128);
                    if (clickBox.Contains(Game1.MouseX, Game1.MouseY) && Game1.GetMouseDown())
                    {
                        modpack.enabled = false;
                        Game1.Modpacks.Remove(modpack.Name);
                    }
                    yOffset += 150;
                    count++;
                }
            }
            RightScrollLimit = 0;
            if (count > 6)
                RightScrollLimit = (count - 6) * 150;
        }

        public void Draw(SpriteBatch _spriteBatch, GameTime gameTime, SpriteFont defaultfont, Texture2D debugbox)
        {
            int yOffset = 0;
            foreach (var modpack in modpacks)
            {
                if (!modpack.enabled)
                {
                    Rectangle clickBox = new Rectangle(50, 100 + yOffset + (int)Math.Round(LeftScroll), 828, 128);
                    if (clickBox.Contains(Game1.MouseX, Game1.MouseY))
                    {
                        _spriteBatch.Draw(debugbox, new Rectangle(40, 90 + yOffset + (int)Math.Round(LeftScroll), 828, 150),Color.White * 0.1f);
                    }
                    _spriteBatch.Draw(debugbox, new Rectangle(50, 100 + yOffset + (int)Math.Round(LeftScroll), 128, 128), Color.White * 0.1f);
                    _spriteBatch.Draw(modpack.Icon ?? debugbox, new Rectangle(50, 100 + yOffset + (int)Math.Round(LeftScroll), 128, 128), Color.White);

                    _spriteBatch.DrawString(defaultfont, modpack.Name, new Vector2(200, 100 + yOffset + LeftScroll), Color.White);
                    _spriteBatch.DrawString(Game1.PixelFont, modpack.Description, new Vector2(200, 130 + yOffset + LeftScroll), Color.White);
                    _spriteBatch.DrawString(Game1.PixelFont, "Mod version " + modpack.ModVersion + " for version " + modpack.GameVersion, new Vector2(200, 200 + yOffset + LeftScroll), Color.Gray);
                    yOffset += 150;
                }
            }
            yOffset = 0;
            foreach (var modpack in modpacks)
            {
                if (modpack.enabled)
                {
                    Rectangle clickBox = new Rectangle(1050, 100 + yOffset + (int)Math.Round(RightScroll), 828, 128);
                    if (clickBox.Contains(Game1.MouseX, Game1.MouseY))
                    {
                        _spriteBatch.Draw(debugbox, new Rectangle(1040, 90 + yOffset + (int)Math.Round(RightScroll), 828, 150), Color.White * 0.1f);
                    }
                    _spriteBatch.Draw(debugbox, new Rectangle(1050, 100 + yOffset + (int)Math.Round(RightScroll), 128, 128), Color.White * 0.1f);
                    _spriteBatch.Draw(modpack.Icon ?? debugbox, new Rectangle(1050, 100 + yOffset + (int)Math.Round(RightScroll), 128, 128), Color.White);

                    _spriteBatch.DrawString(defaultfont, modpack.Name, new Vector2(1200, 100 + yOffset + RightScroll), Color.White);
                    _spriteBatch.DrawString(Game1.PixelFont, modpack.Description, new Vector2(1200, 130 + yOffset + RightScroll), Color.White);
                    _spriteBatch.DrawString(Game1.PixelFont, "Mod version " + modpack.ModVersion + " for version " + modpack.GameVersion, new Vector2(1200, 200 + yOffset + RightScroll), Color.Gray);
                    yOffset += 150;
                }
            }
            _spriteBatch.Draw(debugbox, new Rectangle(0, 0, 1920, 80), Game1.ClearColor);
            _spriteBatch.DrawString(defaultfont, "Cheesenaf/BBGSim Mod Menu", new Vector2(0, 0), Color.White);
            _spriteBatch.DrawString(defaultfont, "Game version " + Game1.Version, new Vector2(1590, 0), Color.White);
            _spriteBatch.DrawString(defaultfont, "Click to enable a mod", new Vector2(250, 40), Color.White);
            _spriteBatch.DrawString(Game1.PixelFont, "Mods are applied in selected order", new Vector2(275, 65), Color.White);
        }
    }
}
