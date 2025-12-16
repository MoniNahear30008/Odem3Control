using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdemControl
{
    public partial class Form1
    {
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
        private void SetDebugView()
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

            OTDelay.Value = lastOTdelay;
            foreach (string r in WriteRegsAdd.Keys)
                RegsNames.Items.Add(r);
            RegsNames.SelectedIndex = 0;

            foreach (string r in I2Cs.Keys)
                I2CsList.Items.Add(r);
            I2CsList.SelectedIndex = 0;

            foreach (string r in Vectors.Keys)
                vecList.Items.Add(r);
            vecList.SelectedIndex = 0;

            if (forceDbgMode)
            {
                splitContainer4.Panel2Collapsed = false;
                this.Width = 1500;
                groupBox5.Visible = false;
                tabControl1.Visible = true;
                debugMode.Visible = false;
            }

        }
        private void GetFIles()
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
        private void LoadFromFile()
        {
            JsonReady = false;
            genJSON.ForeColor = Color.Red;
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
        private void SaveToFile()
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
        private void GetWfFiles()
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
        private void GenJson()
        {
            genJSON.ForeColor = Color.Red;
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
        private int getVal(string sval)
        {
            int val = 0;
            if (sval.StartsWith("0x"))
                val = int.Parse(sval.Replace("0x", ""), System.Globalization.NumberStyles.HexNumber);
            else
                val = int.Parse(sval);

            return val;
        }
        private void CustomCofig()
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

            confFiles["AWG"].Clear();
            confFiles["badGoodIndxs_High"].Clear();
            confFiles["badGoodIndxs_Low"].Clear();
            confFiles["2kWin"].Clear();
            confFiles["128Bins_Final"].Clear();
            confFiles["blackmanHarris_DEC"].Clear();

            string[] lines = File.ReadAllLines(setFiles[0]);
            foreach (string l in lines)
                confFiles["AWG"].Add(uint.Parse(l));

            lines = File.ReadAllLines(setFiles[1]);
            foreach (string l in lines)
                confFiles["badGoodIndxs_High"].Add(uint.Parse(l));

            lines = File.ReadAllLines(setFiles[2]);
            foreach (string l in lines)
                confFiles["badGoodIndxs_Low"].Add(uint.Parse(l));

            lines = File.ReadAllLines(setFiles[3]);
            foreach (string l in lines)
                confFiles["128Bins_Final"].Add(uint.Parse(l));

            lines = File.ReadAllLines(setFiles[4]);
            foreach (string l in lines)
                confFiles["blackmanHarris_DEC"].Add(uint.Parse(l));

            lines = File.ReadAllLines(setFiles[5]);
            foreach (string l in lines)
                confFiles["2kWin"].Add(uint.Parse(l));


           deviceParameters["Capture_Delay"] = getVal(customParams.Rows[0].Cells[1].Value.ToString());
           deviceParameters["Chirp_AWG_gain"] = getVal(customParams.Rows[5].Cells[1].Value.ToString());
           deviceParameters["LO"] = getVal(customParams.Rows[11].Cells[1].Value.ToString());
           deviceParameters["TxSOA1"] = getVal(customParams.Rows[12].Cells[1].Value.ToString());
           deviceParameters["TxSOA2"] = getVal(customParams.Rows[13].Cells[1].Value.ToString());
           deviceParameters["Tx3_0_9"] = getVal(customParams.Rows[14].Cells[1].Value.ToString());
           deviceParameters["Tx3_10_19"] = getVal(customParams.Rows[15].Cells[1].Value.ToString());
           deviceParameters["Tx3_20_29"] = getVal(customParams.Rows[16].Cells[1].Value.ToString());
           deviceParameters["Tx3_30_39"] = getVal(customParams.Rows[17].Cells[1].Value.ToString());

            GeneralParameters["Sensitivity"] = getVal(customParams.Rows[1].Cells[1].Value.ToString());
            GeneralParameters["CFAR"] = getVal(customParams.Rows[2].Cells[1].Value.ToString());
            GeneralParameters["Spurs"] = getVal(customParams.Rows[3].Cells[1].Value.ToString());
            GeneralParameters["Retro"] = getVal(customParams.Rows[4].Cells[1].Value.ToString());
            GeneralParameters["PM1"] = getVal(customParams.Rows[8].Cells[1].Value.ToString());
            GeneralParameters["PM2"] = getVal(customParams.Rows[9].Cells[1].Value.ToString());
            GeneralParameters["SOA"] = getVal(customParams.Rows[10].Cells[1].Value.ToString());
            GeneralParameters["OTD"] = getVal(customParams.Rows[6].Cells[1].Value.ToString());
            OTDelay.Value = GeneralParameters["OTD"];
            ConfigNow("");
        }
    }
}
