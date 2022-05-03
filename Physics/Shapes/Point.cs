using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Physics
{
	class Point : Shape
	{
		public override Vector2 AABB()
		{
			return Vector2.Zero;
		}

		public override bool Contains(Vector2 point)
		{
			return position == point;
		}

		public override Vector2 RandomPointWithin()
		{
			return position;
		}
	}
}
