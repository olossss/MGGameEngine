using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using HYM.System.library;
using HYM.UI.library;

namespace GameSystem
{
    class Screen2 : Screen
    {
        bool go;
        public Screen2()
            : base( "screen2")
        {

        }

        public override bool Init()
        {
            go = false;
            Button button1 = new Button()
            {
                Name = "button-1",
                Position = new Rectangle(600, 200, 250, 50),
                BackgroundImage = "button-1",
                PressImage = "button-1-R",
                Text = "确定",
            };
            GameEvent.Button_Click += new HYM.System.library.UI(this.Button_Click);
            MouseSystem m_MouseSystem = new MouseSystem();
            return base.Init();
        }
        public void Button_Click(string Name)
        {
            if (Name == "button-1")
            {
                go = true;
            }
        }
        public override void Shutdown()
        {
            
            base.Shutdown();
        }

        public override void Draw(GameTime gameTime)
        {
            //_device.Clear(Color.Brown);
            DrawString("中哈哈赶紧回家感来家具有一条短信都不能看ijihugytfdesrf觉文输入测试", 50, 50);
            base.Draw(gameTime);
        }
        private void DrawString(String str, int x, int y)
        {
            base.sprites.DrawString(font, str, new Vector2(x, y), Color.White);
        } 
        public override void Update(GameTime gameTime)
        {
            // Check if n is pressed and go to screen2
            if (Keyboard.GetState().IsKeyDown(Keys.N))
            {
                SCREEN_MANAGER.goto_screen("screen1");
            }
            if (go == true)
            {
                SCREEN_MANAGER.goto_screen("screen1");
            }
            base.Update(gameTime);
        }
    }
}
