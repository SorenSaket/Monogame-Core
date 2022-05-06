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
				Scale = new Vector2(value/Sprite.Width);
			}
		}
		public SheetRenderSettings RenderSettings { get; set; }
		/// <summary> Set single Texture </summary>
		public Texture2D Sprite { get; set; }

		public Vector2 Origin { get; set; }
		public float AnimationSpeed { get; set; } = 1;
		public float Rotation { get; set; } = 0;
		public Color Color { get; set; } = Color.White;
		public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;
		public float Layer { get; set; } = 0.5f;
		public Vector2 Scale { get; set; } = Vector2.One;

		private Transform transform;

		public SpriteRenderer()
		{
			
		}

		/// <summary> Center the <see cref="Origin"></see> to the first sprite in the <see cref="Sprites"></see> array </summary>
		public void CenterOrigin()
		{
			if(Sprite != null)
				Origin = Sprite.Center();
		}



        protected override void Awake()
        {
			transform = GetComponent<Transform>();
			Sprite = TextureHelper.SingleWhite;
			RenderSettings = new SheetRenderSettings(TextureHelper.SingleWhite);
			Scale = new Vector2(64,64);

		}


        protected override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Sprite, transform.Position, RenderSettings.Frames[(int)(Time.TotalTime * AnimationSpeed) % RenderSettings.Frames.Length], Color, Rotation, Origin, Scale, SpriteEffects, Layer);
		}
	}
}
