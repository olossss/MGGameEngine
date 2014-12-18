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
    [ContentImporter(".mesh.xml", DisplayName = "Ogre Mesh XML Importer - Myko", DefaultProcessor = "AbcProcessor")]
    public class MeshImporter : ContentImporter<NodeContent>
    {
        public override NodeContent Import(string filename, ContentImporterContext context)
        {
            var content = new NodeContent();

            var reader = XmlReader.Create(filename);
            var xmlMesh = (XmlMesh)new XmlSerializer(typeof(XmlMesh)).Deserialize(reader);
            reader.Close();

            reader = XmlReader.Create(Path.Combine(Path.GetDirectoryName(filename), xmlMesh.SkeletonLink.Name + ".xml"));
            var xmlSkeleton = (XmlSkeleton)new XmlSerializer(typeof(XmlSkeleton)).Deserialize(reader);
            reader.Close();

            context.Logger.LogImportantMessage("Bones: " + xmlSkeleton.Bones.Length.ToString());

            var bones = new Dictionary<string, BoneContent>();
            foreach (var xmlBone in xmlSkeleton.Bones)
            {
                context.Logger.LogImportantMessage("{0}", "-- " + xmlBone.Name + ": " + xmlBone.Position.AsVector3().ToString() + ", " + xmlBone.Rotation.Angle.ToString() + "/" + xmlBone.Rotation.Axis.AsVector3().ToString());
                var boneContent = new BoneContent()
                {
                    Name = xmlBone.Name,
                    Transform =
                        Matrix.CreateFromAxisAngle(xmlBone.Rotation.Axis.AsVector3(), xmlBone.Rotation.Angle) * 
                        Matrix.CreateTranslation(xmlBone.Position.AsVector3())
                };
                bones.Add(xmlBone.Name, boneContent);
            }

            foreach (var boneParent in xmlSkeleton.BoneParents)
            {
                var parent = bones[boneParent.Parent];
                var bone = bones[boneParent.Bone];
                parent.Children.Add(bone);
            }

            var rootBone = bones.Single(x => x.Value.Parent == null);
            content.Children.Add(rootBone.Value);

            context.Logger.LogImportantMessage("Submeshes: " + xmlMesh.SubMeshes.Length.ToString());

            //context.AddDependency(Path.GetFullPath("HUM_M.MATERIAL"));
            //var materialFile = File.ReadAllText("HUM_M.MATERIAL");
            ////context.Logger.LogImportantMessage("{0}", materialFile);

            foreach (var xmlSubMesh in xmlMesh.SubMeshes)
            {
                context.Logger.LogImportantMessage("Submesh: " + xmlSubMesh.Material);
                context.Logger.LogImportantMessage("-- Faces: " + xmlSubMesh.Faces.Length.ToString());
                if (xmlSubMesh.UseSharedGeometry)
                {
                    context.Logger.LogImportantMessage("-- Uses Shared Geometry");
                }
                else
                {
                    context.Logger.LogImportantMessage("-- Vertexbuffers: " + xmlSubMesh.Geometry.VertexBuffers.Length.ToString());
                    context.Logger.LogImportantMessage("-- Vertices (0): " + xmlSubMesh.Geometry.VertexBuffers[0].Vertices.Length.ToString());
                    context.Logger.LogImportantMessage("-- Vertices (1): " + xmlSubMesh.Geometry.VertexBuffers[1].Vertices.Length.ToString());
                }

                var builder = MeshBuilder.StartMesh(xmlSubMesh.Material);

                //if (xmlSubMesh.Material == "Hum_M/Chest")
                //    builder.SetMaterial(new SkinnedMaterialContent { Texture = new ExternalReference<TextureContent>("TL2_ARMORTEST_CHEST.png") });
                //else if (xmlSubMesh.Material == "Hum_M/MidLeg")
                //    builder.SetMaterial(new SkinnedMaterialContent { Texture = new ExternalReference<TextureContent>("TL2_ARMORTEST_PANTS.png") });
                //else
                int n = filename.Length;
                string Str = filename.Substring(0,n-9);
                builder.SetMaterial(new SkinnedMaterialContent { Texture = new ExternalReference<TextureContent>(Str + ".dds") });

                var normalChannel = builder.CreateVertexChannel<Vector3>(VertexChannelNames.Normal());
                var uvChannel = builder.CreateVertexChannel<Vector2>(VertexChannelNames.TextureCoordinate(0));
                var weightsChannel = builder.CreateVertexChannel<BoneWeightCollection>(VertexChannelNames.Weights());

                var geometry = xmlSubMesh.Geometry;
                if (xmlSubMesh.UseSharedGeometry)
                    geometry = xmlMesh.SharedGeometry;

                foreach (var vertex in geometry.VertexBuffers[0].Vertices)
                {
                    builder.CreatePosition(vertex.Position.AsVector3());
                }

                foreach (var face in xmlSubMesh.Faces)
                {
                    AddTriangleVertex(builder, xmlMesh, xmlSubMesh, xmlSkeleton, face.Vertex1, normalChannel, uvChannel, weightsChannel);
                    AddTriangleVertex(builder, xmlMesh, xmlSubMesh, xmlSkeleton, face.Vertex2, normalChannel, uvChannel, weightsChannel);
                    AddTriangleVertex(builder, xmlMesh, xmlSubMesh, xmlSkeleton, face.Vertex3, normalChannel, uvChannel, weightsChannel);
                }

                content.Children.Add(builder.FinishMesh());
            }

            return content;
        }

        private static void AddTriangleVertex(MeshBuilder builder, XmlMesh xmlMesh, XmlSubMesh xmlSubMesh, XmlSkeleton skeleton, int vertexIndex, int normalChannel, int uvChannel, int weightsChannel)
        {
            var geometry = xmlSubMesh.Geometry;
            if (xmlSubMesh.UseSharedGeometry)
                geometry = xmlMesh.SharedGeometry;

            var vertex = geometry.VertexBuffers[0].Vertices[vertexIndex];
            var uv = geometry.VertexBuffers[1].Vertices[vertexIndex];
            var boneAssignments = xmlSubMesh.BoneAssignments.Where(x => x.VertexIndex == vertexIndex);
            if (xmlSubMesh.UseSharedGeometry)
                boneAssignments = xmlMesh.SharedBoneAssignments.Where(x => x.VertexIndex == vertexIndex);
            builder.SetVertexChannelData(normalChannel, vertex.Normal.AsVector3());
            builder.SetVertexChannelData(uvChannel, new Vector2(uv.TextureCoordinate.U, uv.TextureCoordinate.V));
            var weights = new BoneWeightCollection();
            foreach (var boneAssignment in boneAssignments)
            {
                weights.Add(new BoneWeight(skeleton.Bones[boneAssignment.BoneIndex].Name, boneAssignment.Weight));
            }
            builder.SetVertexChannelData(weightsChannel, weights);
            builder.AddTriangleVertex(vertexIndex);
        }
    }
}
