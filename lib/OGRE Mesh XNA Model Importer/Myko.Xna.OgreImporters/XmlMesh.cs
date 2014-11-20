using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;

namespace Myko.Xna.OgreImporters
{
    [XmlRoot("mesh")]
    public class XmlMesh
    {
        [XmlElement("sharedgeometry")]
        public XmlGeometry SharedGeometry;
        
        [XmlArray("boneassignments"), XmlArrayItem("vertexboneassignment")]
        public XmlBoneAssignment[] SharedBoneAssignments;

        [XmlArray("submeshes"), XmlArrayItem("submesh")]
        public XmlSubMesh[] SubMeshes;

        [XmlElement("skeletonlink")]
        public XmlSkeletonLink SkeletonLink;
    }

    public class XmlSkeletonLink
    {
        [XmlAttribute("name")]
        public string Name;
    }

    public class XmlSubMesh
    {
        [XmlAttribute("material")]
        public string Material;

        [XmlAttribute("usesharedvertices")]
        public bool UseSharedGeometry;

        [XmlArray("faces"), XmlArrayItem("face")]
        public XmlFace[] Faces;

        [XmlElement("geometry")]
        public XmlGeometry Geometry;

        [XmlArray("boneassignments"), XmlArrayItem("vertexboneassignment")]
        public XmlBoneAssignment[] BoneAssignments;
    }

    public class XmlGeometry
    {
        [XmlElement("vertexbuffer")]
        public XmlVertexBuffer[] VertexBuffers;
    }

    public class XmlVertexBuffer
    {
        [XmlElement("vertex")]
        public XmlVertex[] Vertices;
    }

    public class XmlVertex
    {
        [XmlElement("position")]
        public XmlVector3 Position;
        [XmlElement("normal")]
        public XmlVector3 Normal;
        [XmlElement("texcoord")]
        public XmlVector2 TextureCoordinate;
    }

    public class XmlVector3
    {
        [XmlAttribute("x")]
        public double X;
        [XmlAttribute("y")]
        public double Y;
        [XmlAttribute("z")]
        public double Z;

        public Vector3 AsVector3()
        {
            return new Vector3((float)X, (float)Y, (float)Z);
        }
    }

    public class XmlVector2
    {
        [XmlAttribute("u")]
        public float U;
        [XmlAttribute("v")]
        public float V;
    }

    public class XmlFace
    {
        [XmlAttribute("v1")]
        public int Vertex1;
        [XmlAttribute("v2")]
        public int Vertex2;
        [XmlAttribute("v3")]
        public int Vertex3;
    }

    public class XmlBoneAssignment
    {
        [XmlAttribute("vertexindex")]
        public int VertexIndex;
        [XmlAttribute("boneindex")]
        public int BoneIndex;
        [XmlAttribute("weight")]
        public float Weight;
    }
}
