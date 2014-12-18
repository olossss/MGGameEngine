using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Myko.Xna.Animation
{
    public class Skeleton
    {
        private float GameTime;
        public Matrix[] BindPose { get; set; }
        public Matrix[] InverseBindPose { get; set; }
        public List<Bone> Bones { get; set; }
        public float AnimationLength { get; set; }
        public bool loop { get; set; }
        public Skeleton()
        {
            Bones = new List<Bone>();
            loop = true;
            GameTime = 0.0f;
        }
        public void SetGameTime(float time)
        {
            this.GameTime = time;
        }

        public void CopyModelBindpose(Model model)
        {
            var bones = new List<Bone>();
            var bindPose = new List<Matrix>();

            Matrix[] modelBindPose = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(modelBindPose);

            for (int i = 0; i < model.Bones.Count; i++)
            {
                var modelBone = model.Bones[i];
                var bone = Bones.SingleOrDefault(x => x.Name == modelBone.Name);

                if (bone != null)
                {
                    bones.Add(bone);
                    bindPose.Add(modelBindPose[i]);
                }
            }

            Bones = bones;
            BindPose = bindPose.Select(x => x).ToArray();
            InverseBindPose = bindPose.Select(x => Matrix.Invert(x)).ToArray();
        }

        public IEnumerable<Matrix> GetBoneTransforms()
        {
            for (int i = 0; i < Bones.Count; i++)
            {
                yield return Bones[i].AbsoluteTransform;
            }
        }

        public IEnumerable<Matrix> GetBoneTransforms(float time)
        {
            while (time > AnimationLength && AnimationLength > 0)
                time -= AnimationLength;

            for (int i = 0; i < Bones.Count; i++)
            {
                yield return Bones[i].GetAbsoluteTransformAtTime(time);
            }
            
        }

        public IEnumerable<Matrix> GetSkinTransforms()
        {
            for (int i = 0; i < Bones.Count; i++)
            {
                yield return InverseBindPose[i] * Bones[i].AbsoluteTransform;
            }
        }

        public IEnumerable<Matrix> GetSkinTransforms(float time)
        {
            if (loop == true)
            {
                while (time > AnimationLength && AnimationLength > 0)
                    time -= AnimationLength;

                for (int i = 0; i < Bones.Count; i++)
                {
                    yield return InverseBindPose[i] * Bones[i].GetAbsoluteTransformAtTime(time);
                }
            }
            if (loop == false)
            {
                //while (time > AnimationLength && AnimationLength > 0)
                //    time -= AnimationLength;
                float t = time - GameTime;
                if (t <= AnimationLength && AnimationLength > 0)
                {
                    for (int i = 0; i < Bones.Count; i++)
                    {
                        yield return InverseBindPose[i] * Bones[i].GetAbsoluteTransformAtTime(t);
                    }
                }
                else
                {
                    for (int i = 0; i < Bones.Count; i++)
                    {
                        yield return InverseBindPose[i] * Bones[i].GetAbsoluteTransformAtTime(AnimationLength);
                    }
                }

            }
            
        }
    }

    public class Bone
    {
        public string Name { get; set; }
        public Matrix Transform { get; set; }
        [ContentSerializer(SharedResource=true)]
        public Bone Parent { get; set; }
        public List<Keyframe> Keyframes { get; set; }

        private Bone()
        {
            Keyframes = new List<Keyframe>();
        }

        public Bone(string name)
            : this()
        {
            this.Name = name;
        }

        public Matrix AbsoluteTransform
        {
            get
            {
                if (Parent == null)
                    return Transform;
                else
                    return Transform * Parent.AbsoluteTransform;
            }
        }

        public Matrix GetAbsoluteTransformAtTime(float time)
        {
            Matrix parentTransform = Matrix.Identity;

            if (Parent != null)
                parentTransform = Parent.GetAbsoluteTransformAtTime(time);

            Matrix transform = Transform;

            if (Keyframes.Any())
            {
                var framesBefore = Keyframes.Where(x => x.Time < time);
                var framesAfter = Keyframes.Where(x => x.Time > time);
                var frame1 = framesBefore.Any() ? framesBefore.Last() : Keyframes.First();
                var frame2 = framesAfter.Any() ? framesAfter.First() : Keyframes.Last();

                transform = Matrix.Lerp(frame1.Transform, frame2.Transform, Math.Min((time - frame1.Time) / (float)(frame2.Time - frame1.Time), 1f)) * Transform;
            }

            return transform * parentTransform;
        }
    }

    public class Keyframe
    {
        public float Time { get; set; }
        public Matrix Transform { get; set; }
    }
}
