/* Sprite.cs
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
    /// A class is to update and draw title, startMessage and backGround
    /// </summary>
    public class Sprite : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D texture2D;
        private Vector2 position;
        private float opacity;
        private float blinkSpeed;
        public bool isIncreasing = true;
        public bool isVisible;

        /// <summary>
        /// A constructor to assign necessary parameters to the Sprite class
        /// </summary>
        /// <param name="game">Game to call</param>
        /// <param name="spriteBatch">SpriteBatch to call</param>
        /// <param name="texture2D">Texture of the sprite</param>
        /// <param name="position">Position of the Sprite</param>
        /// <param name="isVisible">Is this sprite visible or not</param>
        /// <param name="blinkSpeed">Frequency of blinking effect</param>
        public Sprite(Game game,
            SpriteBatch spriteBatch,
            Texture2D texture2D,
            Vector2 position,
            bool isVisible = true,
            float blinkSpeed = 0) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.texture2D = texture2D;
            this.position = position;
            this.blinkSpeed = blinkSpeed;
            this.isVisible = isVisible;
        }

		/// <summary>
		/// Change the opacity of the sprite repeatedly
        /// according to the blink speed parameter
		/// </summary>
		/// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            //const float BLINKSPEED = 0.02f;
            if (blinkSpeed != 0)
            {
                if (opacity <= 0)
                {
                    opacity += blinkSpeed;
                    isIncreasing = true;
                }
                else if (opacity < 1)
                {
                    if (isIncreasing)
                    {
                        opacity += blinkSpeed;
                    }
                    else
                    {
                        opacity -= blinkSpeed;
                    }
                }
                else
                {
                    opacity -= blinkSpeed;
                    isIncreasing = false;
                }
            }
            else
            {
                opacity = 1;
            }
            base.Update(gameTime);
            
        }

		/// <summary>
		/// Draw the sprite according to the opacity
        /// when it is visible
		/// </summary>
		/// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (isVisible)
            {
                spriteBatch.Draw(texture2D, position, Color.White * opacity);
            }
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
