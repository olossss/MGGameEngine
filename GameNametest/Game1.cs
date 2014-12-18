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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using RaisingStudio.Xna.Graphics;

namespace ScreenManager
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        DrawingBatch drawingBatch;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            SCREEN_MANAGER.add_screen(new Screen1(GraphicsDevice));
            SCREEN_MANAGER.add_screen(new Screen2(GraphicsDevice));

            SCREEN_MANAGER.goto_screen("screen1");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            drawingBatch = new DrawingBatch(GraphicsDevice);
            SCREEN_MANAGER.Init();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.F1))
                this.Exit();

            // Tell ScreenManager to Update
            SCREEN_MANAGER.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // Tell ScreenManager to draw
            SCREEN_MANAGER.Draw(gameTime);

            drawingBatch.Begin();
            drawingBatch.DrawLine(10, 20, 100, 20, Color.Red);
            drawingBatch.DrawRectangle(120, 10, 100, 20, Color.Blue);
            drawingBatch.DrawTriangle(240, 10, 240, 60, 200, 60, Color.Black);
            drawingBatch.DrawEllipse(310, 10, 50, 50, Color.Green);
            drawingBatch.DrawPolyline(new Vector2[] { new Vector2(410, 10), new Vector2(440, 10), new Vector2(420, 20), new Vector2(440, 40), new Vector2(410, 60) }, Color.Aqua);
            drawingBatch.DrawFilledRectangle(120, 110, 50, 0, Color.Blue);
            drawingBatch.DrawFilledTriangle(240, 110, 240, 160, 200, 160, Color.Brown);
            drawingBatch.DrawFilledEllipse(310, 110, 80, 40, Color.Green);
            drawingBatch.End();
            base.Draw(gameTime);
        }
    }
}
