using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Physics
{
	public interface ICollidable
	{
		public Shape Shape { get; }
	}
}
