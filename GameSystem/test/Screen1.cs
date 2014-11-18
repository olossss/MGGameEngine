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
using GameSystem.Components;
using GameSystem.UI;
using GameSystem.Event;

namespace GameSystem
{
    class Screen1 : Screen
    {
        
        bool go;
        public Screen1()
            : base( "screen1")
        {
            
        }
        
        public override bool Init()
        {
            go = false;
            Button button2 = new Button()
            {
                Name = "button-2",
                Position = new Rectangle(200, 370, 250, 50),
                BackgroundImage = "button-1",
                PressImage = "button-1-R",
                Text = "退出",
            };
            Button button1 = new Button()
            {
                Name = "button-1",
                Position = new Rectangle(200, 300, 250, 50),
                BackgroundImage = "button-1",
                PressImage = "button-1-R",
                Text = "确定",
            };
            
            GameEvent.Button_Click += new Event.UI(this.Button_Click);
            MouseSystem m_MouseSystem = new MouseSystem();
           
            return base.Init();
        }
        public void Button_Click(string Name)
        {
            if (Name == "button-1")
            {
                go = true;
            }
            if (Name == "button-2")
            {
                GameEvent.Event_Quit();
            }
        }
        public override void Shutdown()
        {
            base.Shutdown();
        }

        public override void Draw(GameTime gameTime)
        {
            _device.Clear(Color.Maroon);
            DrawString("中哈哈赶紧回家感来家具有一条短信都不能看ijihugytfdesrf觉文输入测试", 50, 50);
            //base.sprites.DrawString(this.font, "每秒帧率", new Vector2(32, 82), Color.Yellow);
            base.Draw(gameTime);
        }
        private void DrawString(String str, int x, int y)
        {
            base.sprites.DrawString(font, str, new Vector2(x, y), Color.White);
        } 
        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                GameEvent.Event_Button_Click("button-1");
            }
            if(go == true)
            {
                SCREEN_MANAGER.goto_screen("screen2");
            }
            
            base.Update(gameTime);
        }
    }
}
