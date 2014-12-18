using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Artemis.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameSystem.Components
{
    public class ImageComponents : IComponent
    {
        public ImageComponents()
            //: this(Texture2D.Empty, Texture2D.Empty, Texture2D.Empty)
        {
        }
        public ImageComponents(Texture2D ImageFile)
        {
            this.ImageFile = ImageFile;
            this.PreesImageFile = ImageFile;
            this.MouseImageFile = ImageFile;
        }
        public ImageComponents(Texture2D ImageFile, Texture2D RImageFile)
        {
            this.ImageFile = ImageFile;
            this.PreesImageFile = RImageFile;
            this.MouseImageFile = ImageFile;
        }
        /// <summary>Initializes a new instance of the <see cref="SpatialFormComponent" /> class.</summary>
        /// <param name="spatialFormFile">The spatial form file.</param>
        public ImageComponents(Texture2D ImageFile, Texture2D RImageFile, Texture2D MImageFile)
        {
            this.ImageFile = ImageFile;
            this.PreesImageFile = RImageFile;
            this.MouseImageFile = MImageFile;
        }
        public Texture2D ImageFile { get; set; }
        public Texture2D PreesImageFile { get; set; }
        public Texture2D MouseImageFile { get; set; }
    }
    public class RectangleComponents : IComponent
    {
        public RectangleComponents()
            : this(new Rectangle(0,0,0,0))
        {

        }
        public RectangleComponents(Rectangle r)
        // : this(string.Empty, string.Empty, string.Empty)
        {
            this.X = r.X;
            this.Y = r.Y;
            this.Width = r.Width;
            this.Height = r.Height;
            this.RectangleFile = r;
        }
        public RectangleComponents(int x, int y, int width, int height)
        // : this(string.Empty, string.Empty, string.Empty)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.RectangleFile = new Rectangle(x,y,width,height);
        }
        public Rectangle RectangleFile { get; set; }
        public int X { 
            get { return RectangleFile.X;}
            set { this.RectangleFile = new Rectangle(X, Y, Width, Height); }
        }
        public int Y { 
            get { return RectangleFile.Y; } 
            set { this.RectangleFile = new Rectangle(X, Y, Width, Height); } }
        public int Width { 
            get { return RectangleFile.Width; }
            set { this.RectangleFile = new Rectangle(X, Y, Width, Height); }
        }
        public int Height {
            get { return RectangleFile.Height; }
            set { this.RectangleFile = new Rectangle(X, Y, Width, Height); }
        }
    }
}
