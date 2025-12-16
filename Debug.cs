using System.Reflection;

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
        bool JsonReady = false;
        sm_params scan_params = new sm_params();
        int BASE_FREQUENCY_HZ = 40000;

        public Debug()
        {
            InitializeComponent();

            //this.mainfrm = mainfrm;
//            SetDebugForm();
        }
        public void SetDebugForm(Form1 mainfrm)
        {
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

            cModeParams.Rows.Add("Mirror freq", "4000.0");
            cModeParams.Rows.Add("FPGA points", "12283");
            cModeParams.Rows.Add("----------", "-----------");
            cModeParams.Rows.Add("Horizontal FOV", "30");
            cModeParams.Rows.Add("Vertical FOV", "19");
            cModeParams.Rows.Add("Horizontal Res.", "0.01");
            cModeParams.Rows.Add("Vertical Res.", "0.12");
            cModeParams.Rows.Add("Lines per frame", "160");
            cModeParams.Rows.Add("frame rate", "5");

            cWaveForm.Rows.Add("waveformX", "");
            cWaveForm.Rows.Add("waveformY", "");

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
                string[] lines = System.IO.File.ReadAllLines(path);
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
                    setting.Add("ScanMode " + customMode.Checked.ToString());
                    foreach (DataGridViewRow row in cModeParams.Rows)
                        setting.Add(row.Cells[0].Value.ToString() + "," + row.Cells[1].Value.ToString());
                    setting.Add("scanFiles");
                    foreach (DataGridViewRow row in cWaveForm.Rows)
                        setting.Add(row.Cells[0].Value.ToString() + "," + row.Cells[1].Value.ToString());

                    System.IO.File.WriteAllLines(dialog.FileName, setting);
                }
            }
        }
        private void loadSetting_Click(object sender, EventArgs e)
        {
            JsonReady = false;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files (*.txt)|*.txt";
            ofd.Title = "Select a Text File";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                bool isfiles = false;
                bool isSM = false;
                string path = ofd.FileName;
                // Read all lines
                string[] lines = System.IO.File.ReadAllLines(path);
                int row = 0;
                foreach (string l in lines)
                {
                    if (l.Contains("Files"))
                    {
                        isfiles = true;
                        row = 0;
                        continue;
                    }
                    else if (l.Contains("ScanMode"))
                    {
                        customMode.Checked = l.Contains("True");
                        isfiles = false;
                        isSM = true;
                        row = 0;
                        continue;
                    }
                    string val = l.Split(',')[1];
                    if (isSM)
                    {
                        if (isfiles)
                            cWaveForm.Rows[row].Cells[1].Value = val;
                        else
                            cModeParams.Rows[row].Cells[1].Value = val;
                    }
                    else
                    {
                        if (isfiles)
                            customFiles.Rows[row].Cells[1].Value = val;
                        else
                            customParams.Rows[row].Cells[1].Value = val;
                    }
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
            if (customMode.Checked && !JsonReady)
            {
                MessageBox.Show("Please genetare json file");
                return;
            }

            List<string> setFiles = new List<string>();
            List<string> wfFiles = new List<string>();
            // Check if all files exsist
            bool all = true;
            foreach (DataGridViewRow row in customFiles.Rows)
            {
                string fln = row.Cells[1].Value.ToString();
                if (System.IO.File.Exists(fln))
                    setFiles.Add(fln);
                else
                {
                    all = false;
                    break;
                }
            }

            if (customMode.Checked)
            {
                foreach (DataGridViewRow row in cWaveForm.Rows)
                {
                    string fln = row.Cells[1].Value.ToString();
                    if (File.Exists(fln))
                        wfFiles.Add(fln);
                    else
                    {
                        all = false;
                        break;
                    }
                }
            }
            if (!all)
            {
                MessageBox.Show("Not all files set or not exist");
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

            string path = "";

            mainfrm.ConfigNow(path);
        }
        private string SetJSON(int xCount)
        {
            scan_params.mirror = double.Parse(cModeParams.Rows[0].Cells[1].Value.ToString());
            scan_params.points = uint.Parse(cModeParams.Rows[1].Cells[1].Value.ToString());
            scan_params.hFOV = uint.Parse(cModeParams.Rows[3].Cells[1].Value.ToString());
            scan_params.vFOV = uint.Parse(cModeParams.Rows[4].Cells[1].Value.ToString());
            scan_params.hRes = double.Parse(cModeParams.Rows[5].Cells[1].Value.ToString());
            scan_params.vRes = double.Parse(cModeParams.Rows[6].Cells[1].Value.ToString());
            scan_params.linesPerF = uint.Parse(cModeParams.Rows[7].Cells[1].Value.ToString());
            scan_params.fpga_points_per_line = (uint)(scan_params.hFOV / scan_params.hRes);
            scan_params.total_fpga_points = scan_params.fpga_points_per_line * scan_params.linesPerF * 2;
            scan_params.fpga_total_scan_time_sec = ((double)scan_params.total_fpga_points * 16.384) / 1000000.0;

            string json = "{\r\n    \"metadata\": {\r\n        \"timestamp\": \"";
            json += DateTime.Now.ToString("yyyyMMdd_HHmmss");
            json += "\",\r\n        \"generation_method\": \"manual_update\",\r\n        \"generation_date\": \" ";
            json += DateTime.Now.ToString("yyyyMMdd");
            json += "\",\r\n        \"generation_time\": \"";
            json += DateTime.Now.ToString("HHmmss");
            json += "\",\r\n        \"waveform_files\": {\r\n            \"x_file\": \"waveformX.csv\",\r\n            \"y_file\": \"waveformY.csv\"\r\n        },\r\n        \"line_counts\": {\r\n            \"x_lines\": ";
            json += xCount.ToString();
            json += ",\r\n            \"y_lines\": ";
            json += xCount.ToString();
            json += "\r\n        }\r\n    },\r\n    \"selected_parameters\": {\r\n        \"mirror_frequency_hz\": ";
            json += scan_params.mirror.ToString("0.0");
            json += ",\r\n        \"achieved_total_fpga_points_to_use\": ";
            json += scan_params.points.ToString();
            json += ",\r\n        \"mirror_points_per_axis\": ";
            json += xCount.ToString();
            json += "\r\n    },\r\n    \"user_entered_parameters\": {\r\n        \"horizontal_fov\": ";
            json += scan_params.hFOV.ToString("0.0");
            json += ",\r\n        \"pixel_angle_h\": ";
            json += scan_params.hRes.ToString("0.00");
            json += ",\r\n        \"vertical_step_angle\": ";
            json += scan_params.vRes.ToString("0.00");
            json += ",\r\n        \"number_lines\": ";
            json += scan_params.linesPerF.ToString();
            json += ",\r\n        \"round_percent\": 0.05\r\n    },\r\n    \"deduced_parameters_from_optimization\": {\r\n        \"fpga_parameters\": {\r\n            \"fpga_points_per_line\": ";
            // fpga_parameters
            json += scan_params.fpga_points_per_line.ToString();
            json += ",\r\n            \"total_fpga_points\": ";
            json += scan_params.total_fpga_points.ToString();
            json += ",\r\n            \"fpga_total_scan_time_sec\": ";
            json += scan_params.fpga_total_scan_time_sec.ToString();
            json += "\r\n        },\r\n        \"mirror_parameters\": {\r\n            \"mirror_points_per_line\": ";
            // mirror_parameters
            mirror_params mp = new mirror_params(scan_params, BASE_FREQUENCY_HZ);
            json += mp.points_per_line;
            json += ",\r\n            \"mirror_points_per_axis\": ";
            json += mp.points_per_axis.ToString();
            json += ",\r\n            \"mirror_frequency_hz\": ";
            json += mp.frequency_hz.ToString();
            json += ",\r\n            \"mirror_total_scan_time_sec\": ";
            json += mp.total_scan_time_sec.ToString();
            json += ",\r\n            \"frequency_divisor\": ";
            json += mp.frequency_divisor.ToString();
            json += ",\r\n            \"base_frequency_hz\": ";
            json += mp.base_frequency_hz.ToString();
            // achieved_parameters
            json += "\r\n        },\r\n        \"achieved_parameters\": {\r\n            \"achieved_fpga_points_per_line\": ";
            double achieved_total_fpga_points = mp.total_scan_time_sec * 1000000.0 / 16.384;
            json += (achieved_total_fpga_points / scan_params.linesPerF / 2).ToString();
            json += ",\r\n            \"achieved_total_fpga_points\": ";
            json += achieved_total_fpga_points.ToString();
            json += ",\r\n            \"achieved_total_fpga_points_to_use\": ";
            json += ((uint)achieved_total_fpga_points).ToString();
            json += ",\r\n            \"achieved_fpga_points_per_line_diff\": ";
            json += (achieved_total_fpga_points / scan_params.linesPerF / 2).ToString();
            // timing_analysis
            json += "\r\n        },\r\n        \"timing_analysis\": {\r\n            \"time_difference_sec\": ";
            json += mp.time_difference_sec.ToString();
            json += ",\r\n            \"time_match_percent\": ";
            json += mp.time_match_percent.ToString();
            json += "\r\n        }\r\n    }\r\n}";
            return json;
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
        private void customMode_CheckedChanged(object sender, EventArgs e)
        {
            cModeParams.Enabled = customMode.Checked;
            cWaveForm.Enabled = customMode.Checked;
        }
        private void customFiles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0) return;
            int row = e.RowIndex;
            string fln = rowFiles[row];

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files (*" + fln + "|*" + fln;
            ofd.Title = "Select a Text File";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                customFiles.Rows[row].Cells[1].Value = ofd.FileName;
            }
        }
        private void selWF_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select a folder";
                dialog.ShowNewFolderButton = true;
                string x = "";
                string y = "";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string path = dialog.SelectedPath;
                    folderName.Text = path;
                    string[] files = Directory.GetFiles(path);

                    foreach (string file in files)
                    {
                        if (file.EndsWith("waveformX.csv"))
                            x = file;
                        else if (file.EndsWith("waveformY.csv"))
                            y = file;
                    }

                    if ((x == "") || (y == ""))
                        MessageBox.Show("wavefromX and/or waveformY not in folder");
                    else
                    {
                        cWaveForm.Rows[0].Cells[1].Value = x;
                        cWaveForm.Rows[1].Cells[1].Value = x;
                    }
                }
            }
        }
        private void genJSON_Click(object sender, EventArgs e)
        {
            JsonReady = false;
            List<string> wfFiles = new List<string>();
            foreach (DataGridViewRow row in cWaveForm.Rows)
            {
                string fln = row.Cells[1].Value.ToString();
                if (File.Exists(fln))
                    wfFiles.Add(fln);
            }
            if (wfFiles.Count < 2)
            {
                MessageBox.Show("Missing waveform file(s)");
                return;
            }
            int xCount = File.ReadLines(wfFiles[0]).Count();
            int yCount = File.ReadLines(wfFiles[1]).Count();
            if (xCount != yCount)
            {
                MessageBox.Show("waveform file line count not the same");
                return;
            }
            string json = SetJSON(xCount);
            string jfln = wfFiles[0].Replace("waveformX.csv", "scan_parameters.json");
            File.WriteAllText(jfln, json);
            JsonReady = true;
        }
        private void cModeParams_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            JsonReady = false;
        }
        private void impSM_Click(object sender, EventArgs e)
        {
            string modeName = mainfrm.modes[mainfrm.appSetting.scanModeNum];
            cModeParams.Rows[0].Cells[1].Value = mainfrm.scanModes[modeName].mirror.ToString();
            cModeParams.Rows[0].Cells[1].Value = mainfrm.scanModes[modeName].nPoints.ToString();
            cModeParams.Rows[3].Cells[1].Value = mainfrm.scanModes[modeName].hFOV.ToString();
            cModeParams.Rows[4].Cells[1].Value = mainfrm.scanModes[modeName].vFOV.ToString();
            cModeParams.Rows[5].Cells[1].Value = mainfrm.scanModes[modeName].hRes.ToString("0.00");
            cModeParams.Rows[6].Cells[1].Value = mainfrm.scanModes[modeName].vRes.ToString("0.00");
            cModeParams.Rows[7].Cells[1].Value = mainfrm.scanModes[modeName].lines.ToString();
            cModeParams.Rows[8].Cells[1].Value = mainfrm.scanModes[modeName].fRate.ToString();
        }
    }
    public class mirror_params
    {
        public int points_per_line;
        public int points_per_axis;
        public double frequency_hz;
        public double total_scan_time_sec;
        public int frequency_divisor;
        public int base_frequency_hz;
        public double time_difference_sec;
        public double time_match_percent;
        public mirror_params()
        {
            points_per_line = 0;
            points_per_axis = 0;
            frequency_hz = 0;
            total_scan_time_sec = 0;
            frequency_divisor = 0;
            base_frequency_hz = 0;
            time_difference_sec = 0;
            time_match_percent = 0;
        }
        public mirror_params(sm_params smPars, int base_freq)
        {
            mirror_params best_match = new mirror_params();

            double best_time_diff = double.MaxValue;
            double best_time_diff_this_points = double.MaxValue;
            bool is_better_match = false;

            int max_ppl = (int)(2500 / (smPars.linesPerF * 2));
            for (int ppl = max_ppl; ppl >= 0; ppl--)
            {
                // Calculate total mirror points per axis
                points_per_axis = ppl * (int)smPars.linesPerF * 2;

                // Skip if exceeds mirror hardware constraint
                if (points_per_axis > 2500)
                    continue;

                // Try different frequency divisors, starting with highest frequencies first
                // Start from divisor=1 (40kHz) down to higher divisors (lower frequencies)
                // But we'll prioritize finding the best match at highest possible frequency
                for (int div = 1; div < base_freq + 1; div++)
                {
                    frequency_hz = (double)base_freq / (double)div;
                    // Skip very low frequencies (below 1 Hz) for practical reasons
                    if (frequency_hz < 1.0)
                        break;

                    // Calculate mirror scan time for this configuration
                    total_scan_time_sec = (double)points_per_axis / frequency_hz;

                    // CONSTRAINT: Mirror time must be equal or larger than FPGA time
                    // Skip configurations where mirror time is shorter than FPGA time
                    if (total_scan_time_sec < smPars.fpga_total_scan_time_sec)
                        continue;

                    // Calculate time difference from FPGA target (mirror time >= FPGA time)
                    double time_diff = total_scan_time_sec - smPars.fpga_total_scan_time_sec;

                    // Check if this is better than current best match
                    // Prioritize higher frequencies when time differences are similar (within 1% of each other)
                    is_better_match = false;

                    if (time_diff < best_time_diff)
                        is_better_match = true;
                    else if (Math.Abs(time_diff - best_time_diff) < (smPars.fpga_total_scan_time_sec * 0.01))  // Within 1% of best time
                    {
                        // If time match is similar, prefer higher frequency
                        if (frequency_hz > best_match.frequency_hz)
                            is_better_match = true;
                     }

                    if (is_better_match)
                    {
                        best_time_diff = time_diff;
                        time_match_percent = 100.0 * (1.0 - time_diff / Math.Max(smPars.fpga_total_scan_time_sec, total_scan_time_sec));
                    }

                    best_match.points_per_line = ppl;
                    best_match.points_per_axis = points_per_axis;
                    best_match.frequency_hz = frequency_hz;
                    best_match.total_scan_time_sec = total_scan_time_sec;
                    best_match.frequency_divisor = div;
                    best_match.time_difference_sec = time_diff;
                    best_match.time_match_percent = time_match_percent;

                    // If we get a very close match, we can stop searching
                    if (time_diff < (smPars.fpga_total_scan_time_sec * 0.001))  // Within 0.1%
                        break;
                }

                // If we found a very good match, no need to try more points
                if (best_match.time_difference_sec < (smPars.fpga_total_scan_time_sec * 0.001))  // Within 0.1%
                    break;
            }

            points_per_line = best_match.points_per_line;
            points_per_axis = best_match.points_per_axis;
            frequency_hz = best_match.frequency_hz;
            total_scan_time_sec = best_match.total_scan_time_sec;
            frequency_divisor = best_match.frequency_divisor;
            base_frequency_hz = base_freq;
            time_difference_sec = best_match.time_difference_sec;
            time_match_percent = best_match.time_match_percent;

        }
    }
    public class sm_params
    {
        public double mirror;
        public uint points;
        public uint hFOV;
        public uint vFOV;
        public double hRes;
        public double vRes;
        public uint linesPerF;
        public uint fpga_points_per_line;
        public uint total_fpga_points;
        public double fpga_total_scan_time_sec; 

        public sm_params()
        {
            mirror = 0;
            points = 0;
            hFOV = 0;
            vFOV = 0;
            hRes = 0;
            vRes = 0;
            linesPerF = 0;
        }
    }
}
