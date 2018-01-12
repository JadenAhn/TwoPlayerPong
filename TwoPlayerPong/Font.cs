/* Font.cs
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
    /// A class to draw playerOneName and playerTwoName
    /// </summary>
    class Font : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private string message;
        private Vector2 position;
        private Color color;
        public bool isVisible;

        /// <summary>
        /// A constructor to assign necessary parameters to the Font class
        /// </summary>
        /// <param name="game">Game to call</param>
        /// <param name="spriteBatch">SpriteBatch to call</param>
        /// <param name="font">SpriteFont to call</param>
        /// <param name="message">Message to draw</param>
        /// <param name="position">Position of the font</param>
        /// <param name="color">Color of the font</param>
        /// <param name="isVisible">Is this sprite font or not</param>
        public Font(Game game,
            SpriteBatch spriteBatch,
            SpriteFont font,
            string message,
            Vector2 position,
            Color color,
            bool isVisible = true) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.font = font;
            this.message = message;
            this.position = position;
            this.color = color;
            this.isVisible = isVisible;
        }

		/// <summary>
		/// Draw the font message when it is visible
		/// </summary>
		/// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (isVisible)
            {
                spriteBatch.DrawString(font, message, position, color);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
