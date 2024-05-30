using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace billiard_laser
{
    public partial class ImageProcessingDebugForm : Form
    {
        public ImageProcessingDebugForm()
        {
            InitializeComponent();
        }

        private void trackBarMaskRed_ValueChanged(object sender, EventArgs e)
        {
            labelMaskRedValue.Text = trackBarMaskRed.Value.ToString();
        }

        private void trackBarMaskGreen_ValueChanged(object sender, EventArgs e)
        {
            labelMaskGreenValue.Text = trackBarMaskGreen.Value.ToString();
        }

        private void trackBarMaskBlue_ValueChanged(object sender, EventArgs e)
        {
            labelMaskBlueValue.Text = trackBarMaskBlue.Value.ToString();
        }
    }
}
