using System;
using System.Drawing;

namespace OpenCharas
{
	public struct Point2
	{
		public int X { get; set; }
		public int Y { get; set; }

		public Point2(int x, int y) :
			this()
		{
			X = x;
			Y = y;
		}

		public static Point2 operator+(Point2 l, Point2 r)
		{
			return new Point2(l.X + r.X, l.Y + r.Y);
		}

		public static Point2 operator-(Point2 l, Point2 r)
		{
			return new Point2(l.X - r.X, l.Y - r.Y);
		}

		public static Point2 operator*(Point2 l, Point2 r)
		{
			return new Point2(l.X * r.X, l.Y * r.Y);
		}

		public static Point2 operator/(Point2 l, Point2 r)
		{
			return new Point2(l.X / r.X, l.Y / r.Y);
		}

		public static Point2 operator+(Point2 l, int r)
		{
			return new Point2(l.X + r, l.Y + r);
		}

		public static Point2 operator-(Point2 l, int r)
		{
			return new Point2(l.X - r, l.Y - r);
		}

		public static Point2 operator*(Point2 l, int r)
		{
			return new Point2(l.X * r, l.Y * r);
		}

		public static Point2 operator/(Point2 l, int r)
		{
			return new Point2(l.X / r, l.Y / r);
		}

		public static implicit operator Point(Point2 pt)
		{
			return new Point(pt.X, pt.Y);
		}

		public static implicit operator Point2(Point p)
		{
			return new Point2(p.X, p.Y);
		}
	}
}
