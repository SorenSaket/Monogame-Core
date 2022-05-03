using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Components
{
	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
	public class RequireComponent : Attribute
	{
		public Type RequiedComponent { get; set; }

		public RequireComponent(Type component)
		{
			if(component == typeof(Component))
				this.RequiedComponent = component;
		}
	}
}