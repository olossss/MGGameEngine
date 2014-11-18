using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace ScreenManager
{
    class Screen2 : Screen
    {
        public Screen2(GraphicsDevice device)
            : base(device,"screen2")
        {

        }

        public override bool Init()
        {

            return base.Init();
        }

        public override void Shutdown()
        {
            base.Shutdown();
        }

        public override void Draw(GameTime gameTime)
        {
            _device.Clear(Color.Brown);
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            // Check if n is pressed and go to screen2
            if (Keyboard.GetState().IsKeyDown(Keys.N))
            {
                SCREEN_MANAGER.goto_screen("screen1");
            }
            base.Update(gameTime);
        }
    }
}