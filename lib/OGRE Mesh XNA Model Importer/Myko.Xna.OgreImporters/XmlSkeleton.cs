using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Myko.Xna.OgreImporters
{
    [XmlRoot("skeleton")]
    public class XmlSkeleton
    {
        [XmlArray("bones"), XmlArrayItem("bone")]
        public XmlBone[] Bones;

        [XmlArray("bonehierarchy"), XmlArrayItem("boneparent")]
        public XmlBoneParent[] BoneParents;

        [XmlArray("animations"), XmlArrayItem("animation")]
        public XmlAnimation[] Animations;
    }

    public class XmlBone
    {
        [XmlAttribute("id")]
        public int Id;

        [XmlAttribute("name")]
        public string Name;

        [XmlElement("position")]
        public XmlVector3 Position;

        [XmlElement("rotation")]
        public XmlRotation Rotation;
    }

    public class XmlBoneParent
    {
        [XmlAttribute("bone")]
        public string Bone;

        [XmlAttribute("parent")]
        public string Parent;
    }

    public class XmlRotation
    {
        [XmlAttribute("angle")]
        public float Angle;

        [XmlElement("axis")]
        public XmlVector3 Axis;
    }

    public class XmlAnimation
    {
        [XmlAttribute("name")]
        public string Name;

        [XmlAttribute("length")]
        public float Length;

        [XmlArray("tracks"), XmlArrayItem("track")]
        public XmlTrack[] Tracks;
    }

    public class XmlTrack
    {
        [XmlAttribute("bone")]
        public string Bone;

        [XmlArray("keyframes"), XmlArrayItem("keyframe")]
        public XmlKeyframe[] Keyframes;
    }

    public class XmlKeyframe
    {
        [XmlAttribute("time")]
        public float Time;

        [XmlElement("translate")]
        public XmlVector3 Translation;

        [XmlElement("rotate")]
        public XmlRotation Rotation;
    }
}
