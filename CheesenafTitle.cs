using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using System.Reflection.Metadata;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace Cheesenaf
{
    internal class CheesenafTitle
    {
        private Song titleMusic;
        private SoundEffect[] sounds;
        private SoundEffectInstance[] soundInstance;
        private Texture2D[] textures;
        float staticTime;
        int staticMultiplier;
        public bool transition;
        int selection = 0;
        int selectionLastFrame;
        bool customScreen;

        int freddyLevel;
        int bonnieLevel;
        int chicaLevel;
        int foxyLevel;

        public bool fromJumpscare;
        Game1 Game1;

        public MouseState mouseState;
        public int MouseX;
        public int MouseY;


        public void LoadContent(ContentManager Content)
        {
            titleMusic = Content.Load<Song>("Audio/cheesenaf title");
            textures = new Texture2D[4];
            textures[0] = Content.Load<Texture2D>("Jumpscares/static");
            textures[1] = Content.Load<Texture2D>("Cams/cam static");
            textures[2] = Content.Load<Texture2D>("menus/PizzaPoster");
            textures[3] = Content.Load<Texture2D>("Cams/cambutton off");
            if (sounds == null)
            {
                sounds = new SoundEffect[2];
                soundInstance = new SoundEffectInstance[2];
            }
            sounds[1] = Content.Load<SoundEffect>("Audio/changecam");
            soundInstance[1] = sounds[1].CreateInstance();
        }


        public void Initialize(Game1 game1)
        {
            sounds = new SoundEffect[2];
            soundInstance = new SoundEffectInstance[2];
            Game1 = game1;
            if (fromJumpscare)
            {
                sounds[0] = Game1.Content.Load<SoundEffect>("Audio/static");
                soundInstance[0] = sounds[0].CreateInstance();
                soundInstance[0].Play();
                staticTime = 8;
            }
            else
            {
                titleMusic = Game1.Content.Load<Song>("Audio/cheesenaf title");
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(titleMusic);
            }
            Game1.ClearColor = Color.Black;
        }


        public void Update(GameTime gameTime)
        {
            mouseState = Game1.mouseState;
            MouseX = Game1.MouseX;
            MouseY = Game1.MouseY;
            if (transition)
            {
                Game1.ChangeScene(3);
            }

            mouseState = Mouse.GetState();

            if (staticTime > 0)
            {
                staticTime -= Game1.delta;
                Trace.WriteLine(staticTime.ToString() + " || " + ((float)gameTime.ElapsedGameTime.TotalSeconds).ToString());
            }
            else if (fromJumpscare)
            {
                fromJumpscare = false;
                soundInstance[0].Stop();
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(titleMusic);
            }

            staticMultiplier++;
            if (staticMultiplier > 7) 
            {
                staticMultiplier = 0;
            }

            if (!fromJumpscare)
            {
                if (!customScreen)
                {
                    if (MouseY >= 390 && MouseY < 590)
                    {
                        selection = (int)Math.Floor((MouseY - 390f) / 50f);
                    }
                    else
                    {
                        selection = 999;
                    }

                    if (selection != selectionLastFrame && transition == false)
                    {
                        if ((!Game1.saveData.SixUnlocked && selection == 2) || (!Game1.saveData.CustomUnlocked && selection == 3) || selection == 999)
                        {
                            //Do Nothing
                        }
                        else
                        {
                            if (soundInstance[1] != null)
                            {
                                soundInstance[1].Stop();
                                soundInstance[1].Play();
                            }
                        }
                    }
                    selectionLastFrame = selection;

                    if (selection == 0 && Game1.GetMouseDown())
                    {
                        Game1.saveData.Night = 1;
                        transition = true;
                        if (soundInstance[1] != null)
                        {
                            soundInstance[1].Stop();
                            soundInstance[1].Play();
                        }
                    }
                    if (selection == 1 && Game1.GetMouseDown())
                    {
                        transition = true;
                        if (soundInstance[1] != null)
                        {
                            soundInstance[1].Stop();
                            soundInstance[1].Play();
                        }
                    }
                    if (selection == 2 && Game1.GetMouseDown() && Game1.saveData.SixUnlocked)
                    {
                        Game1.saveData.Night = 6;
                        transition = true;
                        if (soundInstance[1] != null)
                        {
                            soundInstance[1].Stop();
                            soundInstance[1].Play();
                        }
                    }
                    if (selection == 3 && Game1.GetMouseDown() && Game1.saveData.CustomUnlocked)
                    {
                        customScreen = true;
                        if (soundInstance[1] != null)
                        {
                            soundInstance[1].Stop();
                            soundInstance[1].Play();
                        }
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.S) && Keyboard.GetState().IsKeyDown(Keys.Y) && Keyboard.GetState().IsKeyDown(Keys.O) &&
                        Keyboard.GetState().IsKeyDown(Keys.W) && Keyboard.GetState().IsKeyDown(Keys.E) && Keyboard.GetState().IsKeyDown(Keys.N) && Game1.saveData.Bbg != 1 && Game1.saveData.Bbg != 2 && Game1.saveData.Bbg != 3)
                    {
                        Game1.ResetData();
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.M) && Keyboard.GetState().IsKeyDown(Keys.O) && Keyboard.GetState().IsKeyDown(Keys.C) &&
                        Keyboard.GetState().IsKeyDown(Keys.H) && Keyboard.GetState().IsKeyDown(Keys.A) && Game1.saveData.Bbg == 1)
                    {
                        Game1.ResetData();
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.B) && Keyboard.GetState().IsKeyDown(Keys.R) && Keyboard.GetState().IsKeyDown(Keys.E) &&
                        Keyboard.GetState().IsKeyDown(Keys.T) && Game1.saveData.Bbg == 2)
                    {
                        Game1.ResetData();
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.S) && Keyboard.GetState().IsKeyDown(Keys.A) && Keyboard.GetState().IsKeyDown(Keys.W) &&
                        Keyboard.GetState().IsKeyDown(Keys.Y) && Keyboard.GetState().IsKeyDown(Keys.E) && Keyboard.GetState().IsKeyDown(Keys.R) && Game1.saveData.Bbg == 3)
                    {
                        Game1.ResetData();
                    }
                }
                else
                {
                    if (MouseX >= 400 && MouseX <= 1500 && MouseY >= 450 && MouseY <= 525)
                    {
                        if (MouseX >= 400 && MouseX <= 450)
                            selection = 0;
                        if (MouseX >= 490 && MouseX <= 540)
                            selection = 1;
                        
                        if (MouseX >= 720 && MouseX <= 770)
                            selection = 2;
                        if (MouseX >= 810 && MouseX <= 860)
                            selection = 3;
                        
                        if (MouseX >= 1040 && MouseX <= 1090)
                            selection = 4;
                        if (MouseX >= 1130 && MouseX <= 1180)
                            selection = 5;
                        
                        if (MouseX >= 1360 && MouseX <= 1410)
                            selection = 6;
                        if (MouseX >= 1450 && MouseX <= 1500)
                            selection = 7;

                        if ((MouseX > 540 && MouseX < 720) || (MouseX > 860 && MouseX < 1040) || (MouseX > 1180 && MouseX < 1360))
                            selection = 999;
                    }
                    else if (MouseX >= 400 && MouseX <= 500 && MouseY >= 700 && MouseY <= 750)
                    {
                        selection = 8;
                    }
                    else if (MouseX >= 1300 && MouseX <= 1600 && MouseY >= 700 && MouseY <= 750)
                    {
                        selection = 9;
                    }
                    else if (MouseX >= 400 && MouseX <= 597 && MouseY >= 600 && MouseY <= 663)
                    {
                        selection = 10;
                    }
                    else if (MouseX >= 630 && MouseX <= 827 && MouseY >= 600 && MouseY <= 663)
                    {
                        selection = 11;
                    }
                    else if (MouseX >= 860 && MouseX <= 1057 && MouseY >= 600 && MouseY <= 663)
                    {
                        selection = 12;
                    }
                    else if (MouseX >= 1090 && MouseX <= 1287 && MouseY >= 600 && MouseY <= 663)
                    {
                        selection = 13;
                    }
                    else if (MouseX >= 1320 && MouseX <= 1517 && MouseY >= 600 && MouseY <= 663)
                    {
                        selection = 14;
                    }
                    else
                    {
                        selection = 999;
                    }

                    if (selection == 0 && Game1.GetMouseDown() && freddyLevel > 0)
                    {
                        freddyLevel --;
                    }
                    if (selection == 1 && Game1.GetMouseDown() && freddyLevel < 20)
                    {
                        freddyLevel ++;
                    }
                    if (selection == 2 && Game1.GetMouseDown() && bonnieLevel > 0)
                    {
                        bonnieLevel --;
                    }
                    if (selection == 3 && Game1.GetMouseDown() && bonnieLevel < 20)
                    {
                        bonnieLevel ++;
                    }
                    if (selection == 4 && Game1.GetMouseDown() && chicaLevel > 0)
                    {
                        chicaLevel --;
                    }
                    if (selection == 5 && Game1.GetMouseDown() && chicaLevel < 20)
                    {
                        chicaLevel ++;
                    }
                    if (selection == 6 && Game1.GetMouseDown() && foxyLevel > 0)
                    {
                        foxyLevel --;
                    }
                    if (selection == 7 && Game1.GetMouseDown() && foxyLevel < 20)
                    {
                        foxyLevel ++;
                    }

                    if (selection == 8 && Game1.GetMouseDown())
                    {
                        customScreen = false;
                        if (soundInstance[1] != null)
                        {
                            soundInstance[1].Stop();
                            soundInstance[1].Play();
                        }
                    }
                    if (selection == 9 && Game1.GetMouseDown())
                    {
                        Game1.saveData.Night = 7;
                        Game1.BonnieLevel = bonnieLevel;
                        Game1.FreddyLevel = freddyLevel;
                        Game1.ChicaLevel = chicaLevel;
                        Game1.FoxyLevel = foxyLevel;
                        transition = true;
                        if (soundInstance[1] != null)
                        {
                            soundInstance[1].Stop();
                            soundInstance[1].Play();
                        }
                    }
                    if (selection == 10 && Game1.GetMouseDown())
                    {
                        bonnieLevel = 0;
                        chicaLevel = 0;
                        freddyLevel = 0;
                        foxyLevel = 0;
                    }
                    if (selection == 11 && Game1.GetMouseDown())
                    {
                        bonnieLevel = 5;
                        chicaLevel = 5;
                        freddyLevel = 5;
                        foxyLevel = 5;
                    }
                    if (selection == 12 && Game1.GetMouseDown())
                    {
                        bonnieLevel = 10;
                        chicaLevel = 10;
                        freddyLevel = 10;
                        foxyLevel = 10;
                    }
                    if (selection == 13 && Game1.GetMouseDown())
                    {
                        bonnieLevel = 15;
                        chicaLevel = 15;
                        freddyLevel = 15;
                        foxyLevel = 15;
                    }
                    if (selection == 14 && Game1.GetMouseDown())
                    {
                        bonnieLevel = 20;
                        chicaLevel = 20;
                        freddyLevel = 20;
                        foxyLevel = 20;
                    }
                }
            }
        }

        public void Draw(SpriteBatch _spriteBatch, GameTime gameTime, SpriteFont defaultfont, Texture2D debugbox)
        {
            if (transition)
            {
                if (freddyLevel == 1 && bonnieLevel == 9 && chicaLevel == 8 && foxyLevel == 7)
                {
                    _spriteBatch.DrawString(defaultfont, "nuh uh", new Vector2(950, 540), Color.White);
                }
                else
                if (Game1.saveData.Night != 7)
                {
                    _spriteBatch.DrawString(defaultfont, "Night " + Game1.saveData.Night, new Vector2(950, 540), Color.White);
                }
                else
                    _spriteBatch.DrawString(defaultfont, "Custom Night", new Vector2(900, 540), Color.White);
            }
            else
            {
                if (fromJumpscare)
                {
                    _spriteBatch.Draw(textures[0], new Vector2(0, 0), new Rectangle(0, (staticMultiplier * 720) + 4 + (staticMultiplier * 2), 1280, 720), Color.White, 0, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0);
                }
                else
                {
                    _spriteBatch.Draw(textures[1], new Vector2(0, 0), new Rectangle(0, (staticMultiplier * 720) + 4 + (staticMultiplier * 2), 1280, 720), Color.White, 0, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0);
                    if (!customScreen)
                    {
                        //_spriteBatch.DrawString(defaultfont, selection.ToString(), new Vector2(250, 250), Color.White);
                        _spriteBatch.DrawString(defaultfont, "Cheesenaf", new Vector2(250, 300), Color.White);
                        _spriteBatch.DrawString(defaultfont, "New Game", new Vector2(250, 400), Color.White);
                        _spriteBatch.DrawString(defaultfont, "Continue", new Vector2(250, 450), Color.White);
                        if (Game1.saveData.SixUnlocked)
                            _spriteBatch.DrawString(defaultfont, "Night 6", new Vector2(250, 500), Color.White);
                        if (Game1.saveData.CustomUnlocked)
                            _spriteBatch.DrawString(defaultfont, "Custom Night", new Vector2(250, 550), Color.White);

                        if (Game1.saveData.Bbg != 1 && Game1.saveData.Bbg != 2 && Game1.saveData.Bbg != 3)
                        _spriteBatch.DrawString(defaultfont, "Hold down S, Y, O, W, E, N to Reset Data", new Vector2(250, 950), Color.White);
                        if (Game1.saveData.Bbg == 1)
                        _spriteBatch.DrawString(defaultfont, "Hold down M, O, C, H, A, to Reset Data", new Vector2(250, 950), Color.White);
                        if (Game1.saveData.Bbg == 2)
                        _spriteBatch.DrawString(defaultfont, "Hold down B, R, E, T to Reset Data", new Vector2(250, 950), Color.White);
                        if (Game1.saveData.Bbg == 3)
                        _spriteBatch.DrawString(defaultfont, "Hold down S, A, W, Y, E, R to Reset Data", new Vector2(250, 950), Color.White);

                        //Mouse
                        if (selection == 0) _spriteBatch.DrawString(defaultfont, ">", new Vector2(200, 400), Color.White);
                        if (selection == 1) _spriteBatch.DrawString(defaultfont, ">", new Vector2(200, 450), Color.White);
                        if (selection == 1)
                            _spriteBatch.DrawString(defaultfont, "Night " + Game1.saveData.Night.ToString(), new Vector2(425, 450), Color.White);
                        if (selection == 2 && Game1.saveData.SixUnlocked) _spriteBatch.DrawString(defaultfont, ">", new Vector2(200, 500), Color.White);
                        if (selection == 3 && Game1.saveData.CustomUnlocked) _spriteBatch.DrawString(defaultfont, ">", new Vector2(200, 550), Color.White);
                    }
                    else
                    {
                        _spriteBatch.DrawString(defaultfont, MouseX.ToString() + ", " + MouseY.ToString(), new Vector2(0, 0), Color.White);
                        //Selections
                        _spriteBatch.Draw(textures[3], new Vector2(400,600),new Rectangle(0,0,128,64),Color.White,0,new Vector2(0,0),new Vector2(1.55f,1f),SpriteEffects.None,0);
                        _spriteBatch.DrawString(defaultfont, "SET ALL 0", new Vector2(410, 610), Color.White);

                        _spriteBatch.Draw(textures[3], new Vector2(630,600),new Rectangle(0,0,128,64),Color.White,0,new Vector2(0,0),new Vector2(1.55f,1f),SpriteEffects.None,0);
                        _spriteBatch.DrawString(defaultfont, "SET ALL 5", new Vector2(640, 610), Color.White);

                        _spriteBatch.Draw(textures[3], new Vector2(860,600),new Rectangle(0,0,128,64),Color.White,0,new Vector2(0,0),new Vector2(1.55f,1f),SpriteEffects.None,0);
                        _spriteBatch.DrawString(defaultfont, "SET ALL 10", new Vector2(870, 610), Color.White);

                        _spriteBatch.Draw(textures[3], new Vector2(1090,600),new Rectangle(0,0,128,64),Color.White,0,new Vector2(0,0),new Vector2(1.55f,1f),SpriteEffects.None,0);
                        _spriteBatch.DrawString(defaultfont, "SET ALL 15", new Vector2(1100, 610), Color.White);

                        _spriteBatch.Draw(textures[3], new Vector2(1320,600),new Rectangle(0,0,128,64),Color.White,0,new Vector2(0,0),new Vector2(1.55f,1f),SpriteEffects.None,0);
                        _spriteBatch.DrawString(defaultfont, "SET ALL 20", new Vector2(1330, 610), Color.White);

                        //Freddy
                        _spriteBatch.Draw(textures[2], new Vector2(400,300), new Rectangle(492, 469, 600, 600), Color.White, 0, new Vector2(0,0), 0.25f, SpriteEffects.None,0);
                        _spriteBatch.DrawString(defaultfont, "< " + freddyLevel.ToString("00") + " >", new Vector2(420, 475),Color.White);
                        //Bonnie
                        _spriteBatch.Draw(textures[2], new Vector2(720,300), new Rectangle(990, 462, 600, 600), Color.White, 0, new Vector2(0,0), 0.25f, SpriteEffects.None,0);
                        _spriteBatch.DrawString(defaultfont, "< " + bonnieLevel.ToString("00") + " >", new Vector2(740, 475), Color.White);
                        //Chica
                        _spriteBatch.Draw(textures[2], new Vector2(1040,300), new Rectangle(0, 466, 600, 600), Color.White, 0, new Vector2(0,0), 0.25f, SpriteEffects.None,0);
                        _spriteBatch.DrawString(defaultfont, "< " + chicaLevel.ToString("00") + " >", new Vector2(1060, 475), Color.White);
                        //Foxy
                        _spriteBatch.Draw(textures[2], new Vector2(1360,300), new Rectangle(162, 0, 600, 600), Color.White, 0, new Vector2(0,0), 0.25f, SpriteEffects.None,0);
                        _spriteBatch.DrawString(defaultfont, "< " + foxyLevel.ToString("00") + " >", new Vector2(1380, 475), Color.White);
                        //etc
                        _spriteBatch.DrawString(defaultfont, "<< Back", new Vector2(400, 700), Color.White);
                        _spriteBatch.DrawString(defaultfont, "BEGIN NIGHT >", new Vector2(1300, 700), Color.White);
                    }
                }
            }
        }
    }
}
