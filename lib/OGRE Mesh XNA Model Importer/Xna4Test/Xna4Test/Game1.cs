using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Myko.Xna.Animation;

namespace Xna4Test
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Model model;
        BasicEffect basicEffect;
        Skeleton skeleton1;
        Skeleton skeleton2;
        Skeleton skeleton3;
        Skeleton skeleton4;
        Skeleton skeleton5;
        Skeleton skeleton6;
        Skeleton skeleton;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferMultiSampling = true;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            basicEffect = new BasicEffect(GraphicsDevice);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //model = Content.Load<Model>("Fiend\\FIEND.MESH");
            //skeleton1 = Content.Load<Skeleton>("Fiend\\FIEND.SKELETON");
            //skeleton1.CopyModelBindpose(model);
            //skeleton2 = Content.Load<Skeleton>("Fiend\\IDLE.SKELETON");
            //skeleton2.CopyModelBindpose(model);
            //model = Content.Load<Model>("HUM_M.MESH");
            //skeleton1 = Content.Load<Skeleton>("IDLE.SKELETON");
            //skeleton1.CopyModelBindpose(model);
            //skeleton2 = Content.Load<Skeleton>("RUN.SKELETON");
            //skeleton2.CopyModelBindpose(model);
            //model = Content.Load<Model>("robot.mesh");
            //skeleton1 = Content.Load<Skeleton>("robot.skeleton");
            //skeleton1.CopyModelBindpose(model);
            //skeleton2 = Content.Load<Skeleton>("robot.skeleton");
            //skeleton2.CopyModelBindpose(model);
            //model = Content.Load<Model>("dog\\dog.MESH");
            //skeleton1 = Content.Load<Skeleton>("dog\\attack1.SKELETON");
            //skeleton1.CopyModelBindpose(model);
            //skeleton2 = Content.Load<Skeleton>("dog\\attack2.SKELETON");
            //skeleton2.CopyModelBindpose(model);
            //skeleton3 = Content.Load<Skeleton>("dog\\attack3.SKELETON");
            //skeleton3.CopyModelBindpose(model);
            //skeleton4 = Content.Load<Skeleton>("dog\\fidget_alert.SKELETON");
            //skeleton4.CopyModelBindpose(model);
            //skeleton5 = Content.Load<Skeleton>("dog\\run.SKELETON");
            //skeleton5.CopyModelBindpose(model);
            //skeleton6 = Content.Load<Skeleton>("dog\\walk.SKELETON");
            //skeleton6.CopyModelBindpose(model);
            model = Content.Load<Model>("DragonspawnSpectral\\Dragonspawn.MESH");
            skeleton1 = Content.Load<Skeleton>("Dragonspawn\\Hit.SKELETON");
            skeleton1.CopyModelBindpose(model);
            skeleton2 = Content.Load<Skeleton>("Dragonspawn\\Run.SKELETON");
            skeleton2.CopyModelBindpose(model);
            //model.Meshes
            skeleton = skeleton1;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.F1))
                skeleton = skeleton1;
            if (keyboardState.IsKeyDown(Keys.F2))
                skeleton = skeleton2;
            //if (keyboardState.IsKeyDown(Keys.F3))
            //    skeleton = skeleton3;
            //if (keyboardState.IsKeyDown(Keys.F4))
            //    skeleton = skeleton4;
            //if (keyboardState.IsKeyDown(Keys.F5))
            //    skeleton = skeleton5;
            //if (keyboardState.IsKeyDown(Keys.F6))
            //    skeleton = skeleton6;

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.RasterizerState = new RasterizerState { CullMode = CullMode.None };

            var view = Matrix.CreateLookAt(new Vector3(0, 0.5f, 2), new Vector3(0.5f, 2.5f, 0.5f), new Vector3(0, 1, 0));
            var projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, 16f / 9f, 1, 1000);
            var world = Matrix.Identity; // Matrix.CreateRotationY(MathHelper.PiOver2 + MathHelper.PiOver4 + (float)gameTime.TotalGameTime.TotalSeconds * 0.5f);

            float time = (float)gameTime.TotalGameTime.TotalSeconds;

            var bones = skeleton.GetSkinTransforms(time).ToArray();

            foreach (var mesh in model.Meshes)
            {
                foreach (SkinnedEffect effect in mesh.Effects)
                {
                    effect.World = world;
                    effect.View = view;
                    effect.Projection = projection;
                    effect.EnableDefaultLighting();
                    effect.SetBoneTransforms(bones);
                    effect.PreferPerPixelLighting = true;
                    effect.SpecularPower = 800;
                    mesh.Draw();
                    effect.Texture = Content.Load<Texture2D>("DragonspawnSpectral\\dragonspawn");
                }
            }

            basicEffect.VertexColorEnabled = true;
            basicEffect.World = world;
            basicEffect.View = view;
            basicEffect.Projection = projection;
            basicEffect.Techniques[0].Passes[0].Apply();
            GraphicsDevice.DepthStencilState = new DepthStencilState { DepthBufferEnable = false };

            List<VertexPositionColor> vertices = new List<VertexPositionColor>();
            //bones.
            //foreach (var bone in skeleton.GetBoneTransforms(time))
            //{
            //    vertices.Add(new VertexPositionColor(bone.Translation, Color.Red));
            //    vertices.Add(new VertexPositionColor(bone.Translation + bone.Forward * 0.05f, Color.Red));
            //    vertices.Add(new VertexPositionColor(bone.Translation, Color.Red));
            //    vertices.Add(new VertexPositionColor(bone.Translation + bone.Right * 0.05f, Color.Red));
            //    vertices.Add(new VertexPositionColor(bone.Translation, Color.Red));
            //    vertices.Add(new VertexPositionColor(bone.Translation + bone.Up * 0.05f, Color.Red));
            //}

            //foreach (var bone in skeleton.GetBoneTransforms())
            //{
            //    vertices.Add(new VertexPositionColor(bone.Translation, Color.Blue));
            //    vertices.Add(new VertexPositionColor(bone.Translation + bone.Forward * 0.05f, Color.Blue));
            //    vertices.Add(new VertexPositionColor(bone.Translation, Color.Blue));
            //    vertices.Add(new VertexPositionColor(bone.Translation + bone.Right * 0.05f, Color.Blue));
            //    vertices.Add(new VertexPositionColor(bone.Translation, Color.Blue));
            //    vertices.Add(new VertexPositionColor(bone.Translation + bone.Up * 0.05f, Color.Blue));
            //}

            if (vertices.Any())
                GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, vertices.ToArray(), 0, vertices.Count / 2);

            GraphicsDevice.DepthStencilState = new DepthStencilState { DepthBufferEnable = true };

            base.Draw(gameTime);
        }
    }
}
