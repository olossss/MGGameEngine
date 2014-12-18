using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Artemis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using HYM.UI.library;

namespace GameSystem.GameEntity
{
    class EntityBase 
    {
        protected EntityWorld entityWorld;
        protected ContentManager contentManager;
        protected Entity e;

        public EntityBase()
        {
            this.entityWorld = EntitySystem.BlackBoard.GetEntry<EntityWorld>("EntityWorld");
            this.contentManager = EntitySystem.BlackBoard.GetEntry<ContentManager>("ContentManager");
        }

    }
}
