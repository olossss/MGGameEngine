﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Artemis;
using Artemis.Attributes;
using Artemis.Interface;

using Microsoft.Xna.Framework;

namespace GameSystem.Components
{
    //[ArtemisComponentPool(InitialSize = 5, IsResizable = true, ResizeSize = 20, IsSupportMultiThread = false)]
    public class TransformComponent : IComponent//: ComponentPoolable
    {
        /// <summary>Initializes a new instance of the <see cref="TransformComponent" /> class.</summary>
        public TransformComponent()
            : this(Vector2.Zero)
        {

        }

        /// <summary>Initializes a new instance of the <see cref="TransformComponent" /> class.</summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public TransformComponent(float x, float y)
            : this(new Vector2(x, y))
        {
        }

        /// <summary>Initializes a new instance of the <see cref="TransformComponent" /> class.</summary>
        /// <param name="position">The position.</param>
        public TransformComponent(Vector2 position)
        {
            this.Position = position;
        }

        /// <summary>Gets or sets the position.</summary>
        /// <value>The position.</value>
        public Vector2 Position
        {
            get
            {
                return new Vector2(this.X, this.Y);
            }

            set
            {
                this.X = value.X;
                this.Y = value.Y;
            }
        }

        /// <summary>Gets or sets the x.</summary>
        /// <value>The X.</value>
        public float X { get; set; }

        /// <summary>Gets or sets the y.</summary>
        /// <value>The Y.</value>
        public float Y { get; set; }

        /// <summary>The clean up.</summary>
        //public override void CleanUp()
        //{
        //    this.Position = Vector2.Zero;
        //}
    }
}
