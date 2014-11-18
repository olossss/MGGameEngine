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
using GameSystem.Components;
using GameSystem.Event;
using GameSystem.system;

namespace GameSystem.GameComponents
{
   
    class UIManagerComponent : DrawableGameComponent
    {
         private GraphicsDevice device;
         private EntityWorld m_UIManager;
         SpriteBatch spritesBatch;
         private SpriteFont font;
         public UIManagerComponent(Game game) : base(game)
         {
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
            spritesBatch.End();
            
            base.Draw(gameTime);
        //draw billboards . . .
        }
    }
}
