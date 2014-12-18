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
using HYM.UI.library;
using Myko.Xna.Animation;
using GameSystem.GameEntity;
using HYM.System.library;

namespace GameSystem
{
    class Screen1 : Screen
    {
        
        bool go;
        public Screen1()
            : base( "screen1")
        {
            
        }
        GameTime time;
        public override bool Init()
        {
            go = false;
            time = new GameTime();
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

            GameEvent.Button_Click += new HYM.System.library.UI(this.Button_Click);
            MouseSystem m_MouseSystem = new MouseSystem();
            
           
            /////////////////////
            GameEntity.GameEntity t = new GameEntity.GameEntity()
            {
                Position = Vector3.One,
                Rotation = Vector2.One
            };

            GameEntity.GameEntity tt = new GameEntity.GameEntity()
            {
                Position = new Vector3(1, 1, 5),
                Rotation = new Vector2(0.6f,0.0f)
            };
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
            //_device.Clear(Color.Maroon);
            //DrawString("中哈哈赶紧回家感来家具有一条短信都不能看ijihugytfdesrf觉文输入测试", 50, 50);
            float tt = (float)this.time.TotalGameTime.TotalSeconds;
            DrawString(tt.ToString(), 50, 50);


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
            time = gameTime;
            base.Update(gameTime);
        }
    }
}
