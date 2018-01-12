/* Ball.cs
* Assignment 4
* Revision History
*   Ji Hong Ahn, 2017.12.07: Created
*   Ji Hong Ahn, 2017.12.13: Added header comments
*                            Added documentation comments for constructors
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace TwoPlayerPong
{
    /// <summary>
    /// A class to update and draw a ball
    /// </summary>
    public class Ball : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        public Vector2 position;
        public Vector2 initialPosition;
        public Vector2 speed;
        public Score score;
        public WinMessage winMessage;
        public bool isVisible;
        private Vector2 stage;
        private SoundEffect hitSound;
        private SoundEffect hitWallSound;
        private SoundEffect missSound;


        /// <summary>
        /// A constructor to assign necessary parameters to the Ball class
        /// </summary>
        /// <param name="game">Game to call</param>
        /// <param name="spriteBatch">SpriteBatch to call</param>
        /// <param name="tex">Texture of the ball</param>
        /// <param name="position">Position of the ball</param>
        /// <param name="speed">Speed of the ball</param>
        /// <param name="stage">Screen size</param>
        /// <param name="hitSound">Sound to play when the ball hit the paddle</param>
        /// <param name="hitWallSound">Sound to play when the ball hit the wall</param>
        /// <param name="missSound">Sound to play when the ball goes out of the screen</param>
        /// <param name="score">Game score</param>
        /// <param name="winMessage">Win message</param>
        public Ball(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position,
            Vector2 speed,
            Vector2 stage,
            SoundEffect hitSound,
            SoundEffect hitWallSound,
            SoundEffect missSound,
            Score score,
            WinMessage winMessage
            ) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.initialPosition = position;
            this.speed = speed;
            this.stage = stage;
            this.hitSound = hitSound;
            this.hitWallSound = hitWallSound;
            this.missSound = missSound;
            this.score = score;
            this.winMessage = winMessage;
        }

		/// <summary>
		/// Moving the position of ball, and set the speed
		/// </summary>
		/// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            position += speed;

			//When the ball hit the top or bottom wall, it bounces back
            if (position.Y <= 0 || position.Y + tex.Height >= stage.Y)
            {
                speed.Y = -speed.Y;
                hitWallSound.Play();
            }

            //When the ball hit the left wall, player 2 gets a score and the position is reset
            if (position.X + tex.Width <= 0)
            {
                score.playerTwoScore++;
                winMessage.player = Game1.PLAYER_2;
                missSound.Play();
                this.Enabled = false;
                this.position = initialPosition;
            }

            //When the ball hit the right wall, player 1 gets a score and the position is reset
            if (position.X >= stage.X)
            {
                score.playerOneScore++;
                winMessage.player = Game1.PLAYER_1;
                missSound.Play();
                this.Enabled = false;
                this.position = initialPosition;
            }

            base.Update(gameTime);
        }

		/// <summary>
		/// Draw the ball
		/// </summary>
		/// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            if (isVisible)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(tex, position, Color.White);
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }

		/// <summary>
		/// Return the rectangle position of the ball
		/// </summary>
		/// <returns></returns>
        public Rectangle getBound()
        {
            //return the position of the rectangle
            return new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
        }
    }
}
