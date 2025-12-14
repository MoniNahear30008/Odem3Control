using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace OdemControl
{
    public partial class Debug : Form
    {
        public Form1 mainfrm;
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
            {"Tx3_30_39", new List<uint>() {7, 0x48, 0x1C, 0x5050}}
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
        Dictionary<int, string> rowFiles = new Dictionary<int, string>();

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
            customParams.Rows.Add("OT delay", "0");
            customParams.Rows.Add("-----------", "-----------");
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

            customFiles.Rows.Add("AWG waveform", "Double click to select");
            customFiles.Rows.Add("badGoodIndxs_High", "Double click to select");
            customFiles.Rows.Add("badGoodIndxs_Low", "Double click to select");
            customFiles.Rows.Add("128Bins_Final", "Double click to select");
            customFiles.Rows.Add("blackmanHarris_DEC", "Double click to select");
            customFiles.Rows.Add("2kWin", "Double click to select");

            rowFiles.Add(0, "AWG.txt");
            rowFiles.Add(1, "badGoodIndxs_High.txt");
            rowFiles.Add(2, "badGoodIndxs_Low.txt");
            rowFiles.Add(3, "128Bins_Final.txt");
            rowFiles.Add(4, "blackmanHarris_DEC.txt");
            rowFiles.Add(5, "2kWin.txt");
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
        private void saveSetting_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Title = "Select a file";
                dialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    List<string> setting = new List<string>();
                    foreach (DataGridViewRow row in customParams.Rows)
                        setting.Add(row.Cells[0].Value.ToString() + "," + row.Cells[1].Value.ToString());
                    setting.Add("Files");
                    foreach (DataGridViewRow row in customFiles.Rows)
                        setting.Add(row.Cells[0].Value.ToString() + "," + row.Cells[1].Value.ToString());
                    File.WriteAllLines(dialog.FileName, setting);
                }
            }
        }
        private void loadSetting_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files (*.txt)|*.txt";
            ofd.Title = "Select a Text File";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                bool isfiles = false;
                string path = ofd.FileName;
                // Read all lines
                string[] lines = File.ReadAllLines(path);
                int row = 0;
                foreach (string l in lines)
                {
                    if (l.Contains("Files"))
                    {
                        isfiles = true;
                        row = 0;
                        continue;
                    }
                    string val = l.Split(',')[1];
                    if (isfiles)
                        customFiles.Rows[row].Cells[1].Value = val;
                    else
                        customParams.Rows[row].Cells[1].Value = val;
                    row++;
                }
            }
        }
        private void getFromFolder_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select a folder";
                dialog.ShowNewFolderButton = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string path = dialog.SelectedPath;
                    folderName.Text = path;
                    string[] files = Directory.GetFiles(path);

                    foreach (string file in files)
                    {
                        if (file.EndsWith("AWG.txt"))
                            customFiles.Rows[0].Cells[1].Value = file;
                        else if (file.EndsWith("badGoodIndxs_High.txt"))
                            customFiles.Rows[1].Cells[1].Value = file;
                        else if (file.EndsWith("badGoodIndxs_Low.txt"))
                            customFiles.Rows[2].Cells[1].Value = file;
                        else if (file.EndsWith("128Bins_Final.txt"))
                            customFiles.Rows[3].Cells[1].Value = file;
                        else if (file.EndsWith("blackmanHarris_DEC.txt"))
                            customFiles.Rows[4].Cells[1].Value = file;
                        else if (file.EndsWith("2kWin.txt"))
                            customFiles.Rows[5].Cells[1].Value = file;

                    }
                }
            }
        }
        private void customConfig_Click(object sender, EventArgs e)
        {
            List<string> setFiles = new List<string>();
            // Check if all files exsist
            bool all = true;
            foreach (DataGridViewRow row in customFiles.Rows)
            {
                string fln = row.Cells[1].Value.ToString();
                if (File.Exists(fln))
                    setFiles.Add(fln);
                else
                {
                    all = false;
                    break;
                }
            }
            if (!all)
            {
                MessageBox.Show("Not all files selected or not exist");
                return;
            }

            mainfrm.confFiles["AWG"].Clear();
            mainfrm.confFiles["badGoodIndxs_High"].Clear();
            mainfrm.confFiles["badGoodIndxs_Low"].Clear();
            mainfrm.confFiles["2kWin"].Clear();
            mainfrm.confFiles["128Bins_Final"].Clear();
            mainfrm.confFiles["blackmanHarris_DEC"].Clear();

            string[] lines = File.ReadAllLines(setFiles[0]);
            foreach (string l in lines)
                mainfrm.confFiles["AWG"].Add(uint.Parse(l));

            lines = File.ReadAllLines(setFiles[1]);
            foreach (string l in lines)
                mainfrm.confFiles["badGoodIndxs_High"].Add(uint.Parse(l));

            lines = File.ReadAllLines(setFiles[2]);
            foreach (string l in lines)
                mainfrm.confFiles["badGoodIndxs_Low"].Add(uint.Parse(l));

            lines = File.ReadAllLines(setFiles[3]);
            foreach (string l in lines)
                mainfrm.confFiles["128Bins_Final"].Add(uint.Parse(l));

            lines = File.ReadAllLines(setFiles[4]);
            foreach (string l in lines)
                mainfrm.confFiles["blackmanHarris_DEC"].Add(uint.Parse(l));

            lines = File.ReadAllLines(setFiles[5]);
            foreach (string l in lines)
                mainfrm.confFiles["2kWin"].Add(uint.Parse(l));


            mainfrm.deviceParameters["Capture_Delay"] = getVal(customParams.Rows[0].Cells[1].Value.ToString());
            mainfrm.deviceParameters["Chirp_AWG_gain"] = getVal(customParams.Rows[5].Cells[1].Value.ToString());
            mainfrm.deviceParameters["LO"] = getVal(customParams.Rows[11].Cells[1].Value.ToString());
            mainfrm.deviceParameters["TxSOA1"] = getVal(customParams.Rows[12].Cells[1].Value.ToString());
            mainfrm.deviceParameters["TxSOA2"] = getVal(customParams.Rows[13].Cells[1].Value.ToString());
            mainfrm.deviceParameters["Tx3_0_9"] = getVal(customParams.Rows[14].Cells[1].Value.ToString());
            mainfrm.deviceParameters["Tx3_10_19"] = getVal(customParams.Rows[15].Cells[1].Value.ToString());
            mainfrm.deviceParameters["Tx3_20_29"] = getVal(customParams.Rows[16].Cells[1].Value.ToString());
            mainfrm.deviceParameters["Tx3_30_39"] = getVal(customParams.Rows[17].Cells[1].Value.ToString());

            mainfrm.GeneralParameters["Sensitivity"] = getVal(customParams.Rows[1].Cells[1].Value.ToString());
            mainfrm.GeneralParameters["CFAR"] = getVal(customParams.Rows[2].Cells[1].Value.ToString());
            mainfrm.GeneralParameters["Spurs"] = getVal(customParams.Rows[3].Cells[1].Value.ToString());
            mainfrm.GeneralParameters["Retro"] = getVal(customParams.Rows[4].Cells[1].Value.ToString());
            mainfrm.GeneralParameters["PM1"] = getVal(customParams.Rows[8].Cells[1].Value.ToString());
            mainfrm.GeneralParameters["PM2"] = getVal(customParams.Rows[9].Cells[1].Value.ToString());
            mainfrm.GeneralParameters["SOA"] = getVal(customParams.Rows[10].Cells[1].Value.ToString());
            mainfrm.GeneralParameters["OTD"] = getVal(customParams.Rows[6].Cells[1].Value.ToString());
            OTDelay.Value = mainfrm.GeneralParameters["OTD"];

            mainfrm.ConfigNow();
        }
        private int getVal(string sval)
        {
            int val = 0;
            if (sval.StartsWith("0x"))
                val = int.Parse(sval.Replace("0x", ""), System.Globalization.NumberStyles.HexNumber);
            else
                val = int.Parse(sval);

            return val;
        }
    }
}
