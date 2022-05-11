using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Components
{
	[DisallowMultipleComponent]
	public class Transform : Component, IPositionable
	{
		public Vector2 Position { get; set; }
		public Vector2 Scale	{ get; set; }
		public float Rotation	{ get; set; }

		//public bool HasChanged { get; private set; }
	}
}
