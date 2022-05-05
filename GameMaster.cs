using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Core;
using Microsoft.Xna.Framework.Content;
using System;

namespace Core
{
	public class GameMaster : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private SceneManager sceneManager;

		
		/// <summary> The viewport bounds in pixels. </summary>
		public Vector2 ViewportSize => new Vector2(_graphics.GraphicsDevice.Viewport.Width, _graphics.GraphicsDevice.Viewport.Height);
		

		public GameMaster()
		{
			_graphics = new GraphicsDeviceManager(this);
			_graphics.PreferredBackBufferWidth = 1280;
			_graphics.PreferredBackBufferHeight = 720;

			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		

			Window.AllowUserResizing = true;
			Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);

		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			TextureHelper.SingleWhite = TextureHelper.Single(GraphicsDevice, Color.White);
			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			sceneManager = new SceneManager(this);

			sceneManager.LoadScene(new GameScene());

			// TODO: use this.Content to load your game content here
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			sceneManager.Update();
			Time.Update(gameTime);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			sceneManager.Draw(_spriteBatch);
			// TODO: Add your drawing code here

			base.Draw(gameTime);
		}

		// Contant window aspect ratio
		float AspectRatio = 16f / 9f;
		Point OldWindowSize;

		void Window_ClientSizeChanged(object sender, EventArgs e)
		{
			// https://stackoverflow.com/questions/8396677/uniformly-resizing-a-window-in-xna

			// Remove this event handler, so we don't call it when we change the window size in here
			Window.ClientSizeChanged -= new EventHandler<EventArgs>(Window_ClientSizeChanged);

			if (Window.ClientBounds.Width != OldWindowSize.X)
			{ // We're changing the width
			  // Set the new backbuffer size
				_graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
				_graphics.PreferredBackBufferHeight = (int)(Window.ClientBounds.Width / AspectRatio);
			}
			else if (Window.ClientBounds.Height != OldWindowSize.Y)
			{ // we're changing the height
			  // Set the new backbuffer size
				_graphics.PreferredBackBufferWidth = (int)(Window.ClientBounds.Height * AspectRatio);
				_graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
			}

			_graphics.ApplyChanges();

			// Update the old window size with what it is currently
			OldWindowSize = new Point(Window.ClientBounds.Width, Window.ClientBounds.Height);

			// add this event handler back
			Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);
		}
	}
}
