using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using HYM.System.library;
using HYM.UI.library;
using Myko.Xna.Animation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace GameSystem.GameEntity
{
    class GameEntity : EntityBase 
    {
        public Model model { get; set; }
        public Skeleton skeleton { get; set; }
        private Vector3 position;
        public Vector3 Position { set {
            position = value;
            e.GetComponent<PositionComponent>().Position = value;
        } }
        private Vector2 rotation;
        public Vector2 Rotation
        {
            set
            {
                rotation = value;
                e.GetComponent<RotationComponent>().Rotation = value;
            }
        }
        public void GetJson(string type)
        {
            if (File.Exists(type+".json"))  
            {
                StreamReader sr = new StreamReader(type + ".json");

                //构建Json.net的读取流  
                JsonReader reader = new JsonTextReader(sr);

            } 
        }
        public GameEntity()
        {
            model = contentManager.Load<Model>("Dragonspawn\\Dragonspawn.MESH");
            skeleton = contentManager.Load<Skeleton>("Dragonspawn\\Attack1.SKELETON");
            skeleton.CopyModelBindpose(model);
            Skeleton skeleton1 = contentManager.Load<Skeleton>("Dragonspawn\\Attack2.SKELETON");
            skeleton1.CopyModelBindpose(model);
            Skeleton skeleton2 = contentManager.Load<Skeleton>("Dragonspawn\\Die.SKELETON");
            skeleton2.CopyModelBindpose(model);
            Skeleton skeleton3 = contentManager.Load<Skeleton>("Dragonspawn\\Dragonspawn.SKELETON");
            skeleton3.CopyModelBindpose(model);
            Skeleton skeleton4 = contentManager.Load<Skeleton>("Dragonspawn\\Hit.SKELETON");
            skeleton4.CopyModelBindpose(model);
            Skeleton skeleton5 = contentManager.Load<Skeleton>("Dragonspawn\\Idle.SKELETON");
            skeleton5.CopyModelBindpose(model);
            Skeleton skeleton6 = contentManager.Load<Skeleton>("Dragonspawn\\Run.SKELETON");
            skeleton6.CopyModelBindpose(model);
            Skeleton skeleton7 = contentManager.Load<Skeleton>("Dragonspawn\\Spawn.SKELETON");
            skeleton7.CopyModelBindpose(model);
            Skeleton skeleton8 = contentManager.Load<Skeleton>("Dragonspawn\\Special_Breath.SKELETON");
            skeleton8.CopyModelBindpose(model);
            Skeleton skeleton9 = contentManager.Load<Skeleton>("Dragonspawn\\Stunned.SKELETON");
            skeleton9.CopyModelBindpose(model);
            Skeleton skeleton10 = contentManager.Load<Skeleton>("Dragonspawn\\Walk.SKELETON");
            skeleton10.CopyModelBindpose(model);
            Dictionary<string, Skeleton> SkeletonDictionary = new Dictionary<string, Skeleton>();
            SkeletonDictionary.Add("Attack1", skeleton);
            SkeletonDictionary.Add("Attack2", skeleton1);
            SkeletonDictionary.Add("Die", skeleton2);
            SkeletonDictionary.Add("Dragonspawn", skeleton3);
            SkeletonDictionary.Add("Hit", skeleton4);
            SkeletonDictionary.Add("Idle", skeleton5);
            SkeletonDictionary.Add("Run", skeleton6);
            SkeletonDictionary.Add("Spawn", skeleton7);
            SkeletonDictionary.Add("Special_Breath", skeleton8);
            SkeletonDictionary.Add("Stunned", skeleton9);
            SkeletonDictionary.Add("Walk", skeleton10);
            e = entityWorld.CreateEntity();
            e.AddComponent(new ModelComponent(model));
            e.AddComponent(new SkeletonComponent(SkeletonDictionary));
            e.GetComponent<SkeletonComponent>().SetSkeleton("Walk");
            e.GetComponent<SkeletonComponent>().SkeletonComponentFile.loop = true;
            e.AddComponent(new PositionComponent(position));
            e.AddComponent(new RotationComponent(rotation));
            //e.AddComponent(new MouseStateComponent()); 
        }
    }
}
