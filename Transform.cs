﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Components
{
	[DisallowMultipleComponent]
	public class Transform : Component, IPositionable
	{
		public Vector2 Position { get; set; } = Vector2.Zero;
		public Vector2 Scale { get; set; } = Vector2.One;
		public float Rotation { get; set; } = 0;

		//public bool HasChanged { get; private set; }
	}
}
