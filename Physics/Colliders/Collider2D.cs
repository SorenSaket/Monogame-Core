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

        public Collider2D()
        {
        }

        public Shape Shape { get; set; }

		public PhysicsWorld PhysicsWorld { get; set; }
		
		public Transform Transform { get; private set; }

		/// <summary>
		/// Invoked Every frame another collider2D is within this collider
		/// </summary>
		public Action<Collider2D> OnCollision;

		protected override void Awake()
		{
			Transform = GetComponent<Transform>();
		}

        protected override void EarlyUpdate()
        {
			if(Shape != null)
				Shape.Position = Transform.Position;
        }


		public List<Collider2D> OverlapAll()
        {
			return PhysicsWorld.OverlapAll(this);
		}


	}
}
