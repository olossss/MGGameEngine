using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using Artemis;
using Artemis.System;

namespace GameSystem
{
    public  class Screen
    {
        protected GraphicsDevice _device = null;
        protected SpriteBatch sprites;
        protected EntityWorld entityWorld;
        protected EntityWorld UIManager;
        protected ContentManager contentManager;
        /// <summary>
        /// Screen Constructor
        /// </summary>
        /// <param name="name">Must be unique since when you use ScreenManager is per name</param>
        public Screen(string name)
        {
            Name = name;
            this._device = EntitySystem.BlackBoard.GetEntry<GraphicsDevice>("GraphicsDevice");
            this.font = EntitySystem.BlackBoard.GetEntry<SpriteFont>("SpriteFont");
            this.sprites = EntitySystem.BlackBoard.GetEntry<SpriteBatch>("Sprites");
            this.entityWorld = EntitySystem.BlackBoard.GetEntry<EntityWorld>("EntityWorld");
            this.UIManager = EntitySystem.BlackBoard.GetEntry<EntityWorld>("UIManager");
            this.contentManager  = EntitySystem.BlackBoard.GetEntry<ContentManager>("ContentManager");
        }

        ~Screen()
        {
            
        }

        public string Name
        {
            get;
            set;
        }

        public SpriteFont font
        {
            get;
            set;
        }
        public virtual bool Init()
        {
            
            return true;
        }
        public virtual void Shutdown()
        {
            UIManager.Clear();
            entityWorld.Clear();
        }
        public virtual void Update(GameTime gameTime)
        {
            //this.entityWorld.Update();
        }

        public virtual void Draw(GameTime gameTime)
        {
            //this.entityWorld.Draw();
        }
    }
}
