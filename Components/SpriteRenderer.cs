using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Core;
using System;
using System.Collections.Generic;
using System.Text;
namespace Core.Components
{
	/// <summary>
	/// Simple Helper to draw sprites and sprite animations
	/// </summary>
	[RequireComponent(typeof(Transform))]
	public class SpriteRenderer : Component
	{
		/// <summary> Set the absolute width in pixels and adjust height accordingly  </summary>
		public float WidthInPixels
		{
			set
			{
				Scale = new Vector2(value/ Sprites[0].Width);
			}
		}

		/// <summary> Set single Texture </summary>
		public Texture2D Texture
		{
			set
			{
				Sprites = new Texture2D[] { value };
			}
		}

		public Texture2D[] Sprites { get; set; }
		public Vector2 Origin { get; set; }
		public float AnimationSpeed { get; set; } = 1;
		public float Rotation { get; set; } = 0;
		public Color Color { get; set; } = Color.White;
		public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;
		public float Layer { get; set; } = 0.5f;
		public Vector2 Scale { get; set; } = Vector2.One;

		private Transform transform;

		/// <summary> Center the <see cref="Origin"></see> to the first sprite in the <see cref="Sprites"></see> array </summary>
		public void CenterOrigin()
		{
			if(Sprites != null && Sprites.Length > 0)
				Origin = Sprites[0].Center();
		}



        protected override void Awake()
        {
			transform = GetComponent<Transform>();

		}


        protected override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Sprites[(int)(Time.TotalTime * AnimationSpeed) % Sprites.Length], transform.Position, null, Color, Rotation, Origin, Scale, SpriteEffects, Layer);
		}
	}
}
