using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
	public static class VectorHelper
	{
		public static Vector2 XY (this Vector3 v)
		{
			return new Vector2(v.X, v.Y);
		}

		public static Vector3 XY(this Vector2 v)
		{
			return new Vector3(v.X, v.Y, 0);
		}

		public static Vector3 XYZ(this Vector2 v, float z)
		{
			return new Vector3(v.X, v.Y, z);
		}

		// Det er umuligt at beskrive hvordan disse funktioner fungerer
		// Deres anvendelsesområde er også begrænset så de burde ikke være i base
		public static Vector2 RectNormal(this Vector2 v)
		{
			if (MathF.Abs(v.X) > MathF.Abs(v.Y))
				return new Vector2(MathF.Sign(v.X), 0);
			return new Vector2(0, MathF.Sign(v.Y));
		}
	

		public static Vector2 Normalized(this Vector2 v)
		{
			v.Normalize();
			return v;
		}
		public static Vector2 Abs(this Vector2 v)
		{
			return new Vector2(MathF.Abs(v.X), MathF.Abs(v.Y));
		}
		public static Vector2 Sign(this Vector2 v)
		{
			return new Vector2(MathF.Sign(v.X), MathF.Sign(v.Y));
		}
		public static Vector2 RoundF(this Vector2 v)
		{
			return new Vector2(MathF.Round(v.X), MathF.Round(v.Y));
		}

		public static Vector2 ToVector2(this float value)
		{
			return new Vector2(value, value);
		}

		public static Vector2 AngleToVector2(this float angle)
		{
			return new Vector2(MathF.Cos(angle), MathF.Sin(angle));
		}

		public static float Atan2(this Vector2 v)
		{
			return MathF.Atan2(v.Y, v.X);
		}

		public static Vector2 Rotate(this in Vector2 point, Vector2 pivot, float angle)
		{
			float relativeX = (point.X - pivot.X);
			float relativeY = (point.Y - pivot.Y);
			float angleCos = MathF.Cos(angle);
			float angleSin = MathF.Sin(angle);
			return new Vector2(
				relativeX * angleCos - relativeY * angleSin + pivot.X,
				relativeY * angleCos + relativeX * angleSin + pivot.Y);
		}
		public static Vector2 Average<TSource>(this IEnumerable<TSource> source, Func<TSource, Vector2> selector)
		{
			IEnumerator<TSource> enumerator = source.GetEnumerator();

			Vector2 total = Vector2.One;
			int count = 0;
			while (enumerator.MoveNext())
			{
				total += selector(enumerator.Current);
				count++;
			}

			return total / count;
		}


		/// <summary>
		/// AngleBetween - the angle between 2 vectors
		/// </summary>
		/// <returns>
		/// Returns the the angle in degrees between vector1 and vector2
		/// </returns>
		/// <param name="vector1"> The first Vector </param>
		/// <param name="vector2"> The second Vector </param>
		public static float AngleBetween(this Vector2 vector1, Vector2 vector2)
		{
			float sin = vector1.X * vector2.Y - vector2.X * vector1.Y;
			float cos = vector1.X * vector2.X + vector1.Y * vector2.Y;

			return MathF.Atan2(sin, cos) * (180f / MathF.PI);
		}
	}
}
