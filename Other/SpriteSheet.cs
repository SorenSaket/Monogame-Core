using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
	/// <summary>
	/// 
	/// </summary>
	public class SpriteSheet
	{
		/// <summary> The texture of the spritesheet</summary>
		public Texture2D Texture => texture;
		/// <summary> The frames of the spritesheet</summary>
		public Rectangle[] Frames => frames;
		/// <summary> Origin </summary>
		public Vector2 Origin => origin;
		/// <summary> The Width of a frame </summary>
		public int Width { get; private set; }
		/// <summary> The Height of a frame </summary>
		public int Height { get; private set; }

		private readonly Texture2D texture;
		private readonly Rectangle[] frames;
		private readonly Vector2 origin;

		public SpriteSheet(Texture2D texture, int columns = 1, int rows = 1, int startElement = 0, int elements = -1, int elementWidth = 0, int elementHeight = 0)
		{
			this.texture = texture;

			// If no element width is set calculate from texture width
			if (elementWidth <= 0)
				Width = texture.Width / columns;
			else
				Width = elementWidth;

			if (elementHeight <= 0)
				Height = texture.Height / rows;
			else
				Height = elementHeight;

			// stride is always constant based on texture size and columns/rows
			int strideWidth = texture.Width / columns;
			int strideHeight = texture.Height / rows;

			// If no element count is already specified uses the whole texture
			if (elements <= 0)
				elements = columns * rows;

			// The number of frames to select
			int frameCount = elements - startElement;

			// initialize the frames with the computed framecount
			this.frames = new Rectangle[frameCount]; 

			// Compute each Rectangle/Frame
			for (int i = 0; i < frames.Length; i++)
            {
				int x = ((startElement + i) % columns);
				int y = (((startElement + i) / columns));
				frames[i] = new Rectangle( x * strideWidth, y * strideHeight, Width, Height);
			}

			// Compute origin as center
			this.origin = new Vector2(Width, Height) / 2f;
		}
	}
}
