using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Artemis;
using Artemis.System;

namespace HYM.UI.library
{
    public class UIManager : DrawableGameComponent
    {
        private GraphicsDevice device;
        private EntityWorld m_UIManager;
        public SpriteBatch spritesBatch;
        private SpriteFont font;
        public UIManager(Game game)
            : base(game)
        {
            this.DrawOrder = 100;
        }

        public override void Initialize()
        {
            device = Game.GraphicsDevice;
            spritesBatch = new SpriteBatch(device);
            this.m_UIManager = new EntityWorld();

            EntitySystem.BlackBoard.SetEntry("SpriteBatch", this.spritesBatch);
            EntitySystem.BlackBoard.SetEntry("UIManager", this.m_UIManager);
            this.font = EntitySystem.BlackBoard.GetEntry<SpriteFont>("SpriteFont");
#if XBOX
            this.UIWorld.InitializeAll( System.Reflection.Assembly.GetExecutingAssembly());
#else
            this.m_UIManager.InitializeAll(true);
#endif
            base.Initialize();
        }
        protected override void LoadContent()
        {


        }
        public override void Update(GameTime gameTime)
        {
            this.m_UIManager.Update();
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spritesBatch.Begin();
            this.m_UIManager.Draw();
            //this.spritesBatch.DrawString(this.font, "中哈哈赶紧回家感来家具有一条短信都不能看ijihugytfdesrf觉文输入测试", new Vector2(100, 100), Color.White);
            spritesBatch.End();

            base.Draw(gameTime);
            //draw billboards . . .
        }
    }
}
