using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Core;
using System;
using System.Collections.Generic;
using System.Text;
namespace Core.Components
{
	public enum AnimationType
	{
		None,
		Anim
	}
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
				Scale = new Vector2(value/SpriteSheet.Width);
			}
		}

		/// <summary> Sheet Settings for using Spritesheets </summary>
		public SpriteSheet SpriteSheet { get; set; }
		/// <summary> Color tint of sprite </summary>
		public Color Color { get; set; } = Color.White;
		/// <summary> R</summary>
		public float Rotation { get; set; } = 0;
		/// <summary> Origin of the sprite relative to transform</summary>
		public Vector2? Origin { get; set; } = null;
		/// <summary> Scale of the Sprite </summary>
		public Vector2 Scale { get; set; } = Vector2.One;
		/// <summary> Scale of the Sprite </summary>
		public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;
		/// <summary> Scale of the Sprite </summary>
		public float Layer { get; set; } = 0.5f;

		/// <summary> The type of animation to be played </summary>
		public AnimationType AnimationType { get; set; } = AnimationType.None;
		/// <summary> Frames per Second </summary>
		public float AnimationSpeed { get; set; } = 1f;
		/// <summary> Current Sprite Sheet Element</summary>
		public int ElementIndex { get; set; } = 0;
		
		private Transform transform;


		public SpriteRenderer()
		{
			
		}

        protected override void Awake()
        {
			transform = GetComponent<Transform>();
			SpriteSheet = new SpriteSheet(TextureHelper.White64x);
		}

        protected override void Update()
        {
			// Animate
			if (AnimationType == AnimationType.Anim)
				ElementIndex = ((int)((Time.TotalTime * AnimationSpeed) % SpriteSheet.Frames.Length));
		}

        protected override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(
				SpriteSheet.Texture, 
				transform.Position, 
				SpriteSheet.Frames[ElementIndex], 
				Color, 
				transform.Rotation + Rotation, 
				Origin ?? SpriteSheet.Origin, 
				Scale * transform.Scale,
				SpriteEffects, Layer);
		}
	}
}
