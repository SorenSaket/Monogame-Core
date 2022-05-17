using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Components
{
	[DisallowMultipleComponent]
	public class Transform : Component, IPositionable
	{
		public Transform Parent { get; set; } = null;

		public Vector2 Position => LocalPosition + (Parent?.Position ?? Vector2.Zero);
		public Vector2 Scale => LocalScale + (Parent?.Scale ?? Vector2.Zero);
		public float Rotation => LocalRotation + (Parent?.Rotation ?? 0);

		public Vector2 LocalPosition { get; set; } = Vector2.Zero;
		public Vector2 LocalScale { get; set; } = Vector2.One;
		public float LocalRotation { get; set; } = 0;

		//public bool HasChanged { get; private set; }
	}
}
