using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Artemis.Interface;
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HYM.UI.library
{
    public class MouseStateComponent : IComponent
    {
        public MouseStateComponent()
        {
        }
        public MouseState CurrentMouseState { get; set; }
        public MouseState LastMouseState { get; set; }
    }
    public class MouseComponent : IComponent
    {
        public MouseComponent()
            : this(false, null)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="SpatialFormComponent" /> class.</summary>
        /// <param name="spatialFormFile">The spatial form file.</param>
        public MouseComponent(bool MouseX, Texture2D modelComponentFile)
        {
            this.Mouse = MouseX;
            this.ModelComponentFile = modelComponentFile;
        }

        /// <summary>Gets or sets the spatial form file.</summary>
        /// <value>The spatial form file.</value>
        public bool Mouse { get; set; }
        public Texture2D ModelComponentFile { get; set; }
    }
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Draw, Layer = 9)]
    public class MouseRander : EntityComponentProcessingSystem<MouseComponent>
    {
        /// <summary>The content manager.</summary>
        private ContentManager contentManager;

        /// <summary>The spatial name.</summary>
        //private string spatialName;

        /// <summary>The sprite batch.</summary>
        private SpriteBatch spriteBatch;

        //private SpriteFont font;
        /// <summary>Override to implement code that gets executed when systems are initialized.</summary>
        public override void LoadContent()
        {
            this.spriteBatch = EntitySystem.BlackBoard.GetEntry<SpriteBatch>("SpriteBatch");
        }
        public override void Process(Entity entity, MouseComponent mouseComponent)
        {
            if (mouseComponent.Mouse != false)
            {

                MouseState mouseState = Mouse.GetState();
                Texture2D backgroundTexture = mouseComponent.ModelComponentFile;
                int m_X, m_Y;

                if (mouseState.Position.X < 0)
                {
                    m_X = 0;
                }
                else if (mouseState.Position.X > this.spriteBatch.GraphicsDevice.Viewport.Width)
                {
                    m_X = this.spriteBatch.GraphicsDevice.Viewport.Width;
                }
                else
                {
                    m_X = mouseState.Position.X;
                }
                if (mouseState.Position.Y < 0)
                {
                    m_Y = 0;
                }
                else if (mouseState.Position.Y > this.spriteBatch.GraphicsDevice.Viewport.Height)
                {
                    m_Y = this.spriteBatch.GraphicsDevice.Viewport.Height;
                }
                else
                {
                    m_Y = mouseState.Position.Y;
                }
                this.spriteBatch.Draw(backgroundTexture, new Vector2(m_X, m_Y), Color.White);
            }
        }
    }
}
