using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Core.Physics
{
	public static class Physics 
	{
		public static ICollidable[] collidables;

		public static void Update(List<GameObject> objects)
		{
			// This is stupid and slow as heck
			List<ICollidable> c = new List<ICollidable>();
			for (int i = 0; i < objects.Count; i++)
			{
				if(objects[i].Active)
					if (objects[i] is ICollidable col)
						c.Add(col);
			}
			collidables = c.ToArray();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		public static ICollidable[] OverlapPointAll(Vector2 point)
		{
			List<ICollidable> c = new List<ICollidable>();

			for (int i = 0; i < collidables.Length; i++)
			{
				if (collidables[i].Shape.Contains(point))
					c.Add(collidables[i]);
			}
			return c.ToArray();
		}
		public static ICollidable OverlapPoint(Vector2 point)
		{
			for (int i = 0; i < collidables.Length; i++)
			{
				if (collidables[i].Shape.Contains(point))
					return collidables[i];
			}
			return null;
		}
		public static bool IsOverlapPoint(Vector2 point, out ICollidable collidable)
		{
			for (int i = 0; i < collidables.Length; i++)
			{
				if (collidables[i].Shape.Contains(point))
				{
					collidable = collidables[i];
					return true;
				}
			}
			collidable = null;
			return false;
		}
		/// <summary>
		/// Checks if point is within any <see cref="ICollidable"/>  
		/// </summary>
		/// <param name="point">The point</param>
		/// <returns></returns>
		public static bool IsOverlapPoint(Vector2 point)
		{
			for (int i = 0; i < collidables.Length; i++)
			{
				if (collidables[i].Shape.Contains(point))
				{
					return true;
				}
			}
			return false;
		}


		public static ICollidable[] OverlapAll(ICollidable colliable)
		{
			List<ICollidable> c = new List<ICollidable>();

			for (int i = 0; i < collidables.Length; i++)
			{
				if (collidables[i] == colliable)
					continue;
				if (collidables[i].Shape.Intersects(colliable.Shape))
					c.Add(collidables[i]);
			}
			return c.ToArray();
		}
		public static ICollidable Overlap(ICollidable colliable)
		{
			for (int i = 0; i < collidables.Length; i++)
			{
				if (collidables[i] == colliable)
					continue;
				if (collidables[i].Shape.Intersects(colliable.Shape))
					return collidables[i];
			}
			return null;
		}
		public static bool IsOverlap(ICollidable colliable, out ICollidable other)
		{
			for (int i = 0; i < collidables.Length; i++)
			{
				if (collidables[i] == colliable)
					continue;
				if (collidables[i].Shape.Intersects(colliable.Shape))
				{
					other = collidables[i];
					return true;
				}
			}
			other = null;
			return false;
		}
		public static bool IsOverlap(ICollidable colliable)
		{
			for (int i = 0; i < collidables.Length; i++)
			{
				if (collidables[i] == colliable)
					continue;
				if (collidables[i].Shape.Intersects(colliable.Shape))
					return true;
			}
			return false;
		}
		
		public static ICollidable[] OverlapAll(Shape shape)
		{
			List<ICollidable> c = new List<ICollidable>();

			for (int i = 0; i < collidables.Length; i++)
			{
				if (collidables[i].Shape.Intersects(shape))
					c.Add(collidables[i]);
			}
			return c.ToArray();
		}
		public static ICollidable Overlap(Shape shape)
		{
			for (int i = 0; i < collidables.Length; i++)
			{
				if (collidables[i].Shape.Intersects(shape))
					return collidables[i];
			}
			return null;
		}
		public static bool IsOverlap(Shape shape, out ICollidable other)
		{
			for (int i = 0; i < collidables.Length; i++)
			{
				if (collidables[i].Shape.Intersects(shape))
				{
					other = collidables[i];
					return true;
				}
			}
			other = null;
			return false;
		}
		public static bool IsOverlap(Shape shape)
		{
			for (int i = 0; i < collidables.Length; i++)
			{
				if (collidables[i].Shape.Intersects(shape))
					return true;
			}
			return false;
		}

		public static bool Intersects(this Shape self, Shape other)
		{
			if(self is Circle selfCircle)
			{
				if(other is Circle otherCircle)
				{
					return IntersectsWith(selfCircle, otherCircle);
				}
				else if (other is Rectangle otherRect)
				{
					return IntersectsWith(otherRect, selfCircle);
				}
			}
			else if (self is Rectangle selfRect)
			{
				if (other is Circle otherCircle)
				{
					return IntersectsWith(selfRect, otherCircle);
				}
				else if (other is Rectangle otherRect)
				{
					return IntersectsWith(selfRect, otherRect);
				}
			}
			return false;
		}
		public static bool IntersectsWith(this Rectangle self, Rectangle other)
		{
			return	(MathF.Abs(self.Position.X - other.Position.X) < (self.Size.X/2f + other.Size.X/2f)) && 
					(MathF.Abs(self.Position.Y - other.Position.Y) < (self.Size.Y/2f + other.Size.Y/2f));
		}
		public static bool IntersectsWith(this Circle self, Circle other)
		{
			return (Vector2.Distance(self.Position, other.Position) < (self.Radius + other.Radius));
		}
		public static bool IntersectsWith(this Rectangle rect, Circle circle)
		{
			return	(MathF.Abs(rect.Position.X - circle.Position.X) <= (rect.Size.X/2f + circle.Radius)) && 
					(MathF.Abs(rect.Position.Y - circle.Position.Y) <= (rect.Size.Y/2f + circle.Radius));
		}
	}
}
