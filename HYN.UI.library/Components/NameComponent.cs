using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Artemis.Interface;
using Microsoft.Xna.Framework;

namespace HYM.UI.library
{
    public class NameComponent : IComponent
    {
        public NameComponent() 
            : this(string.Empty)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="SpatialFormComponent" /> class.</summary>
        /// <param name="spatialFormFile">The spatial form file.</param>
        public NameComponent(string modelComponentFile)
        {
            this.Name = modelComponentFile;
        }

        /// <summary>Gets or sets the spatial form file.</summary>
        /// <value>The spatial form file.</value>
        public string Name { get; set; }
    }
}
