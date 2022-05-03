using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Physics
{
	public class Rectangle : Shape
	{
		public override Vector2 AABB()  => size;
		public Vector2 Size => size;
		private Vector2 size;

		public Rectangle(float x, float y, float width, float height)
		{
			this.position = new Vector2(x, y);
			this.size = new Vector2(width, height);
		}
		public Rectangle(Vector2 position, float width, float height)
		{
			this.position = position;
			this.size = new Vector2(width, height);
		}
		public Rectangle(Vector2 position, Vector2 size)
		{
			this.position = position;
			this.size = size;
		}
		public Rectangle(Rectangle rect)
		{
			this.position = rect.position;
			this.size = rect.size;
		}


		public override bool Contains(Vector2 point)
		{
			return ContainsPointUnrotated(point);
			//return ContainsPointUnrotated(point.Rotate(position, -Rotation));
		}

		private bool ContainsPointUnrotated(Vector2 point)
		{
			return	MathF.Abs(point.X - position.X) < (size.X/2f) && 
					MathF.Abs(point.Y - position.Y) < (size.Y/2f);
		}

		public override Vector2 RandomPointWithin()
		{
			return position + new Vector2(Randoms.Rangeminus11() * (size.X / 2f), Randoms.Rangeminus11() * (size.Y / 2f));
		}

		public static implicit operator Rectangle(Microsoft.Xna.Framework.Rectangle r) => new Rectangle(r.X+r.Width/2f, r.Y+r.Height/2f, r.Width,r.Height);
	}
}
