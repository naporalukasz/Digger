using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Xml.Serialization;
using System.IO;


namespace Digger
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Logic oLogic;
        Source oSource;
        HighScores data;
        int counter = 0;
        string PlayerName;
        int screenWidth = 816, screenHeight = 672;

        Dictionary<Buttons, cButton> Button= new Dictionary<Buttons,cButton>();
        string HighScoresFilename = "highscores.dat";

        /// <summary>
        /// Okresla obecny stan gry.
        /// </summary>
        public static GameState CurrentGameState = GameState.Login;

        /// <summary>
        /// Tworzy nowy biekt typu Game.
        /// </summary>
        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            initFile();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Screen stuff
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ApplyChanges();
            //oLogic = new Logic(oSource, graphics.GraphicsDevice);
            oSource = new Source(Content.Load<Texture2D>("Object\\kopark2"), Content.Load<Texture2D>("Object\\Robak"), Content.Load<Texture2D>("Object\\RobakS"), Content.Load<Texture2D>("Object\\Pole"),
                Content.Load<Texture2D>("State\\TloScore"), Content.Load<Texture2D>("Object\\Rubin"), Content.Load<Texture2D>("Object\\Diament"),
                Content.Load<SpriteFont>("InfBoxFont"), Content.Load<Texture2D>("Object\\pistolet"), Content.Load<Texture2D>("Object\\dzida"),
                Content.Load<Texture2D>("Object\\pocisk"), Content.Load<Texture2D>("Object\\laser"), Content.Load<Texture2D>("Object\\Glaz"));
            oLogic = new Logic( graphics.GraphicsDevice);
            Initbuttons();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            Content.Unload();
        }

        bool raz = false;
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            MouseState mouse = Mouse.GetState();
            
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    if (Button[Buttons.btnPlay].isClicked)  CurrentGameState = GameState.Difficulty; 
                    if (Button[Buttons.btnExit].isClicked) this.Exit();
                    if (Button[Buttons.btnHightScores].isClicked) CurrentGameState = GameState.HighScore;
                    if (Button[Buttons.btnHelp].isClicked) CurrentGameState = GameState.Help;
                    if (Button[Buttons.btnLoadGame].isClicked)  CurrentGameState = GameState.Load;
                    Button[Buttons.btnLoadGame].Update(mouse);
                    Button[Buttons.btnPlay].Update(mouse);
                    Button[Buttons.btnHelp].Update(mouse);
                    Button[Buttons.btnHightScores].Update(mouse);
                    Button[Buttons.btnExit].Update(mouse);                  
                    break;
                case GameState.Playing:
                    oLogic.Update(gameTime);
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        LoadGame.SaveGame(new SaveGame { currpoint = oLogic.StartPoints, currlevel = Logic.numLevel, currLife = 3 - counter });
                        CurrentGameState = GameState.MainMenu;
                       
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.P)) CurrentGameState = GameState.Pause;
                    break;
                case GameState.Difficulty:
                    if (Button[Buttons.btnBack].isClicked) CurrentGameState = GameState.MainMenu;
                    if (Button[Buttons.btnEasy].isClicked)
                    {

                        oLogic.startLevel();
                        CurrentGameState = GameState.Playing;

                    }
                    if (Button[Buttons.btnMedium].isClicked)
                    {
                        Logic.numLevel += 5;
                        oLogic.startLevel();
                        CurrentGameState = GameState.Playing;
                    }
                    if (Button[Buttons.btnHard].isClicked)
                    {
                        Logic.numLevel += 10;
                        oLogic.startLevel();
                        CurrentGameState = GameState.Playing;
                    }
                    Button[Buttons.btnHard].Update(mouse);
                    Button[Buttons.btnMedium].Update(mouse);
                    Button[Buttons.btnEasy].Update(mouse);
                    Button[Buttons.btnBack].Update(mouse);
                    break;
                case GameState.HighScore:
                    if (Button[Buttons.btnBack].isClicked) CurrentGameState = GameState.MainMenu;
                    Button[Buttons.btnBack].Update(mouse);
                    break;
                case GameState.Help:
                    if (Button[Buttons.btnBack].isClicked) CurrentGameState = GameState.MainMenu;
                    Button[Buttons.btnBack].Update(mouse);
                    
                    break;
                case GameState.Pause:
                    if (Button[Buttons.btnBackPause].isClicked) CurrentGameState = GameState.Playing;
                    if (Button[Buttons.btnExitPause].isClicked)
                    {
                        LoadGame.SaveGame(new SaveGame { currpoint = oLogic.StartPoints, currlevel = Logic.numLevel, currLife = 3 - counter });
                        CurrentGameState = GameState.MainMenu;
                    }
                    Button[Buttons.btnExitPause].Update(mouse);
                    Button[Buttons.btnBackPause].Update(mouse);
                    break;
                case GameState.Load:
                    var data2 = LoadGame.LoadGames();                     
                        Logic.numLevel = data2.currlevel;
                       oLogic.startLevel(data2.currpoint,3-data2.currLife);
                        CurrentGameState = GameState.Playing;
                   CurrentGameState = GameState.Playing; 
                   break;
                case GameState.WinGame:
                   if (!raz)
                   {
                       data = data.SaveHighScore(new Score { name = PlayerName, score = oLogic.Points });
                       raz = true;
                   }
                   if (Button[Buttons.btnPlayWin].isClicked) CurrentGameState = GameState.MainMenu; 
                   if (Button[Buttons.btnExitWin].isClicked) this.Exit();
                   Button[Buttons.btnPlayWin].Update(mouse);
                   Button[Buttons.btnExitWin].Update(mouse);

                   break;
                case GameState.Login:
                    loginmenu();
                    CurrentGameState = GameState.MainMenu;
                   break;
                case GameState.Dead:
                   if (Button[Buttons.btnMainmenu].isClicked)
                   {
                       CurrentGameState = GameState.MainMenu;
                   }
                    if (Button[Buttons.btnRepeat].isClicked)
                    {
                        var obl = Logic.numLevel;
                        Logic.numLevel = obl;
                        var obpoint = oLogic.StartPoints;
                        counter++;
                        oLogic.startLevel(obpoint,counter);
                        CurrentGameState = GameState.Playing;
                    }

                    Button[Buttons.btnMainmenu].Update(mouse);
                    Button[Buttons.btnRepeat].Update(mouse);
                    break;
                case GameState.Loss:
                    if (!raz)
                    {
                        data = data.SaveHighScore(new Score { name = PlayerName, score = oLogic.Points });
                        raz = true;
                    }
                    if (Button[Buttons.btnMainmenu].isClicked)
                    {
                        CurrentGameState = GameState.MainMenu;
                    }
                    if (Button[Buttons.btnExitLose].isClicked) this.Exit();
                    Button[Buttons.btnMainmenu].Update(mouse);
                    Button[Buttons.btnExitLose].Update(mouse);
                    break;
             }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    spriteBatch.Draw(Content.Load<Texture2D>("State\\Tlo"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    Button[Buttons.btnLoadGame].Draw(spriteBatch);
                    Button[Buttons.btnPlay].Draw(spriteBatch);
                    Button[Buttons.btnHelp].Draw(spriteBatch);
                    Button[Buttons.btnHightScores].Draw(spriteBatch);
                    Button[Buttons.btnExit].Draw(spriteBatch);
                    break;
                case GameState.Playing:
                    Initbuttons();
                    spriteBatch.Draw(Content.Load<Texture2D>("State\\Tlogame"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    oLogic.Draw(spriteBatch);
                    break;
                case GameState.Difficulty:
                    spriteBatch.Draw(Content.Load<Texture2D>("State\\Tlo"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    Button[Buttons.btnHard].Draw(spriteBatch);
                    Button[Buttons.btnMedium].Draw(spriteBatch);
                    Button[Buttons.btnEasy].Draw(spriteBatch);
                    Button[Buttons.btnBack].Draw(spriteBatch);
                    break;
                case GameState.HighScore:
                    spriteBatch.Draw(Content.Load<Texture2D>("State\\Tlo"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    data.Draw(spriteBatch);
                    Button[Buttons.btnBack].Draw(spriteBatch);
                    break;
                case GameState.Help:
                    spriteBatch.Draw(Content.Load<Texture2D>("State\\Help"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    Button[Buttons.btnBack].Draw(spriteBatch);
                    break;
                case GameState.Pause:
                    spriteBatch.Draw(Content.Load<Texture2D>("State\\Pause"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    Button[Buttons.btnExitPause].Draw(spriteBatch);
                    Button[Buttons.btnBackPause].Draw(spriteBatch);
                    break;
                case GameState.WinGame:
                    spriteBatch.Draw(Content.Load<Texture2D>("State\\Win"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    spriteBatch.DrawString(Source.infBoxFont, "Congratulations:  " + PlayerName, new Vector2(300, 350), Color.Black);
                    spriteBatch.DrawString(Source.infBoxFont, "Your score:  " + oLogic.Points.ToString(), new Vector2(300,400), Color.Black);
                    Button[Buttons.btnPlayWin].Draw(spriteBatch);
                    Button[Buttons.btnExitWin].Draw(spriteBatch);
                    break;
                case GameState.Dead:
                    spriteBatch.Draw(Content.Load<Texture2D>("State\\Tlo"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    Button[Buttons.btnMainmenu].Draw(spriteBatch);
                    Button[Buttons.btnRepeat].Draw(spriteBatch);
                    break;
                case GameState.Loss:
                     spriteBatch.Draw(Content.Load<Texture2D>("State\\Loss"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    spriteBatch.DrawString(Source.infBoxFont, "Your score:  " + oLogic.Points.ToString(), new Vector2(300,300), Color.Black);
                    Button[Buttons.btnMainmenu].Draw(spriteBatch);
                    Button[Buttons.btnExitLose].Draw(spriteBatch);
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Inicjalizuje przuciski wystepujace w menu.
        /// </summary>
        private void Initbuttons()
        {
            Button = new Dictionary<Buttons, cButton>();  
            Button.Add(Buttons.btnPlay, new cButton(Content.Load<Texture2D>("buttons\\NewGame"), graphics.GraphicsDevice));
            Button.Add(Buttons.btnLoadGame, new cButton(Content.Load<Texture2D>("buttons\\Load"), graphics.GraphicsDevice));
            Button.Add(Buttons.btnHelp, new cButton(Content.Load<Texture2D>("buttons\\Help"), graphics.GraphicsDevice));
            Button.Add(Buttons.btnHightScores, new cButton(Content.Load<Texture2D>("buttons\\HighScores"), graphics.GraphicsDevice));
            Button.Add(Buttons.btnExit, new cButton(Content.Load<Texture2D>("buttons\\Exit"), graphics.GraphicsDevice));

            Button.Add(Buttons.btnBack, new cButton(Content.Load<Texture2D>("buttons\\Back"), graphics.GraphicsDevice));

            Button.Add(Buttons.btnEasy, new cButton(Content.Load<Texture2D>("buttons\\Easy"), graphics.GraphicsDevice));
            Button.Add(Buttons.btnMedium,new cButton(Content.Load<Texture2D>("buttons\\Medium"), graphics.GraphicsDevice));
            Button.Add(Buttons.btnHard, new cButton(Content.Load<Texture2D>("buttons\\Hard"), graphics.GraphicsDevice));

            Button.Add(Buttons.btnPlayWin,new cButton(Content.Load<Texture2D>("buttons\\NewGame"),graphics.GraphicsDevice));
            Button.Add(Buttons.btnExitWin, new cButton(Content.Load<Texture2D>("buttons\\Exit"), graphics.GraphicsDevice));

            Button.Add(Buttons.btnMainmenu, new cButton(Content.Load<Texture2D>("buttons\\mainmenu"), graphics.GraphicsDevice));
            Button.Add(Buttons.btnRepeat, new cButton(Content.Load<Texture2D>("buttons\\Repeat"), graphics.GraphicsDevice));

            Button.Add(Buttons.btnBackPause, new cButton(Content.Load<Texture2D>("buttons\\Unpause"), graphics.GraphicsDevice));
            Button.Add(Buttons.btnExitPause, new cButton(Content.Load<Texture2D>("buttons\\ExitGame"), graphics.GraphicsDevice));

            Button.Add(Buttons.btnExitLose, new cButton(Content.Load<Texture2D>("buttons\\ExitGame"), graphics.GraphicsDevice));
            
            Button[Buttons.btnPlay].setPosition(new Vector2(300, 200));
            Button[Buttons.btnLoadGame].setPosition(new Vector2(300, 250));
            Button[Buttons.btnHelp].setPosition(new Vector2(300, 300));
            Button[Buttons.btnHightScores].setPosition(new Vector2(300, 350));
            Button[Buttons.btnExit].setPosition(new Vector2(300, 400));

            Button[Buttons.btnEasy].setPosition(new Vector2(300, 250));
            Button[Buttons.btnMedium].setPosition(new Vector2(300, 300));
            Button[Buttons.btnHard].setPosition(new Vector2(300, 350));

            Button[Buttons.btnBack].setPosition(new Vector2(300, 600));

            Button[Buttons.btnPlayWin].setPosition(new Vector2(150, 600));
            Button[Buttons.btnExitWin].setPosition(new Vector2(450, 600));

            Button[Buttons.btnMainmenu].setPosition(new Vector2(150, 400));
            Button[Buttons.btnRepeat].setPosition(new Vector2(450, 400));

            Button[Buttons.btnExitPause].setPosition(new Vector2(150, 400));
            Button[Buttons.btnBackPause].setPosition(new Vector2(450, 400));
            Button[Buttons.btnExitLose].setPosition(new Vector2(450, 400));
        }

        /// <summary>
        /// Wczytuje zappisane wyniki.
        /// </summary>
        private void loadScore()
        {
            data = HighScores.LoadHighScores();
        }

        /// <summary>
        /// Wywoluje powitalne okno, gdzie uzytkownik moze podac swoje imie.
        /// </summary>
        private void loginmenu()
        {
            System.Windows.Forms.Form prompt = new System.Windows.Forms.Form();
            prompt.Width = 300;
            prompt.Height = 150;
            prompt.Text = "Welcome!!!";
            prompt.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            System.Windows.Forms.Label textLabel = new System.Windows.Forms.Label() { Left = 50, Top = 20, Text = "Enter your name" };
            System.Windows.Forms.TextBox textBox = new System.Windows.Forms.TextBox() { Left = 50, Top = 50, Width = 200, Text = "Player" };
            System.Windows.Forms.Button confirmation = new System.Windows.Forms.Button() { Text = "Ok", Left = 150, Width = 100, Top = 70 };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.ShowDialog();
            PlayerName = textBox.Text;
        }

        /// <summary>
        /// Inicjalizuje pliki odpowiedzialne za zapisywanie gry i najlepszych wynikow.
        /// </summary>
        private void initFile()
        {
            string fullpath = "highscores.dat";
            if (!File.Exists(fullpath))
            {
                //If the file doesn't exist, make a fake one...
                // Create the data to save
                data = new HighScores();
                data.Players.Add(new Score { name = "neil", score = 5000 });
                data.Players.Add(new Score { name = "shawn", score = 4500 });
                data.Players.Add(new Score { name = "mark", score = 4000 });
                data.Players.Add(new Score { name = "cindy", score = 3500 });
                data.Players.Add(new Score { name = "sam", score = 3000 });
                data.Players.Add(new Score { name = "neil", score = 2500 });
                data.Players.Add(new Score { name = "shawn", score = 2000 });
                data.Players.Add(new Score { name = "mark", score = 1500 });
                data.Players.Add(new Score { name = "cindy", score = 1000 });
                data.Players.Add(new Score { name = "sam", score = 500 });
                HighScores.SaveHighScores(data);
            }
            loadScore();
            fullpath = "mysave.dat";
            if (!File.Exists(fullpath))
            {
                LoadGame.SaveGame(new SaveGame { currlevel = 1, currLife = 3, currpoint = 0 });
            }
        }
    }
}
