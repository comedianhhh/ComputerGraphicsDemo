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

        private void SpaceFighterRB_CheckedChanged(object sender, EventArgs e)
        {
            _game.SetScene("SpaceFighterMaps", true);
            _game.SetScene("SpaceScene", false);
            _game.SetScene("PostProcessing", false);


        }

        private void SpaceSceneRB_CheckedChanged(object sender, EventArgs e)
        {
            _game.SetScene("SpaceScene", true);
            _game.SetScene("SpaceFighterMaps", false);
            _game.SetScene("PostProcessing", false);
        }

        private void PostProcessingRB_CheckedChanged(object sender, EventArgs e)
        {
            _game.SetScene("PostProcessing", true);
            _game.SetScene("SpaceFighterMaps", false);
            _game.SetScene("SpaceScene", false);
        }

        private void BlackandWhiteRB_CheckedChanged(object sender, EventArgs e)
        {
            _game.SetPostProcessing("BlackAndWhite", true);
            _game.SetPostProcessing("UnderWater", false);
        }

        private void UnderWaterSceneRB_CheckedChanged(object sender, EventArgs e)
        {
            _game.SetPostProcessing("Underwater", true);
            _game.SetPostProcessing("BlackAndWhite", false);
        }

        private void AmplitudeTB_Scroll(object sender, EventArgs e)
        {
            double amplitudeValue = (double)AmplitudeTB.Value / 100;
            _game.SetPPValue("Amplitude", amplitudeValue);
            AmplitudeValueLB.Text = amplitudeValue.ToString("F2");
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            double frequencyValue = (double)trackBar2.Value / 100;
            _game.SetPPValue("Frequency", frequencyValue);
            FrequencyValueLB.Text = frequencyValue.ToString("F2");
        }

        private void TintBlueCB_CheckedChanged(object sender, EventArgs e)
        {
            _game.SetPostProcessing("TintBlue", TintBlueCB.Checked);
        }
    }
}
