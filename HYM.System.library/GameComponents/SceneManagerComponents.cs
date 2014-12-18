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
//using Microsoft.Xna.Framework.Net;
using Artemis;
using Artemis.System;

namespace HYM.System.library
{
    public class SceneManagerComponents : DrawableGameComponent
    {
        private GraphicsDevice device;
        SpriteBatch sprites;
        // private SpriteFont font;
        private EntityWorld entityWorld;
        public SceneManagerComponents(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            device = Game.GraphicsDevice;
            sprites = new SpriteBatch(device);
            this.entityWorld = new EntityWorld();

            EntitySystem.BlackBoard.SetEntry("Sprites", this.sprites);
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

        }
        public override void Update(GameTime gameTime)
        {
            SCREEN_MANAGER.Update(gameTime);
            this.entityWorld.Update();
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            sprites.GraphicsDevice.Clear(Color.CornflowerBlue);
            device.RasterizerState = new RasterizerState { CullMode = CullMode.None };
            device.DepthStencilState = DepthStencilState.Default;
            sprites.Begin();
            this.entityWorld.Draw();
            SCREEN_MANAGER.Draw(gameTime);
            sprites.End();
            base.Draw(gameTime);
        }
    }
}
