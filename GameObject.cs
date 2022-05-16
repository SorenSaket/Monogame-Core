using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Core.Components;
namespace Core
{
	/// <summary>
	///
	/// 
	/// </summary>
	/// <remarks>
	/// A simple container for Active and Destoryed state.
	/// Provides easily overloadable functions for lifetime functions.
	/// Contains references to the Game and Scene in which the object has been instantiated.
	/// </remarks>
	/// Size: 4+4+64+64 : 136 bits
	public class GameObject : ICloneable
	{
		public event Action<Component> OnComponentAdded;

		#region Variables
		/// <summary>
		/// Returns true if the object is active and not destroyed
		/// </summary>
		public bool Active => activeSelf && !destroyed;
		/// <summary>
		/// Returns true if the object is destroyed
		/// </summary>
		public bool Destroyed => destroyed;

		protected Game Game => game; 
		protected Scene Scene => scene; 

		private bool activeSelf = true;
		private bool destroyed = false;
		private Game game;
		private Scene scene;

		private List<Core.Components.Component> components;
		#endregion

		#region Lifetime
		/// <summary>
		/// Called when the object is created
		/// </summary>
		/// <param name="Content"></param>
		/// <param name="GraphicsDevice"></param>
		public void DoAwake(Game game, Scene scene) { 
			this.game = game; 
			this.scene = scene; 
			
			Awake();

            for (int i = 0; i < components.Count; i++)
            {
				components[i].DoAwake(this, game, scene);
			}
		}
		protected virtual void Awake() { }
		/// <summary>
		/// Called when the object is spawned. Called after awake.
		/// </summary>

		public void DoStart() { 
			
			Start();

			for (int i = 0; i < components.Count; i++)
			{
				components[i].DoStart();
			}
		}
		protected virtual void Start() { }

		public void DoEarlyUpdate()
		{
			if (!Active)
				return;

			EarlyUpdate();
			for (int i = 0; i < components.Count; i++)
			{
				components[i].DoEarlyUpdate();
			}
		}
		/// <summary>
		/// Called every frame. Will not be called if object is inactive
		/// </summary>
		protected virtual void EarlyUpdate() { }

		public void DoUpdate() {
			if (!Active)
				return;

			Update();
			for (int i = 0; i < components.Count; i++)
			{
				components[i].DoUpdate();
			}
		}
		/// <summary>
		/// Called every frame. Will not be called if object is inactive
		/// </summary>
		protected virtual void Update() { }


		public void DoLateUpdate() {
			if (!Active)
				return;

			LateUpdate();
			for (int i = 0; i < components.Count; i++)
			{
				components[i].DoLateUpdate();
			}

		}
		protected virtual void LateUpdate() { }

		public void DoDraw(SpriteBatch spriteBatch) {
			if (!Active)
				return;
			
			Draw(spriteBatch);
			
			for (int i = 0; i < components.Count; i++)
			{
				components[i].DoDraw(spriteBatch);
			}
		}
		/// <summary>
		/// Called every draw call. Will not be called if object is inactive
		/// </summary>
		/// <param name="spriteBatch"></param>
		protected virtual void Draw(SpriteBatch spriteBatch) { }
		#endregion

		#region Components
		// Non generic and generic implementations 
		public Component AddComponent(Component component)
		{
			if (component == null)
				return null;

			if (!CanAddComponent(component.GetType()))
                return null;

			EnsureRequireComponentAttributes(component.GetType());

			components.Add(component);
			component.DoAwake(this, game, scene);
			component.DoStart();
			OnComponentAdded?.Invoke(component);
			return component;
		}
		public Component AddComponent(Type component)
		{
			if (component == null)
				return null;
			if(!component.IsSubclassOf( typeof(Component)))
				throw new Exception("Type is not IsSubclassOf component");

			if (!CanAddComponent(component))
				return null;
			EnsureRequireComponentAttributes(component);

			Component componentToAdd = (Component)Activator.CreateInstance(component);
			components.Add(componentToAdd);
			componentToAdd.DoAwake(this, game, scene);
			componentToAdd.DoStart();
			OnComponentAdded?.Invoke(componentToAdd);
			return componentToAdd;
		}
		public T AddComponent<T>() where T : Component, new()
		{
			if (!CanAddComponent(typeof(T)))
				return null;
			EnsureRequireComponentAttributes(typeof(T));

			T componentToAdd = new T();
			components.Add(componentToAdd);
			componentToAdd.DoAwake(this, game, scene);
			componentToAdd.DoStart();
			OnComponentAdded?.Invoke(componentToAdd);
			return componentToAdd;
		}


