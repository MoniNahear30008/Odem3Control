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
            dbgControl.SelectedTab = Monitor;
            OTDelay.Value = mainfrm.lastOTdelay;
            //int y = 0;
            //int x = 1 / y;
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
                if (pw.Text == "macamor")
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
            mainfrm.LogMessage("Configuring: Set OT Delay");
            string mode = mainfrm.modes[mainfrm.appSetting.scanModeNum];
            int nPoints = mainfrm.scanModes[mode].nPoints;
            int otd = (int)OTDelay.Value;
            uint iotd = (uint)(nPoints - otd);
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
    }
}
