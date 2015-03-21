using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Breakout
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private bool ballLeft = false, ballUp = true;
        Texture2D Ball;
        Texture2D Padle;
        Vector2 padlePosition = Vector2.Zero;
        Vector2 ballPosition = Vector2.Zero;
        Texture2D [] blocks = new Texture2D [40];
        Vector2 [] Blocksposition = new Vector2 [40];


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            base.Initialize();
        }


        protected override void LoadContent()
        {   
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Padle = Content.Load<Texture2D>("Padle");
            Ball = Content.Load<Texture2D>("Ball");
            padlePosition = new Vector2((graphics.GraphicsDevice.Viewport.Width/2) - (Padle.Width/2), (graphics.GraphicsDevice.Viewport.Height -50));
            ballPosition = new Vector2((graphics.GraphicsDevice.Viewport.Width / 2) - (Ball.Width / 2), (graphics.GraphicsDevice.Viewport.Height - 200));

            int bCount = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    blocks[bCount] = Content.Load<Texture2D>("Block");
                    Blocksposition[bCount].X = 130 + (i * 75);
                    Blocksposition[bCount].Y = 25 + (j * 50);
                    bCount++;
                }
                
            }
            
        }

        protected override void UnloadContent()
        {
           
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Left) && padlePosition.X >= 0)
            {
                padlePosition.X -= 4;

            }
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Right) && padlePosition.X <= graphics.GraphicsDevice.Viewport.Width-Padle.Width)
            {
                padlePosition.X += 4;
            }


            if (ballLeft)
            {
                if (ballPosition.X >= 0 && ballPosition.X != padlePosition.X)
                    ballPosition.X -= 4;

                else
                {
                    ballLeft = false;
                    ballPosition.X += 4;
                }
            }
            if (!ballLeft)
            {
                if (ballPosition.X <= graphics.GraphicsDevice.Viewport.Width - Ball.Width)
                    ballPosition.X += 4;

                else
                {
                    ballLeft = true;
                    ballPosition.X -= 4;
                }
            }
           ////////////////////////////////////////////////////////////////////////////////////
            if (ballUp)
            {
                if (ballPosition.Y >= 0)
                    ballPosition.Y -= 4;

                else
                {
                    ballUp = false;
                    ballPosition.Y += 4;
                }
            }
            if (!ballUp)
            {
                  ballPosition.Y += 4;
            }

            // detect collision

            if (CollisionPadBall())
            {
                    ballUp = true;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            spriteBatch.Draw(Padle, padlePosition, Color.White);
            spriteBatch.Draw(Ball, ballPosition, Color.White);
            for (int i = 0; i < 40; i++)
            {
                spriteBatch.Draw(blocks[i], Blocksposition[i], Color.White);
            }
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public bool CollisionPadBall()
        {
            if ((ballPosition.Y + Ball.Height) >= padlePosition.Y && ballPosition.Y < padlePosition.Y + 4 && (ballPosition.X +Ball.Width) > padlePosition.X && ballPosition.X < (padlePosition.X + Padle.Width))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
