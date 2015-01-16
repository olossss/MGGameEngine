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

namespace HYM.Terrain.library
{
    public class Terrain
    {
        Color[] data;
        public Vector2 Order;//引索   做上角位置  X   -Z
        Noise PerlinNoise;
        public int Size { get; set; }
        public int Scale { get; set; }
        public Terrain(Vector2 order, Noise perlinNoise, int size, int scale)
        {
            Order = order;
            PerlinNoise = perlinNoise;
        }
        private VertexPositionNormalTexture[] CreateTerrainVertices()
        {
            VertexPositionNormalTexture[] terrainVertices = new VertexPositionNormalTexture[Size * Size];

            int i = 0;
            for (int z = 0; z < Size; z++)
            {
                for (int x = 0; x < Size; x++)
                {
                    float n = Math.Abs((float)PerlinNoise.Get2D(x + Size * ID_x, j + Size * ID_y));
                    float t = n - (float)Math.Floor(n);
                    Vector3 position = new Vector3(x, heightData[x, z], -z);
                    Vector3 normal = new Vector3(0, 0, 1);
                    Vector2 texCoord = new Vector2((float)x / 30.0f, (float)z / 30.0f);
                    terrainVertices[i++] = new VertexPositionNormalTexture(position, normal, texCoord);
                }
            }

            return terrainVertices;
        }
        /// <summary>
        /// 更新
        /// </summary>
        public  void Update(GameTime gameTime)
        {
            //this.entityWorld.Update();
        }
        /// <summary>
        /// 绘制
        /// </summary>
        public  void Draw(GameTime gameTime)
        {
            //this.entityWorld.Draw();
        }
    }
}
