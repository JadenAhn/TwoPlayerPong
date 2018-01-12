/* Paddle.cs
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

namespace TwoPlayerPong
{
    /// <summary>
    /// A class to update and draw a paddle
    /// </summary>
    public class Paddle : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        public Vector2 position;
        public Vector2 speed;
        private Vector2 stage;
        private Keys up;
        private Keys down;

        /// <summary>
        /// A constructor to assign necessary parameters to the Paddle class
        /// </summary>
        /// <param name="game">Game to call</param>
        /// <param name="spriteBatch">SpriteBatch to call</param>
        /// <param name="tex">Texture of the paddle</param>
        /// <param name="position">Position of the paddle</param>
        /// <param name="speed">Speed of the paddle</param>
        /// <param name="stage">Screen size</param>
        /// <param name="up">Key to move the paddle up</param>
        /// <param name="down">Key to move the paddle down</param>
        public Paddle(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position,
            Vector2 speed,
            Vector2 stage,
            Keys up,
            Keys down
            ) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.speed = speed;
            this.stage = stage;
            this.up = up;
            this.down = down;
        }

		/// <summary>
		/// Collision check with the window screen, and moves the paddle up and down
		/// </summary>
		/// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
			//moving up
            if (Keyboard.GetState().IsKeyDown(up))
            {
                position -= speed;
            }
			//moving down
            if (Keyboard.GetState().IsKeyDown(down))
            {
                position += speed;
            }

            //bottom
            if (position.Y + tex.Height > stage.Y)
            {
                position.Y = stage.Y - tex.Height;
            }
            //top
            if (position.Y < 0)
            {
                position.Y = 0;
            }

            base.Update(gameTime);
        }

		/// <summary>
		/// Draw the paddle
		/// </summary>
		/// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Return the rectangle position of the paddle
        /// </summary>
        /// <returns></returns>
        public Rectangle getBound()
        {
            //return the position of the rectangle
            return new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
        }
    }
}
