using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class Camera : GameObject
    {
        public float Zoom { get; set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }

        public Vector2 viewport;

        private float shakePower;
        private float shakeTimer;
        private Vector2 internalOffset;


        public Camera()
        {
            Zoom = 1;
            Position = Vector2.Zero;
            Rotation = 0;
            Origin = Vector2.Zero;
            Position = Vector2.Zero;
        }

        protected override void Awake()
        {

        }
        protected override void Update()
        {
            viewport = Game.GraphicsDevice.Viewport.Bounds.Size.ToVector2();
            if (shakeTimer > 0)
            {
                internalOffset = Randoms.Velocity() * shakePower;
                shakeTimer -= Time.DeltaTime;
            }
            else
            {
                internalOffset = Vector2.Zero;
            }

        }




        public Matrix GetTransformMatrix()
        {
            var translationMatrix = Matrix.CreateTranslation(new Vector3(Position.X + internalOffset.X, Position.Y + internalOffset.Y, 0));
            var rotationMatrix = Matrix.CreateRotationZ(Rotation);
            var scaleMatrix = Matrix.CreateScale(new Vector3(Zoom, Zoom, 1));
            var originMatrix = Matrix.CreateTranslation(new Vector3(Origin.X, Origin.Y, 0));

            return translationMatrix * rotationMatrix * scaleMatrix * originMatrix;
        }


        public void Shake(float power, float time)
        {
            shakePower = power;
            shakeTimer = time;
        }


        // Helpers

        public Vector2 WorldToScreenPoint(in Vector2 point)
        {
            return Vector2.Transform(point, GetTransformMatrix());
        }
        public Vector2 WorldToViewportPoint(in Vector2 point)
        {
            return Vector2.Transform(point, GetTransformMatrix()) / viewport;
        }
        public Vector2 ScreenToWorldSpace(in Vector2 point)
        {
            return Vector2.Transform(point, Matrix.Invert(GetTransformMatrix()));
        }
        public Vector2 ScreenToViewportPoint(in Vector2 point)
        {
            return new Vector2(MathHelper.Clamp(point.X / viewport.X, 0f, 1f), MathHelper.Clamp(point.Y / viewport.Y, 0f, 1f));
        }
        public bool IsWithinScreen(in Vector2 worldPosition)
        {
            Vector2 point = WorldToViewportPoint(worldPosition);
            return (point.X < viewport.X && point.X > 0 && point.Y < viewport.Y && point.Y > 0);
        }
    }
}
