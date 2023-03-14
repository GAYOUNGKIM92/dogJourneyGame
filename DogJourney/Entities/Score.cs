/*  Score.cs
 *  Final Project
 *  Revision History:
 *      2022.12.9: Created by Gayoung Kim
 *      2022.12.11: Modified by Gayoung Kim
 *      
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogJourney.Entities
{
    public class Score : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;
        private Vector2 position;
        public int score;
        private int delay;
        private int delayCount;
        string text;
        public int level = 1;
        private Song levelReachedSound;

        public Score(Game game, SpriteBatch spriteBatch, SpriteFont spriteFont, Vector2 position, Song levelReachedSound, int delay) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.spriteFont = spriteFont;
            this.position = position;
            this.delay = delay;
            this.levelReachedSound = levelReachedSound;
        }

        public override void Update(GameTime gameTime)
        {
            delayCount++;
            if (delayCount > delay)
            {
                // if the score reaches at 100 and 300, the level reached sound plays
                if (score == 100 || score == 300)
                {
                    level++;
                    MediaPlayer.Play(levelReachedSound);
                }
                score++;
                delayCount = 0;
            }
            text = "score: " + score.ToString() + "   level:" + level;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, text, position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
