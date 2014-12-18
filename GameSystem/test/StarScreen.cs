using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using HYM.System.library;
using HYM.UI.library;

namespace GameSystem
{
    class StarScreen : Screen
    {
        //SpriteBatch sprites;
        HYM.System.library.GameTimer delayTimer = new HYM.System.library.GameTimer();
        private Texture2D backgroundTexture;
        public StarScreen()
            : base( "StarScreen")
        {
            this.backgroundTexture = contentManager.Load<Texture2D>("back");
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
            _device.Clear(Color.Aqua);
            base.sprites.Draw(backgroundTexture, _device.Viewport.Bounds, Color.Red);           
            base.Draw(gameTime);
        }
        private void DrawString(String str, int x, int y)
        {
           // base.sprites.DrawString(font, str, new Vector2(x, y), Color.White);
        } 
        public override void Update(GameTime gameTime)
        {
            delayTimer.Update(gameTime);
            if (delayTimer.TimeElapsed > 2000 )
            {
                SCREEN_MANAGER.goto_screen("screen1");
            }
            base.Update(gameTime);
        }
    }
}
