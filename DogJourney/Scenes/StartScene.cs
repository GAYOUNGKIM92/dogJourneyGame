/*  StartScene.cs
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
using DogJourney.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DogJourney
{
    public class StartScene : GameScene
    {
        //menu component needs to be created.
        public Background background { get; set; }
        public MenuComponent menu { get; set; }
        public Dog dog { get; set; }

        Game1 g;
        SpriteBatch spriteBatch;
        string[] menuItems = {"Start game", "Help", "About", "Quit" };

        public StartScene(Game game) : base(game)
        {
            g = (Game1)game;
            this.spriteBatch = g._spriteBatch;

            // Add background image animation to menu screen
            Texture2D backgroundTex = game.Content.Load<Texture2D>("Images/background");
            Vector2 backgroundPos = new Vector2(0, 0);
            Vector2 backgroundSpeed = new Vector2(4, 0);
            background = new Background(g, spriteBatch, backgroundTex, backgroundPos, backgroundSpeed);
            this.components.Add(background);

            // Add menu items
            SpriteFont regular = game.Content.Load<SpriteFont>("Fonts/regularFont");
            SpriteFont hilight = g.Content.Load<SpriteFont>("Fonts/highlightFont");
            menu = new MenuComponent(g, spriteBatch, regular, hilight, menuItems);
            this.components.Add(menu);

            //Add dog animation to menu screen
            Song jumpSound = game.Content.Load<Song>("Sounds/jump");
            Texture2D dogTex = game.Content.Load<Texture2D>("images/dog");
            Texture2D additionalTex = game.Content.Load<Texture2D>("images/avoidingDog");
            Vector2 dogPos = new Vector2(45, Shared.stage.Y - dogTex.Height * 1.7f);
            Vector2 dogAdditionalPos = new Vector2(48, Shared.stage.Y - additionalTex.Height * 2.1f);
            Vector2 dogSpeed = new Vector2(4, 0);
            dog = new Dog(game, spriteBatch, dogTex, additionalTex, dogPos, dogAdditionalPos,dogSpeed,jumpSound, 3);
            this.components.Add(dog);

        }
    }
}
