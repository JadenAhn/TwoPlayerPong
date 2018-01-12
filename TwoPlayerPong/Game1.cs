/* Game1.cs
* Assignment 4
* Revision History
*   Ji Hong Ahn, 2017.12.07: Created
*   Ji Hong Ahn, 2017.12.13: Added header comments
*                            Added documentation comments for constructors
*   Ji Hong Ahn, 2018.01.02: Added GetRandomDirectionSpeed()
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace TwoPlayerPong
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Sprite title;
        Sprite startMessage;
        Sprite backGround;
        Ball ball;
        Paddle playerOnePaddle;
        Paddle playerTwoPaddle;
        Texture2D ballTexture;
        Vector2 stage;
        Vector2 initialBallPosition;
        Vector2 playerOnePosition;
        Vector2 playerTwoPosition;
        Song songGameTitle;
        Song songGameStart;
        Song SongGameOver;
        GamePhase gamePhase;
        Random rand;
        Score score;
        WinMessage winMessage;
        Font playerOneName;
        Font playerTwoName;
        const int SPEED = 5;
        const int WINNING_POINT = 2;
        public const string PLAYER_ONE_NAME = "PLAYER 1";
        public const string PLAYER_TWO_NAME = "PLAYER 2";
		public const int PLAYER_1 = 1;
		public const int PLAYER_2 = 2;

		/// <summary>
		/// This enum shows all the game states
		/// </summary>
		public enum GamePhase
        {
            Title,
            Playing,
            End
        }

        /// <summary>
        /// A constructor to assign necessary parameters to the Game1 class
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            rand = new Random();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            bool isVisible = true;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gamePhase = GamePhase.Title;
            stage = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            //Load background image
            Texture2D backgroundTexture = this.Content.Load<Texture2D>("Images/Background");
            Vector2 backgroundPosition = new Vector2(0, 0);
            backGround = new Sprite(this, spriteBatch, backgroundTexture, backgroundPosition);

            //Load start message
            Texture2D startMessageTexture = this.Content.Load<Texture2D>("Images/StartMessage");
            Vector2 startMessagePosition = new Vector2(getCenter(startMessageTexture).X, getCenter(startMessageTexture).Y + 50);
            startMessage = new Sprite(this, spriteBatch, startMessageTexture, startMessagePosition, isVisible, 0.02f);

            //Load title
            Texture2D titleTexture = this.Content.Load<Texture2D>("Images/Title");
            Vector2 titlePosition = getCenter(titleTexture);
            title = new Sprite(this, spriteBatch, titleTexture, titlePosition);

            //Load BGM
            songGameTitle = Content.Load<Song>("Musics/SongGameTitle");
            songGameStart = Content.Load<Song>("Musics/SongGameStart");
            SongGameOver = Content.Load<Song>("Musics/SongGameOver");

            //Load Score
            SpriteFont scoreFont = Content.Load<SpriteFont>("Fonts/ScoreFont");
            score = new Score(this, spriteBatch, scoreFont, stage);

            //Load win message
            SpriteFont messageFont = Content.Load<SpriteFont>("Fonts/NameFont");
            winMessage = new WinMessage(this, spriteBatch, messageFont, stage);

            //Load player name
            SpriteFont playerName = Content.Load<SpriteFont>("Fonts/NameFont");
            Vector2 playerOneNameLength = playerName.MeasureString(PLAYER_ONE_NAME);
            Vector2 playerOneNamePosition = new Vector2((stage.X / 2) - playerOneNameLength.X - 20, stage.Y - 100);
            Vector2 playerTwoNamePosition = new Vector2((stage.X / 2) + 20, stage.Y - 100);
            playerOneName = new Font(this, spriteBatch, playerName, PLAYER_ONE_NAME, playerOneNamePosition, new Color(255, 204, 51), false);
            playerTwoName = new Font(this, spriteBatch, playerName, PLAYER_TWO_NAME, playerTwoNamePosition, new Color(255, 204, 51), false);

            //Load Ball
            ballTexture = this.Content.Load<Texture2D>("Images/Ball");
            initialBallPosition = new Vector2(getCenter(ballTexture).X, getCenter(ballTexture).Y);
            Vector2 ballSpeed = new Vector2(0, 0);
            SoundEffect hitSound = this.Content.Load<SoundEffect>("Musics/SFBounce");
            SoundEffect hitWallSound = this.Content.Load<SoundEffect>("Musics/SFBounceWall");
            SoundEffect missSound = this.Content.Load<SoundEffect>("Musics/SFScore");
            ball = new Ball(this, spriteBatch, ballTexture, initialBallPosition, ballSpeed, stage, hitSound, hitWallSound, missSound, score, winMessage);
            ball.Enabled = false;

            //Load 2 paddles
            Texture2D paddleTexture = this.Content.Load<Texture2D>("Images/Paddle");
            playerOnePosition = new Vector2(10, getCenter(paddleTexture).Y);
            playerTwoPosition = new Vector2(graphics.PreferredBackBufferWidth - paddleTexture.Width - 10, getCenter(paddleTexture).Y);
            Vector2 paddleSpeed = new Vector2(0, SPEED);
            playerOnePaddle = new Paddle(this, spriteBatch, paddleTexture, playerOnePosition, paddleSpeed, stage, Keys.A, Keys.Z);
            playerTwoPaddle = new Paddle(this, spriteBatch, paddleTexture, playerTwoPosition, paddleSpeed, stage, Keys.Up, Keys.Down);

            //Load collision detection
            CollisionDetection collisionOne = new CollisionDetection(this, ball, playerOnePaddle, hitSound);
            CollisionDetection collisionTwo = new CollisionDetection(this, ball, playerTwoPaddle, hitSound);
			

            //Add all the components
            this.Components.Add(backGround);
            this.Components.Add(title);
            this.Components.Add(startMessage);
            this.Components.Add(ball);
            this.Components.Add(playerOnePaddle);
            this.Components.Add(playerTwoPaddle);
            this.Components.Add(score);
            this.Components.Add(winMessage);
            this.Components.Add(playerOneName);
            this.Components.Add(playerTwoName);
            this.Components.Add(collisionOne);
            this.Components.Add(collisionTwo);
            PlayMusic();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            
            if (gamePhase == GamePhase.Title)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    title.isVisible = false;
                    startMessage.isVisible = false;
                    score.isVisible = true;
                    winMessage.isVisible = false;
                    ball.isVisible = true;
                    playerOneName.isVisible = true;
                    playerTwoName.isVisible = true;
                    gamePhase = GamePhase.Playing;
                    PlayMusic();
                }
            }
            else if (gamePhase == GamePhase.Playing)
            {				
				if (score.playerOneScore == WINNING_POINT || score.playerTwoScore == WINNING_POINT)
                {
                    gamePhase = GamePhase.End;
                    ball.isVisible = false;
                    winMessage.isVisible = true;
                    startMessage.isVisible = true;
                    PlayMusic();
                }
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    score.playerOneScore = 0;
                    score.playerTwoScore = 0;
                    ball.position = initialBallPosition;
                    playerOnePaddle.position = playerOnePosition;
                    playerTwoPaddle.position = playerTwoPosition;
                    PlayMusic();
                    gamePhase = GamePhase.Title;
                }
            }

            Restart();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }

		/// <summary>
		/// This method returns the center position
		/// </summary>
		/// <param name="texture2D">texture to get the width and height</param>
		/// <returns></returns>
        public Vector2 getCenter(Texture2D texture2D)
        {
            return new Vector2((graphics.PreferredBackBufferWidth / 2) - (texture2D.Width / 2),
                (graphics.PreferredBackBufferHeight / 2) - (texture2D.Height / 2));
        }

		/// <summary>
		/// Play different music according to the game phase
		/// </summary>
        private void PlayMusic()
        {
            switch (gamePhase)
            {
                case GamePhase.Title:
                    MediaPlayer.Play(songGameTitle);
                    MediaPlayer.IsRepeating = true;
                    break;
                case GamePhase.Playing:
                    MediaPlayer.Stop();
                    MediaPlayer.Play(songGameStart);
                    MediaPlayer.IsRepeating = true;
                    break;
                case GamePhase.End:
                    MediaPlayer.Stop();
                    MediaPlayer.Play(SongGameOver);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        ///Cheking the ball state and if user press enter, retart the game
        ///and reinitialize the game setting.
        /// </summary>
        private void Restart()
        {
            //&& ball.Enabled == false
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && ball.Enabled == false && gamePhase == GamePhase.Playing)
            {
                ball.Enabled = true;
                Vector2 ballSpeed = new Vector2(GetRandomDirectionSpeed(), GetRandomDirectionSpeed());
                ball.position = initialBallPosition;
                ball.speed = ballSpeed;
            }
        }

        /// <summary>
        /// Get Random direction and speed for ball
        /// </summary>
        /// <returns></returns>
        private int GetRandomDirectionSpeed()
        {
            //Random.Next picks a random integer between the lower bound (inclusive) and the upper bound (exclusive).
            int direction = rand.Next(0, 2);
            int randSpeed = rand.Next(3, 10);

            if (direction == 1)
            {
                randSpeed = -randSpeed;
            }
            Console.WriteLine("direction: " + direction);
            return randSpeed;
        }
    }
}
