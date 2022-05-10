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

		public SpriteSheet(Texture2D texture, int columns = 1, int rows = 1, int startElement = 0, int elements = -1)
		{
			this.texture = texture;
		
			// If no element count is already specified uses the whole texture
			if (elements <= 0)
				elements = columns * rows;

			// The number of frames to select
			int frameCount = elements - startElement;

			// The size of a single frame
			Width = texture.Width / columns;
			Height = texture.Height / rows;

			// initialize the frames with the computed framecount
			this.frames = new Rectangle[frameCount]; 

			// Compute each Rectangle/Frame
			for (int i = 0; i < frames.Length; i++)
				frames[i] = new Rectangle(((startElement + i) % rows) * Width, (((startElement + i) / rows)) * Height, Width, Height);

			// Compute origin as center 
			// Todo make customizable
			this.origin = new Vector2(Width, Height) / 2f;
		}
	}
}
