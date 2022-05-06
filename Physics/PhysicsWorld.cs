using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Core.Physics
{
	public class PhysicsWorld 
	{
		private Scene scene;

		public List<Collider2D> collidables;

		public PhysicsWorld(Scene scene)
		{
			this.scene = scene;
			collidables = new List<Collider2D>();
			scene.OnGameObjectInstantiated += (o) => { o.OnComponentAdded += (c) => {
				// On New Component Added
				if (c is Collider2D collider)
				{
					collidables.Add(collider);
				}
			};};
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		public List<Collider2D> OverlapPointAll(Vector2 point)
		{
			List<Collider2D> c = new List<Collider2D>();

			for (int i = 0; i < collidables.Count; i++)
			{
				if (!collidables[i].Gameobject.Active)
					continue;

				if (collidables[i].Shape.Contains(point))
					c.Add(collidables[i]);
			}
			return c;
		}
		public Collider2D OverlapPoint(Vector2 point)
		{
			for (int i = 0; i < collidables.Count; i++)
			{
				if (!collidables[i].Gameobject.Active)
					continue;

				if (collidables[i].Shape.Contains(point))
					return collidables[i];
			}
			return null;
		}
		public bool IsOverlapPoint(Vector2 point, out ICollidable collidable)
		{
			for (int i = 0; i < collidables.Count; i++)
			{
				if (!collidables[i].Gameobject.Active)
					continue;
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
		public bool IsOverlapPoint(Vector2 point)
		{
			for (int i = 0; i < collidables.Count; i++)
			{
				if (collidables[i].Shape.Contains(point))
				{
					return true;
				}
			}
			return false;
		}


		public ICollidable[] OverlapAll(ICollidable colliable)
		{
			List<ICollidable> c = new List<ICollidable>();

			for (int i = 0; i < collidables.Count; i++)
			{
				if (collidables[i] == colliable)
					continue;
				if (collidables[i].Shape.Intersects(colliable.Shape))
					c.Add(collidables[i]);
			}
			return c.ToArray();
		}
		public ICollidable Overlap(ICollidable colliable)
		{
			for (int i = 0; i < collidables.Count; i++)
			{
				if (collidables[i] == colliable)
					continue;
				if (collidables[i].Shape.Intersects(colliable.Shape))
					return collidables[i];
			}
			return null;
		}
		public bool IsOverlap(ICollidable colliable, out ICollidable other)
		{
			for (int i = 0; i < collidables.Count; i++)
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
		public bool IsOverlap(ICollidable colliable)
		{
			for (int i = 0; i < collidables.Count; i++)
			{
				if (collidables[i] == colliable)
					continue;
				if (collidables[i].Shape.Intersects(colliable.Shape))
					return true;
			}
			return false;
		}
		
		public ICollidable[] OverlapAll(Shape shape)
		{
			List<ICollidable> c = new List<ICollidable>();

			for (int i = 0; i < collidables.Count; i++)
			{
				if (collidables[i].Shape.Intersects(shape))
					c.Add(collidables[i]);
			}
			return c.ToArray();
		}
		public ICollidable Overlap(Shape shape)
		{
			for (int i = 0; i < collidables.Count; i++)
			{
				if (collidables[i].Shape.Intersects(shape))
					return collidables[i];
			}
			return null;
		}
		public bool IsOverlap(Shape shape, out ICollidable other)
		{
			for (int i = 0; i < collidables.Count; i++)
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
		public bool IsOverlap(Shape shape)
		{
			for (int i = 0; i < collidables.Count; i++)
			{
				if (collidables[i].Shape.Intersects(shape))
					return true;
			}
			return false;
		}

		
	}

	public static class Physics
	{
		public static bool Intersects(this Shape self, Shape other)
		{
			if (self is Circle selfCircle)
			{
				if (other is Circle otherCircle)
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
			return (MathF.Abs(self.Position.X - other.Position.X) < (self.Size.X / 2f + other.Size.X / 2f)) &&
					(MathF.Abs(self.Position.Y - other.Position.Y) < (self.Size.Y / 2f + other.Size.Y / 2f));
		}
		public static bool IntersectsWith(this Circle self, Circle other)
		{
			return (Vector2.Distance(self.Position, other.Position) < (self.Radius + other.Radius));
		}
		public static bool IntersectsWith(this Rectangle rect, Circle circle)
		{
			return (MathF.Abs(rect.Position.X - circle.Position.X) <= (rect.Size.X / 2f + circle.Radius)) &&
					(MathF.Abs(rect.Position.Y - circle.Position.Y) <= (rect.Size.Y / 2f + circle.Radius));
		}
	}
}
