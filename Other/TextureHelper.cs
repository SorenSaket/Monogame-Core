using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
	static class TextureHelper
	{
		public static Vector2 Size(this Texture2D texture)
		{
			return new Vector2(texture.Width, texture.Height);
		}

		public static Vector2 Center(this Texture2D texture)
		{
			return new Vector2(texture.Width / 2f, texture.Height / 2f);
		}

		public static Texture2D Single(GraphicsDevice graphicsDevice, Color color)
		{
			Texture2D oneByOne = new Texture2D(graphicsDevice, 1, 1);
			oneByOne.SetData(new Color[] { color });
			return oneByOne;
		}
		public static Texture2D Single(GraphicsDevice graphicsDevice)
		{
			Texture2D oneByOne = new Texture2D(graphicsDevice, 1, 1);
			oneByOne.SetData(new Color[] { Color.White });
			return oneByOne;
		}


		


		/*
		public static Texture2D Cricle(GraphicsDevice graphicsDevice, int size)
		{
			return null;

			Texture2D t = new Texture2D(graphicsDevice, size, size);
			Color[] colors = new Color[size * size];

			for (int i = 0; i < colors.Length; i++)
			{
				int fa = (int)MathF.Sin(i / size);
				
			}
			t.SetData(colors);
			return t;
		}*/


	}
}
