using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ScreenManager
{
    class Screen1 : Screen
    {
        public Screen1(GraphicsDevice device) :base(device,"screen1")
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
            _device.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            // Check if m is pressed and go to screen2
            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                SCREEN_MANAGER.goto_screen("screen2");
            }
            base.Update(gameTime);
        }
    }
}
