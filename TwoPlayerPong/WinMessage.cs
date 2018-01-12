/* WinMessage.cs
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
    /// A class to update and draw win message
    /// </summary>
    public class WinMessage : DrawableGameComponent
    {
        public int player = 0;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        Vector2 stage;
        public bool isVisible;
        private string playerName = "";
        private string message = "";
        private Vector2 stringLength;

        /// <summary>
        /// A constructor to assign necessary parameters to the WinMessage class
        /// </summary>
        /// <param name="game">Game to call</param>
        /// <param name="spriteBatch">SpriteBatch to call</param>
        /// <param name="spriteFont">SpriteFont to call</param>
        /// <param name="stage">Screen size</param>
        /// <param name="isVisible">Is this win message visible or not</param>
        public WinMessage(Game game,
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
		/// Update the player name of the winner
		/// </summary>
		/// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (player == Game1.PLAYER_1)
            {
                playerName = Game1.PLAYER_ONE_NAME;
            }
            if(player == Game1.PLAYER_2)
            {
                playerName = Game1.PLAYER_TWO_NAME;
            }
            message = $"WINNER   is   {playerName}!";
            stringLength = spriteFont.MeasureString(message);

            base.Update(gameTime);
        }

        /// <summary>
        /// Draw the player name of the winner
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (isVisible)
            {
                Vector2 winMessagePosition = new Vector2((stage.X / 2) - (stringLength.X / 2), stage.Y / 2);
                spriteBatch.DrawString(spriteFont, message, winMessagePosition, new Color(255, 204, 51));
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
