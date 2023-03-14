/*  Background.cs
 *  Final Project
 *  Revision History:
 *      2022.12.9: Created by Gayoung Kim
 *      
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DogJourney.Entities
{
    public class Background : DrawableGameComponent
    {

        // variables for Background animation
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 pos1;
        private Vector2 pos2;
        private Vector2 pos3;
        private Vector2 speed;

        // Constructor
        public Background(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position, Vector2 speed) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.pos1 = position;
            this.pos2 = new Vector2(pos1.X + tex.Width, pos1.Y);
            this.pos3 = new Vector2(pos2.X+ tex.Width, pos2.Y);
            this.speed = speed;
        }
        /// <summary>
        /// Position the background back to the end of each background to create a continuous animation effect
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            pos1 = pos1 - speed;
            pos2 = pos2 - speed;
            pos3 = pos3 - speed;

            if (pos1.X < -tex.Width)
            {
                pos1.X = pos3.X + tex.Width;
            }

            if (pos2.X < -tex.Width)
            {
                pos2.X = pos1.X + tex.Width;
            }

            if (pos3.X < -tex.Width)
            {
                pos3.X = pos2.X + tex.Width;
            }

            base.Update(gameTime);
        }
        /// <summary>
        /// Three backgrounds were drawn to create an animation in which the background moved.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, pos1, Color.White);
            spriteBatch.Draw(tex, pos2, Color.White);
            spriteBatch.Draw(tex, pos3, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
