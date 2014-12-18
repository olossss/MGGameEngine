using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Artemis;
using Artemis.System;

namespace HYM.System.library
{
    public class Layer
    {
        public Layer()
         {

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
