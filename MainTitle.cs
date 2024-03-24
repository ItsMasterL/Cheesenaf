using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Cheesenaf
{
    internal class MainTitle
    {
        private Song titleMusic;
        private SoundEffect unlock;
        private SoundEffect jumpscare;
        private SoundEffect select;
        private SoundEffect wumbo;
        Game1 Game1;
        string[] names;
        Texture2D bg;
        Texture2D title;
        Texture2D icons;
        float bounceTextScale;
        float titleOffsetY;
        float startScale = 1;
        float startLerp;
        float unlockAlpha;
        Color rainbow;
        float jumpscareCountdown = 1000;
        int jumpscareIndex = -1;
        int loadInt;
        int splashesSeen;
        Texture2D[] jumpscareframes = new Texture2D[120];
        Rectangle[] iconSpaces = new Rectangle[5]
        {
            new Rectangle(193, 0, 232, 240), new Rectangle(0, 0, 192, 240), new Rectangle(425, 0, 200, 240), new Rectangle(625, 0, 184, 240), new Rectangle(840, 0, 185, 240),
        };
        int selection()
        {
            if (Game1.MouseX >= 1400 && Game1.MouseX <= 1525)
            {
                if(Game1.MouseY >= 900 && Game1.MouseY <= 950)
                {
                    return 0;
                }
            }
            if (Game1.MouseX >= 1360 && Game1.MouseX <= 1400)
            {
                if(Game1.MouseY >= 850 && Game1.MouseY <= 900)
                {
                    return 1;
                }
            }
            if (Game1.MouseX >= 1460 && Game1.MouseX <= 1500)
            {
                if(Game1.MouseY >= 850 && Game1.MouseY <= 900)
                {
                    return 2;
                }
            }
            return 999;
        }
        readonly string[] splashtexts =
            [
                "Not a dating sim!",
                "WOOOOOOOOOOOO!",
                "I gotta go to the bathroom!",
                "I gotta run!",
                "That's the thrill of these roll hacks!",
                "No, seriously, this isn't a dating sim!",
                "Bread Ryes!",
                "Out of chocolate (Thanks Alan)!",
                "I think ya nuts!",
                "Not compatable with Archipelago!",
                "Not even joking. This isn't a dating simulator. It's just a fun silly little april fools game. \nI mean honestly, were you expecting to find love here today? \nBrett is literally a minor, not only have I chosen to keep romance \nout but it would actually be illegal and gross!",
                "toto mina!",
                "*superman wave gif* hi {0}",
                "You should totally pick {1}!",
                "So the {0} should be here soon...",
                "Don't worry guys, I'll save you!",
                "Hey guys, {0} here, well, their friend {1}, you know, the cameraman!",
                "I'll be the turkey!",
                "No more than {2} splash texts!",
                "The animatronic characters do get a bit quirky at night!",
                "Uh, hello? Hello hello?",
                "OoOOOoOOh yellow text jumpscare!",
                "1 1/4 cup water\r\n1 1/2 teaspoon citric acid\r\n1/4 rennet tablet or 1/4 teaspoon liquid rennet (Not Junket rennet)\r\n1 gallon milk, whole or 2%, not ultra-pasteurized*\r\n1 teaspoon kosher salt",
                "5 quart or larger non-reactive pot\r\nMeasuring cups and spoons\r\nThermometer\r\n8\" knife, off-set spatula, or similar slim instrument for cutting the curds\r\nSlotted spoon\r\nMicrowavable bowl\r\nRubber gloves",
                "1: Prepare the Citric Acid and Rennet: Measure out 1 cup of water. Stir in the citric acid until dissolved. \nMeasure out 1/4 cup of water in a separate bowl. Stir in the rennet until dissolved.",
                "2: Warm the Milk: Pour the milk into the pot. Stir in the citric acid solution. Set the pot over medium-high \nheat and warm to 90°F, stirring gently.",
                "3: Add the Rennet: Remove the pot from heat and gently stir in the rennet solution. Count to 30. Stop stirring, \ncover the pot, and let it sit undisturbed for 5 minutes.",
                "4: Cut the Curds: After five minutes, the milk should have set, and it should look and feel like soft silken \ntofu. If it is still liquidy, re-cover the pot and let it sit for another five minutes. Once the milk has set, cut it into uniform curds: \nmake several parallel cuts vertically through the curds and then several parallel cuts horizontally, creating a grid-like \npattern. Make sure your knife reaches all the way to the bottom of the pan.",
                "5: Cook the Curds: Place the pot back on the stove over medium heat and warm the curds to 105°F. Stir slowly as \nthe curds warm, but try not to break them up too much. The curds will eventually clump together and separate more completely from the yellow whey.",
                "6: Remove the Curds from Heat and Stir: Remove the pan from the heat and continue stirring gently for another 5 minutes.",
                "7: Separate the Curds from the Whey: Ladle the curds into a microwave-safe bowl with the slotted spoon.",
                "8: Microwave the Curds: (No microwave? See the Notes splash for directions on making mozzarella without a \nmicrowave.) Microwave the curds for one minute. Drain off the whey. Put on your rubber gloves and fold the curds over on themselves a \nfew times. At this point, the curds will still be very loose and cottage-cheese-like.",
                "9: Microwave the Curds to 135°F: Microwave the curds for another 30 seconds and check their internal temperature.\n If the temperature has reached 135°F, continue with stretching the curds. If not, continue microwaving in 30-second bursts until\n they reach temperature. The curds need to reach this temperature in order to stretch properly.",
                "10: Stretch and Shape the Mozzarella: Sprinkle the salt over the cheese and squish it with your fingers to \nincorporate. Using both hands, stretch and fold the curds repeatedly. It will start to tighten, become firm, and take on a glossy sheen. \nWhen this happens, you are ready to shape the mozzarella. Make one large ball, two smaller balls, or several bite-sized bocconcini.\n Try not to over-work the mozzarella.",
                "11: Using and Storing Your Mozzarella: The mozzarella can be used immediately or kept refrigerated for a week. \nTo refrigerate, place the mozzarella in a small container. Mix a teaspoon of salt with a cup of cool whey and pour this over the \nmozzarella. Cover and refrigerate. (Credit: Emma Christensen)",
                "Making Mozzarella Without the Microwave: Instead of microwaving the curds to make mozzarella, warm a large pot \nof water to just below boiling (about 190°F). Pour the curds into a strainer and nestle the strainer into the pot so the curds are \nsubmerged in the hot water. Let the curds sit for about five minutes. Wearing rubber gloves, fold the curds under the water and check \ntheir internal temperature. If it has not reached 135°F, let the curds sit for another few minutes until it does. Once the curds have \nreached 135°, lift them from the water and stretch as directed.",
                "Yo what up guys!",
                "In today's video...",
                "It's child time!",
                "Hey gang, it's me, Tony, and today we're gonna see if I'm faster than this mousetrap!",
                "Turns out, I'm not faster than this mousetrap, and, update, it hurts!",
                "Dude, this is just like-",
                "What's this? A memory?",
                "Hello, young lady!",
                "You stop eating the US oil reserve, dude!",
                "Josh Hutcherson - Whistle!",
                "I can't believe I have to drive all the way to work on Saturday, ALL THE WAY TO WORK!",
                "Scraggity shraggity",
                "Don't forget Saturday! You want them all to be in one place!",
                "See you on the flipside!",
                "You won't die!",
                "This peace is what all true warriors strive for!",
                "Doo doodee... what? Moo! what what",
                "...whhat.",
                "Now I know that sounds bad-",
                "It's joever!",
                "If you guys need {1}, he'll be in the corner taking poison damage!",
                "You should not kiss Brett!",
                "You're so caterpillar!",
                "Request Pokemon!",
                "Mario. Kart. WII!",
                "Disk horse!",
                "Shot the wall!",
                "Don't boil your Eevees!",
                "Mr. White, listen to me. We don't have the McRib!",
                "Hi, hello, everyone!",
                "Hello everybody, my name is Markiplier and welcome to Five Nights at Freddy's, an indie horror game you all suggested \nin mass and I saw that yamimash played it and he said it was really really good, so I'm very eager to see what is up, and that \nis a terrifying animatronic bear!",
                "The first night is never usually that bad in any of the games so I'll play through and",
                "Chocolate. did you say... CHOCOLATE???",
                "Gumbo'd for life!",
                "My heart! I loved her!\n\n\nwaaaaa",
                "You're orange juice!",
                "Christmas is ruined!",
                "It would be quite shocking if that cat hit the whip!",
                "Tell us something your mum doesnt know!",
                "It's aborbing the light shield!",
                "The wall breaka 3000!",
                "We love Dale representation!",
                "I cant believe mysterio would just tell everyone about peter parker's fursona!",
                "Can you please exit the vehicle, I'm trying to ascend!",
                "Consider joining the voice chat on discord! Also, use /hub to change modes!",
                "It's look who it is!",
                "Maybe it's the way you're dressed!",
                "I wish the Psybollium mines weren't so far!",
                "Did someone say Team Element?!",
                "Press F11 to toggle fullscreen!",
                "Press Alt + F4 to unlock the secret character!",
                "Made with love... and Monogame!",
                "It's the most average time of the year!",
                "Lucky you! There is a 1/{2} chance that you'll read this when this screen loads!",
                "Oof... Once more you've landed on the {3}/{2} chance of not hitting that rare message...",
                "Ballin, ballin! Ballinnnn~",
                "Thank you, MatPat!",
                "I love Mario! (I can't take it anymore)",
                "Look around you, imagine... dragons",
                "Where am I? Oh god, where am I? Where am I?",
                "As far as other things go, this is all I have to offer!",
                "Brett likes Pilk (Pepsi milk)!",
                "No Guards!",
                "I'm I and YOU'RE YOU!",
                "Body shape changes!",
                "Quantam physics is fun!",
                "Arbitrary is the kind of guy to read textbooks for fun!",
                "You can only hold me hostage from 8:33 AM to 11:28 PM on business days!",
                "My lips are livid because I haven't payed the price!",
                "You give cape, I give flight!",
                "You give fire, I give fight!",
                "The community was in remiss. They had not seen a parady game such as this",
                "Har har harhar har har harhar harhar!",
                "It's so sad that Ligma died of Steve Jobs!",
                "Gonk!",
                "Syowen is so close to 5 trillion subscribers!",
                "Also try Minecraft! And Terraria!",
                "You have to play OneShot. This isn't a suggestion. Play it.",
                "Look for the gummy bear album in stores on November 13th!",
                "so THAT'S-",
                "Killer fish from San Diego!",
                "Epic win!!!",
                "AAAAAAAAAAAAAOhhhohh hey, Taco Bell! Phew, gimme some that! Ahh, 64!",
                "Wyvern",
                "Subscribe to @thegamingpotatoepotato9657 on Youtube!",
                "Hatsune Miku Sandwich!",
                "What's up guys, AmericanVlogger27 here",
                "Step away from me!",
                "Super Mario in real loife!",
                "Howard the Alien approves this message I think!",
                "E",
                "More than {3} splash texts!",
                "My name is tulin\nTodat is noverber 23rd 2017",
                "I just won half a car!",
                "Completing the game gives you the thrilling unlock of credits!",
                "Try to speedrun this game, {0}!",
                "Books are neatly placed on the shelf!",
                "Books are placed neatly on the shelf!",
                "Placed books are neatly on the shelf!",
                "On the shelf are neatly placed books!",
                "Neatly placed books are on the shelf!",
                "On the shelf neatly are placed books!",
                "Placed books are on the shelf neatly!",
                "Books are on the shelf, neatly placed!",
                "This message is slightly more likely than the others for some reason!",
                "This message is slightly more likely than the others for some reason!",
                "AAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAA",
                "This text is hard to read if you play the game at the default resolution, but at 1080p it's fine!",
                "How did this happen :(",
                "Press 'S' on your keyboard while holding both shift keys to get a new splash text!",
                "What am I doing down here?\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\nWhat am I doing down here?",
                "There's a special secret for naming yourself after the main character!",
                "May or may not have a baseball minigame!",
                "Walkies!",
                "He's gonna show up!",
                "What even is an uncleared level, anyway?",
                "You are allowed to do that, but you will be banned!",
                "Ain't you Nathaniel B?",
                "It's about the Mets, baby, love the Mets!",
                "I always come back!",
                "Cool idea!",
                "Thank you Babe Ruth for inventing Tetris!",
                "Oh no, the instant ramen's been released!",
                "Welcome, Welcome new galaxy!",
                "Are you having fun yet?",
                "Haha, LOOMBA!",
                "OURPLE GUY?!",
                "That's just a theory!",
                "Lack of romance guarenteed!",
                "Gay people, tomorrow morning!",
                "I am a Zora. Have you seen a pretty Zora girl around here?",
                "Zoo wee mama!",
                "I've made jokes about being ace a lot\nThankfully I'm not actually an attorney!",
                "You are valid!",
                "{4}",
                "Try W for Wumbo!",
                "Oh hi Mark!",
                "I'm holding out for a hero!",
                "I think you dropped an apple, \"Bubby\"!",
                "What are you gonna do, stab me?",
                "They used to walk around during the day!",
                "Did you ever see Foxy the Pirate? Oh wait, Foxy... Oh yeah, Foxy!",
                "Gordon doesn't need to hear all this, he's a highly trained professional!",
                "Drink plenty of water!",
                "B-b-b-brian, s-s-stop!",
                "So sing along, it's such a silly song!",
                "Stand back Ashley, this resident is getting evil!",
                "What is this, some kind of BBG simulator?",
                "Im a tire!",
                "Woauoauoaaauaouoaaauooaah, story of Undertale!",
                "Nuh uh!",
                "I seem to have lost my phone!",
                "Moms are heavy!",
                "Goodnight gurl, I'll see you tomorrow!",
                "Was that the bite of '87?!",
                "5/9ths Already!",
                "I think we're gonna have to kill this guy, {0}",
                "It is good day to be not dead!",
                "I hope Benny the Octopus is okay!",
                "Hey, man, what's with the funky hairdo?",
                "Exotic butters!",
                "It'll make it feel, really authentic I think!",
                "Personality!?!?!?!?!",
                "Peter, what are you doing?"
            ];
        int splashid;
        float time;
        int[] splashCoords = new int[2] { 1500, 250 };
        float[] splashSize = new float[2];
        Random rng;
        public void LoadContent(ContentManager Content)
        {
            bg = Content.Load<Texture2D>("BBGSim/Cherry_Blossom_Tree");
            title = Content.Load<Texture2D>("BBGSim/titlepng");
            unlock = Content.Load<SoundEffect>("Audio/unlock");
            jumpscare = Content.Load<SoundEffect>("Audio/jumpscare2");
            select = Content.Load<SoundEffect>("BBGSim/bbgtext");
            wumbo = Content.Load<SoundEffect>("Audio/wumbo");
            icons = Content.Load<Texture2D>("BBGSim/select icons");
        }


        public void Initialize(Game1 game1)
        {
            Game1 = game1;
            titleMusic = Game1.Content.Load<Song>("Audio/title");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(titleMusic);
            Game1.ClearColor = Color.LightPink;
            names = ["Syowen", "Mocha", "Brett", "Alan", "Berry"];
            rng = new Random();
            splashid = rng.Next(0, splashtexts.Length);
            Game1.saveData.splashesSeen[splashid] = true;
            Game1.Save(Game1.saveData);
            bounceTextScale = 1;
            int index = 0;
            foreach (string text in splashtexts)
            {
                splashtexts[index] = string.Format(text, Game1.saveData.Username == null ? "player" : Game1.saveData.Username, names[rng.Next(0,4)], splashtexts.Length, splashtexts.Length - 1,MathF.Ceiling(jumpscareCountdown));
                index++;
            }
            splashSize[0] = Game1.PixelFont.MeasureString(splashtexts[splashid]).X;
            splashSize[1] = Game1.PixelFont.MeasureString(splashtexts[splashid]).Y;
            Game1.Window.Title = "BBGSim - " + splashtexts[splashid];
            Game1.Window.AllowAltF4 = false;
            splashesSeen = 0;
            foreach (bool value in Game1.saveData.splashesSeen)
            {
                if (value) splashesSeen++;
            }
        }


        public void Update(GameTime gameTime)
        {
            if (loadInt < 120)
            {
                jumpscareframes[loadInt] = Game1.Content.Load<Texture2D>("Jumpscares/Title/" + (loadInt + 1).ToString("0000"));
                loadInt++;
            }
            if (Game1.GetMouseDown() && selection() == 0)
            {
                Game1.Save(Game1.saveData);
                Game1.Window.AllowAltF4 = true;
                Game1.ChangeScene(1);
            }
            if (Game1.GetMouseDown() && selection() == 2)
            {
                if (Game1.saveData.Bbg < 3)
                {
                    Game1.saveData.Bbg++;
                    select.Play();
                    Game1.Save(Game1.saveData);
                }
                else if (Game1.saveData.Bbg < 4 && Game1.saveData.UnlockedSecret)
                {
                    Game1.saveData.Bbg++;
                    select.Play();
                    Game1.Save(Game1.saveData);
                }
            }
            if (Game1.GetMouseDown() && selection() == 1 && Game1.saveData.Bbg > 0)
            {
                Game1.saveData.Bbg--;
                select.Play();
                Game1.Save(Game1.saveData);
            }
            if (selection() == 0)
            {
                startLerp += Game1.delta * 3;
                startLerp = Math.Clamp(startLerp, 0, 1);
                startScale = MathHelper.Lerp(startScale, 1.5f, startLerp);
            }
            else
            {
                startLerp -= Game1.delta * 3;
                startLerp = Math.Clamp(startLerp, 0, 1);
                startScale = MathHelper.Lerp(1, startScale, startLerp);
            }

            if (Game1.GetKeyUp(Keys.S) && Game1.keyboardThisFrame.IsKeyDown(Keys.LeftShift) && Game1.keyboardThisFrame.IsKeyDown(Keys.RightShift))
            {
                splashid = rng.Next(0, splashtexts.Length);
                Game1.saveData.splashesSeen[splashid] = true;
                Game1.Save(Game1.saveData);
                splashSize[0] = Game1.PixelFont.MeasureString(splashtexts[splashid]).X;
                splashSize[1] = Game1.PixelFont.MeasureString(splashtexts[splashid]).Y;
                Game1.Window.Title = "BBGSim - " + splashtexts[splashid];
                splashesSeen = 0;
                foreach(bool value in Game1.saveData.splashesSeen)
                {
                    if (value) splashesSeen++;
                }
            }
            if (Game1.GetKeyUp(Keys.Up) && splashid < splashtexts.Length - 1 && Game1.isDebugMode)
            {
                splashid += 1;
                Game1.saveData.splashesSeen[splashid] = true;
                Game1.Save(Game1.saveData);
                splashSize[0] = Game1.PixelFont.MeasureString(splashtexts[splashid]).X;
                splashSize[1] = Game1.PixelFont.MeasureString(splashtexts[splashid]).Y;
                Game1.Window.Title = "BBGSim - " + splashtexts[splashid];
            }
            if (Game1.GetKeyUp(Keys.Down) && splashid > 0 && Game1.isDebugMode)
            {
                splashid -= 1;
                Game1.saveData.splashesSeen[splashid] = true;
                Game1.Save(Game1.saveData);
                splashSize[0] = Game1.PixelFont.MeasureString(splashtexts[splashid]).X;
                splashSize[1] = Game1.PixelFont.MeasureString(splashtexts[splashid]).Y;
                Game1.Window.Title = "BBGSim - " + splashtexts[splashid];
            }
            if (Game1.keyboardThisFrame.IsKeyDown(Keys.LeftAlt) || Game1.keyboardThisFrame.IsKeyDown(Keys.RightAlt))
            {
                if (Game1.GetKeyDown(Keys.F4) && Game1.saveData.UnlockedSecret == false)
                {
                    unlock.Play();
                    Game1.saveData.UnlockedSecret = true;
                    Game1.saveData.Bbg = 4;
                    Game1.Save(Game1.saveData);
                    unlockAlpha = 5;
                }
            }
            
            if (Game1.GetKeyUp(Keys.W) && splashid == 171)
            {
                wumbo.Play();
            }

            //Non inputs
            time += Game1.delta;
            bounceTextScale = (2 + (MathF.Sin(time * 4) / 2)) / (0.004f * splashSize[0]);
            titleOffsetY = MathF.Sin(time) * 10;
            unlockAlpha -= Game1.delta;
            unlockAlpha = Math.Clamp(unlockAlpha, 0, 5);
            rainbow = new Color(MathF.Sin(time * 3), -MathF.Sin((time * 3) + 1), MathF.Cos((time * 3) + 0.5f));
            if (splashid == 170)
            {
                jumpscareCountdown -= Game1.delta;
                if (jumpscareCountdown < 0) jumpscareCountdown = 0;
                splashtexts[170] = string.Format(splashtexts[170], Game1.saveData.Username == null ? "player" : Game1.saveData.Username, names[rng.Next(0, 4)], splashtexts.Length, splashtexts.Length - 1, jumpscareCountdown);
                splashSize[0] = Game1.PixelFont.MeasureString(MathF.Ceiling(jumpscareCountdown).ToString()).X;
                splashSize[1] = Game1.PixelFont.MeasureString(MathF.Ceiling(jumpscareCountdown).ToString()).Y;
                Game1.Window.Title = "BBGSim - " + MathF.Ceiling(jumpscareCountdown).ToString();
            }
            if (jumpscareCountdown <= 0) jumpscareIndex += 1;
            if (jumpscareIndex == jumpscareframes.Length - 1) jumpscare.Play();
            jumpscareIndex = Math.Clamp(jumpscareIndex, 0, jumpscareframes.Length - 1);

        }

        public void Draw(SpriteBatch _spriteBatch, GameTime gameTime, SpriteFont defaultfont, SpriteFont bbgfont, Texture2D debugbox)
        {
            _spriteBatch.Draw(bg,new Vector2(-100,0),new Rectangle(0,0, 3000,1500),Color.White, 0, new Vector2(0,0), 0.72f, SpriteEffects.None, 0);
            _spriteBatch.Draw(title,new Vector2(1275,150 + titleOffsetY),new Rectangle(0,0, 1536,218),Color.White, 0, new Vector2(title.Width/2, title.Height/2), 0.72f, SpriteEffects.None, 0);
            _spriteBatch.DrawString(Game1.PixelFont, splashid == 170 ? MathF.Ceiling(jumpscareCountdown).ToString() : splashtexts[splashid], new Vector2(splashCoords[0], splashCoords[1]), Color.SandyBrown, -titleOffsetY / 30,
                new Vector2((splashSize[0] / 2) - 1.8f, (splashSize[1] / 2) - 1.8f), bounceTextScale, splashid == 144 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            _spriteBatch.DrawString(Game1.PixelFont, splashid == 170 ? MathF.Ceiling(jumpscareCountdown).ToString() : splashtexts[splashid], new Vector2(splashCoords[0], splashCoords[1]), Color.Yellow, -titleOffsetY / 30,
                new Vector2(splashSize[0] / 2, splashSize[1] / 2), bounceTextScale, splashid == 144 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            _spriteBatch.DrawString(bbgfont, "Pick your bbg: " + names[Game1.saveData.Bbg], new Vector2(1100,800), Color.Black);
            _spriteBatch.DrawString(defaultfont, "<-    ->", new Vector2(1360,850), Color.Black);
            _spriteBatch.DrawString(Game1.PixelFont, "Unique Splashes seen: " + splashesSeen, new Vector2(12,1052), splashesSeen == splashtexts.Length ? Color.DarkCyan : Color.SaddleBrown);
            _spriteBatch.DrawString(Game1.PixelFont, "Unique Splashes seen: " + splashesSeen, new Vector2(10,1050), splashesSeen == splashtexts.Length ? Color.Cyan : Color.Yellow);
            if (Game1.saveData.UnlockedSecret && Game1.saveData.SixUnlocked && Game1.saveData.CustomUnlocked && splashesSeen == splashtexts.Length)
            {
                _spriteBatch.DrawString(Game1.PixelFont, "Save file at 100%!! Thank you for playing!", new Vector2(12, 1027), new Color(rainbow.R / 2, rainbow.G / 2, rainbow.B / 2));
                _spriteBatch.DrawString(Game1.PixelFont, "Save file at 100%!! Thank you for playing!", new Vector2(10, 1025), rainbow);
            }
            _spriteBatch.Draw(icons, new Vector2(1325, 600), iconSpaces[Game1.saveData.Bbg], Color.White);
            _spriteBatch.DrawString(bbgfont, "Start", new Vector2(1460,925), Color.Black, 0, new Vector2(bbgfont.MeasureString("Start").X/2, bbgfont.MeasureString("Start").Y / 2),
                startScale, SpriteEffects.None, 0);
            if (unlockAlpha > 0)
            _spriteBatch.DrawString(Game1.PixelFont, "SECRET CHARACTER UNLOCKED!!!", new Vector2(550, 400), rainbow * Math.Clamp(unlockAlpha, 0, 1), 0, new Vector2(0,0), 3f, SpriteEffects.None, 0);

            if (Game1.isDebugMode)
            {
                _spriteBatch.Draw(debugbox, new Rectangle(1400, 900,125,50), new Color(139, 51, 255, 20));
                _spriteBatch.Draw(debugbox, new Rectangle(1360, 850,40,50), new Color(139, 51, 255, 20));
                _spriteBatch.Draw(debugbox, new Rectangle(1460, 850,40,50), new Color(139, 51, 255, 20));
            }

            if (jumpscareCountdown <= 0)
            {
                _spriteBatch.Draw(jumpscareframes[jumpscareIndex], new Vector2(0, 0), new Rectangle(0,0,1280,720), Color.White,0,new Vector2(0,0),1.5f,SpriteEffects.None,0);
            }
        }
    }
}
