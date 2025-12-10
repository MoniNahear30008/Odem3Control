using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OdemControl
{
    public partial class Debug : Form
    {
        Form1 mainfrm;
        public Debug(Form1 mainfrm)
        {
            InitializeComponent();
            this.mainfrm = mainfrm;
            MonitorView.Clear();
            OTDelay.Value = mainfrm.lastOTdelay;
            if (mainfrm.forceDbgMode)
            {
                pwBox.Visible = false;
                dbgControl.Visible = true;
            }
        }

        private void Debug_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainfrm.debugMode.Enabled = true;
        }
        public void UpdateMonitor(string text)
        {
            if (showCom.Checked)
            {
                MonitorView.AppendText(text + Environment.NewLine);
                if (AutoScroll.Checked)
                {
                    MonitorView.SelectionStart = MonitorView.Text.Length;
                    MonitorView.ScrollToCaret();
                }
            }
        }

        private void pw_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (pw.Text == mainfrm.appSetting.dbgPW)
                {
                    pwBox.Visible = false;
                    dbgControl.Visible = true;
                }
                else
                {
                    pw.Text = "";
                    MessageBox.Show("Incorrect Password");
                }
            }
        }

        private void wrOTDelay_Click(object sender, EventArgs e)
        {
            if (!mainfrm.isConnected) return;

            mainfrm.LogMessage("Configuring: Set OT Delay");
            string mode = mainfrm.modes[mainfrm.appSetting.scanModeNum];
            int nPoints = mainfrm.scanModes[mode].nPoints;
            int otd = (int)OTDelay.Value;
            uint iotd = (uint)Math.Abs(otd);
            if (otd < 0)
                iotd = (uint)(nPoints - iotd);
            mainfrm.lastOTdelay = otd;
            string Error = mainfrm.WriteRegWaitResp(mainfrm.WriteRegs[(int)confStates.SET_OT_DELAY], new List<uint> { iotd });
            if (Error.Length > 0)
            {
                mainfrm.LogMessage("Configuring Error: " + Error);
                MessageBox.Show("Error sending  Set OT Delay:\n" + Error, "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void showCom_CheckedChanged(object sender, EventArgs e)
        {
            MonitorView.Clear();
            if (showVer.Checked)
            {
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OdemControl.version.txt");
                if (stream != null)
                {
                    StreamReader reader = new StreamReader(stream);
                    string ver = reader.ReadToEnd();
                    MonitorView.AppendText(ver);
                }

            }
        }

        private void clr_Click(object sender, EventArgs e)
        {
            MonitorView.Clear();
        }

        private void WriteReg_Click(object sender, EventArgs e)
        {
            if (!mainfrm.isConnected) return;
            uint add = 0;
            if (!uint.TryParse(regAdd.Text, System.Globalization.NumberStyles.HexNumber, null, out add))
            {
                regAdd.Text = "FF000000";
                MessageBox.Show("Register address must be Hex number");
                return;
            }

            uint val = 0;
            if (regVal.Text.StartsWith("0x"))
            {
                if (!uint.TryParse(regVal.Text.Replace("0x", ""), System.Globalization.NumberStyles.HexNumber, null, out val))
                {
                    regVal.Text = "";
                    MessageBox.Show("Invalid value number");
                    return;
                }
            }
            else
            {
                if (!uint.TryParse(regVal.Text, out val))
                {
                    regVal.Text = "";
                    MessageBox.Show("Invalid value number");
                    return;
                }
            }

            string err = mainfrm.WriteRegWaitResp(add, new List<uint>() { val });
        }
    }
}
