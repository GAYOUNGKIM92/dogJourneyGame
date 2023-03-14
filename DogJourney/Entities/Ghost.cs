/*  Ghost.cs
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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogJourney.Entities
{
    public class Ghost : DrawableGameComponent
    {
        // variables for ghost animation
        private Vector2 initialPosition;
        private double acceleration = 0;

        // constructor
        public Ghost(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position, Vector2 speed) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.speed = speed;
            initialPosition = position;

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
        /// Returns the ghost's body area
        /// </summary>
        /// <returns>Returns the ghost's body area</returns>
        public Rectangle GetCollisionBox()
        {
            int wPadding = tex.Width / 4;
            int hPadding = tex.Height / 4;

            return new Rectangle((int)position.X + wPadding, (int)position.Y - hPadding, tex.Width - wPadding, tex.Height - hPadding);
        }

        public override void Update(GameTime gameTime)
        {
            
            if (this.Enabled)
            {
                acceleration += 0.2;
                // Subtract sin from position to make the ghost move up and down like a sin function graph
                position = position - speed - new Vector2(0,(float)Math.Sin(acceleration)*3);

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
            spriteBatch.Draw(tex, position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
