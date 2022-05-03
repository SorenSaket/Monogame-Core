using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Core.Physics
{
	public class Circle : Shape
	{
		public override Vector2 AABB() => new Vector2(radius*2);
		public float Radius => radius;

		private float radius;

		public Circle(Vector2 position, float radius)
		{
			this.radius = radius;
			this.position = position;
		}

		public override bool Contains(Vector2 point)
		{
			return (Vector2.Distance(position, point) < (radius));
		}

		public override Vector2 RandomPointWithin()
		{
			float r = radius * MathF.Sqrt(Randoms.Range01());
			float theta = Randoms.Rotation();
			return new 
				Vector2(position.X + r * MathF.Cos(theta),
						position.Y + r * MathF.Sin(theta));
		}
	}
}
