/*  Rock.cs
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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace DogJourney.Entities
{
    public class Rock : DrawableGameComponent
    {

        //variables for the animation 
        private Vector2 initialPosition;
        private Random random = new Random();

        // constructor
        public Rock(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position, Vector2 speed) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.pos1 = position;
            this.pos2 = new Vector2(pos1.X + Shared.stage.X/2, pos1.Y);
            this.speed = speed;
            initialPosition = pos1;
            show();
        }

        public SpriteBatch spriteBatch { get; set; }
        public Texture2D tex { get; set; }
        public Vector2 pos1 { get; set; }
        public Vector2 pos2 { get; set; }
        public Vector2 speed { get; set; }

        public void show()
        {
            this.Enabled = true;
            this.Visible = true;
            this.pos1 = initialPosition;
            this.pos2 = new Vector2(pos1.X + Shared.stage.X / 2, pos1.Y);
        }

        public void hide()
        {
            this.Enabled = false;
            this.Visible = false;
        }

        /// <summary>
        /// Returns the rock's area
        /// </summary>
        /// <returns>Returns the rock's area</returns>
        public Rectangle GetCollisionBox()
        {
            int wSubtraction = tex.Width / 4;
            int hSubtraction = tex.Height / 5;

            return new Rectangle((int)pos1.X + wSubtraction, (int)pos1.Y + hSubtraction, tex.Width - wSubtraction, tex.Height - hSubtraction);
        }
        /// <summary>
        /// Returns the rock's area
        /// </summary>
        /// <returns>Returns the second rock's area</returns>
        public Rectangle GetCollisionBox2()
        {
            int wSubtraction = tex.Width / 4;
            int hSubtraction = tex.Height / 5;

            return new Rectangle((int)pos2.X + wSubtraction, (int)pos2.Y + hSubtraction, tex.Width - wSubtraction, tex.Height - hSubtraction);
        }

        /// <summary>
        /// If only one stone comes out at the same speed, the game is not fun, so the second stone comes out in a random position
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {

            if (this.Enabled)
            {
                pos1 = pos1 - speed;
                pos2 = pos2 - speed;

                if(pos1.X <= -tex.Width)
                {
                    pos1 = new Vector2(Shared.stage.X, pos1.Y);
                }
                if (pos2.X <= -tex.Width && pos1.X >= Shared.stage.X*0.67)
                {
                    pos2 = new Vector2(pos1.X + random.Next((int)Shared.stage.X / 3, (int)Shared.stage.X/2), pos2.Y); 
                }
                base.Update(gameTime);
            }

        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, pos1, Color.White);
            spriteBatch.Draw(tex, pos2, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
