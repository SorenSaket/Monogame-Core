using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
	static class Utils
	{
		public static Vector2 Center(this Viewport v)
		{
			return new Vector2(v.Width / 2f, v.Height / 2f);
		}

		public static T[] LoadAll<T>(this ContentManager content, string[] paths)
		{
			T[] r = new T[paths.Length];

			for (int i = 0; i < r.Length; i++)
			{
				r[i] = content.Load<T>(paths[i]);
			}
			return r;
		}

		public static Microsoft.Xna.Framework.Color ToXNAColor(System.Drawing.Color c)
        {
			return new Microsoft.Xna.Framework.Color(c.R,c.G,c.B,c.A);
        }
	}
}
