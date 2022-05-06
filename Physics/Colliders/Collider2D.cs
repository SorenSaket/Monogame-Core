using Core.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Physics
{
	[RequireComponent(typeof(Transform))]
	public abstract class Collider2D : Component
	{

		public abstract Shape Shape { get; }

		public Transform Transform { get; private set; }

		protected override void Awake()
		{
			Transform = GetComponent<Transform>();
		}



	}
}
