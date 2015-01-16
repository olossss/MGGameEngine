#region Using Statements
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
using Artemis;
using Artemis.System;
using HYM.UI.library;
using Myko.Xna.Animation;
using HYM.System.library;
using HYM.Terrain.library;
#endregion

namespace GameSystem
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameEngine : Game
    {
        GraphicsDeviceManager graphics;

        SpriteBatch sprites;
        SpriteFont font;
       
        Camera3DComponents camera;
        GameTime Updatetime;
        GameTime Drawtime;
        //public static GameSystem.PluginServices Plugins = new PluginServices();
        public GameEngine()
            : base()
        {
           // this.elapsedTime = TimeSpan.Zero;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferMultiSampling = true;
            Content.RootDirectory = "Content";
            IsMouseVisible = false;//true;//显示鼠标
            this.IsFixedTimeStep = false;//可以让XNA不按固定时间间隔调用Update方法
            camera = new Camera3DComponents(this, new Vector3(0, 0.5f, 2), new Vector3(0, 0.5f, 0), Vector3.Up);
            Components.Add(camera);
            Components.Add(new UIManager(this));
            Components.Add(new SceneManagerComponents(this));
            
            Components.Add(new TerrainComponents(this));
            //加载插件
            PluginManager.Plugins.FindPlugins("Plugins");
            foreach (Types.AvailablePlugin pluginOn in PluginManager.Plugins.AvailablePlugins)
            {
                //TreeNode newNode = new TreeNode(pluginOn.Instance.Name);
                //this.tvwPlugins.Nodes.Add(newNode);
                //newNode = null;
            }
        }
        protected override void Initialize()
        {
            //graphics.ToggleFullScreen();
            //Updatetime = new GameTime();
            Window.Title = "game";
            sprites = new SpriteBatch(graphics.GraphicsDevice);
            font = Content.Load<SpriteFont>("SpriteFont1");
            EntitySystem.BlackBoard.SetEntry("ContentManager", this.Content);
            EntitySystem.BlackBoard.SetEntry("GraphicsDevice", this.GraphicsDevice);
            EntitySystem.BlackBoard.SetEntry("SpriteFont", this.font);
            EntitySystem.BlackBoard.SetEntry("EnemyInterval", 500);
            EntitySystem.BlackBoard.SetEntry("camera", this.camera);
            

            base.Initialize();
        }
        protected override void LoadContent()
        {

            SCREEN_MANAGER.add_screen(new Screen1());
            SCREEN_MANAGER.add_screen(new Screen2());
            SCREEN_MANAGER.add_screen(new StarScreen());
            SCREEN_MANAGER.goto_screen("StarScreen");
            SCREEN_MANAGER.Init();
            GameEvent.Quit += new HYM.System.library.System(this.Quit);

            base.LoadContent();
        }
        protected override void UnloadContent()
        {
            base.UnloadContent();
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Updatetime = gameTime;
            EntitySystem.BlackBoard.SetEntry("Updatetime", this.Updatetime);
            //PluginManager.Plugins.Update(gameTime);
            base.Update(gameTime);

            
        }
         
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Green, 1, 0);
            Drawtime = gameTime;
            EntitySystem.BlackBoard.SetEntry("Drawtime", this.Drawtime);
            //PluginManager.Plugins.Draw(gameTime);
            base.Draw(gameTime);
            
        }
        private void Quit()
        {
            Exit();
        }
    }
}
