using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
	public interface IPositionable
	{
		public Vector2 LocalPosition { get; set; }
	}
}
