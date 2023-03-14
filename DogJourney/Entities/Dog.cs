/*  Dog.cs
 *  Final Project
 *  Revision History:
 *      2022.12.9: Created by Gayoung Kim
 *      2022.12.11: Modified by Gayoung Kim
 *      
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogJourney.Entities
{

    public class Dog : DrawableGameComponent
    {
        // variables for dog's animation
        private Vector2 dimension;
        private Vector2 dimension_avoidingDog;
        private const int NUM_OF_FRAMES = 6;
        private List<Rectangle> frames = new List<Rectangle>();
        private List<Rectangle> additionalFrames = new List<Rectangle>();
        private int frameIndex = 0;
        private int delay, delayCount;
        private Vector2 initialPosition;
        private Vector2 jumpingSpeed;
        private Song jumpSound;

        public SpriteBatch spriteBatch { get; set; }
        public Texture2D tex { get; set; }
        public Texture2D additionalTex { get; set; }
        public Vector2 position { get; set; }
        public Vector2 additionalPosition { get; set; }
        public Vector2 speed { get; set; }
        public DogState dogState { get; set; }

        public Dog(Game game, SpriteBatch spriteBatch, Texture2D tex, Texture2D additionalTex, Vector2 position, Vector2 additionalPosition, Vector2 speed, Song jumpSound, int delay) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.additionalTex = additionalTex;
            this.position = position;
            this.additionalPosition = additionalPosition;
            this.initialPosition = this.position;
            this.speed = speed;
            this.jumpingSpeed = new Vector2(0, 5);
            this.dogState = DogState.Running;
            this.jumpSound= jumpSound;

            this.delay = delay;
            this.dimension = new Vector2(tex.Width / NUM_OF_FRAMES, tex.Height);
            this.dimension_avoidingDog = new Vector2(additionalTex.Width / NUM_OF_FRAMES, additionalTex.Height);

            createFrames();
            createAvoidingDogFrames();
        }

        /// <summary>
        /// Since the jpg of a dog contains 6 motions, divide it into 6 equal parts and add it to the frame list.
        /// </summary>
        private void createFrames()
        {
            for (int i = 0; i < NUM_OF_FRAMES; i++)
            {
                int x = i * (int)dimension.X;

                Rectangle r = new Rectangle(x, 0, (int)dimension.X, (int)dimension.Y);
                frames.Add(r);
            }
        }
        /// <summary>
        /// Since the jpg of a avoidingDog contains 6 motions, divide it into 6 equal parts and add it to the frame list.
        /// </summary>
        private void createAvoidingDogFrames()
        {
            for (int i = 0; i < NUM_OF_FRAMES; i++)
            {
                int x = i * (int)dimension_avoidingDog.X;

                Rectangle r = new Rectangle(x, 0, (int)dimension_avoidingDog.X, additionalTex.Height);
                additionalFrames.Add(r);
            }
        }

        private void Run()
        {
            delayCount++;
            if (delayCount > delay)
            {
                frameIndex++;
                if (frameIndex > NUM_OF_FRAMES - 1)
                {
                    frameIndex = 0;
                }
                delayCount = 0;
            }
        }

        private void Avoid()
        {
            delayCount++;
            if (delayCount > delay)
            {
                frameIndex++;
                if (frameIndex > NUM_OF_FRAMES - 1)
                {
                    frameIndex = 0;
                }
                delayCount = 0;
            }
        }

        /// <summary>
        /// By adding or subtracting jumpingspeed to the position variable, it makes the dog jump and fall
        /// </summary>
        private void Jump()
        {
            float jumpingHeight = initialPosition.Y - tex.Height;

            frameIndex = 0;
            position = position - jumpingSpeed;

            if(position.Y <= jumpingHeight) // if character is higher than jumpingheight
            {
                jumpingSpeed = -jumpingSpeed; //falling
            }
            else if(position.Y >= initialPosition.Y) // if character is lower than initial pos1
            {
                jumpingSpeed = -jumpingSpeed;
                dogState = DogState.Running;
            }  
        }

        /// <summary>
        /// Returns the dog's body area
        /// </summary>
        /// <returns>Returns the dog's body area</returns>
        public Rectangle GetCollisionBox()
        {
            if(dogState == DogState.Avoiding)
            {
                return new Rectangle((int)additionalPosition.X, (int)additionalPosition.Y, additionalTex.Width/NUM_OF_FRAMES, additionalTex.Height+30);
            }
            else
            {
                return new Rectangle((int)position.X+30, (int)position.Y, tex.Width/NUM_OF_FRAMES - 40, tex.Height );
            }

        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            // pressing the down keyboard, the dog is ducking
            if (ks.IsKeyDown(Keys.Down) && dogState != DogState.Jumping)
            {
                dogState = DogState.Avoiding;
            }
            if ( ks.IsKeyUp(Keys.Down) && dogState != DogState.Jumping)
            {
                dogState = DogState.Running;
            }
            if(ks.IsKeyDown(Keys.Space) && dogState != DogState.Jumping)
            {// pressing space key, the dog is jumping
                dogState= DogState.Jumping;
                //when dog is jump, the jumpsound will play
                MediaPlayer.Play(jumpSound);
            }

            switch (dogState)
            {
                case DogState.Running:
                    Run();
                    break;
                case DogState.Jumping:

                    Jump();
                    break;
                case DogState.Avoiding:
                    Avoid();
                    break;
                default:
                    break;
            }


            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (dogState != DogState.Avoiding)
            {
                spriteBatch.Draw(tex, position, frames[frameIndex], Color.White);
            }
            else
            {
                spriteBatch.Draw(additionalTex, additionalPosition, additionalFrames[frameIndex], Color.White);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
