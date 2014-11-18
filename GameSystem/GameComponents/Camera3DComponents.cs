using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameSystem.GameComponents
{
    public class Camera3DComponents:Microsoft.Xna.Framework.GameComponent
    {
        public Matrix view { get; protected set; } //视图矩阵
        public Matrix projection { get; protected set; } //投影矩阵

        public Vector3 cameraPosition { get; protected set; } //摄像机的位置坐标
        public Vector3 cameraDirection { get; protected set; }//摄像机朝向的目标
        public Vector3 cameraUp { get; protected set; }//指明那个方向是向上
        float fieldOfView;//摄像机的视角弧度,通常是45度或者π/4
        float aspectRatio;//摄像机长宽比,通常使用屏幕宽度除以屏幕高度
        float nearPlaneDistance;//当距离摄像机多近时,无法看清物体
        float farPlaneDistance;//当距离摄像机多远时,无法看清物体
        MouseState prevMouseState;
        float speed = 0.05f;
        public Camera3DComponents(Game game, Vector3 pos, Vector3 target, Vector3 up)
            : base(game)
        {
            cameraPosition = pos;
            cameraDirection = target - pos;
            cameraDirection.Normalize();
            cameraUp = up;
            CreateLookAt();
            //view = Matrix.CreateLookAt(pos, target, up);
            projection = Matrix.CreatePerspectiveFieldOfView((float)Math.PI / 4.0f, (float)Game.Window.ClientBounds.Width / (float)Game.Window.ClientBounds.Height, 1.0f, 1000.0f);
        }
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            // Set mouse position and do initial get state
            Mouse.SetPosition(Game.Window.ClientBounds.Width / 2, Game.Window.ClientBounds.Height / 2);

            prevMouseState = Mouse.GetState();

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here


            // Move forward and backward

            if (Keyboard.GetState().IsKeyDown(Keys.W))
                cameraPosition += cameraDirection * speed;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                cameraPosition -= cameraDirection * speed;

            if (Keyboard.GetState().IsKeyDown(Keys.A))
                cameraPosition += Vector3.Cross(cameraUp, cameraDirection) * speed;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                cameraPosition -= Vector3.Cross(cameraUp, cameraDirection) * speed;



            // Rotation in the world
            cameraDirection = Vector3.Transform(cameraDirection, Matrix.CreateFromAxisAngle(cameraUp, (-MathHelper.PiOver4 / 150) * (Mouse.GetState().X - prevMouseState.X)));


            cameraDirection = Vector3.Transform(cameraDirection, Matrix.CreateFromAxisAngle(Vector3.Cross(cameraUp, cameraDirection), (MathHelper.PiOver4 / 100) * (Mouse.GetState().Y - prevMouseState.Y)));
            cameraUp = Vector3.Transform(cameraUp, Matrix.CreateFromAxisAngle(Vector3.Cross(cameraUp, cameraDirection), (MathHelper.PiOver4 / 100) * (Mouse.GetState().Y - prevMouseState.Y)));

            // Reset prevMouseState
            prevMouseState = Mouse.GetState();

            CreateLookAt();
            base.Update(gameTime);
        }

        private void CreateLookAt()
        {
            view = Matrix.CreateLookAt(cameraPosition, cameraPosition + cameraDirection, cameraUp);
        }
    }
}
