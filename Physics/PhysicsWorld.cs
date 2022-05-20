using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Core.Math;

namespace Core.Physics
{
	public class PhysicsWorld : GameObject
	{
		public const float gridSize = 256;
		private KeyedHashSet<int2, Collider2D> grid;
		private KeyedHashSet<Collider2D, int2> collidablesCells;

		public List<Collider2D> collidables;

		protected override void Awake()
		{
			collidablesCells = new KeyedHashSet<Collider2D, int2>();
			grid = new KeyedHashSet<int2, Collider2D>();
			collidables = new List<Collider2D>();
			Scene.OnGameObjectInstantiated += (o) => { o.OnComponentAdded += (c) => {
				// On New Component Added
				if (c is Collider2D collider)
				{
					collidables.Add(collider);
					collider.PhysicsWorld = this;
				}
			};};
		}


        protected override void EarlyUpdate()
        {
			BroadPhase();
			NarrowPhase();
		}

		protected void BroadPhase()
        {
            // Clear Grid
            foreach (var item in grid)
            {
				item.Value.Clear();
            }
			foreach (var item in collidablesCells)
			{
				item.Value.Clear();
			}

			for (int i = 0; i < collidables.Count; i++)
			{
				if (!collidables[i].Active)
					continue;
				if (collidables[i].Shape == null)
					continue;

				Vector2 AABB = collidables[i].Shape.AABB();

				int2 min = new int2(
					(int)((collidables[i].Shape.Position.X - AABB.X) / gridSize),
					(int)((collidables[i].Shape.Position.Y - AABB.Y) / gridSize)
					);
				int2 max = new int2(
					(int)MathF.Ceiling((collidables[i].Shape.Position.X + AABB.X)/gridSize), 
					(int)MathF.Ceiling((collidables[i].Shape.Position.Y + AABB.Y) / gridSize)
					);

                for (int x = min.X; x < max.X; x++)
                {
                    for (int y = min.Y; y < max.Y; y++)
                    {
						int2 key = new int2(x, y);
						if (!grid.ContainsKey(key))
						{
							var hashset = new HashSet<Collider2D>();
							hashset.Add(collidables[i]);
							grid.Add(key, hashset);
						}
						else
							grid[key].Add(collidables[i]);

						if (!collidablesCells.ContainsKey(collidables[i]))
						{
							var cells = new HashSet<int2>();
							cells.Add(key);
							collidablesCells.Add(collidables[i], cells);
						}
						else
							collidablesCells[collidables[i]].Add(key);
					}
                }
			}
		}
		protected void NarrowPhase()
        {
			for (int i = 0; i < collidables.Count; i++)
			{
				if (!collidables[i].Active)
					continue;
				var collisions = OverlapAll(collidables[i]);
                foreach (var item in collisions)
                {
					collidables[i].OnCollision?.Invoke(item);
				}
			}
		}

		/*
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
				if (!collidables[i].Active)
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
				if (!collidables[i].Active)
					continue;

				if (collidables[i].Shape.Contains(point))
					return collidables[i];
			}
			return null;
		}
		public bool IsOverlapPoint(Vector2 point, out Collider2D collidable)
		{
			for (int i = 0; i < collidables.Count; i++)
			{
				if (!collidables[i].Active)
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
		}*/

		public HashSet<Collider2D> OverlapAll(Collider2D collider)
		{
			HashSet<Collider2D> c = new HashSet<Collider2D>();

            if (collidablesCells.ContainsKey(collider))
            {
				foreach (var cell in collidablesCells[collider])
				{
					foreach (var item in grid[cell])
					{
						if (!item.Active)
							continue;
						if (item == collider)
							continue;

						if (Collision.Intersects(collider, item))
							c.Add(item);
					}
				}
			}

			return c;
		}
	}

	public static class Collision
	{
		public static bool Intersects(this Collider2D self, Collider2D other)
		{
			if(self.Shape != null && other.Shape != null)
				return self.Shape.Intersects(other.Shape);
			return false;
		}

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
