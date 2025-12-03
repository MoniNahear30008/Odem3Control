using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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
        }

        private void Debug_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainfrm.debugMode.Enabled = true;
        }
        public void UpdateMonitor(string text)
        {
            MonitorView.AppendText(text + Environment.NewLine);
            if (AutoScroll.Checked)
            {
                MonitorView.SelectionStart = MonitorView.Text.Length;
                MonitorView.ScrollToCaret();
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
    }
}
