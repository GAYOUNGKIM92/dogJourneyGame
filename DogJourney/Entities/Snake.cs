/*  Snake.cs
 *  Final Project
 *  Revision History:
 *      2022.12.9: Created by Gayoung Kim
 *      2022.12.11: Modified by Gayoung Kim
 *      
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogJourney.Entities
{
    public class Snake : DrawableGameComponent
    {
        // variables for snake animation
        private Vector2 initialPosition;
        private float acceleration = 2;
        private int delay, delayCount;
        private SpriteEffects snakeSpriteFlip;

        //constructor
        public Snake(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position, Vector2 speed, int delay) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.speed = speed;
            initialPosition = position;
            this.delay = delay;

            hide();
        }


        public SpriteBatch spriteBatch { get; set; }
        public Texture2D tex { get; set; }
        public Vector2 position { get; set; }
        public Vector2 speed { get; set; }

        public void show()
        {

            this.Enabled = true;
            this.Visible = true;
        }

        public void hide()
        {
            this.Enabled = false;
            this.Visible = false;
            this.position = initialPosition;
        }

        /// <summary>
        /// Returns the snake's body area
        /// </summary>
        /// <returns>Returns the snake's body area</returns>
        public Rectangle GetCollisionBox()
        {
            int wPadding = tex.Width / 5;
            int hPadding = tex.Height / 5;

            return new Rectangle((int)position.X + wPadding, (int)position.Y + hPadding, tex.Width - wPadding, tex.Height - hPadding*2);
        }

        /// <summary>
        /// subtracting or adding acceleration from position to allow the snake to move back and forth.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {

            if (this.Enabled)
            {
                delayCount++;
                if (delayCount > delay)
                {
                    acceleration = -acceleration;
                    // flip effect when a snake changes direction
                    if (acceleration < 0)
                    {
                        snakeSpriteFlip = SpriteEffects.FlipHorizontally;
                    }
                    else
                    {
                        snakeSpriteFlip = SpriteEffects.None;
                    }
                    delayCount = 0;
                }

                position = position - speed - new Vector2(acceleration, 0);

                if (position.X <= -tex.Width)
                {
                    hide();
                }

                base.Update(gameTime);
            }

        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, null, Color.White, 0, Vector2.Zero, 1, snakeSpriteFlip, 0);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
