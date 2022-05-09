using Core.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Physics
{
	[RequireComponent(typeof(Transform))]
	public class Collider2D : Component
	{
        public Collider2D(Shape shape)
        {
            Shape = shape;
        }

        public Shape Shape { get; private set; }

		public PhysicsWorld PhysicsWorld { get; set; }
		
		public Transform Transform { get; private set; }

		public Action<Collider2D> OnCollision;

		protected override void Awake()
		{
			Transform = GetComponent<Transform>();
		}

        protected override void EarlyUpdate()
        {
			Shape.Position = Transform.Position;
        }
	}
}
