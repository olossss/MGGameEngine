﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameSystem.RandomGame
{
    public static class RulCol
    {
        #region Private Fields
        private static Dictionary<Hues, Dictionary<LuminosityTypes, Col>> _predefinedColors;
        private const float DEFAULT_MAX_VARIANCE = 0.15F;
        #endregion
        #region Public Methods
        /// <summary>
        /// Returns a completely random, opaque color
        /// </summary>
        public static Color RandColor()
        {
            return new Color(Rul.RandInt(255), Rul.RandInt(255), Rul.RandInt(255));
        }

        /// <summary>
        /// Returns a random, opaque color with the specified lightness
        /// </summary>
        /// <param name="lightness">Lightness between 0 and 255</param>
        /// <returns></returns>
        public static Color RandColor(int lightness)
        {
            return AdjustLightness(RandColor(), lightness);
        }

        /// <summary>
        /// Returns a color that looks similar to the specified base color
        /// </summary>
        /// <param name="baseColor">The base for the random color</param>
        /// <param name="maxRelativeVariance">A value between 0 and 1 specifying the maximum variance from the base color's RGB components</param>
        /// <returns></returns>
        public static Color RandColor(Color baseColor, float maxRelativeVariance)
        {
            return RandomizeColor(baseColor, maxRelativeVariance);
        }

        /// <summary>
        /// Returns a random color with the specified hue and luminosity
        /// </summary>
        /// <param name="hue">The approximate hue of the random color</param>
        /// <param name="luminosity">The approximate luminosity of the random color</param>
        public static Color RandColor(Hues hue, LuminosityTypes luminosity)
        {
            if (_predefinedColors == null)
                DefineColors();
            Color baseColor = GetPredefinedColor(hue, luminosity);
            return RandomizeColor(baseColor, DEFAULT_MAX_VARIANCE, hue == Hues.Monochrome);
        }

        /// <summary>
        /// Returns a random color with the specified hue and random luminosity
        /// </summary>
        /// <param name="hue">The approximate hue of the random color</param>
        public static Color RandColor(Hues hue)
        {
            LuminosityTypes luminosity = Rul.RandElement(LuminosityTypes.Light, LuminosityTypes.Medium, LuminosityTypes.Dark);          
            return RandColor(hue, luminosity);
        }

        /// <summary>
        /// Returns a color that is randomly interpolated between the given colors
        /// </summary>
        public static Color RandColorBetween(Color colA, Color colB)
        {
            float v = Rul.RandFloat();
            int r = (int)Math.Round(colA.R + (colB.R - colA.R) * v);
            int g = (int)Math.Round(colA.G + (colB.G - colA.G) * v);
            int b = (int)Math.Round(colA.B + (colB.B - colA.B) * v);
            int a = (int)Math.Round(colA.A + (colB.A - colA.A) * v);
            return new Color(r, g, b, a);
        }
        #endregion

        #region Private Methods
        private static Color AdjustLightness(Color baseColor, int lightness)
        {
            if (lightness >= 0 && lightness <= 255)
            {
                //Lightness is the average intensity of the color : (r+g+b) / 3
                lightness *= 3;

                float correction = 0;
                try
                {
                    correction = (float)lightness / (float)(baseColor.R + baseColor.G + baseColor.B);
                }
                catch (DivideByZeroException)
                {
                }

                //Rounding error is a feature, not a bug ... for now
                int r = Math.Min(255, (int)(baseColor.R * correction));
                int g = Math.Min(255, (int)(baseColor.G * correction));
                int b = Math.Min(255, (int)(baseColor.B * correction));
                return new Color(r, g, b);
            }
            else
                throw new ArgumentException("Value must be between 0 and 255");
        }

        private static void DefineColors()
        {
            Dictionary<LuminosityTypes, Col> red = new Dictionary<LuminosityTypes, Col>()
            {
                { LuminosityTypes.Light, new Col(255,0,0) },
                { LuminosityTypes.Medium, new Col(165,0,0) },
                { LuminosityTypes.Dark, new Col(100,0,0) }

            };
            Dictionary<LuminosityTypes, Col> green = new Dictionary<LuminosityTypes, Col>()
            {
                { LuminosityTypes.Light, new Col(0,255,0) },
                { LuminosityTypes.Medium, new Col(0,165,0) },
                { LuminosityTypes.Dark, new Col(0,75,0) }

            };
            Dictionary<LuminosityTypes, Col> blue = new Dictionary<LuminosityTypes, Col>()
            {
                { LuminosityTypes.Light, new Col(0,0,255) },
                { LuminosityTypes.Medium, new Col(0,0,175) },
                { LuminosityTypes.Dark, new Col(0,0,100) }

            };
            Dictionary<LuminosityTypes, Col> orange = new Dictionary<LuminosityTypes, Col>()
            {
                { LuminosityTypes.Light, new Col(255,160,0) },
                { LuminosityTypes.Medium, new Col(235,145,0) },
                { LuminosityTypes.Dark, new Col(200,110,0) }

            };
            Dictionary<LuminosityTypes, Col> yellow = new Dictionary<LuminosityTypes, Col>()
            {
                { LuminosityTypes.Light, new Col(255,255,0) },
                { LuminosityTypes.Medium, new Col(220,220,0) },
                { LuminosityTypes.Dark, new Col(200,200,0) }

            };
            Dictionary<LuminosityTypes, Col> cyan = new Dictionary<LuminosityTypes, Col>()
            {
                { LuminosityTypes.Light, new Col(0,255,255) },
                { LuminosityTypes.Medium, new Col(0,210,210) },
                { LuminosityTypes.Dark, new Col(0,165,165) }

            };
            Dictionary<LuminosityTypes, Col> purple = new Dictionary<LuminosityTypes, Col>()
            {
                { LuminosityTypes.Light, new Col(255,0,255) },
                { LuminosityTypes.Medium, new Col(210,0,210) },
                { LuminosityTypes.Dark, new Col(165,0,165) }

            };
            Dictionary<LuminosityTypes, Col> pink = new Dictionary<LuminosityTypes, Col>()
            {
                { LuminosityTypes.Light, new Col(255,192,203) },
                { LuminosityTypes.Medium, new Col(255,105,180) },
                { LuminosityTypes.Dark, new Col(255,20,147) }

            };
            Dictionary<LuminosityTypes, Col> monochrome = new Dictionary<LuminosityTypes, Col>()
            {
                { LuminosityTypes.Light, new Col(255,255,255) },
                { LuminosityTypes.Medium, new Col(127,127,127) },
                { LuminosityTypes.Dark, new Col(0,0,0) }

            };
            _predefinedColors = new Dictionary<Hues, Dictionary<LuminosityTypes, Col>>()
            {
                {Hues.Red, red},
                {Hues.Blue, blue},
                {Hues.Green, green},
                {Hues.Orange, orange},
                {Hues.Yellow, yellow},
                {Hues.Purple, purple},
                {Hues.Pink, pink},
                {Hues.Cyan, cyan},
                {Hues.Monochrome, monochrome},
            };

        }

        private static Color GetPredefinedColor(Hues hue, LuminosityTypes luminosity)
        {
            if (_predefinedColors.ContainsKey(hue) && _predefinedColors[hue].ContainsKey(luminosity))
            {
                Col rulCol = _predefinedColors[hue][luminosity];
                return new Color(rulCol.R, rulCol.G, rulCol.B, rulCol.A);
            }
            return new Color();
        }

        /// <summary>
        /// Returns a coefficient between 0 and 1 that determines how much variety will actually be permitted.
        /// This makes getting natural, appealing variants of colors easier.
        /// </summary>
        private static float GetVarianceFactor(int value)
        {
            //This method works best with values > 115
            if (value >= 125) 
                return (float)(value * value) / (255F * 255F);
            return 0.24F;
        }

        private static Color RandomizeColor(Color baseColor, float maxRelativeVariance, bool monochrome = false)
        {
            if (maxRelativeVariance >= 0 && maxRelativeVariance <= 1)
            {
                //Monochrome colors should stay monochrome
                if (!monochrome)
                {
                    float variance = Rul.RandFloat(maxRelativeVariance * GetVarianceFactor(baseColor.R));
                    int r = MathHelper.Clamp(baseColor.R + (int)(Rul.RandSign() * 255 * variance), 0, 255);
                    variance = Rul.RandFloat(maxRelativeVariance * GetVarianceFactor(baseColor.G));
                    int g = MathHelper.Clamp(baseColor.G + (int)(Rul.RandSign() * 255 * variance), 0, 255);
                    variance = Rul.RandFloat(maxRelativeVariance * GetVarianceFactor(baseColor.B));
                    int b = MathHelper.Clamp(baseColor.B + (int)(Rul.RandSign() * 255 * variance), 0, 255);
                    return new Color(r, g, b, baseColor.A);
                }
                else
                {
                    float variance = Rul.RandFloat(maxRelativeVariance * GetVarianceFactor(baseColor.R));
                    int newValue = MathHelper.Clamp(baseColor.R + (int)(Rul.RandSign() * 255 * variance), 0, 255);
                    return new Color(newValue, newValue, newValue, baseColor.A);
                }
            }
            else
                throw new ArgumentException("Value must be between 0 and 1");
        }
        #endregion
    }
}
