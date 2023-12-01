using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FighterAndFish
{
    public partial class Form1 : Form
    {

        private Game1 _game;
        public Form1(Game1 game)
        {
            InitializeComponent();
            _game = game;
        }

        private void AddSpaceFighterBT_Click(object sender, EventArgs e)
        {
            _game.AddNewSpaceFighter();
        }

        private void DiffuseCB_CheckedChanged(object sender, EventArgs e)
        {
            _game.SetShaderOption("Diffuse", DiffuseCB.Checked);
        }

        private void SpecularCB_CheckedChanged(object sender, EventArgs e)
        {
            _game.SetShaderOption("Specular", SpecularCB.Checked);
        }

        private void NormalCB_CheckedChanged(object sender, EventArgs e)
        {
            _game.SetShaderOption("Normal", NormalCB.Checked);
        }

        private void WireFrameCB_CheckedChanged(object sender, EventArgs e)
        {
            _game.SetShaderOption("Wireframe", WireFrameCB.Checked);
        }
    }
}
