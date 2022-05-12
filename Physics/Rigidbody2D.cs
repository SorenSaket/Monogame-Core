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

        private Transform transform;

        protected override void Awake()
        {
            transform = GetComponent<Transform>();
        }

        protected override void Update()
        {
            Velocity *= Friction;
            transform.Position += Velocity;
        }
    }
}
