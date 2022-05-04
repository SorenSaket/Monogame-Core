using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Components
{

	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
	public class DisallowMultipleComponent : Attribute
	{
		public DisallowMultipleComponent()
		{
		}
	}
}
