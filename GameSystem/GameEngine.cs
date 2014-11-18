#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Artemis;
using Artemis.System;
using GameSystem.Components;
using GameSystem.GameComponents;
using GameSystem.Event;
using GameSystem.system;
#endregion

namespace GameSystem
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameEngine : Game
    {
        GraphicsDeviceManager graphics;
        private EntityWorld entityWorld;
        private static readonly TimeSpan OneSecond = TimeSpan.FromSeconds(1);
        /// <summary>The frame counter.</summary>
        private int frameCounter;
        /// <summary>The frame rate.</summary>
        private int frameRate;
        /// <summary>The elapsed time.</summary>
        private TimeSpan elapsedTime;

        SpriteBatch sprites;
        private SpriteFont font;
        Model shipModel;
        public GameEngine()
            : base()
        {
            this.elapsedTime = TimeSpan.Zero;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;//true;//显示鼠标
            this.IsFixedTimeStep = false;//可以让XNA不按固定时间间隔调用Update方法
            Components.Add(new UIManagerComponent(this)); 
        }
        protected override void Initialize()
        {
            //graphics.ToggleFullScreen();
            Window.Title = "game";
            sprites = new SpriteBatch(graphics.GraphicsDevice);
            //////////////////////////////
            font = Content.Load<SpriteFont>("SpriteFont1");
            this.entityWorld = new EntityWorld();
            EntitySystem.BlackBoard.SetEntry("ContentManager", this.Content);
            EntitySystem.BlackBoard.SetEntry("GraphicsDevice", this.GraphicsDevice);
            EntitySystem.BlackBoard.SetEntry("Sprites", this.sprites);
            EntitySystem.BlackBoard.SetEntry("SpriteFont", this.font);
            EntitySystem.BlackBoard.SetEntry("EnemyInterval", 500);
            EntitySystem.BlackBoard.SetEntry("EntityWorld", this.entityWorld);
#if XBOX
            this.entityWorld.InitializeAll( System.Reflection.Assembly.GetExecutingAssembly());
#else
            this.entityWorld.InitializeAll(true);
#endif
            base.Initialize();
        }
        protected override void LoadContent()
        {
            shipModel = Content.Load<Model>("Ship");
            SCREEN_MANAGER.add_screen(new Screen1());
            SCREEN_MANAGER.add_screen(new Screen2());
            SCREEN_MANAGER.add_screen(new StarScreen());
            SCREEN_MANAGER.goto_screen("StarScreen");
            SCREEN_MANAGER.Init();
            GameEvent.Quit += new Event.System(this.Quit);
        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            SCREEN_MANAGER.Update(gameTime);
            this.entityWorld.Update();
            ++this.frameCounter;
            this.elapsedTime += gameTime.ElapsedGameTime;
            if (this.elapsedTime > OneSecond)
            {
                this.elapsedTime -= OneSecond;
                this.frameRate = this.frameCounter;
                this.frameCounter = 0;
            }
            base.Update(gameTime);

            
        }
         
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Green, 1, 0);
            
            string fps = string.Format("每秒帧率: {0}", this.frameRate);
            string fpss = string.Format("每秒帧率: {0}", this.frameCounter);
            
            this.sprites.Begin();
            this.entityWorld.Draw();
            SCREEN_MANAGER.Draw(gameTime);
            sprites.DrawString(this.font, fpss, new Vector2(32, 30), Color.Yellow);
            sprites.DrawString(this.font, fps, new Vector2(32, 82), Color.Yellow);
            this.sprites.End();

             //首先要求XNA Framework替我們清除螢幕成淺藍色的底色
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            //將模型放置在遊戲世界的中心
            Matrix world = Matrix.CreateTranslation(0.0f, 0.0f, 0.0f);
            //從離Z軸50單位的地方向著原點看
            Matrix view = Matrix.CreateLookAt(new Vector3(0.0f, 0.0f, 50.0f), Vector3.Zero, Vector3.Up);
            //將3D到2D螢幕的投影轉換
            Matrix projection = Matrix.CreatePerspectiveFieldOfView((float)Math.PI / 4.0f, 800.0f / 600.0f, 1.0f, 1000.0f);

            //宣告變數儲存機器人的骨骼資訊
            Matrix[] temprobot = new Matrix[shipModel.Bones.Count];
            shipModel.CopyAbsoluteBoneTransformsTo(temprobot);
            //將機器人的每個部位畫出來
            foreach (ModelMesh mesh in shipModel.Meshes)
            {
                // 這邊使用XNA提供的基本效果來繪製模型
                foreach (BasicEffect effect in mesh.Effects)
                {
                    // 使用XNA提供的打光方法，才不會一片漆黑
                    effect.EnableDefaultLighting();
                    // 將模型的位置傳給效果來繪製模型
                    effect.World = temprobot[mesh.ParentBone.Index] * world;
                    effect.View = view;
                    effect.Projection = projection;
                    // 實際把模型畫出來
                    mesh.Draw();
                }
            }

            base.Draw(gameTime);
        }
        private void DrawString(String str, int x, int y)
        {
            this.sprites.DrawString(font, str, new Vector2(x, y), Color.White);
        } 
        private void Quit()
        {
            Exit();
        }
    }
}