		public Component GetComponent(Type component)
		{
			if (component == null)
				return null;

			for (int i = 0; i < components.Count; i++)
			{
				if (components[i] == null)
					continue;
				if (component.IsAssignableFrom(components[i].GetType()))
					return components[i];
			}
			return null;
		}
		public T GetComponent<T>()
		{
            for (int i = 0; i < components.Count; i++)
            {
				if (components[i] is T r)
					return r;
            }
			return default(T);
		}
		public List<T> GetComponents<T>()
		{
			List<T> r = new List<T>();
			for (int i = 0; i < components.Count; i++)
			{
				if (components[i] is T t)
					r.Add(t);
			}
			return r;
		}


		public bool TryGetComponent<T>(out T a)
        {
			a = GetComponent<T>();
			return a != null;
        }

		private bool CanAddComponent(Type component)
        {
			System.Attribute[] attrs = System.Attribute.GetCustomAttributes(component);

			if (attrs.OfType<DisallowMultipleComponent>().Any())
			{
				if (GetComponent(component) != null)
				{
					return false;
				}
			}
			return true;
		}
		private void EnsureRequireComponentAttributes(Type component)
        {
			System.Attribute[] attrs = System.Attribute.GetCustomAttributes(component);

			for (int i = 0; i < attrs.Length; i++)
			{
				// If the attribute is of type require component
				if (attrs[i] is RequireComponent requireComponent)
				{
					// Get the type
					Type comp = requireComponent.RequiedComponent;
					// 
					if (comp == null)
						continue;
					//
					if (GetComponent(comp) == null)
						AddComponent(comp);
				}
			}
		}

		#endregion

		/// <summary>
		/// Called when the state changes.
		/// </summary>
		/// <param name="isActive"></param>
		protected virtual void OnActiveStateChanged(bool isActive) { }
		protected virtual void OnDestroyed() { }

		// -------- State Changers --------

		private void SetDestroyed(bool value)
		{
			bool lastState = Active;
			destroyed = value;
			if (lastState != Active) {
				if (destroyed)
					OnDestroyed();
				OnActiveStateChanged(Active);
			}
		}
		/// <summary>
		/// Change the active state of the object.
		/// </summary>
		/// <param name="value"></param>
		public void SetActive(bool value)
		{
			bool lastState = Active;
			activeSelf = value;
			if (lastState != Active)
				OnActiveStateChanged(Active);
		}
		/// <summary>
		/// Destroy the object
		/// </summary>
		public void Destroy()
		{
			SetDestroyed(true);
		}
		/// <summary>
		/// Resets the interal state of the GameObject. Sets Active.
		/// </summary>
		public void ResetInternalState()
		{
			destroyed = false;
			activeSelf = true;
			OnActiveStateChanged(true);
		}






		// -------- Statics --------
		/// <inheritdoc cref="Core.Scene.Instantiate"/>
		public GameObject Instantiate(GameObject obj) => scene.Instantiate(obj);
		/// <inheritdoc cref="Core.Scene.Instantiate{T}"/>
		[Obsolete("Use Pool Instead")]
		public T Instantiate<T>() where T : GameObject => scene.Instantiate<T>();
		/// <inheritdoc cref="Core.Scene.FindObjectOfType{T}"/>
		public T FindObjectOfType<T>() where T : GameObject => scene.FindObjectOfType<T>();
		/// <inheritdoc cref="Core.Scene.FindObjectsOfType{T}"/>
		public T[] FindObjectsOfType<T>() where T : GameObject => scene.FindObjectsOfType<T>();



		public GameObject()
		{
			components = new List<Component>();
		}
		public GameObject(GameObject obj)
		{
			game = obj.game;
			scene = obj.scene;
			activeSelf = obj.activeSelf;
			destroyed = obj.destroyed;
			// todo implement component cloning
		}
		public virtual object Clone()
		{
			GameObject obj = new GameObject
			{
				game = game,
				scene = scene,
				activeSelf = activeSelf,
				destroyed = destroyed
			};
			return obj;
		}
	}
}