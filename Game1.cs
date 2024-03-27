using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text.Json;
using System.Threading;

namespace Cheesenaf
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public int CurrentScene = 0;
        //0: Title (Dating Sim), 1: Dating Scene, 2: Title (CheeseNAF), 3: Office

        private Office office;
        private MainTitle mainTitle;
        private CheesenafTitle cheesenafTitle;
        private BBGSim BBGSim;

        public Color ClearColor = Color.CornflowerBlue;
        public Texture2D debugbox;

        public KeyboardState keyboardThisFrame;
        public KeyboardState keyboardLastFrame;

        public MouseState mouseThisFrame;
        public MouseState mouseLastFrame;

        public static SaveData saveData;
        public const string SAVEPATH = "data.json";

        public bool passJumpscare;
        public bool passNightSixWin;
        public bool passStartNight;

        public int FreddyLevel;
        public int BonnieLevel;
        public int ChicaLevel;
        public int FoxyLevel;

        public float delta;
        public bool isDebugMode; //This will say in the final build, but it will not be toggleable
        public bool showFPS;

        public int ScreenWidth;
        public int ScreenHeight;
        public float ScaleX;
        public float ScaleY;

        public MouseState mouseState;
        public int MouseX;
        public int MouseY;

        public bool sneakyLoad;

        public bool canEscape = true;

        public SpriteFont defaultfont;
        public SpriteFont PixelFont;
        public SpriteFont BBGFont;


        // Initialization
        RenderTarget2D _nativeRenderTarget;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.IsFullScreen = true;
            _graphics.HardwareModeSwitch = false;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            ScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            ScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnResize;
            Window.AllowAltF4 = true;
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            if (CurrentScene == 0)
                mainTitle.LoadContent(Content);
            if (CurrentScene == 2)
                cheesenafTitle.LoadContent(Content);
        }


        public void OnResize(Object sender, EventArgs e)
        {
            ScreenWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;
            ScreenHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;
            ScaleX = 1920f / ScreenWidth;
            ScaleY = 1080f / ScreenHeight;
        }
        public void OnResize()
        {
            ScreenWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;
            ScreenHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;
            ScaleX = 1920f / ScreenWidth;
            ScaleY = 1080f / ScreenHeight;
        }

        protected override void Initialize()
        {
            OnResize();
            // TODO: Add your initialization logic here
            Trace.WriteLine(ScreenWidth + ", " + ScreenHeight);
            Trace.WriteLine(ScaleX + ", " + ScaleY);
            defaultfont = Content.Load<SpriteFont>("File");
            PixelFont = Content.Load<SpriteFont>("Mojangles");
            BBGFont = Content.Load<SpriteFont>("Fennario");
            debugbox = new Texture2D(GraphicsDevice, 1, 1);
            debugbox.SetData(new[] { Color.White });
            office = new Office();
            BBGSim = new BBGSim();
            mainTitle = new MainTitle();
            cheesenafTitle = new CheesenafTitle();
            _nativeRenderTarget = new RenderTarget2D(GraphicsDevice, 1920, 1080);
            //Save Data
            if (File.Exists(SAVEPATH))
            {
                saveData = LoadSave();
                saveData.Bbg = Math.Clamp(saveData.Bbg, 0, 4);
                saveData.Night = Math.Clamp(saveData.Night, 1, 5);
                if (saveData.AltTitle)
                {
                    CurrentScene = 2;
                }
                if (!saveData.FullScreen) ToggleFullscreen();
            }
            else
            {
                ResetData();
            }
            //SceneLoad
            if (CurrentScene == 0)
                mainTitle.Initialize(this);
            if (CurrentScene == 1)
                BBGSim.Initialize(this);
            if (CurrentScene == 2)
                cheesenafTitle.Initialize(this);
            if (CurrentScene == 3)
                office.Initialize(this);

            base.Initialize();

        }


        protected override void Update(GameTime gameTime)
        {
            
            delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            mouseState = Mouse.GetState();
            MouseX = (int)MathF.Round(mouseState.X * ScaleX);
            MouseY = (int)MathF.Round(mouseState.Y * ScaleY);
            
            if (this.IsActive || mainTitle.loading || (CurrentScene == 3 && office.loaded == false))
            {
                if (GetKeyDown(Keys.Escape) && CurrentScene != 3 && CurrentScene != 1 && canEscape)
                    Exit();
                if (GetKeyDown(Keys.Tab) && isDebugMode)
                {
                    showFPS = !showFPS;
                }
                if (GetKeyDown(Keys.F11))
                {
                    ToggleFullscreen();
                }
                //if (GetKeyDown(Keys.OemTilde)) //DELETE IN FINAL BUILD
                //{
                //    isDebugMode = !isDebugMode;
                //}

                keyboardLastFrame = keyboardThisFrame;
                keyboardThisFrame = Keyboard.GetState();

                mouseLastFrame = mouseThisFrame;
                mouseThisFrame = Mouse.GetState();

                if (CurrentScene == 0)
                    mainTitle.Update(gameTime);
                if (CurrentScene == 1)
                    BBGSim.Update(gameTime);
                if (CurrentScene == 2)
                    cheesenafTitle.Update(gameTime);
                if (CurrentScene == 3)
                    if (office.loaded == false)
                    {
                        office.LoadContent(Content);
                    }
                    else
                        office.Update(gameTime);
                if (isDebugMode)
                {
                    if (GetKeyDown(Keys.D1)) ChangeScene(1);
                    if (GetKeyDown(Keys.D2)) ChangeScene(2);
                    if (GetKeyDown(Keys.D3)) ChangeScene(3);
                    if (GetKeyDown(Keys.D4)) ChangeScene(4);
                    if (GetKeyDown(Keys.D5)) ChangeScene(5);
                    if (GetKeyDown(Keys.D6)) ChangeScene(6);
                    if (GetKeyDown(Keys.D7)) ChangeScene(7);
                    if (GetKeyDown(Keys.D8)) ChangeScene(8);
                    if (GetKeyDown(Keys.D9)) ChangeScene(9);
                    if (GetKeyDown(Keys.D0)) ChangeScene(0);
                }

                base.Update(gameTime);
            }
        }

        public void ToggleFullscreen()
        {
            _graphics.IsFullScreen = !_graphics.IsFullScreen;
            if (!_graphics.IsFullScreen)
            {
                _graphics.PreferredBackBufferWidth = 1280;
                _graphics.PreferredBackBufferHeight = 720;
            }
            _graphics.HardwareModeSwitch = false;
            _graphics.ApplyChanges();
            saveData.FullScreen = _graphics.IsFullScreen;
            Save(saveData);
            OnResize();
        }

        protected override void Draw(GameTime gameTime)
        {
            // Draw call
            GraphicsDevice.SetRenderTarget(_nativeRenderTarget);
            GraphicsDevice.Clear(ClearColor);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            if (CurrentScene == 0)
            mainTitle.Draw(_spriteBatch, gameTime, defaultfont, BBGFont, debugbox);
            if (CurrentScene == 1)
            BBGSim.Draw(_spriteBatch, gameTime, BBGFont, debugbox);
            if (CurrentScene == 2)
            cheesenafTitle.Draw(_spriteBatch, gameTime, defaultfont, debugbox);
            if (CurrentScene == 3)
            {
                if (office.loaded == false)
                {
                    office.LoadDraw(_spriteBatch, gameTime, defaultfont, PixelFont, debugbox);
                }
                else
                    office.Draw(_spriteBatch, gameTime, defaultfont, PixelFont, debugbox);
            }
            //FPS
            if (showFPS) _spriteBatch.DrawString(PixelFont, Math.Round(1 / gameTime.ElapsedGameTime.TotalSeconds).ToString() + " FPS", new Vector2(0, 0), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);


            GraphicsDevice.SetRenderTarget(null);
            _spriteBatch.Begin(samplerState: SamplerState.LinearWrap);
            _spriteBatch.Draw(_nativeRenderTarget, new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.White);
            _spriteBatch.End();
        }

        public void ChangeScene(int SceneID)
        {
            CurrentScene = SceneID;
            Content.Unload();
            defaultfont = Content.Load<SpriteFont>("File");
            PixelFont = Content.Load<SpriteFont>("Mojangles");
            BBGFont = Content.Load<SpriteFont>("Fennario");
            switch (SceneID)
            {
                default:
                    CurrentScene = 0;
                    mainTitle = new MainTitle();
                    mainTitle.Initialize(this);
                    mainTitle.LoadContent(Content);
                    break;

                case 0:
                    mainTitle = new MainTitle();
                    mainTitle.Initialize(this);
                    mainTitle.LoadContent(Content);
                    break;
                case 1:
                    BBGSim = new BBGSim();
                    BBGSim.Initialize(this);
                    BBGSim.LoadContent(Content);
                    break;
                case 2:
                    cheesenafTitle = new CheesenafTitle();
                    if (passJumpscare) cheesenafTitle.fromJumpscare = true;
                    if (passStartNight) cheesenafTitle.transition = true;
                    if (passNightSixWin) cheesenafTitle.fromNightSixWin = true;
                    passJumpscare = false;
                    passStartNight = false;
                    passNightSixWin = false;
                    cheesenafTitle.Initialize(this);
                    cheesenafTitle.LoadContent(Content);
                    break;

                case 3:
                    office = new Office();
                    office.Initialize(this);
                    office.LoadContent(Content);
                    break;
            }
        }

        public bool GetKeyUp(Keys key)
        {
            if (keyboardLastFrame.GetPressedKeys().Contains(key) == true && keyboardThisFrame.GetPressedKeys().Contains(key) == false) return true;
            else return false;
        }
        public bool GetKeyDown(Keys key)
        {
            if (keyboardLastFrame.GetPressedKeys().Contains(key) == false && keyboardThisFrame.GetPressedKeys().Contains(key) == true) return true;
            else return false;
        }
        public bool GetMouseUp()
        {
            if (mouseLastFrame.LeftButton == ButtonState.Pressed && mouseThisFrame.LeftButton == ButtonState.Released) return true;
            else return false;
        }
        public bool GetMouseDown()
        {
            if (mouseLastFrame.LeftButton == ButtonState.Released && mouseThisFrame.LeftButton == ButtonState.Pressed) return true;
            else return false;
        }

        public SaveData LoadSave()
        {
            var fileContents = File.ReadAllText(SAVEPATH);
            Trace.WriteLine("Loaded: " + fileContents);
            return JsonSerializer.Deserialize<SaveData>(fileContents);
        }

        public void Save(SaveData data)
        {
            string serializedText = JsonSerializer.Serialize<SaveData>(data);
            File.WriteAllText(SAVEPATH, serializedText);
            Trace.WriteLine("Saved: " + serializedText);
        }

        public void ResetData()
        {
            saveData = new SaveData()
            {
                AltTitle = false,
                Night = 1,
                CustomUnlocked = false,
                SixUnlocked = false,
                Bbg = 0,
                Username = null,
                FullScreen = true,
                UnlockedSecret = false,
                splashesSeen = new bool[200],
            };
            Save(saveData);
            if (CurrentScene != 0) ChangeScene(0);
        }

    }
}