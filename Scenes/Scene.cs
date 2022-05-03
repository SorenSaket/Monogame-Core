using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;

namespace Core
{
	public class Scene
	{
		public event Action<GameObject> OnGameObjectInstantiated;

		private List<GameObject> gameObjects;
		private Game game;
		private Color background = Color.Black;

		public Color Background { get => background; set => background = value; }
		protected Game Game { get => game; }
		/// <summary> A list of all the objects in the scene </summary>
		public List<GameObject> Objects => gameObjects;


		public void Load(Game game)
		{
			this.game = game;
			gameObjects = new List<GameObject>();
			OnSceneLoaded();
		}
		
		/// <summary>
		/// Add Object to the scene
		/// </summary>
		/// <param name="original">The gameobject to instantiate</param>
		/// <returns>return Original</returns>
		public GameObject Instantiate(GameObject original)
		{
			gameObjects.Add(original);
			original.DoAwake(Game, this);
			original.DoStart();
			OnGameObjectInstantiated?.Invoke(original);
			return original;
		}

		/// <summary>
		/// Instantiates or returns an instance of Type T
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns>Instance of object Type T</returns>
		[Obsolete("Use Pool Instead")]
		public T Instantiate<T>() where T  : GameObject
		{
			for (int i = 0; i < gameObjects.Count; i++)
			{
				if (!gameObjects[i].Destroyed) continue;
				if (gameObjects[i].GetType() != typeof(T)) continue;
				
				gameObjects[i].DoStart();
				gameObjects[i].ResetInternalState();
				return gameObjects[i] as T;
			}
			T obj = (T)Activator.CreateInstance(typeof(T));
			Instantiate(obj);
			return obj;
		}


		/// <summary>
		/// Searches the current scene for an active GameObject that match specified type. Returns null if no object is found.
		/// </summary>
		/// <typeparam name="T">Type to find</typeparam>
		/// <returns>Found Object or Null</returns>
		public T FindObjectOfType<T>() where T : GameObject
		{
			GameObject obj = gameObjects.Find((x) => x.GetType() == typeof(T) && x.Active);
			
			return (T)obj;
		}

		/// <summary>
		/// Searches the current scene for all active GameObjects that match specified type
		/// </summary>
		/// <typeparam name="T">Type to find</typeparam>
		/// <returns>Returns null if no object is found</returns>
		public T[] FindObjectsOfType<T>() where T : GameObject
		{
			// Slow as heck?
			return gameObjects.Where((x) => x.Active).OfType<T>().ToArray();
		}


		public T FindObjectOrSubType<T>() where T : GameObject
		{
			GameObject obj = gameObjects.Find((x) => {
				if (!x.Active) return false;
				Type type = x.GetType();
				return (type.IsSubclassOf(typeof(T)) || type == typeof(T));
			});
			return (T)obj;
		}
		public T FindObjectAssignableFromType<T>() where T : GameObject
		{
			GameObject obj = gameObjects.Find((x) => x.GetType().IsAssignableFrom(typeof(T)) && x.Active);

			return (T)obj;
		}



		/// <summary>
		/// Called when the scene is loaded
		/// </summary>
		protected virtual void OnSceneLoaded() { }
		/// <summary>
		/// Called every frame
		/// </summary>
		public virtual void Update() 
		{
			for (int i = 0; i < gameObjects.Count; i++)
			{
				gameObjects[i].DoUpdate();
			}
			for (int i = 0; i < gameObjects.Count; i++)
			{
				gameObjects[i].DoLateUpdate();
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="spriteBatch"></param>
		public virtual void Draw(SpriteBatch spriteBatch)
		{
			Game.GraphicsDevice.Clear(Background);
			spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, null);
			for (int i = 0; i < gameObjects.Count; i++)
			{
				gameObjects[i].DoDraw(spriteBatch);
			}
			spriteBatch.End();
		}


		public void DrawDebug(SpriteBatch spriteBatch, SpriteFont font)
		{
			int count = Objects.Count;
			int active = Objects.Count((x) => x.Active);
			int inactive = count - active;


			spriteBatch.Begin();

			spriteBatch.DrawString(font, "Total Objects: " + count, new Vector2(16,16), Color.Green);
			spriteBatch.DrawString(font, "Total Active: " + active, new Vector2(16, 32), Color.Green);
			spriteBatch.DrawString(font, "Total Inactive: " + inactive, new Vector2(16, 48), Color.Green);
			spriteBatch.DrawString(font, "FPS: " + 1f/Time.DeltaTime, new Vector2(16, 64), Color.Green);

			spriteBatch.End();
		}
	}
}