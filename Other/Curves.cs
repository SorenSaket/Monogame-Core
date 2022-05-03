using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
	public static class Curves
	{
		static Curves()
		{
			Linear01 = Linear(0f, 1f);
			Linear10 = Linear(1f, 0f);
		}
		public static Curve Linear01 { get; private set; }

		public static Curve Linear10 { get; private set; }




		public static Curve Constant(float val)
		{
			Curve c = new Curve();
			c.Keys.Add(new CurveKey(0, val));
			c.Keys.Add(new CurveKey(1, val));
			return c;
		}

		public static Curve Linear(float start, float end)
		{
			Curve c = new Curve();
			c.Keys.Add(new CurveKey(0, start));
			c.Keys.Add(new CurveKey(1, end));
			return c;
		}

		public static Curve Normal(float min, float max)
		{
			Curve c = new Curve();
			c.Keys.Add(new CurveKey(0, min));
			c.Keys.Add(new CurveKey(0.5f, max));
			c.Keys.Add(new CurveKey(1, min));
			return c;
		}

		public static Curve Exp10()
		{
			Curve c = new Curve();
			c.Keys.Add(new CurveKey(0, 1,-1,-1));
			c.Keys.Add(new CurveKey(0.5f, 0,0,0));
			c.Keys.Add(new CurveKey(1, 0, 0, 0));
			return c;
		}
	}
}
