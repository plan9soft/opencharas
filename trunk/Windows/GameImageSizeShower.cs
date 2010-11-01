using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenCharas.Windows
{
	public partial class GameImageSizeShower : Form
	{
		public GameImageSizeShower()
		{
			InitializeComponent();
		}

		public class GameObjectThing
		{
			RPGGameFile _gameFile;
			GameNodeEntry _gameEntry;

			public RPGGameFile GameFile
			{
				get { return _gameFile; }
			}

			public GameNodeEntry GameNode
			{
				get { return _gameEntry; }
			}

			public GameObjectThing(RPGGameFile gameFile, GameNodeEntry gameNode)
			{
				_gameFile = gameFile;
				_gameEntry = gameNode;
			}

			public override string ToString()
			{
				return _gameFile.GameName;
			}
		}

		private void GameImageSizeShower_Load(object sender, EventArgs e)
		{
			for (int i = 0; i < Images.RPGGames.Count; ++i)
				listBox1.Items.Add(new GameObjectThing(Images.RPGGames[i], Images.RPGNodes[i]));

			listBox1.SelectedIndexChanged += new EventHandler(listBox1_SelectedIndexChanged);
		}

		void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			listBox2.Items.Clear();
			GameObjectThing ged = listBox1.SelectedItem as GameObjectThing;

			for (int i = 0; i < ged.GameNode.Images.Count; ++i)
			{
				var pt = ged.GameNode.Images[i].RawBitmap.Size;
				listBox1.Items.Add(pt.Width.ToString() + " x " + pt.Height.ToString());
			}
		}
	}
}
