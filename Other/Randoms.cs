using Microsoft.Xna.Framework;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Core
{
	/// <summary>
	/// A Static class built on top of System.Random that provides easy access to random functions. To avoid naming conflict with System.Random this class has been named RandomS
	/// </summary>
	static class Randoms
	{
		private static System.Random rnd = new System.Random();


		public static int Index(ICollection col)
		{
			return Range(0, col.Count);
		}

		public static int Range(int inclMin, int exclMax)
		{
			return rnd.Next(inclMin, exclMax);
		}

		/// <summary>
		/// Returns a random float between minimum and maximum
		/// </summary>
		/// <param name="min">Minimum value</param>
		/// <param name="max">Maximum value</param>
		/// <returns></returns>
		public static float Range(float min, float max)
		{
			return (float)(rnd.NextDouble() * (max - min) + min);
		}
		public static double Range(double min, double max)
		{
			return (double)(rnd.NextDouble() * (max - min) + min);
		}
		
		public static float Rotation()
		{
			return Range(0, 2f * MathF.PI);
		}

		public static float Range01()
		{
			return Range(0f, 1f);
		}
		
		public static float Rangeminus11()
		{
			return Range(-1f, 1f);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="var"></param>
		/// <returns> Range(1f - var, 1f + var)</returns>
		public static float Variation(float var)
		{
			return Range(1f - var, 1f + var);
		}

		public static Vector2 Velocity()
		{
			return new Vector2(Range(-1f,1f), Range(-1f, 1f)).Normalized();
		}
		
		/// <summary>
		/// Returns a random normalized Vector2
		/// </summary>
		/// <returns></returns>
		public static Vector2 Direction()
		{
			return Velocity().Normalized();
		}
	}
}