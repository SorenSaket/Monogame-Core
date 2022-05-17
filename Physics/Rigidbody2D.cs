using Core.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Physics
{
	[RequireComponent(typeof(Transform))]
	public class Rigidbody2D : Component
	{
        public float Friction { get; set; } = 0.95f;
		public Vector2 Velocity { get; set; }

        public Transform Transform { get; private set; }

        protected override void Awake()
        {
            Transform = GetComponent<Transform>();
        }

        protected override void Update()
        {
            Velocity *= Friction;
            Transform.LocalPosition += Velocity;
        }
    }
}
