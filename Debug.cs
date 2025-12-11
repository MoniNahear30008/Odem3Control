using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OdemControl
{
    public partial class Debug : Form
    {
        custom_dev cdev;
        Form1 mainfrm;
        string pushed = "";
        Dictionary<string, uint> WriteRegsAdd = new Dictionary<string, uint>()
        {
            {"Capture_Delay", 0xFF200024 },
            {"Sensitivity", 0xFF200010 },
            {"CFAR Multiplication", 0xFF20007C},
            {"Spurs & NN filter", 0xFF200074},
            {"Retro level", 0xFF200070},
            {"OT delay", 0xFF20003C}
        };
        Dictionary<string, List<uint>> I2Cs = new Dictionary<string, List<uint>>()
        {
            {"Chirp AWG gain", new List<uint>() {3, 0x4B, 0x1C, 0x7000}},
            { "PM1 Control", new List<uint>() {3, 0x4A, 0x1C, 0}},
            { "PM2 Control", new List<uint>() {3, 0x48, 0x1C, 0}},
            { "LO", new List<uint>() {7, 0x4B, 0x1C, 0x7000}},
            {"TxSOA1", new List<uint>() {7, 0x4A, 0x19, 0x2050}},
            {"TxSOA2", new List<uint>() {7, 0x4A, 0x1C, 0x5050}},
            {"Tx3_0_9", new List<uint>() {7, 0x49, 0x19, 0x5050}},
            {"Tx3_10_19", new List<uint>() {7, 0x49, 0x1C, 0x5050}},
            {"Tx3_20_29", new List<uint>() {7, 0x48, 0x19, 0x5050}},
            { "Tx3_30_39", new List<uint>() {7, 0x48, 0x1C, 0x5050}}
        };
        uint I2C_ch = 0;
        uint I2C_dev = 0;
        uint I2C_reg = 0;
        uint I2C_val = 0;
        Dictionary<string, uint> Vectors = new Dictionary<string, uint>()
        {
            {"badGoodIndxs_High", 0xFF200028 },
            {"badGoodIndxs_Low", 0xFF20002C },
            {"128Bins_Final_1", 0xFF248000},
            {"128Bins_Final_2", 0xFF248200},
            {"blackmanHarris_DEC", 0xFF340000},
            {"2kWin", 0xFF346000}
        };
        uint VecDest = 0;
        List<uint> VecData = new List<uint>();

        public Debug(Form1 mainfrm)
        {
            InitializeComponent();
            this.mainfrm = mainfrm;
            MonitorView.Clear();
            OTDelay.Value = mainfrm.lastOTdelay;
            foreach (string r in WriteRegsAdd.Keys)
                RegsNames.Items.Add(r);
            RegsNames.SelectedIndex = 0;

            foreach (string r in I2Cs.Keys)
                I2CsList.Items.Add(r);
            I2CsList.SelectedIndex = 0;

            foreach (string r in Vectors.Keys)
                vecList.Items.Add(r);
            vecList.SelectedIndex = 0;

            if (mainfrm.forceDbgMode)
            {
                pwBox.Visible = false;
                groupBox1.Visible = true;
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
                    groupBox1.Visible = true;
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
                wrOTDelay.BackColor = Color.Red;
            }
            else
                wrOTDelay.BackColor = Color.Lime;
            pushed = "OTDelay";
            timer1.Start();
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

            string Error = mainfrm.WriteRegWaitResp(add, new List<uint>() { val });
            if (Error.Length > 0)
            {
                mainfrm.LogMessage("Configuring Error: " + Error);
                WriteReg.BackColor = Color.Red;
            }
            else
                WriteReg.BackColor = Color.Lime;
            pushed = "WriteReg";
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            switch (pushed)
            {
                case "OTDelay":
                    wrOTDelay.BackColor = SystemColors.Control;
                    break;
                case "resetDSP":
                    resetDSP.BackColor = SystemColors.Control;
                    break;
                case "WriteReg":
                    WriteReg.BackColor = SystemColors.Control;
                    break;
                case "WriteI2C":
                    WriteI2C.BackColor = SystemColors.Control;
                    break;
                case "WrVec":
                    WrVec.BackColor = SystemColors.Control;
                    break;
            }
        }
        private void resetDSP_Click(object sender, EventArgs e)
        {
            string Error = mainfrm.WriteRegWaitResp(mainfrm.WriteRegs[(int)confStates.RESET_DSP], new List<uint> { 0x4100004 });
            if (Error.Length > 0)
            {
                mainfrm.LogMessage("Configuring Error: " + Error);
                resetDSP.BackColor = Color.Red;
            }
            else
                resetDSP.BackColor = Color.Lime;
            pushed = "resetDSP";
            timer1.Start();
        }
        private void RegsNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            string rn = RegsNames.SelectedItem as string;
            regAdd.Text = WriteRegsAdd[rn].ToString("X08");
        }
        private void I2CsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string rn = I2CsList.SelectedItem as string;
            I2C_ch = I2Cs[rn][0];
            I2C_dev = I2Cs[rn][1];
            I2C_reg = I2Cs[rn][2];
            I2C_val = I2Cs[rn][3];
            I2Cdest.Text = "Ch: " + I2C_ch.ToString() + " ;  Dev: 0x" + I2C_dev.ToString("X02") +
                "; Reg: 0x" + I2C_reg.ToString("X02");
            I2Cval.Text = "0x" + I2C_val.ToString("X04");
        }
        private void WriteI2C_Click(object sender, EventArgs e)
        {
            uint val = 0;
            if (I2Cval.Text.StartsWith("0x"))
            {
                if (!uint.TryParse(I2Cval.Text.Replace("0x", ""), System.Globalization.NumberStyles.HexNumber, null, out val))
                {
                    I2Cval.Text = "";
                    MessageBox.Show("Invalid value number");
                    return;
                }
            }
            else
            {
                if (!uint.TryParse(I2Cval.Text, out val))
                {
                    I2Cval.Text = "";
                    MessageBox.Show("Invalid value number");
                    return;
                }
            }

            if (val > 0xffff)
            {
                I2Cval.Text = "";
                MessageBox.Show("Invalid value - must be 16 bits");
                return;
            }

            string Error = mainfrm.WriteI2CWaitResp(I2C_ch, I2C_dev, 0x14, I2C_reg, new List<uint> { val });
            if (Error.Length > 0)
            {
                mainfrm.LogMessage("Configuring Error: " + Error);
                WriteI2C.BackColor = Color.Red;
            }
            else
                WriteI2C.BackColor = Color.Lime;
            pushed = "WriteI2C";
            timer1.Start();

        }
        private void vecList_SelectedIndexChanged(object sender, EventArgs e)
        {
            VecFln.Text = "Double click to select file";
            VecData.Clear();
            string rn = vecList.SelectedItem as string;
            VecDest = Vectors[rn];
            vecReg.Text = "0x" + VecDest.ToString("X08");

        }
        private void VecFln_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            VecFln.Text = "Double click to select file";
            VecData.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files (*.txt)|*.txt";
            ofd.Title = "Select a Text File";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string path = ofd.FileName;
                VecFln.Text = path;
                // Read all lines
                string[] lines = File.ReadAllLines(path);
                foreach (string l in lines)
                    VecData.Add(uint.Parse(l));

            }
        }

        private void WrVec_Click(object sender, EventArgs e)
        {
            if (VecData.Count == 0)
            {
                MessageBox.Show("Vector not loaded");
                return;
            }

            string Error = mainfrm.WriteRegWaitResp(VecDest, VecData);
            if (Error.Length > 0)
            {
                mainfrm.LogMessage("Configuring Error: " + Error);
                WrVec.BackColor = Color.Red;
            }
            else
                WrVec.BackColor = Color.Lime;
            pushed = "WrVec";
            timer1.Start();
        }

        private void startCustom_Click(object sender, EventArgs e)
        {
            cdev = new custom_dev(this);
            cdev.Show();
        }
    }
}
