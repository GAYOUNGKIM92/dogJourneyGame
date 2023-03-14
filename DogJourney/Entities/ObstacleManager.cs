/*  ObstacleManager.cs
 *  Final Project
 *  Revision History:
 *      2022.12.9: Created by Gayoung Kim
 *      2022.12.11: Modified by Gayoung Kim
 *      
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogJourney.Entities
{
    public class ObstacleManager : GameComponent
    {
        private Game1 game;
        private Dog dog;
        private Ghost ghost;
        private Rock rock;
        private Snake snake;
        private Score scoreBoard;

        // variables for spawning obstacles
        private const int GHOST_APPEAR_SCORE_MIN = 100;
        private const int SNAKE_APPEAR_SCORE_MIN = 300;
        private const int OBSTACLE_APPEAR_DELAY = 60;
        private Random random;
        private int obstacleDelayCount;
        private Song dieSound; 

        // constroctor
        public ObstacleManager(Game game, Dog dog, Ghost ghost, Rock rock, Snake snake, Score scoreBoard, Song dieSound) : base(game)
        {
            this.game = (Game1)game;
            this.dog = dog;
            this.ghost = ghost;
            this.rock = rock;
            this.snake = snake;
            this.scoreBoard = scoreBoard;
            random = new Random();
            this.dieSound= dieSound;
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            // pressing Escape, game is over and return to meno
            if (ks.IsKeyDown(Keys.Escape))
            {
                GameOver();
                game.ReturnToMenu();
            }

            CheckCollision();
            if(game.isPlaying)
            {
                PutRandomObstacle();
            }
        }
        /// <summary>
        /// This method causes ghosts and snakes to appear.
        /// </summary>
        public void PutRandomObstacle()
        {
            if (scoreBoard.score >= SNAKE_APPEAR_SCORE_MIN) // level3
            {
                obstacleDelayCount++;
                if (obstacleDelayCount > OBSTACLE_APPEAR_DELAY)
                {    //This is to prevent stones and obstacles from coming together
                    if ((rock.pos1.X <= Shared.stage.X * 0.4)
                        && (rock.pos2.X <= Shared.stage.X * 0.5 || rock.pos2.X >= Shared.stage.X * 1.3)) 
                    {
                        //  snakes and ghosts come out randomly
                        switch (random.Next(2))
                        {
                            case 0:
                                if (!ghost.Enabled)
                                {
                                    ghost.show();
                                }
                                break;
                            case 1:
                                if (!snake.Enabled)
                                {
                                    snake.show();
                                }
                                break;
                        }

                        obstacleDelayCount = 0;
                    }
                }
            }
            else if (scoreBoard.score > GHOST_APPEAR_SCORE_MIN) // level2 
            {
                obstacleDelayCount++;
                if(obstacleDelayCount > OBSTACLE_APPEAR_DELAY)
                {
                    if ((rock.pos1.X <= Shared.stage.X * 0.35)
                        && (rock.pos2.X <= Shared.stage.X * 0.5 || rock.pos2.X >= Shared.stage.X * 1.4))
                    {
                        //Prevent ghosts from coming out at a constant speed
                       
                        
                            if (!ghost.Enabled)
                            {
                                ghost.show();
                            }
                        
                        obstacleDelayCount = 0;
                    }
                }
            }


        }

        /// <summary>
        /// This method checks if the dog hits the obstacles or not
        /// </summary>
        public void CheckCollision()
        {
            Rectangle dogCollisionBox = dog.GetCollisionBox();
            Rectangle rockCollisionBox = rock.GetCollisionBox();
            Rectangle rockCollisionBox2 = rock.GetCollisionBox2();
            Rectangle ghostCollisionBox = ghost.GetCollisionBox();
            Rectangle snakeCollisionBox = snake.GetCollisionBox();

            if(dogCollisionBox.Intersects(rockCollisionBox) || dogCollisionBox.Intersects(rockCollisionBox2) || dogCollisionBox.Intersects(ghostCollisionBox) || dogCollisionBox.Intersects(snakeCollisionBox))
            { 
                // if the dog hits the obstacles, die sound plays game is over.
                MediaPlayer.Play(dieSound);
                GameOver();
            }
        }

        /// <summary>
        /// this method is executed when the game is over
        /// initialize..
        /// </summary>
        public void GameOver()
        {
            scoreBoard.score = 0;
            scoreBoard.level = 1;
            game.isPlaying = false;
            rock.show();
            ghost.hide();
            snake.hide();
            game.ReturnToMenu();
        }
    }
}
