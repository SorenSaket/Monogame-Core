using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Components
{
	public class Component
	{
		public bool Active { get; set; } = true;
		protected GameObject gameobject;
		protected Game game;
		protected Scene scene;

		#region Lifetime
		/// <summary>
		/// Called when the object is created
		/// </summary>
		/// <param name="Content"></param>
		/// <param name="GraphicsDevice"></param>
		public void DoAwake(GameObject gameobject, Game game, Scene scene) {
			this.gameobject = gameobject;
			this.game = game;
			this.scene = scene;
			Awake(); 
		}
		protected virtual void Awake() { }
		/// <summary>
		/// Called when the object is spawned. Called after awake.
		/// </summary>

		public void DoStart() { Start(); }
		protected virtual void Start() { }

		public void DoUpdate() { if (Active && gameobject.Active) Update(); }
		/// <summary>
		/// Called every frame. Will not be called if object is inactive
		/// </summary>
		protected virtual void Update() { }


		public void DoLateUpdate() { if (Active && gameobject.Active) LateUpdate(); }
		protected virtual void LateUpdate() { }

		public void DoDraw(SpriteBatch spriteBatch) { if (Active && gameobject.Active) Draw(spriteBatch); }
		/// <summary>
		/// Called every draw call. Will not be called if object is inactive
		/// </summary>
		/// <param name="spriteBatch"></param>
		protected virtual void Draw(SpriteBatch spriteBatch) { }
		#endregion

		#region Components
		// Non generic and generic implementations 
		public Component AddComponent(Type component) => gameobject.GetComponent(component);
		public T AddComponent<T>() where T : Component, new() => gameobject.AddComponent<T>();
		public Component GetComponent(Type component) => gameobject.GetComponent(component);
		public T GetComponent<T>() where T : Component => gameobject.GetComponent<T>();
		#endregion
	}
}
