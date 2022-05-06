using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{ 
	/// <summary>
	/// 
	/// </summary>
	class WindowAspectRatioMaintainer
	{  
		public float AspectRatio { get; set; }

		private Point OldWindowSize;
		private GraphicsDeviceManager graphicsDeviceManager;
		private GameWindow window;

		private EventHandler<EventArgs> windowResizeEvent;

		public WindowAspectRatioMaintainer(GameWindow window, GraphicsDeviceManager graphicsDeviceManager, float aspectRatio = 16f / 9f)
		{
			this.window = window;
			this.graphicsDeviceManager = graphicsDeviceManager;
			this.AspectRatio = aspectRatio;


			windowResizeEvent = new EventHandler<EventArgs>(Window_ClientSizeChanged);


			window.ClientSizeChanged += windowResizeEvent;
		}
		~WindowAspectRatioMaintainer()
		{
			window.ClientSizeChanged -= windowResizeEvent;
		}

		void Window_ClientSizeChanged(object sender, EventArgs e)
		{
			// https://stackoverflow.com/questions/8396677/uniformly-resizing-a-window-in-xna

			// Remove this event handler, so we don't call it when we change the window size in here
			window.ClientSizeChanged -= windowResizeEvent;

			if (window.ClientBounds.Width != OldWindowSize.X)
			{ // We're changing the width
			  // Set the new backbuffer size
				graphicsDeviceManager.PreferredBackBufferWidth = window.ClientBounds.Width;
				graphicsDeviceManager.PreferredBackBufferHeight = (int)(window.ClientBounds.Width / AspectRatio);
			}
			else if (window.ClientBounds.Height != OldWindowSize.Y)
			{ // we're changing the height
			  // Set the new backbuffer size
				graphicsDeviceManager.PreferredBackBufferWidth = (int)(window.ClientBounds.Height * AspectRatio);
				graphicsDeviceManager.PreferredBackBufferHeight = window.ClientBounds.Height;
			}

			graphicsDeviceManager.ApplyChanges();

			// Update the old window size with what it is currently
			OldWindowSize = new Point(window.ClientBounds.Width, window.ClientBounds.Height);

			// add this event handler back
			window.ClientSizeChanged += windowResizeEvent;
		}
	}
}