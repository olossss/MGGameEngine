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
using HYM.System.library;

namespace HYM.UI.library
{
    //public delegate void UI();        //声明一个代理类

    public class Button : UIBase 
    {
        

        public Texture2D ImageFile;
        /// <summary>
        /// Path to the default background image.
        /// </summary>
        private string backgroundImage;
        public string BackgroundImage
        {
            set
            {
                this.backgroundImage = value;
                e.GetComponent<ImageComponents>().ImageFile = this.contentManager.Load<Texture2D>(value);
            }
        }
        private string pressImage;
        public string PressImage
        {
            set
            {
                this.pressImage = value;
                e.GetComponent<ImageComponents>().PreesImageFile = this.contentManager.Load<Texture2D>(value);
            }
        }
        private string mouseImage;
        public string MouseImage
        {
            set
            {
                this.mouseImage = value;
                e.GetComponent<ImageComponents>().MouseImageFile = this.contentManager.Load<Texture2D>(value);
            }
        }
        
        
        public Button()
            : this("Button" )
        {

        }
         public  Button(string name)
        {
            ImageFile = this.contentManager.Load<Texture2D>("button-1");
            e = UIManager.CreateEntity();
            e.AddComponent(new NameComponent(name));
            e.AddComponent(new RectangleComponents(position));
            e.AddComponent(new TextComponent(text));
            e.AddComponent(new ImageComponents(ImageFile, ImageFile, ImageFile));
            e.AddComponent(new MouseStateComponent()); 
        }

    }
    
}
