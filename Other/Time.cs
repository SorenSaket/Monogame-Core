using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Core
{
	static class Time
	{
		public static float TimeScale { get; set; } = 1f;

		public static float UnscaledDeltaTime { get; private  set; }
		public static float DeltaTime { get; private set; }
		public static float UnscaledTotalTime { get; private set; }
		public static float TotalTime { get; private set; }


		public static void Update(GameTime gameTime)
		{
			UnscaledDeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
			DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds * TimeScale;

			UnscaledTotalTime = (float)gameTime.TotalGameTime.TotalSeconds;
			TotalTime = (float)gameTime.TotalGameTime.TotalSeconds * TimeScale;
		}
	}
}