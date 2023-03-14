/*  AboutScene.cs
 *  Final Project
 *  Revision History:
 *      2022.12.9: Created by Gayoung Kim
 *      
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace DogJourney
{
    public class AboutScene : GameScene
    {
        private Game1 g;
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;
        private Vector2 position;

        public AboutScene(Game game) : base(game)
        {
            g = (Game1)game;
            this.spriteBatch = g._spriteBatch;
            this.spriteFont = g.Content.Load<SpriteFont>("fonts/aboutFont");
            position = new Vector2(Shared.stage.X/2, Shared.stage.Y/2);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, "Gayoung Kim", position, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
