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

namespace TPTRemakeWindows
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        const int width = 640;
        const int height = 480;

        int xIndex = 0, yIndex = 0, brushRadius = 0;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        PixelBuffer screen = new PixelBuffer(width + 20, height + 20);
        Texture2D defaultPixel, cursor;
        MouseState mouseState, oldMouseState;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = height;
            graphics.PreferredBackBufferWidth = width;
        }
        protected override void Initialize()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    screen.buffer[x, y] = new Pixel();
                }
            }

            this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 500.0f);

            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            defaultPixel = Content.Load<Texture2D>("pixel");
            cursor = Content.Load<Texture2D>("redCircle");
        }
        protected override void Update(GameTime gameTime)
        {
            xIndex++;
            yIndex++;

            oldMouseState = mouseState;
            mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                for (int x = brushRadius * -1; x < brushRadius; x++)
                {
                    for (int y = brushRadius * -1; y < brushRadius; y++)
                    {
                        screen.buffer[mouseState.X + x, mouseState.Y + y].setType(5);
                    }
                }
            }

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    spriteBatch.Draw(defaultPixel, new Rectangle(x, y, 1, 1), screen.buffer[x, y].color);
                }
            }

            spriteBatch.Draw(cursor, new Rectangle(mouseState.X - 8, mouseState.Y - 8, 16, 16), Color.White);

            spriteBatch.End();
            
            base.Draw(gameTime);
        }

        static double distanceFormula(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }
    }

    public class PixelBuffer
    {
        public int width = 0;
        public int height = 0;
        public Pixel[,] buffer = new Pixel[1,1];

        public PixelBuffer(int inputWidth, int inputHeight)
        {
            buffer = new Pixel[inputWidth, inputHeight];
            width = inputWidth;
            height = inputHeight;
        }
    }

    public class Pixel
    {
        public int id = 0;

        public int xPosition = 0;
        public int yPosition = 0;

        public Color color;

        public Pixel()
        {
            color = Color.Green;
            id = 0;
        }

        public void setType(int type)
        {
            id = type;
            switch (type)
            {
                case 0:
                    color = Color.Blue;
                    break;
                case 1:
                    color = Color.Orange;
                    break;
                case 2:
                    color = Color.Green;
                    break;
                case 3:
                    color = Color.Brown;
                    break;
                case 4:
                    color = Color.SlateGray;
                    break;
                case 5:
                    color = Color.White;
                    break;
                case 6:
                    color = Color.Red;
                    break;
                case 7:
                    color = Color.Black;
                    break;
                case 8:
                    color = Color.Yellow;
                    break;
                case 9:
                    color = Color.Violet;
                    break;
                case 10:
                    color = Color.MistyRose;
                    break;
                case 11:
                    color = Color.Aqua;
                    break;
            }
        }
    }
}
