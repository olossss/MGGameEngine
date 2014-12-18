using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYM.System.library;
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
                    //GameTime time = EntitySystem.BlackBoard.GetEntry<GameTime>("Drawtime");
                    //float tt = (float)time.TotalGameTime.TotalSeconds;
                    //spriteBatch.DrawString(font, tt.ToString(), new Vector2(transformComponent.X - n * 18, transformComponent.Y - 8), Color.White);
                    spriteBatch.DrawString(font, textComponent.TextComponentFile, new Vector2(transformComponent.X - n * 18, transformComponent.Y - 8), Color.White);
                }
            }
        }
    }
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Draw, Layer = 3)]
    public class Render3DSystem : EntityComponentProcessingSystem<ModelComponent, SkeletonComponent, PositionComponent, RotationComponent>
    {
        public override void LoadContent()
        {
            
        }
        public override void Process(Entity entity, ModelComponent modelComponent, SkeletonComponent skeletonComponent, PositionComponent positionComponent, RotationComponent rotationComponent)
        {
            //skeletonComponent.SkeletonComponentFile.CopyModelBindpose(modelComponent.model);
            var view = Matrix.CreateLookAt(new Vector3(0, 0.5f, 2), new Vector3(0, 0.5f, 0), new Vector3(0, 1, 0));
            var projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, 16f / 9f, 1, 1000);
            var world = Matrix.Identity; // Matrix.CreateRotationY(MathHelper.PiOver2 + MathHelper.PiOver4 + (float)gameTime.TotalGameTime.TotalSeconds * 0.5f);

            //skeletonComponent.SetSkeleton("Die");
            GameTime gameTime = EntitySystem.BlackBoard.GetEntry<GameTime>("Drawtime");
            float time = (float)gameTime.TotalGameTime.TotalSeconds;
            
            var bones = skeletonComponent.SkeletonComponentFile.GetSkinTransforms(time).ToArray();
            //skeletonComponent.SkeletonComponentFile.loop = false;

            Camera3DComponents camera =  EntitySystem.BlackBoard.GetEntry<Camera3DComponents>("camera");

            Matrix[] transforms = new Matrix[modelComponent.model.Bones.Count];
            modelComponent.model.CopyAbsoluteBoneTransformsTo(transforms);
            foreach (var mesh in modelComponent.model.Meshes)
            {
                foreach (SkinnedEffect effect in mesh.Effects)
                {
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateRotationY(time)*Matrix.CreateTranslation(positionComponent.Position);
                    effect.View = camera.view;
                    effect.Projection = camera.projection;
                    effect.EnableDefaultLighting();
                    effect.SetBoneTransforms(bones);
                    //effect.GetBoneTransforms().
                    effect.PreferPerPixelLighting = true;
                    effect.SpecularPower = 800;
                }
                mesh.Draw();
            }
        }
    }
}
