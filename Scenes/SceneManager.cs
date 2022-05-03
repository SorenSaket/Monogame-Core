using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
	public class SceneManager
	{
		public Scene Current => current;

		private Scene current;

		private Game game;

		public SceneManager(Game game)
		{
			this.game = game;
		}

		public void LoadScene(Scene scene)
		{
			current = scene;
			scene.Load(game);
		}

		public void Update()
		{
			if(current != null)
				current.Update();
		}
		public void Draw(SpriteBatch _spriteBatch)
		{
			if (current != null)
				current.Draw(_spriteBatch);
		}
	}
}
