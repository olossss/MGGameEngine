using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HYM.Terrain.library
{
    public static class TerrainManager
    {
        /// <summary>
        /// 地形块列表
        /// </summary>
        static private List<Terrain> _Terrains = new List<Terrain>();
        /// <summary>
        /// 柏林噪声发生器
        /// </summary>
        static public Noise PerlinNoise;
        /// <summary>
        /// 设置柏林噪声发生器
        /// </summary>
        static public void perlinNoise(int seed, int octaves, float amplitude, float persistence, float frequency)
        {
            PerlinNoise = new Noise(persistence, frequency, amplitude, octaves, seed);
        }
        /// <summary>
        /// 设置柏林噪声发生器
        /// </summary>
        static public void perlinNoise(int seed)
        {
            PerlinNoise = new Noise(0.6, 0.033, 0.5, 6, seed);
        }
        /// <summary>
        /// 添加地形块
        /// </summary>
        static public void add_Terrain(Vector2 Order)
        {
            foreach (Terrain scr in _Terrains)
            {
                if (scr.Order == Order)
                {
                    return;
                }
            }
            Terrain terrain = new Terrain(Order, PerlinNoise,256,30);
            _Terrains.Add(terrain);
        }
        /// <summary>
        /// 移除地形块
        /// </summary>
        static public void Remove_Terrain(Vector2 Order)
        {
            foreach (Terrain scr in _Terrains)
            {
                if (scr.Order == Order)
                {
                    _Terrains.Remove(scr);
                }
            }
        }
        /// <summary>
        /// 更新
        /// </summary>
        static public void Update(GameTime gameTime)
        {
            foreach (Terrain scr in _Terrains)
            {
                scr.Update(gameTime);
            }
        }
        /// <summary>
        /// 绘制
        /// </summary>
        static public void Draw(GameTime gameTime)
        {
            foreach (Terrain scr in _Terrains)
            {
                scr.Draw(gameTime);
            }
        }
    }
}
