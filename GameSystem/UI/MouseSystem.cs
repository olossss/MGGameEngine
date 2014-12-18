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
using GameSystem.Components;

namespace GameSystem.UI
{
    class MouseSystem : UIBase
    {
        //private Entity e;
        protected bool Display;
        public virtual bool MouseDisplay
        {
            get { return Display; }
            set
            {
                Display = value;
                e.GetComponent<MouseComponent>().Mouse = value;
            }
        }
        /// 鼠标图片
        /// </summary>
        protected Texture2D Picture;
        /// <summary>
        /// 获取或设置鼠标图片
        /// </summary>
        public virtual Texture2D MousePicture
        {
            get { return Picture; }
            set
            {
                Picture = value;
                e.GetComponent<MouseComponent>().ModelComponentFile = value;
            }
        }
        public MouseSystem()
            : this(true)
        {

        }
        public MouseSystem(bool t)
        {
            Texture2D m = this.contentManager.Load<Texture2D>("cursor");
            e = UIManager.CreateEntity();
            e.AddComponent(new MouseComponent(t, m));
        }
    }
}
