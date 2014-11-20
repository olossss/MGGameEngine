using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Myko.Xna.Animation;

namespace Myko.Xna.OgreImporters
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to import a file from disk into the specified type, TImport.
    /// 
    /// This should be part of a Content Pipeline Extension Library project.
    /// 
    /// TODO: change the ContentImporter attribute to specify the correct file
    /// extension, display name, and default processor for this importer.
    /// </summary>
    [ContentImporter(".skeleton.xml", DisplayName = "Ogre Skeleton XML Importer - Myko", DefaultProcessor = "AbcProcessor")]
    public class SkeletonImporter : ContentImporter<Skeleton>
    {
        public override Skeleton Import(string filename, ContentImporterContext context)
        {
            var skeleton = new Skeleton();

            var reader = XmlReader.Create(filename);
            var xmlSkeleton = (XmlSkeleton)new XmlSerializer(typeof(XmlSkeleton)).Deserialize(reader);
            reader.Close();

            context.Logger.LogImportantMessage("Bones: " + xmlSkeleton.Bones.Length.ToString());

            var bones = new Dictionary<string, Bone>();
            foreach (var xmlBone in xmlSkeleton.Bones)
            {
                context.Logger.LogImportantMessage("{0}", "-- " + xmlBone.Name + ": " + xmlBone.Position.AsVector3().ToString() + ", " + xmlBone.Rotation.Angle.ToString() + "/" + xmlBone.Rotation.Axis.AsVector3().ToString());
                var bone = new Bone(xmlBone.Name)
                {
                    Transform = 
                        Matrix.CreateFromAxisAngle(xmlBone.Rotation.Axis.AsVector3(), xmlBone.Rotation.Angle) * 
                        Matrix.CreateTranslation(xmlBone.Position.AsVector3())
                };
                bones.Add(xmlBone.Name, bone);
                skeleton.Bones.Add(bone);
            }

            foreach (var boneParent in xmlSkeleton.BoneParents)
            {
                var parent = bones[boneParent.Parent];
                var bone = bones[boneParent.Bone];
                bone.Parent = parent;
            }

            foreach (var xmlAnimation in xmlSkeleton.Animations)
            {
                skeleton.AnimationLength = xmlAnimation.Length;

                foreach (var xmlTrack in xmlAnimation.Tracks)
                {
                    var bone = bones[xmlTrack.Bone];
                    foreach (var xmlKeyframe in xmlTrack.Keyframes)
                    {
                        bone.Keyframes.Add(new Keyframe 
                        { 
                            Time = xmlKeyframe.Time,
                            Transform = 
                                Matrix.CreateFromAxisAngle(xmlKeyframe.Rotation.Axis.AsVector3(), xmlKeyframe.Rotation.Angle) *
                                Matrix.CreateTranslation(xmlKeyframe.Translation.AsVector3())
                        });
                    }
                }
            }
            
            return skeleton;
        }
    }
}
