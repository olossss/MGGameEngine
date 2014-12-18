using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HYM.System.library
{
    public class GameTimer
    {
        protected long timeElapsed;

        public GameTimer()
        {
            timeElapsed = 0;
        }

        public void RestartTimer()
        {
            timeElapsed = 0;
        }
        public void Update(GameTime gameTime)
        {
            timeElapsed += gameTime.ElapsedGameTime.Milliseconds;
        }
        public long TimeElapsed
        {
            get { return timeElapsed; }
            set { timeElapsed = value; }
        } 
    }
}
