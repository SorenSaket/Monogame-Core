using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
	public static class Layer
	{
		private static List<string> layers = new List<string>();

		public static List<string> Layers { get => layers; set => layers = value; }

		public static void AddLayer(string layerName)
		{
			layers.Add(layerName);
		}
		/// Burde ikke kaldes i draw eller update da mange objecter vil hurtigt kræve meget cpu. O(n)
		public static float GetLayer(string layer)
		{
			int index = layers.IndexOf(layer);

			if (index == -1)
				return 0; // Error layer not found

			return (1f / layers.Count) * index;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="layer"></param>
		/// <returns>Returns a minimum X and Maximum Y</returns>
		public static Vector2 GetLayerRange(string layer)
		{
			int index = layers.IndexOf(layer);

			if(index == -1)
				return Vector2.Zero; // Error layer not found

			float rangePerLayer = 1f / layers.Count;

			return new Vector2(rangePerLayer * index, rangePerLayer * (index + 1));
		}


	}
}
