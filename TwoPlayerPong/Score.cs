/* Score.cs
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

namespace TwoPlayerPong
{
    /// <summary>
    /// A class to draw score
    /// </summary>
    public class Score : DrawableGameComponent
    {
        public int playerOneScore = 0;
        public int playerTwoScore = 0;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        Vector2 stage;
        public bool isVisible;

        /// <summary>
        /// A constructor to assign necessary parameters to the Score class
        /// </summary>
        /// <param name="game">Game to call</param>
        /// <param name="spriteBatch">SpriteBatch to call</param>
        /// <param name="spriteFont">SpriteFont to call</param>
        /// <param name="stage">Screen size</param>
        /// <param name="isVisible">Is this score visible or not</param>
        public Score(Game game,
            SpriteBatch spriteBatch,
            SpriteFont spriteFont,
            Vector2 stage,
            bool isVisible = true) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.spriteFont = spriteFont;
            this.stage = stage;
        }

		/// <summary>
		/// Draw the score of player 1 and player 2
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (isVisible)
            {
                Vector2 playerOneScorePosition = new Vector2((stage.X / 2) - 74, stage.Y - 70);
                Vector2 playerTwoScorePosition = new Vector2((stage.X / 2) + 40, stage.Y - 70);

                spriteBatch.DrawString(spriteFont, playerOneScore.ToString(), playerOneScorePosition, new Color(255, 204, 51));
                spriteBatch.DrawString(spriteFont, playerTwoScore.ToString(), playerTwoScorePosition, new Color(255, 204, 51));
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}