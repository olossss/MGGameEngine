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
//using GameSystem.Components;

namespace HYM.UI.library
{
    public class UIBase
    {
        protected EntityWorld entityWorld;
        protected EntityWorld UIManager;
        protected ContentManager contentManager;
        protected Entity e;
        public UIBase()
        {
            this.entityWorld = EntitySystem.BlackBoard.GetEntry<EntityWorld>("EntityWorld");
            this.UIManager = EntitySystem.BlackBoard.GetEntry<EntityWorld>("UIManager");
            this.contentManager = EntitySystem.BlackBoard.GetEntry<ContentManager>("ContentManager");
        }


        #region 属性
        /// <summary>
        /// 控件上显示的文字
        /// </summary>
        protected string text;
        /// <summary>
        /// 获取或设置按钮上的文字
        /// </summary>
        public virtual string Text
        {
            get { return text; }
            set
            {
                text = value;
                e.GetComponent<TextComponent>().TextComponentFile = value;
            }
        }
        /// <summary>
        /// 按钮文字颜色
        /// </summary>
        protected Color textColor;
        /// <summary>
        /// 获取或设置文字原始颜色
        /// </summary>
        public Color TextColor
        {
            get { return textColor; }
            set { textColor = value; }
        }
        /// <summary>
        /// 按钮文字高亮颜色
        /// </summary>
        protected Color textColorHighlight;
        /// <summary>
        /// 获取或设置文字高亮颜色
        /// </summary>
        public Color TextColorHighlight
        {
            get { return textColorHighlight; }
            set { textColorHighlight = value; }
        }
        /// <summary>
        /// 文字使用的字体
        /// </summary>
        protected SpriteFont font;
        /// <summary>
        /// 获取或设置文字使用的字体
        /// </summary>
        public virtual SpriteFont Font
        {
            get { return font; }
            set { font = value; }
        }

        /// <summary>
        /// 控件位置
        /// </summary>
        protected Rectangle position;
        /// <summary>
        /// 获取或设置位置
        /// </summary>
        public virtual Rectangle Position
        {
            get { return position; }
            set
            {
                position = value;
                e.GetComponent<RectangleComponents>().RectangleFile = value;
            }
        }
        /// 控件名字
        /// </summary>
        protected string name;
        /// <summary>
        /// 获取或设置按钮名字
        /// </summary>
        public virtual string Name
        {
            get { return name; }
            set
            {
                name = value;
                e.GetComponent<NameComponent>().Name = value;
            }
        }
        #endregion


    }
}
