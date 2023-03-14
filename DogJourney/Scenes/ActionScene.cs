/*  ActionScene.cs
 *  Final Project
 *  Revision History:
 *      2022.12.9: Created by Gayoung Kim
 *      2022.12.11: Modified by Gayoung Kim
 *      
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogJourney.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DogJourney
{
    public class ActionScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Game1 g;

        Background background;
        Score score;
        Dog dog;
        Rock rock;
        Ghost ghost;
        Snake snake;
        ObstacleManager obstacleManager;

        public ActionScene(Game game) : base(game)
        {
            g = (Game1)game;
            this.spriteBatch = g._spriteBatch;

            // sound effect
            Song jumpSound = game.Content.Load<Song>("Sounds/jump");
            Song dieSound = game.Content.Load<Song>("Sounds/die");
            Song levelReachedSound = game.Content.Load<Song>("Sounds/level-reached");

            // background
            Texture2D backgroundTex = game.Content.Load<Texture2D>("images/background");
            Vector2 backgroundPos = new Vector2(0, 0);
            Vector2 backgroundSpeed = new Vector2(4, 0);
            background = new Background(game,spriteBatch,backgroundTex, backgroundPos,backgroundSpeed);
            this.components.Add(background);

            // score board
            SpriteFont scoreFont = game.Content.Load<SpriteFont>("fonts/scoreFont");
            Vector2 scorePos = new Vector2(Shared.stage.X/2 - 10, scoreFont.LineSpacing);
            score = new Score(game, spriteBatch, scoreFont, scorePos,levelReachedSound, 3);
            this.components.Add(score);


            // Dog
            Texture2D dogTex = game.Content.Load<Texture2D>("images/dog");
            Texture2D additionalTex = game.Content.Load<Texture2D>("images/avoidingDog");
            Vector2 dogPos = new Vector2(45, Shared.stage.Y - dogTex.Height*1.7f);
            Vector2 dogAdditionalPos = new Vector2(48, Shared.stage.Y - additionalTex.Height * 2.1f);
            Vector2 dogSpeed = new Vector2(4, 0);
            dog = new Dog(game, spriteBatch, dogTex, additionalTex, dogPos, dogAdditionalPos, dogSpeed,jumpSound, 3);
            this.components.Add(dog);

            //rock
            Texture2D rockTex = game.Content.Load<Texture2D>("images/rock");
            Vector2 rockPos = new Vector2(backgroundTex.Width, dogPos.Y + rockTex.Height *2);
            Vector2 rockSpeed = new Vector2(4, 0);
            rock = new Rock(game, spriteBatch, rockTex, rockPos, rockSpeed);
            this.components.Add(rock);

            //ghost
            Texture2D ghostTex = game.Content.Load<Texture2D>("images/ghost");
            Vector2 ghostPos = new Vector2(backgroundTex.Width, ghostTex.Height* 3.65f) ;
            Vector2 ghostSpeed = new Vector2(4, 0);
            ghost = new Ghost(game, spriteBatch, ghostTex, ghostPos, ghostSpeed);
            this.components.Add(ghost);

            //snake
            Texture2D snakeTex = game.Content.Load<Texture2D>("images/snake");
            Vector2 snakePos = new Vector2(backgroundTex.Width, dogPos.Y + snakeTex.Height * 1.8f);
            Vector2 snakeSpeed = new Vector2(4, 0);
            snake = new Snake(game, spriteBatch, snakeTex, snakePos, snakeSpeed, 30);
            this.components.Add(snake);

            // obstacle manager
            obstacleManager = new ObstacleManager(game, dog, ghost, rock, snake, score, dieSound);
            this.components.Add(obstacleManager);

        }
    }
}
