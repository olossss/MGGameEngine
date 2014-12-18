using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Artemis.Interface;

namespace GameSystem.Components
{
    public class ModelComponent : IComponent
    {
        public ModelComponent() 
            : this(string.Empty)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="SpatialFormComponent" /> class.</summary>
        /// <param name="spatialFormFile">The spatial form file.</param>
        public ModelComponent(string modelComponentFile)
        {
            this.ModelComponentFile = modelComponentFile;
        }

        /// <summary>Gets or sets the spatial form file.</summary>
        /// <value>The spatial form file.</value>
        public string ModelComponentFile { get; set; }
    }
    public class TextComponent : IComponent
    {
        public TextComponent()
            : this(string.Empty)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="SpatialFormComponent" /> class.</summary>
        /// <param name="spatialFormFile">The spatial form file.</param>
        public TextComponent(string textComponentFile)
        {
            this.TextComponentFile = textComponentFile;
        }

        /// <summary>Gets or sets the spatial form file.</summary>
        /// <value>The spatial form file.</value>
        public string TextComponentFile { get; set; }
    }
}
