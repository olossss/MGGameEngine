using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Artemis.Interface;
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myko.Xna.Animation;

namespace HYM.UI.library
{
    public class ModelComponent : IComponent
    {
        public ModelComponent() 
            //: this(string.Empty)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="SpatialFormComponent" /> class.</summary>
        /// <param name="spatialFormFile">The spatial form file.</param>
        public ModelComponent(Model modelComponentFile)
        {
            this.model = modelComponentFile;
        }

        /// <summary>Gets or sets the spatial form file.</summary>
        /// <value>The spatial form file.</value>
        public Model model { get; set; }
    }
    public class SkeletonComponent : IComponent
    {
        public SkeletonComponent()
        //: this(string.Empty)
        {
        }
        /// <summary>Initializes a new instance of the <see cref="SpatialFormComponent" /> class.</summary>
        /// <param name="spatialFormFile">The spatial form file.</param>
        //public SkeletonComponent(string Name,Skeleton modelComponentFile)
        //{
        //    SkeletonDictionary.Add(Name, modelComponentFile);
        //    //this.SkeletonComponentFile = modelComponentFile;
        //}
        public SkeletonComponent(Dictionary<string, Skeleton> skeletonDictionary)
        {
            this.SkeletonDictionary = skeletonDictionary;
            //this.SkeletonComponentFile = modelComponentFile;
        }
        public void SetSkeleton(string Name)
        {
            if (SkeletonDictionary.ContainsKey(Name))
            {
                this.SkeletonComponentFile = SkeletonDictionary[Name];
                GameTime gameTime = EntitySystem.BlackBoard.GetEntry<GameTime>("Drawtime");
                float time = (float)gameTime.TotalGameTime.TotalSeconds;
                this.SkeletonComponentFile.SetGameTime(time);
            } 
        }
        /// <summary>Gets or sets the spatial form file.</summary>
        /// <value>The spatial form file.</value>
        public Skeleton SkeletonComponentFile { get; set; }
        public Dictionary<string, Skeleton> SkeletonDictionary = new Dictionary<string, Skeleton>();
    }
    public class PositionComponent : IComponent
    {
        public PositionComponent()
        : this(Vector3.One)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="SpatialFormComponent" /> class.</summary>
        /// <param name="spatialFormFile">The spatial form file.</param>
        public PositionComponent(Vector3 modelComponentFile)
        {
            this.Position = modelComponentFile;
            this.X = modelComponentFile.X;
            this.Y = modelComponentFile.Y;
            this.Z = modelComponentFile.Z;
        }

        /// <summary>Gets or sets the spatial form file.</summary>
        /// <value>The spatial form file.</value>
        public Vector3 Position { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
    }
    public class RotationComponent : IComponent
    {
        public RotationComponent()
            : this(Vector2.One)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="SpatialFormComponent" /> class.</summary>
        /// <param name="spatialFormFile">The spatial form file.</param>
        public RotationComponent(float x, float y)
        {
            this.X = x;
            this.Y = y;
            this.Rotation = new Vector2(x, y);
        }
        public RotationComponent(Vector2 m)
        {
            this.X = m.X;
            this.Y = m.Y;
            this.Rotation = m;
        }
        /// <summary>Gets or sets the spatial form file.</summary>
        /// <value>The spatial form file.</value>
        public float X { get; set; }
        public float Y { get; set; }
        public Vector2 Rotation { get; set; }
    }
    public class EntityNameComponent : IComponent
    {
        public EntityNameComponent()
        //: this(string.Empty)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="SpatialFormComponent" /> class.</summary>
        /// <param name="spatialFormFile">The spatial form file.</param>
        public EntityNameComponent(string name)
        {
            this.Name = name;
        }

        /// <summary>Gets or sets the spatial form file.</summary>
        /// <value>The spatial form file.</value>
        public string Name { get; set; }
    }
    public class EntityTypeComponent : IComponent
    {
        public EntityTypeComponent()
        //: this(string.Empty)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="SpatialFormComponent" /> class.</summary>
        /// <param name="spatialFormFile">The spatial form file.</param>
        public EntityTypeComponent(string name)
        {
            this.EntityType = name;
        }

        /// <summary>Gets or sets the spatial form file.</summary>
        /// <value>The spatial form file.</value>
        public string EntityType { get; set; }
    }
}
