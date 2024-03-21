using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework.Audio;

namespace Cheesenaf
{
    internal class BBGSim
    {
        Game1 Game1;
        string[] names;
        string[] syowenDialogue;
        string[] mochaDialogue;
        string[] brettDialogue;
        string[] alanDialogue;
        string[] berryDialogue;
        string[] playerDialogue;
        string display = "";
        string buffer = "";
        string userInput;

        int dialoguePart = -1;

        int[] textcoords = new int[2] {360,900};
        const int dialogueCount = 11;
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
        int emotion = 1;
        Color[] nametagColor = [new Color(185, 56, 255), new Color(20, 148, 222), new Color(23, 189, 131), new Color(8, 140, 37), new Color(255, 143, 186)];
        Song song;

        Texture2D dialogueUI;
        Rectangle[] UIRects = new Rectangle[4];

        SoundEffect[] sounds;
        SoundEffectInstance soundInstance;
        SoundEffectInstance boxZooms;
        public void LoadContent(ContentManager Content)
        {
            bbgs = new Texture2D[5];
            if (Game1.saveData.Bbg == 0)
            {
                bbgs[0] = Content.Load<Texture2D>("BBGSim/syowen");
                bbgs[1] = Content.Load<Texture2D>("BBGSim/syowenhappy");
                bbgs[2] = Content.Load<Texture2D>("BBGSim/syowenneutral");
                bbgs[3] = Content.Load<Texture2D>("BBGSim/syowenunhappy");
            }
            if (Game1.saveData.Bbg == 1)
            {
                bbgs[0] = Content.Load<Texture2D>("BBGSim/mocha");
                bbgs[1] = Content.Load<Texture2D>("BBGSim/mochahappy");
                bbgs[2] = Content.Load<Texture2D>("BBGSim/mochaneutral");
                bbgs[3] = Content.Load<Texture2D>("BBGSim/mochaunhappy");
            }
            if (Game1.saveData.Bbg == 2)
            {
                bbgs[0] = Content.Load<Texture2D>("BBGSim/brett");
                bbgs[1] = Content.Load<Texture2D>("BBGSim/bretthappy");
                bbgs[2] = Content.Load<Texture2D>("BBGSim/brettneutral");
                bbgs[3] = Content.Load<Texture2D>("BBGSim/brettunhappy");
            }
            if (Game1.saveData.Bbg == 3)
            {
                bbgs[0] = Content.Load<Texture2D>("BBGSim/alan");
                bbgs[1] = Content.Load<Texture2D>("BBGSim/alanhappy");
                bbgs[2] = Content.Load<Texture2D>("BBGSim/alanneutral");
                bbgs[3] = Content.Load<Texture2D>("BBGSim/alanunhappy");
            }
            if (Game1.saveData.Bbg == 4)
            {
                bbgs[0] = Content.Load<Texture2D>("BBGSim/alan");
                bbgs[1] = Content.Load<Texture2D>("BBGSim/alanhappy");
                bbgs[2] = Content.Load<Texture2D>("BBGSim/alanneutral");
                bbgs[3] = Content.Load<Texture2D>("BBGSim/alanunhappy");
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
            names = ["Syowen", "Mocha", "Brett", "Alan", "Berry"];
            syowenDialogue = new string[dialogueCount]
            {
                "Oh, hey. I'm Syowen. What's your name?", "Huh. {0}, huh?", "You seem pretty cool, want to check out the star festival tonight?", "Awesome, see you there bbg",
                "Oh hey, you made it!", "Oh cool, thanks!", "Pizza? Erm.. well...", "Just, bad memories with that.", "Yeah, the pizza.", "Oh, okay. See you later.", "Welcome back, {0}..."
            };
            mochaDialogue = new string[dialogueCount]
            {
                "Heyo! I'm Mocha, and you areeee...?", "You said {0}? Cool, nice to meet you!", "Hey, theres this cool star festival thing going on, wanna check it out?", "Alright, see you there!",
                "Glad you made it!", "Ooh! You're so thoughtful, thank you!", "Pizza...? With.. stuffed crust..?", "...Don't worry about it, just... bad memories...", "mhm...", "Oh... Alright. bye guys", "Welcome back, {0}..."
            };
            brettDialogue = new string[dialogueCount]
            {
                "Yo, what up... Why are you wearing a nametag?", "Dude, your name is {0}? That's awesome", "Yo they got the star festival going on tonight, who out here marioing they galaxy",
                "That's whats up", "Yo what up", "thanks dude", "Pizza? Really? why pizza", "Pizza is mid. I had bad stuff happen around it before", "yeah", "That's lame", "Welcome back, {0}..."
            };
            alanDialogue = new string[dialogueCount]
            {
                "Hey there! What's your name?", "Nice to meet you, {0}, I'm Alan!", "Hey theres a star festival thing tonight, you should be there tonight", "Alright, sounds good!",
                "Oh hey, you're here!", "Awesome, thanks!", "Ohhh. Pizza...", "Yeeeaaah, no I dont have good memories of Pizza.", "Yyyyyeahh.", "Oh. okay bye", "Welcome back, {0}..."
            };
            berryDialogue = new string[dialogueCount]
            {
                "Hi! My name is Berry, what's yours?", "Nice to meet you, {0}!", "Hey theres a star festival thing tonight, you should come with me there!", "Great! See you there, {0}!",
                "About time you made it!", "Awe, how thoughtful of you!", "...\niiit's a slice of pizza, yaayyyyy...", "I'm sorry, I just, really haven't had a good experience with pizza, especially with the stuffed crust...", "Yeah, no, none at all.", "Alright, your loss.", "Welcome back, {0}..."
            };
            playerDialogue = new string[50]
            {
                "Sure!","Nah","Sounds good!","No I don't","Dude I am","Not me lol","Okay","No", "Sure!", "No thanks",
                "I got you some\n food!","","I got you some\n food!","","I got you some\n food!","","I got you some\n food!","","I got you some\n food!","",
                "What's wrong?","","What's wrong?","","What's wrong?","","What's wrong?","","What's wrong?","",
                "With... Pizza?","","From... Pizza?","","Bad stuff?","","You don't?","","Really?","",
                "What \nhappened?","","What \nhappened?","","What \nhappened?","","What \nhappened?","","What \nhappened?",""
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
                switch (Game1.saveData.Bbg)
                {
                    default:
                        buffer = WrapText(Game1.BBGFont, syowenDialogue[dialoguePart], wraplimit);
                        break;
                    case 1:
                        buffer = WrapText(Game1.BBGFont, mochaDialogue[dialoguePart], wraplimit);
                        break;
                    case 2:
                        buffer = WrapText(Game1.BBGFont, brettDialogue[dialoguePart], wraplimit);
                        break;
                    case 3:
                        buffer = WrapText(Game1.BBGFont, alanDialogue[dialoguePart], wraplimit);
                        break;
                    case 4:
                        buffer = WrapText(Game1.BBGFont, berryDialogue[dialoguePart], wraplimit);
                        break;
                }
            }
            else
            {
                switch (Game1.saveData.Bbg)
                {
                    default:
                        buffer = WrapText(Game1.BBGFont, syowenDialogue[0], wraplimit);
                        break;
                    case 1:
                        buffer = WrapText(Game1.BBGFont, mochaDialogue[0], wraplimit);
                        break;
                    case 2:
                        buffer = WrapText(Game1.BBGFont, brettDialogue[0], wraplimit);
                        break;
                    case 3:
                        buffer = WrapText(Game1.BBGFont, alanDialogue[0], wraplimit);
                        break;
                    case 4:
                        buffer = WrapText(Game1.BBGFont, berryDialogue[0], wraplimit);
                        break;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            if (Game1.GetKeyUp(Keys.Escape))
            {
                Game1.ChangeScene(0);
            }
            if (display.Length < buffer.Length)
            {
                display += buffer.ToCharArray()[display.Length];
            }

            if (scale[0] == 0 && dialoguePart == 8)
            {
                Game1.sneakyLoad = true;
                Game1.saveData.AltTitle = true;
                Game1.saveData.Night = 1;
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

            if (Game1.GetKeyDown(Keys.Enter) && display.Length == buffer.Length)
            {
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
                        else if (inChoice && selection == 1)
                        {
                            emotion = 2;
                            inChoice = false;
                            choicesPulledUp++;
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
            if (bbgs[0] != null)
            {
                _spriteBatch.Draw(bbgs[0], new Vector2(400, 50), new Rectangle(0, 0, 1600, 1600), Color.White, 0, new Vector2(0, 0), 0.65f, SpriteEffects.None, 0);
                _spriteBatch.Draw(bbgs[emotion + 1], new Vector2(400, 50), new Rectangle(0, 0, 1600, 1600), Color.White, 0, new Vector2(0, 0), 0.65f, SpriteEffects.None, 0);
            }
            _spriteBatch.Draw(dialogueUI, new Vector2(900, 1000), UIRects[0], Color.White, 0, new Vector2(UIRects[0].Width / 2, UIRects[0].Height / 2), 0.75f * scale[0], SpriteEffects.None, 0);
            _spriteBatch.Draw(dialogueUI, new Vector2(900, 1000), UIRects[1], nametagColor[Game1.saveData.Bbg], -0.35f, new Vector2(1400, 1100), 0.35f * scale[0], SpriteEffects.None, 0);
                _spriteBatch.DrawString(defaultfont, names[Game1.saveData.Bbg], new Vector2(960, 1000), Color.White, -0.35f, new Vector2(300, 235), 1.65f * scale[0], SpriteEffects.None, 0);
            //Input
            _spriteBatch.Draw(dialogueUI, new Vector2(900, 100), UIRects[3], Color.White, 0, new Vector2(UIRects[3].Width / 2, UIRects[3].Height / 2), 0.5f * scale[1], SpriteEffects.None, 0);
            if (userInput != null)
                _spriteBatch.DrawString(defaultfont, userInput, new Vector2(900, 100), Color.Black, 0, new Vector2(310, 20), scale[1], SpriteEffects.None, 0);
            //Choices
            _spriteBatch.Draw(dialogueUI, new Vector2(1500, 850), UIRects[2], Color.White, 0, new Vector2(UIRects[2].Width / 2, UIRects[2].Height / 2), 0.75f * scale[2], SpriteEffects.None, 0);
            if (scale[2] == 1 && choicesPulledUp <= 4 && inChoice)
            {
                _spriteBatch.DrawString(defaultfont, (selection == 0 ? "> " : "") + playerDialogue[(Game1.saveData.Bbg * 2) + (choicesPulledUp * 10)], new Vector2(1400, 775), Color.Chocolate, 0, new Vector2(50, 0), scale[2], SpriteEffects.None, 0);
                if (currentSelectionCount >= 2)
                    _spriteBatch.DrawString(defaultfont, (selection == 1 ? "> " : "") + playerDialogue[((Game1.saveData.Bbg * 2) + 1) + (choicesPulledUp * 10)], new Vector2(1500, 900), Color.Chocolate, 0, new Vector2(50, 0), scale[2], SpriteEffects.None, 0);
            }
            //Dialogue
            _spriteBatch.DrawString(defaultfont, display, new Vector2(textcoords[0], textcoords[1]), Color.Black);
        }
    }
}
