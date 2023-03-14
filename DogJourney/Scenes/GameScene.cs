/*  GameScene.cs
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
    public abstract class GameScene : DrawableGameComponent
    {
        public List<GameComponent> components { get; set; }

        public virtual void show()
        {
            this.Visible = true;
            this.Enabled = true;
        }

        public virtual void hide()
        {
            this.Visible = false;
            this.Enabled = false;
        }

        public GameScene(Game game) : base(game)
        {
            components = new List<GameComponent>();
            hide();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent  item in components)
            {
                if (item.Enabled)
                {
                    item.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            foreach (GameComponent item in components)
            {
                if (item is DrawableGameComponent)
                {
                    DrawableGameComponent comp = (DrawableGameComponent)item;
                    if (comp.Visible)
                    {
                        comp.Draw(gameTime);
                    }
                }
            }

            base.Draw(gameTime);
        }
    }
}
