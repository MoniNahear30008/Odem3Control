using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OdemControl
{
    public partial class custom_dev : Form
    {
        Debug parent;
        public custom_dev(Debug parent)
        {
            InitializeComponent();
            this.parent = parent;

            SetCustomParams();
        }
        private void SetCustomParams()
        {
            customParams.Rows.Add("Capture Delay", "3200");
            customParams.Rows.Add("Sensitivity", "0x81010E3C");
            customParams.Rows.Add("CFAR Multiplication", "0x00000404");
            customParams.Rows.Add("Spurs & NN filter", "0x20023C78");
            customParams.Rows.Add("Retro level", "10000");
            customParams.Rows.Add("Chirp AWG gain", "0x7000");
            customParams.Rows.Add("PM1 Control", "0");
            customParams.Rows.Add("PM2 Control", "0");
            customParams.Rows.Add("SOA enable", "2");
            customParams.Rows.Add("LO", "0x7000");
            customParams.Rows.Add("TxSOA1", "0x2050");
            customParams.Rows.Add("TxSOA2", "0x5050");
            customParams.Rows.Add("Tx3_0_9", "0x5050");
            customParams.Rows.Add("Tx3_10_19", "0x5050");
            customParams.Rows.Add("Tx3_20_29", "0x5050");
            customParams.Rows.Add("Tx3_30_39", "0x5050");
            customParams.Rows.Add("OT delay", "0");
        }

    }
}
