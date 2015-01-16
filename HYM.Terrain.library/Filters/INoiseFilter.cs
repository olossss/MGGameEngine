using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYM.Terrain.library
{
    public interface INoiseFilter<T, U>
    {
        /// <summary>
        /// Performs the filter.
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        NoiseField<U> Filter(NoiseField<T> field);
    }
}
