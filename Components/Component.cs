using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Components
{
	public class Component
	{
		public bool ActiveSelf { get; set; } = true;
		public bool Active => ActiveSelf && Gameobject.Active;

		public Game Game { get; private set;}
		public GameObject Gameobject { get; private set; }
		public Scene Scene { get; private set; }


		public void SetActive(bool active)
        {
			ActiveSelf = active;
        }



		#region Lifetime
		/// <summary>
		/// Called when the object is created
		/// </summary>
		/// <param name="Content"></param>
		/// <param name="GraphicsDevice"></param>
		public void DoAwake(GameObject Gameobject, Game game, Scene scene) {
			this.Gameobject = Gameobject;
			this.Game = game;
			this.Scene = scene;
			Awake(); 
		}
		protected virtual void Awake() { }
		/// <summary>
		/// Called when the object is spawned. Called after awake.
		/// </summary>

		public void DoStart() { Start(); }
		protected virtual void Start() { }

		public void DoEarlyUpdate() { if (Active) EarlyUpdate(); }
		protected virtual void EarlyUpdate() { }

		public void DoUpdate() { if (Active) Update(); }
		/// <summary>
		/// Called every frame. Will not be called if object is inactive
		/// </summary>
		protected virtual void Update() { }


		public void DoLateUpdate() { if (Active) LateUpdate(); }
		protected virtual void LateUpdate() { }

		public void DoDraw(SpriteBatch spriteBatch) { if (Active) Draw(spriteBatch); }
		/// <summary>
		/// Called every draw call. Will not be called if object is inactive
		/// </summary>
		/// <param name="spriteBatch"></param>
		protected virtual void Draw(SpriteBatch spriteBatch) { }
		#endregion

		#region Components
		/// <inheritdoc cref="Core.Scene.Instantiate"/>
		public GameObject Instantiate(GameObject obj) => Gameobject.Instantiate(obj);
		// Non generic and generic implementations 
		public Component AddComponent(Component component) => Gameobject.AddComponent(component);
		public Component AddComponent(Type component) => Gameobject.AddComponent(component);
		public T AddComponent<T>() where T : Component, new() => Gameobject.AddComponent<T>();

		public Component GetComponent(Type component) => Gameobject.GetComponent(component);
		public T GetComponent<T>() => Gameobject.GetComponent<T>();
		public List<T> GetComponents<T>() => Gameobject.GetComponents<T>();

		public bool TryGetComponent<T>(out T component) => Gameobject.TryGetComponent(out component); 
		#endregion
	}
}
