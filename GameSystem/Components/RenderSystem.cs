using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameSystem.Components
{
    #region Using statements


    using Artemis;
    using Artemis.Attributes;
    using Artemis.Manager;
    using Artemis.System;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    #endregion

    [ArtemisEntitySystem(GameLoopType = GameLoopType.Draw, Layer = 3)]
    public class RenderUISystem : EntityComponentProcessingSystem<TextComponent,ImageComponents, RectangleComponents>
    {
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        MouseState mouseState = new MouseState();
        MouseState lastMouseState = new MouseState();
        /// <summary>Override to implement code that gets executed when systems are initialized.</summary>
        public override void LoadContent()
        {
            this.spriteBatch = EntitySystem.BlackBoard.GetEntry<SpriteBatch>("SpriteBatch");
            this.font = EntitySystem.BlackBoard.GetEntry<SpriteFont>("SpriteFont");
        }
        public override void Process(Entity entity, TextComponent textComponent, ImageComponents modelComponent, RectangleComponents transformComponent)
        {
            
            lastMouseState = mouseState;
            mouseState = Mouse.GetState();

            if (modelComponent != null)
            {
                if (transformComponent.X >= 0 &&
                    transformComponent.Y >= 0 &&
                    transformComponent.X < this.spriteBatch.GraphicsDevice.Viewport.Width &&
                    transformComponent.Y < this.spriteBatch.GraphicsDevice.Viewport.Height)
                {
                    Rectangle rect = new Rectangle(transformComponent.RectangleFile.X - (transformComponent.RectangleFile.Width /2),
                        transformComponent.RectangleFile.Y - (transformComponent.RectangleFile.Height /2),
                        transformComponent.RectangleFile.Width, transformComponent.RectangleFile.Height);
                     mouseState = Mouse.GetState();
                    if (rect.Contains(mouseState.X, mouseState.Y))
                    {
                        if (mouseState.LeftButton == ButtonState.Pressed  )
                        {
                                spriteBatch.Draw(modelComponent.PreesImageFile, rect, Color.Red);
                        }else
                        {
                                spriteBatch.Draw(modelComponent.MouseImageFile, rect, Color.Red);
                        }
                    }
                    else
                    {
                        spriteBatch.Draw(modelComponent.ImageFile, rect, Color.Red);
                    }
                }
            }
            if (textComponent != null)
            {
                if (transformComponent.X >= 0 &&
                    transformComponent.Y >= 0 &&
                    transformComponent.X < this.spriteBatch.GraphicsDevice.Viewport.Width &&
                    transformComponent.Y < this.spriteBatch.GraphicsDevice.Viewport.Height)
                {
                    int n = textComponent.TextComponentFile.Length/2;
                    spriteBatch.DrawString(font, textComponent.TextComponentFile, new Vector2(transformComponent.X - n * 18, transformComponent.Y - 8), Color.White);
                }
            }
        }
    }
}
