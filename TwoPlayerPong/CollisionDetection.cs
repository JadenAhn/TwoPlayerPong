/* CollisionDetection.cs
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
    /// A class to detect collision between a ball and a paddle
    /// </summary>
    public class CollisionDetection : GameComponent
    {
        private Ball ball;
        private Paddle paddle;
        private SoundEffect hitSound;

        /// <summary>
        /// A constructor to assign necessary parameters to the CollisionDetection class
        /// </summary>
        /// <param name="game">Game to call</param>
        /// <param name="ball">First spriteBatch to check the collision</param>
        /// <param name="paddle">Second spriteBatch to check the collision</param>
        /// <param name="hitSound">Sound to play when the ball hits the paddle</param>
        public CollisionDetection(Game game,
            Ball ball,
            Paddle paddle,
            SoundEffect hitSound) : base(game)
        {
            this.ball = ball;
            this.paddle = paddle;
            this.hitSound = hitSound;
        }

		/// <summary>
		/// In this update method, it is checking collision of the ball and the each side of the paddle
		/// </summary>
		/// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            //ball is going right and touching left side of the paddle
            if (ball.speed.X > 0 &&
                ball.getBound().Right + ball.speed.X > paddle.getBound().Left &&
                ball.getBound().Left < paddle.getBound().Left &&
                ball.getBound().Bottom > paddle.getBound().Top &&
                ball.getBound().Top < paddle.getBound().Bottom)
            {
                ball.speed.X = -ball.speed.X;
                hitSound.Play();
            }

            //ball is going left and touching right side of the paddle
            if (ball.speed.X < 0 &&
                ball.getBound().Left + ball.speed.X < paddle.getBound().Right &&
                ball.getBound().Right > paddle.getBound().Right &&
                ball.getBound().Bottom > paddle.getBound().Top &&
                ball.getBound().Top < paddle.getBound().Bottom)
            {
                ball.speed.X = -ball.speed.X;
                hitSound.Play();
            }

            //ball is going down and touching top side of the paddle
            if (ball.speed.Y > 0 &&
                ball.getBound().Bottom + ball.speed.Y > paddle.getBound().Top &&
                ball.getBound().Top < paddle.getBound().Top &&
                ball.getBound().Right > paddle.getBound().Left &&
                ball.getBound().Left < paddle.getBound().Right)
            {
                ball.speed.Y = -ball.speed.Y;
                hitSound.Play();
            }

            //ball is going up and touching bottom side of the paddle
            if (ball.speed.Y < 0 &&
                ball.getBound().Top + ball.speed.Y < paddle.getBound().Bottom &&
                ball.getBound().Bottom > paddle.getBound().Bottom &&
                ball.getBound().Right > paddle.getBound().Left &&
                ball.getBound().Left < paddle.getBound().Right)
            {
                ball.speed.Y = -ball.speed.Y;
                hitSound.Play();
            }

            base.Update(gameTime);
        }
    }
}
