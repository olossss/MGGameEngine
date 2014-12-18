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
using HYM.System.library;

namespace GameSystem
{
    public class Game1 : Game
    {
        ////GraphicsDeviceManager graphics;
        //SpriteBatch spriteBatch;
        //private GraphicsDeviceManager _graphics;
        //GraphicsDevice _device;

        //private Texture2D grassTexture;
        //private float[,] heightData;
        //private VertexBuffer terrainVertexBuffer;
        //private IndexBuffer terrainIndexBuffer;
        //private VertexPositionColor[] verts;
        //int width;
        //int height;
        //Camera3DComponents camera;
        //public Game1()
        //{

        //    _graphics = new GraphicsDeviceManager(this);
        //    _graphics.PreferMultiSampling = true;
        //    Content.RootDirectory = "Content";
        //    camera = new Camera3DComponents(this, new Vector3(0, 0.5f, 2), new Vector3(0, 0.5f, 0), Vector3.Up);
        //    Components.Add(camera);
        //    Components.Add(new TerrainManagerComponents(this));
        //}

       
       
        ///// <summary>
        ///// Allows the game to perform any initialization it needs to before starting to run.
        ///// This is where it can query for any required services and load any non-graphic
        ///// related content.  Calling base.Initialize will enumerate through any components
        ///// and initialize them as well.
        ///// </summary>
        //protected override void Initialize()
        //{
        //    // TODO: Add your initialization logic here
        //    //basicEffect = new BasicEffect(GraphicsDevice);

        //    base.Initialize();
        //}

        ///// <summary>
        ///// LoadContent will be called once per game and is the place to load
        ///// all of your content.
        ///// </summary>
        //protected override void LoadContent()
        //{
        //    // Create a new SpriteBatch, which can be used to draw textures.
        //    spriteBatch = new SpriteBatch(GraphicsDevice);
            
        //}

        ///// <summary>
        ///// UnloadContent will be called once per game and is the place to unload
        ///// all content.
        ///// </summary>
        //protected override void UnloadContent()
        //{
        //    // TODO: Unload any non ContentManager content here
        //}

        ///// <summary>
        ///// Allows the game to run logic such as updating the world,
        ///// checking for collisions, gathering input, and playing audio.
        ///// </summary>
        ///// <param name="gameTime">Provides a snapshot of timing values.</param>
        //protected override void Update(GameTime gameTime)
        //{
        //    // Allows the game to exit
        //    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
        //        this.Exit();

            
        //    // TODO: Add your update logic here

        //    base.Update(gameTime);
        //}

        ///// <summary>
        ///// This is called when the game should draw itself.
        ///// </summary>
        ///// <param name="gameTime">Provides a snapshot of timing values.</param>
        //protected override void Draw(GameTime gameTime)
        //{
        //    GraphicsDevice.Clear(Color.CornflowerBlue);
        //    GraphicsDevice.RasterizerState = new RasterizerState { CullMode = CullMode.None };

            
        //    base.Draw(gameTime);
        //}
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        VertexPositionColor[] vertices;
        VertexDeclaration vertexDeclaration;
        BasicEffect basicEffect;
        int[] Indices;

        Matrix worldMatrix;
        Matrix viewMatrix;
        Matrix projectionMatrix;

