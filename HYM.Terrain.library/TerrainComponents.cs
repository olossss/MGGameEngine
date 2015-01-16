using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using HYM.System.library;
using Artemis;
using Artemis.System;

namespace HYM.Terrain.library
{
    public class TerrainComponents : DrawableGameComponent
    {
        private GraphicsDevice device;
        SpriteBatch sprites;
        public BasicEffect Effect;
        //Terrain m_Terrain;
        // private SpriteFont font;
        private EntityWorld entityWorld;
        NoiseField<float> perlinNoise;
        Texture2D noiseTexture;
        SpriteFont font;
        public TerrainComponents(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            device = Game.GraphicsDevice;
            Effect = new BasicEffect(device);
            sprites = new SpriteBatch(device);
            this.entityWorld = new EntityWorld();

#if XBOX
                        this.entityWorld.InitializeAll( System.Reflection.Assembly.GetExecutingAssembly());
#else
            this.entityWorld.InitializeAll(true);
#endif

            base.Initialize();
        }
        Texture2D texture;
        Texture2D texture1;
        public void GenerateNoiseTexture()
        {
            PerlinNoiseGenerator gen = new PerlinNoiseGenerator();
            gen.OctaveCount = 7;
            gen.Persistence = .55f;
            // gen.Random = new Random(51);
            // gen.Interpolation = InterpolationAlgorithms.CosineInterpolation;
            gen.Interpolation = InterpolationAlgorithms.LinearInterpolation;

            perlinNoise = gen.GeneratePerlinNoise(256, 256);
            CustomGradientColorFilter filter = new CustomGradientColorFilter();
            Texture2DTransformer transformer = new Texture2DTransformer(device);

            filter.AddColorPoint(0.0f, 0.40f, Color.Blue);
            filter.AddColorPoint(0.4f, 0.50f, Color.LawnGreen);
            filter.AddColorPoint(0.50f, 0.70f, Color.LimeGreen);
            filter.AddColorPoint(0.70f, 0.90f, Color.SaddleBrown);
            filter.AddColorPoint(0.90f, 1.00f, Color.White);

            noiseTexture = transformer.Transform(filter.Filter(perlinNoise));
            /////////////////////////////////
            HeightMap h = new HeightMap(512);
            h.SetRandon(8);
            h.AddPerlinNoise(8.0f);
            h.Perturb(32.0f, 32.0f);//扰动
            for (int i = 0; i < 10; i++)
            {
                h.Erode(6.0f);//侵蚀
            }
            h.Smoothen();//平滑
            texture = new Texture2D(device, 256, 256);
            texture1 = new Texture2D(device, 256, 256);

            TerrainTiled m_TerrainTiled = new TerrainTiled(115, 17, 256);
            for (int i = 0; i < 10; i++)
            {
                m_TerrainTiled.Erode(116.0f);
            }
            TerrainTiled m_TerrainTiled1 = new TerrainTiled(116, 17, 256);
            ///////////////////////////////////////////////////////////////////////
            texture.SetData<Color>(m_TerrainTiled.Data);
            texture1.SetData<Color>(m_TerrainTiled1.Data);
        }
        protected override void LoadContent()
        {
            //m_Terrain = new Terrain(12,12,65,10);
            font = Game.Content.Load<SpriteFont>("SpriteFont1");
            GenerateNoiseTexture();
            ////////////////////////////////////
            Effect = new BasicEffect(device);

            Effect.EnableDefaultLighting();
            TerrainManager.add_Terrain(new Vector2( 256,256));
            ////////////////////////////////////
        }
        bool generated = false;
        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.R) && !generated)
            {
                GenerateNoiseTexture();
                generated = true;
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.R))
            {
                generated = false;
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            //sprites.Begin();
            //sprites.Draw(noiseTexture, new Vector2(0, 0), Color.White);
            //sprites.Draw(texture, new Vector2(256, 0), Color.Aquamarine);
            //sprites.Draw(texture1, new Vector2(512, 0), Color.Aquamarine);
            //sprites.End();
            //foreach (var pass in Effect.CurrentTechnique.Passes)
            //{
            //    pass.Apply();
            //    device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, m_Terrain.Vertices.Length, 0, m_Terrain.Size * 2 * (m_Terrain.Size - 1) - 2);
            //}
          // Camera3DComponents camera =  EntitySystem.BlackBoard.GetEntry<Camera3DComponents>("camera");

        }
    }
}
