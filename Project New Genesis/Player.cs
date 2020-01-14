using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace Project_New_Genesis
{
    public partial class Player : Form
    {
        private readonly String[] files;
        public Player(String[] x, String today)
        {
            InitializeComponent();
            files = x;

            if (x.Length > 1)
            {
                this.Text = "Todays Readings: " + today;
            }
            else
            {
                this.Text = x[0].Replace(".mp3", "");
            }
        }

        private void Player_Load(object sender, EventArgs e)
        {
            WMPLib.IWMPPlaylist playlist = wmp.playlistCollection.newPlaylist("myplaylist");
            WMPLib.IWMPMedia media;

            foreach (string file in files)
            {
                media = wmp.newMedia("Audio Files\\" + file);
                playlist.appendItem(media);
            }

            wmp.currentPlaylist = playlist;
            wmp.Ctlcontrols.stop();


        }
    }
}