        private float[,] heightData;
        Camera3DComponents camera;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            camera = new Camera3DComponents(this, new Vector3(-3.2f, 0.0f, 5.0f), Vector3.Zero, Vector3.Up);
            Components.Add(camera);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        private void InitVertices()
        {
            //viewMatrix = Matrix.CreateLookAt(
            //    new Vector3(0.0f, 0.0f, 1.0f),
            //    Vector3.Zero,
            //    Vector3.Up
            //    );

            //projectionMatrix = Matrix.CreateOrthographicOffCenter(
            //    0,
            //    (float)GraphicsDevice.Viewport.Width,
            //    (float)GraphicsDevice.Viewport.Height,
            //    0,
            //    1.0f, 1000.0f);
            viewMatrix = camera.view;
            projectionMatrix = camera.projection;
            vertexDeclaration = new VertexDeclaration(new VertexElement[]
                {
                    new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
                    new VertexElement(12, VertexElementFormat.Color, VertexElementUsage.Color, 0)
                }
            );
            basicEffect = new BasicEffect(GraphicsDevice);
            basicEffect.VertexColorEnabled = true;

            worldMatrix = Matrix.Identity;
            basicEffect.World = worldMatrix;
            basicEffect.View = viewMatrix;
            basicEffect.Projection = projectionMatrix;

            //lightDirection.Normalize();
            //basicEffect.DirectionalLight0.Direction = lightDirection;
            basicEffect.DirectionalLight0.Enabled = true;
            basicEffect.AmbientLightColor = new Vector3(0.3f, 0.3f, 0.3f);
            basicEffect.DirectionalLight1.Enabled = false;
            basicEffect.DirectionalLight2.Enabled = false;
            basicEffect.SpecularColor = new Vector3(0, 0, 0);
        }
        private void LoadHeightData(Texture2D heightMap)
        {
            float minimumHeight = 255;
            float maximumHeight = 0;

            int width = heightMap.Width;
            int height = heightMap.Height;

            Color[] heightMapColors = new Color[width * height];
            heightMap.GetData<Color>(heightMapColors);

            heightData = new float[width, height];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    heightData[x, y] = heightMapColors[x + y * width].R;
                    if (heightData[x, y] < minimumHeight) minimumHeight = heightData[x, y];
                    if (heightData[x, y] > maximumHeight) maximumHeight = heightData[x, y];
                }

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    heightData[x, y] = (heightData[x, y] - minimumHeight) / (maximumHeight - minimumHeight) * 30.0f;
        }
        private VertexPositionColor[] CreateTerrainVertices()
        {
            int width = heightData.GetLength(0);
            int height = heightData.GetLength(1);
            VertexPositionColor[] terrainVertices = new VertexPositionColor[width * height];

            int i = 0;
            for (int z = 0; z < height; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    Vector3 position = new Vector3(x, heightData[x, z], -z);

                    terrainVertices[i++] = new VertexPositionColor(position, Color.White);
                }
            }

            return terrainVertices;
        }
        private int[] CreateTerrainIndices()
        {
            int width = heightData.GetLength(0);
            int height = heightData.GetLength(1);

            int[] terrainIndices = new int[(width) * 2 * (height - 1)];

            int i = 0;
            int z = 0;
            while (z < height - 1)
            {
                for (int x = 0; x < width; x++)
                {
                    terrainIndices[i++] = x + z * width;
                    terrainIndices[i++] = x + (z + 1) * width;
                }
                z++;

                if (z < height - 1)
                {
                    for (int x = width - 1; x >= 0; x--)
                    {
                        terrainIndices[i++] = x + (z + 1) * width;
                        terrainIndices[i++] = x + z * width;
                    }
                }
                z++;
            }

            return terrainIndices;
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            InitVertices();
            Texture2D heightMap = Content.Load<Texture2D>("heightmap");
            LoadHeightData(heightMap);
            vertices = CreateTerrainVertices();
            Indices = CreateTerrainIndices();
            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            int width = heightData.GetLength(0);
            int height = heightData.GetLength(1);

            // TODO: Add your drawing code here
            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                for (int i = 0; i < vertices.Length; i++)
                    vertices[i].Color = Color.Aqua;

                GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(
                    PrimitiveType.TriangleStrip,
                    vertices,
                    0,  // vertex buffer offset to add to each element of the index buffer
                    width * height,  // number of vertices to draw
                    Indices,
                    0,  // first index element to read
                    width * 2 * (height - 1) - 2   // number of primitives to draw
                );
                //for (int i = 0; i < vertices.Length; i++)
                //    vertices[i].Color = Color.White;


                // GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(
                //PrimitiveType.TriangleList,
                //vertices,
                //0,   // vertex buffer offset to add to each element of the index buffer
                //width * height,   // number of vertices to draw
                //Indices,
                //0,   // first index element to read
                //width * 2 * (height - 1) - 2 
                //// number of primitives to draw
                //  );

            }
            base.Draw(gameTime);
        }
    }
}
