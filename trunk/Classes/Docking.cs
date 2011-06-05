using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Paril.Windows.Forms.Docking
{
	public abstract class DockingWindow
	{
		public abstract Point Location { get; set; }
		public abstract Size Size { get; set; }
		public DockNode DockNode { get; set; }
		public abstract string ID { get; }
		public abstract bool CanDock { get; }

		public DockingWindow(DockingContainer container)
		{
			container.AddWindow(this);
		}

		// Connect this window to us
		public void ConnectWindow(DockingWindow window)
		{
			if (window.DockNode.Prev != null)
				DisconnectWindow(window);

			DockNode.Connections.Add(window.DockNode);
			window.DockNode.Prev = DockNode;
		}

		// Disconnect this window from us
		public void DisconnectWindow(DockingWindow window)
		{
			if (window.DockNode.Prev != this.DockNode)
				throw new Exception();

			DockNode.Connections.Remove(window.DockNode);
			window.DockNode.Prev = null;
		}
	}

	// allows a rectangle to be used as a dock. 
	public class DockingWindowRectangleEnabler : DockingWindow
	{
		Rectangle _rectangle;

		public Rectangle Rectangle { get { return _rectangle; } set { _rectangle = value; } }
		public override string ID { get { return "other"; } }
		public override bool CanDock
		{
			get { return true; }
		}

		public override Point Location
		{
			get { return _rectangle.Location; }
			set { _rectangle.Location = value; }
		}

		public override Size Size
		{
			get { return _rectangle.Size; }
			set { _rectangle.Size = value; }
		}

		public DockingWindowRectangleEnabler(Rectangle rect, DockingContainer container) :
			base(container)
		{
			_rectangle = rect;
		}
	}

	public class DockNode
	{
		public DockingContainer Container { get; set; }
		public DockingWindow Window { get; set; }
		public List<DockNode> Connections { get; set; }
		public DockNode Prev { get; set; }

		public bool CanDock { get { return Window.CanDock; } }

		public DockNode(DockingContainer container, DockingWindow window, DockNode parent)
		{
			Connections = new List<DockNode>();
			Container = container;
			Prev = parent;
			Window = window;

			if (parent != null)
				parent.Connections.Add(this);
		}
	}

	enum DockSide
	{
		Top		= 1,
		Right	= 2,
		Bottom	= 4,
		Left	= 8
	}

	struct DockCandidate
	{
		public DockNode Window { get; set; }
		public DockSide Side { get; set; }

		public DockCandidate(DockNode window, DockSide side) :
			this()
		{
			Window = window;
			Side = side;
		}
	}

	public class DockingContainer
	{
		public List<DockNode> nodes = new List<DockNode>();
		const int ConnectionSize = 12;
		const int ConnectionSizeHalf = (ConnectionSize / 2);
		const int ConnectionSizeHeight = 0;
		const int ConnectionSizeHeight2 = (ConnectionSizeHeight * 2);

		public int WindowCount
		{
			get { return nodes.Count; }
		}

		public DockingContainer(bool addScreen)
		{
			if (addScreen)
			{
				foreach (var s in Screen.AllScreens)
				{
					DockingWindowRectangleEnabler rect = new DockingWindowRectangleEnabler(new Rectangle(
						s.Bounds.X, s.Bounds.Y,
						0, s.Bounds.Height), this);

					rect = new DockingWindowRectangleEnabler(new Rectangle(
						s.Bounds.X, s.Bounds.Y,
						s.Bounds.Width, 0), this);

					rect = new DockingWindowRectangleEnabler(new Rectangle(
						s.Bounds.X, s.Bounds.Y + s.Bounds.Height,
						s.Bounds.Width, 0), this);

					rect = new DockingWindowRectangleEnabler(new Rectangle(
						s.Bounds.X + s.Bounds.Width, s.Bounds.Y,
						0, s.Bounds.Height), this);
				}
			}
		}

		public void AddWindow(DockingWindow window)
		{
			DockNode node = new DockNode(this, window, null);
			window.DockNode = node;

			nodes.Add(node);
		}

		public void RemoveWindow(DockingWindow window)
		{
			nodes.Remove(window.DockNode);
		}

		internal static Rectangle CalcSideRectangle(Rectangle rect, DockSide side)
		{
			if (side == DockSide.Left)
				return new Rectangle(rect.X - ConnectionSizeHalf, rect.Y + ConnectionSizeHeight, ConnectionSize, rect.Height - ConnectionSizeHeight2);
			else if (side == DockSide.Right)
				return new Rectangle((rect.X + rect.Width) - ConnectionSizeHalf, rect.Y + ConnectionSizeHeight, ConnectionSize, rect.Height - ConnectionSizeHeight2);
			else if (side == DockSide.Bottom)
				return new Rectangle(rect.X + ConnectionSizeHeight, (rect.Y + rect.Height) - ConnectionSizeHalf, rect.Width - ConnectionSizeHeight2, ConnectionSize);
			else if (side == DockSide.Top)
				return new Rectangle(rect.X + ConnectionSizeHeight, rect.Y - ConnectionSizeHalf, rect.Width - ConnectionSizeHeight2, ConnectionSize);

			return new Rectangle(0, 0, 0, 0);
		}

		static DockSide CalcCandidateSide(Rectangle leftRect, Rectangle rightRect)
		{
			if (CalcSideRectangle(leftRect, DockSide.Right).IntersectsWith(CalcSideRectangle(rightRect, DockSide.Left)))
				return DockSide.Left;
			else if (CalcSideRectangle(leftRect, DockSide.Left).IntersectsWith(CalcSideRectangle(rightRect, DockSide.Right)))
				return DockSide.Right;
			else if (CalcSideRectangle(leftRect, DockSide.Top).IntersectsWith(CalcSideRectangle(rightRect, DockSide.Bottom)))
				return DockSide.Bottom;
			else if (CalcSideRectangle(leftRect, DockSide.Bottom).IntersectsWith(CalcSideRectangle(rightRect, DockSide.Top)))
				return DockSide.Top;

			return 0;
		}

		internal List<DockCandidate> GetDockCandidates(DockNode node)
		{
			List<DockCandidate> candidates = new List<DockCandidate>();

			Rectangle myRectangle = new Rectangle(node.Window.Location, node.Window.Size);

			foreach (var cand in nodes)
			{
				if (!cand.CanDock)
					continue;
				
				if (cand == node ||
					node.Connections.Contains(cand))
					continue;

				Rectangle nodeRectangle = new Rectangle(cand.Window.Location, cand.Window.Size);

				// quick-check
				if (myRectangle.IntersectsWith(new Rectangle(nodeRectangle.X - ConnectionSize, nodeRectangle.Y - ConnectionSizeHeight,
					nodeRectangle.Width + ConnectionSize * 2, nodeRectangle.Height + ConnectionSizeHeight2)))
				{
					var c = new DockCandidate(cand, CalcCandidateSide(myRectangle, nodeRectangle));

					if (c.Side != 0)
						candidates.Add(c);
				}
			}

			return candidates;
		}
	}

	public class DockingWindowFormEnabler : DockingWindow
	{
		public DockingWindowForm Form { get; set; }
		public override string ID { get { return Form.Text; } }
		public override bool CanDock
		{
			get { return Form.Visible; }
		}

		public override Point Location
		{
			get { return Form.Location; }
			set { Form.Location = value; }
		}

		public override Size Size
		{
			get { return Form.Size; }
			set { Form.Size = value; }
		}

		public DockingWindowFormEnabler(DockingWindowForm form, DockingContainer container) :
			base(container)
		{
			Form = form;
		}
	}

	public class DockingWindowForm : Form
	{
		DockingWindowFormEnabler _enabler;
		Point _oldLocation = new Point(-9999, -9999);

		public DockingWindowFormEnabler Enabler { get { return _enabler; } }

		public DockingWindowForm()
		{
		}

		public DockingWindowForm(DockingContainer container)
		{
			_enabler = new DockingWindowFormEnabler(this, container);
		}

		static Point DockTo(Point location, Size size, DockCandidate candidate)
		{
			switch (candidate.Side)
			{
			case DockSide.Right:
				location.X = candidate.Window.Window.Location.X + candidate.Window.Window.Size.Width;
				break;
			case DockSide.Left:
				location.X = candidate.Window.Window.Location.X - size.Width;
				break;
			case DockSide.Top:
				location.Y = candidate.Window.Window.Location.Y - size.Height;
				break;
			case DockSide.Bottom:
				location.Y = candidate.Window.Window.Location.Y + candidate.Window.Window.Size.Height;
				break;
			}

			return location;
		}

		static bool CandidatesContain(IEnumerable<DockCandidate> candidates, DockNode window)
		{
			foreach (var x in candidates)
				if (x.Window == window)
					return true;

			return false;
		}

		static bool WindowLinked(DockNode docker, DockNode candidate)
		{
			if (candidate == docker ||
				(candidate.Prev != null && WindowLinked(docker, candidate.Prev)))
				return true;

			return false;
		}

		Point SafeLocation
		{
			get { return Location; }

			set
			{
				_skipLocationChanged = true;
				Location = value;
				_skipLocationChanged = false;
			}
		}

		bool _skipLocationChanged;
		protected override void OnLocationChanged(EventArgs e)
		{
			if (_enabler == null || _enabler.DockNode == null || DesignMode)
				return;

			if (_oldLocation.Equals(new Point(-9999, -9999)))
				_oldLocation = Location;
			else
			{
				Point delta = new Point(_oldLocation.X - Location.X, _oldLocation.Y - Location.Y);

				foreach (var n in _enabler.DockNode.Connections)
					(n.Window as DockingWindowFormEnabler).Form.SafeLocation = new Point(n.Window.Location.X - delta.X, n.Window.Location.Y - delta.Y);

				_oldLocation = Location;
			}

			if (_skipLocationChanged)
				return;

			// search for dockers
			var candidates = _enabler.DockNode.Container.GetDockCandidates(_enabler.DockNode);

			// did we want to undock?
			if (_enabler.DockNode.Prev != null &&
				!CandidatesContain(candidates, _enabler.DockNode.Prev))
			{
				//Console.WriteLine("Disconnecting "+_enabler.ID+" from "+_enabler.DockNode.Prev.Window.ID);
				_enabler.DockNode.Prev.Window.DisconnectWindow(_enabler);
			}

			bool connected = false;
			Point finalLocation = Location;
			foreach (var cand in candidates)
			{
				bool shouldConnect =
					(_enabler.DockNode.Prev == null || !(_enabler.DockNode.Prev != null && cand.Window != _enabler.DockNode.Prev)) &&
					!cand.Window.Window.DockNode.Connections.Contains(_enabler.DockNode) &&
					!connected;

				if (WindowLinked(_enabler.DockNode, cand.Window))
					continue;

				//Console.WriteLine("Docking "+_enabler.ID+" to "+cand.Window.Window.ID);
				finalLocation = DockTo(finalLocation, Size, cand);

				if (shouldConnect)
				{
					connected = true;
					//Console.WriteLine("Connecting "+_enabler.ID+" to "+cand.Window.Window.ID);
					cand.Window.Window.ConnectWindow(_enabler);
				}
			}

			if (!SafeLocation.Equals(finalLocation))
				SafeLocation = finalLocation;

			base.OnLocationChanged(e);
		}
	}
}
