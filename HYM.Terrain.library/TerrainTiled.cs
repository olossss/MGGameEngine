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
    public class TerrainTiled
    {
        Color[] data;
        public  Color[] Data{ get { return data; }  }
        Vector3 _position;//做上角位置
        //Texture2D texture;
        //public Texture2D Texture { get { return texture; } }
        public VertexPositionNormalTexture[] Vertices;
        public VertexPositionNormalTexture this[int index]
        {
            get { return Vertices[index]; }
            set { Vertices[index] = value; }
        }
        int ID_x;
        int ID_y;
        public int Size { get; set; }
        public int _scale { get; set; }
        Noise mperlinNoise;
        public TerrainTiled(int x, int y, int size)
        {
            ID_x = x;
            ID_y = y;
            _position = new Vector3(x * (Size - 1) * _scale, 0, y * (Size - 1) * _scale);
            Size = size;
            int seed = 11; // Change to whatever
            int octaves = 6; // Number of layers of perlin noise (stick with 1 for now)
            double amplitude = 0.5; // affects world height (default 4)
            double persistence = 0.6; // How much it stays at a particular height. Only has any affect when octaves > 1
            double frequency = 0.033; // Adjust for mountains/hills/plains (default 0.01)
            mperlinNoise = new Noise(persistence, frequency, amplitude, octaves, seed);
            data = new Color[Size * Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    float n = Math.Abs((float)mperlinNoise.Get2D(i + Size * ID_x, j + Size * ID_y));
                    float t = n - (float)Math.Floor(n);
                    //data[i + (j * 512)] = new Color((int)(255 * t), (int)(255 * t), (int)(255 * t));
                    data[i + (j * Size)] = new Color(t, t, t);
                }
            }
        }
        public void Erode(float smoothness) //侵蚀
        {
            for (int i = 1; i < Size - 1; i++)
            {
                for (int j = 1; j < Size - 1; j++)
                {
                    float d_max = 0.0f;
                    int[] match = { 0, 0 };

                    for (int u = -1; u <= 1; u++)
                    {
                        for (int v = -1; v <= 1; v++)
                        {
                            if (Math.Abs(u) + Math.Abs(v) > 0)
                            {
                                float d_i = (float)(data[i + j * Size].R - data[(i + u) + (j + v) * Size].R);
                                if (d_i > d_max)
                                {
                                    d_max = d_i;
                                    match[0] = u; match[1] = v;
                                }
                            }
                        }
                    }

                    if (0 < d_max && d_max <= (smoothness / (float)Size))
                    {
                        float d_h = 0.5f * d_max;
                        data[i + j * Size].R -= (byte)d_h;
                        data[i + j * Size].G -= (byte)d_h;
                        data[i + j * Size].B -= (byte)d_h;
                        //Heights[i + match[0], j + match[1]] += d_h;
                        data[i + match[0] + (j + match[1]) * Size].R += (byte)d_h;
                        data[i + match[0] + (j + match[1]) * Size].G += (byte)d_h;
                        data[i + match[0] + (j + match[1]) * Size].B += (byte)d_h;
                    }
                }
            }
        }
        int _vertexCount;
        int _topSize;
        int _halfSize;
        public void TreeVertexCollection(  int scale)
        {
            _scale = scale;
            _vertexCount = Size * Size;
            _topSize = Size - 1;
            _halfSize = _topSize / 2;
            Vertices = new VertexPositionNormalTexture[_vertexCount];
            BuildVertices();
            
        }
        private void BuildVertices()
        {
            //var heightMapColors = new Color[_vertexCount];
            var heightMapColors = data;
            //heightMap.GetData(heightMapColors);

            float x = _position.X;
            float z = _position.Z;
            float y = _position.Y;
            float maxX = x + _topSize;

            //for (int i = 0; i < Size; i++)
            //{
            //    for (int j = 0; j < Size; j++)
            //    {
            //        y = _position.Y + (heightMapColors[i].R / 5.0f);
            //        var vert = new VertexPositionNormalTexture(new Vector3(x * TiledSize, y * TiledSize, z * TiledSize), Vector3.Zero, Vector2.Zero);
            //        vert.TextureCoordinate = new Vector2((vert.Position.X - _position.X) / (Size - 1), (vert.Position.Z - _position.Z) / (Size - 1));
            //        Vertices[i + (j * Size)] = vert;
            //    }
            //}
            for (int i = 0; i < _vertexCount; i++)
            {
                if (x > maxX)
                {
                    x = _position.X;
                    z++;
                }

                y = _position.Y + (heightMapColors[i].R / 5.0f);
                var vert = new VertexPositionNormalTexture(new Vector3(x * _scale, y * _scale, z * _scale), Vector3.Zero, Vector2.Zero);
                vert.TextureCoordinate = new Vector2((vert.Position.X - _position.X) / _topSize, (vert.Position.Z - _position.Z) / _topSize);
                Vertices[i] = vert;
                x++;
            }
        }
        private void CalculateAllNormals()
        {
            if (_vertexCount < 9)
                return;

            int i = _topSize + 2, j = 0, k = i + _topSize;

            for (int n = 0; i <= (_vertexCount - _topSize) - 2; i += 2, n++, j += 2, k += 2)
            {

                if (n == _halfSize)
                {
                    n = 0;
                    i += _topSize + 2;
                    j += _topSize + 2;
                    k += _topSize + 2;
                }

                //Calculate normals for each of the 8 triangles
                SetNormals(i, j, j + 1);
                SetNormals(i, j + 1, j + 2);
                SetNormals(i, j + 2, i + 1);
                SetNormals(i, i + 1, k + 2);
                SetNormals(i, k + 2, k + 1);
                SetNormals(i, k + 1, k);
                SetNormals(i, k, i - 1);
                SetNormals(i, i - 1, j);
            }
        }

        private void SetNormals(int idx1, int idx2, int idx3)
        {
            if (idx3 >= Vertices.Length)
                idx3 = Vertices.Length - 1;

            var normal = Vector3.Cross(Vertices[idx2].Position - Vertices[idx1].Position, Vertices[idx1].Position - Vertices[idx3].Position);
            normal.Normalize();
            Vertices[idx1].Normal += normal;
            Vertices[idx2].Normal += normal;
            Vertices[idx3].Normal += normal;
        }
    }
}
