using ClosedXML.Excel;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

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
        uint I2C_ch = 0;
        uint I2C_dev = 0;
        uint I2C_reg = 0;
        uint I2C_val = 0;
        Dictionary<string, uint> Vectors = new Dictionary<string, uint>()
        {
            {"badGoodIndxs_High", 0xFF20002C },
            {"badGoodIndxs_Low", 0xFF200028 },
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
        Dictionary<string, object> OT_Delay = new Dictionary<string, object>();
        Dictionary<string, object> ScanModes = new Dictionary<string, object>();
        Dictionary<string, object> devConfig = new Dictionary<string, object>();

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

            foreach (string r in I2CConf.Keys)
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
                this.Location = new System.Drawing.Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                    (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            }
        }
        private void DownLoadFromSharePoint()
        {
        }
        private bool parseXlsx(string path)
        {
            bool all = true;
            using (var workbook = new XLWorkbook(path+"\\odem_op.xlsx"))
            {
                all &= workbook.Worksheets.Contains("OT Delay");
                all &= workbook.Worksheets.Contains("Scan modes");
                all &= workbook.Worksheets.Contains("Config");
                if (!all)
                    return true;
                GetOtDelay(workbook.Worksheet("OT Delay"));
                getSmPars(workbook.Worksheet("Scan modes"));
//                getConfigPars(workbook.Worksheet("Config"));
            }
            return false;
        }
        private void getConfigPars(IXLWorksheet worksheet)
        {
            Dictionary<int, string> smIdx = new Dictionary<int, string>();
            int totalRows = worksheet.Rows().Count();
            var row = worksheet.Row(1);       // Row 1
            int totalCells = row.Cells().Count();
            int idx = 0;
            for (int i = 0; i < totalCells + 1; i++)
            {
                string mn = row.Cell(i + 3).GetString();
                if (mn == "")
                    continue;
                ScanModes.Add(mn, new Dictionary<string, double>());
                smIdx.Add(idx, mn);
                idx++;
            }

            Dictionary<string, List<double>> pars = new Dictionary<string, List<double>>();
            for (int r = 2; r <= totalRows; r++)
            {
                row = worksheet.Row(r);
                if (row.Cells().Count() < totalCells)
                    continue;
                string pname = worksheet.Row(r).Cell(2).GetString();
                if (pname == "")
                    continue;
                pars.Add(pname, new List<double>());
                List<string> p = new List<string>();
                for (int s = 0; s < smIdx.Count(); s++)
                {
                    string pval = row.Cell(s + 3).GetString();
                    var match = Regex.Match(pval, @"-?\d+(\.\d+)?");
                    pars[pname].Add(double.Parse(match.Value));
                }
            }

            for (int s = 0; s < smIdx.Count(); s++)
            {
                string sname = smIdx[s];
                foreach (KeyValuePair<string, List<double>> kv in pars)
                {
                    ((Dictionary<string, double>)ScanModes[sname]).Add(kv.Key, kv.Value[s]);
                }
            }
        }
        private void getSmPars(IXLWorksheet worksheet)
        {
            Dictionary<int, string> smIdx = new Dictionary<int, string>();
            int totalRows = worksheet.Rows().Count();
            var row = worksheet.Row(1);       // Row 1
            int totalCells = row.Cells().Count();
            int idx = 0;
            for (int i = 0; i < totalCells + 1; i++)
            {
                string mn = row.Cell(i + 3).GetString();
                if (mn == "")
                    continue;
                ScanModes.Add(mn, new Dictionary<string, double>());
                smIdx.Add(idx, mn);
                idx++;
            }

            Dictionary<string, List<double>> pars = new Dictionary<string, List<double>>();
            for (int r = 2; r <= totalRows; r++)
            {
                row = worksheet.Row(r);
                if (row.Cells().Count() < totalCells)
                    continue;
                string pname = worksheet.Row(r).Cell(2).GetString();
                if (pname == "")
                    continue;
                pars.Add(pname, new List<double>());
                List<string> p = new List<string>();
                for (int s = 0; s < smIdx.Count(); s++)
                {
                    string pval = row.Cell(s + 3).GetString();
                    var match = Regex.Match(pval, @"-?\d+(\.\d+)?");
                    pars[pname].Add(double.Parse(match.Value));
                }
            }

            for (int s = 0; s < smIdx.Count(); s++)
            {
                string sname = smIdx[s];
                foreach (KeyValuePair<string, List<double>> kv in pars)
                {
                    ((Dictionary<string, double>)ScanModes[sname]).Add(kv.Key, kv.Value[s]);
                }
            }
        }
        private void GetOtDelay(IXLWorksheet worksheet)
        {
            int totalRows = worksheet.Rows().Count();
            var row = worksheet.Row(1);       // Row 1
            int totalCells = row.Cells().Count();
            for (int i = 1; i < totalCells; i++)
                OT_Delay.Add(row.Cell(i + 1).GetString(), null);

            Dictionary<string, List<int>> ot = new Dictionary<string, List<int>>();
            for (int r = 2; r <= totalRows; r++)
            {
                string modeName = worksheet.Row(r).Cell(1).GetString();
                if (modeName == "")
                    continue;
                ot.Add(modeName, new List<int>());
                for (int i = 1; i < totalCells; i++)
                {
                    string cellVal = worksheet.Row(r).Cell(i + 1).GetString();
                    ot[modeName].Add(int.Parse(cellVal));
                }
            }

            int dnun = 0;
            foreach (string dname in OT_Delay.Keys)
            {
                Dictionary<string, int> otmp = new Dictionary<string, int>();
                foreach (string mname in ot.Keys)
                    otmp.Add(mname, ot[mname][dnun]);
                OT_Delay[dname] = otmp;
                dnun++;
            }
        }
        private void GetOtDelay(string path)
        {
            List<string> lst = System.IO.File.ReadAllLines(path +"\\OT_Delay.csv").ToList();
            List<string> dv = lst[0].Split(',').ToList();
            dv.RemoveAt(0);
            foreach (string d in dv)
                OT_Delay.Add(d, null);

            Dictionary<string, List<int>> ot = new Dictionary<string, List<int>>();
            for (int l = 1; l < lst.Count(); l++)
            {
                List<string> parts = lst[l].Split(',').ToList();
                string modeName = parts[0];
                if (modeName == "")
                    continue;
                ot.Add(modeName, new List<int>());
                for (int i = 1; i < parts.Count(); i++)
                {
                    ot[modeName].Add(int.Parse(parts[i]));
                }
            }

            int dnun = 0;
            foreach (string dname in OT_Delay.Keys)
            {
                Dictionary<string, int> otmp = new Dictionary<string, int>();
                foreach (string mname in ot.Keys)
                    otmp.Add(mname, ot[mname][dnun]);
                OT_Delay[dname] = otmp;
                dnun++;
            }
        }
        private void GenerateEncryptedFile()
        {
            DownLoadFromSharePoint();
            OT_Delay.Clear();
            ScanModes.Clear();

            List<string> allFiles = new List<string>();
            Dictionary<string, string> dict = new Dictionary<string, string>()
            {
                {"badGoodIndxs_High", "" },
                {"badGoodIndxs_Low", "" },
                {"128Bins_Final", ""},
                {"AWG", ""},
                {"2kWin", ""},
                {"blackmanHarris_DEC", ""},
                {"General_Params","" }
            };
            int found = 0;
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select a folder";
                dialog.ShowNewFolderButton = true;
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                    string path = dialog.SelectedPath;
                    folderName.Text = path;

                if (!File.Exists(path + "\\odem_op.xlsx"))
                {
                    MessageBox.Show(path + "\\odem_op.xlsx not found");
                    return;
                }
                bool err = parseXlsx(path);
                if (err)
                {
                    MessageBox.Show("Error while Parsing " + path + "\\odem_op.xlsx not found");
                    return;
                }

                string[] folders = Directory.GetDirectories(path);
                foreach (string folder in folders)
                    {
                        string dev = folder.Substring(folder.IndexOf("SN"));
                        string[] files = Directory.GetFiles(folder);
                        foreach (string file in files)
                        { 
                            if (file.EndsWith("AWG.txt"))
                            {
                                dict["AWG"] = file;
                                found++;
                            }
                            else if (file.EndsWith("badGoodIndxs_High.txt"))
                            {
                                dict["badGoodIndxs_High"] = file;
                                found++;
                            }
                            else if (file.EndsWith("badGoodIndxs_Low.txt"))
                            {
                                dict["badGoodIndxs_Low"] = file;
                                found++;
                            }
                            else if (file.EndsWith("128Bins_Final.txt"))
                            {
                                dict["128Bins_Final"] = file;
                                found++;
                            }
                            else if (file.EndsWith("blackmanHarris_DEC.txt"))
                            {
                                dict["blackmanHarris_DEC"] = file;
                                found++;
                            }
                            else if (file.EndsWith("2kWin.txt"))
                            {
                                dict["2kWin"] = file;
                                found++;
                            }
                            else if (file.EndsWith("General_Params.csv"))
                            {
                                dict["General_Params"] = file;
                                found++;
                            }
                        }
                        if (found < dict.Count)
                        {
                            MessageBox.Show("Not all required files found in folder for " + folder);
                            return;
                        }

                        allFiles.Add("New Device: " + dev);
                        foreach (KeyValuePair<string, string> f in dict)
                        {
                            allFiles.Add("New file: " + f.Key);
                            List<string> filelines = System.IO.File.ReadAllLines(f.Value).ToList();
                            allFiles.AddRange(filelines);
                            if (f.Key == "General_Params")
                            {
                                Dictionary<string, int> devOTD = (Dictionary<string, int>)OT_Delay[dev];
                                List<string> ot = new List<string>();
                                foreach (KeyValuePair<string, int> o in devOTD)
                                    ot.Add(o.Key + "," + o.Value.ToString());
                                allFiles.AddRange(ot);
                            }
                        }

                    }
            }

            EncryptFile(allFiles);
        }
        private void EncryptFile(List<string> data)
        {
            // 32-byte (256-bit) key
            byte[] key = Convert.FromBase64String("w4Zs9kVjX4R9P8vYx8a2+JQ+H4R0kBzLhJ6xK0uFJX4=");
            // 16-byte (128-bit) IV
            byte[] iv = Convert.FromBase64String("h1V3fT9tQ+X6sE1sFvJ3Wg==");

            using Aes aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;

            string filePath = "c:\\lidwave\\sensor_info.dat";
            using StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8);

            foreach (string s in data)
            {
                byte[] plainBytes = Encoding.UTF8.GetBytes(s);
                byte[] encrypted = aes.CreateEncryptor()
                    .TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                writer.WriteLine(Convert.ToBase64String(encrypted));
            }
            writer.Close();
        }
        private void GetEncryptedFile(string fln)
        {
            DevInFile.Clear();
            AllDevicesFiles.Clear();
            var output = new List<string>();
            // 32-byte (256-bit) key
            byte[] key = Convert.FromBase64String("w4Zs9kVjX4R9P8vYx8a2+JQ+H4R0kBzLhJ6xK0uFJX4=");
            // 16-byte (128-bit) IV
            byte[] iv = Convert.FromBase64String("h1V3fT9tQ+X6sE1sFvJ3Wg==");

            using Aes aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;

            string filePath = "c:\\lidwave\\sensor_info.dat";
            if (System.IO.File.Exists(filePath) == false)
                return;


            foreach (string line in System.IO.File.ReadLines(filePath))
            {
                byte[] encrypted = Convert.FromBase64String(line);
                byte[] decrypted = aes.CreateDecryptor()
                    .TransformFinalBlock(encrypted, 0, encrypted.Length);

                string nl = Encoding.UTF8.GetString(decrypted);
                AllDevicesFiles.Add(nl);
                if (nl.Contains("New Device: "))
                {
                    string dev = nl.Replace("New Device: ", "");
                    DevInFile.Add(dev, AllDevicesFiles.Count);
                }
            }
        }
        private void GetDeviceFiles(string dev)
        {
            int start = DevInFile[dev];
            string currentFile = "";
            bool isParams = false;
            for (int i = start; i < AllDevicesFiles.Count; i++)
            {
                string l = AllDevicesFiles[i];
                if (l.StartsWith("New Device: "))
                    break;
                else if (l.StartsWith("New file: "))
                {
                    currentFile = l.Replace("New file: ", "");
                    isParams = currentFile == "General_Params";
                    if (!isParams)
                    {
                        if (confFiles.ContainsKey(currentFile))
                            confFiles[currentFile].Clear();
                    }
                    continue;
                }
                else if (isParams)
                {
                    string[] parts = l.Split(',');
                    if (parts.Length != 2)
                        continue;
                    string pname = parts[0].Trim();
                    int pval = 0;
                    if (parts[1].Contains("0x"))
                        pval = int.Parse(parts[1].Replace("0x", ""), System.Globalization.NumberStyles.HexNumber);
                    else
                        pval = int.Parse(parts[1]);
                    if (deviceParameters.ContainsKey(pname))
                        deviceParameters[pname] = (int)pval;
                    else
                    {
                        MessageBox.Show("Unknown parameter in general parameters file.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                }
                else
                    confFiles[currentFile].Add(uint.Parse(l));

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
                if (System.IO.File.Exists(fln))
                    wfFiles.Add(fln);
            }
            if (wfFiles.Count < 2)
            {
                MessageBox.Show("Missing waveform file(s)");
                return;
            }
            int xCount = System.IO.File.ReadLines(wfFiles[0]).Count();
            int yCount = System.IO.File.ReadLines(wfFiles[1]).Count();
            if (xCount != yCount)
            {
                MessageBox.Show("waveform file line count not the same");
                return;
            }
            string json = SetJSON(xCount);
            string jfln = wfFiles[0].Replace("waveformX.csv", "scan_parameters.json");
            System.IO.File.WriteAllText(jfln, json);
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
                    if (System.IO.File.Exists(fln))
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

            string[] lines = System.IO.File.ReadAllLines(setFiles[0]);
            foreach (string l in lines)
                confFiles["AWG"].Add(uint.Parse(l));

            lines = System.IO.File.ReadAllLines(setFiles[1]);
            foreach (string l in lines)
                confFiles["badGoodIndxs_High"].Add(uint.Parse(l));

            lines = System.IO.File.ReadAllLines(setFiles[2]);
            foreach (string l in lines)
                confFiles["badGoodIndxs_Low"].Add(uint.Parse(l));

            lines = System.IO.File.ReadAllLines(setFiles[3]);
            foreach (string l in lines)
                confFiles["128Bins_Final"].Add(uint.Parse(l));

            lines = System.IO.File.ReadAllLines(setFiles[4]);
            foreach (string l in lines)
                confFiles["blackmanHarris_DEC"].Add(uint.Parse(l));

            lines = System.IO.File.ReadAllLines(setFiles[5]);
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
        private List<uint> ReadEEPROM(uint add, uint len)
        {
            List<uint> t = new List<uint>();
            string err = ReadI2C(0, 0x70, 1, 0x50, 0x24, add, len, out t);
            if (err != "")
                return null;
            return t;
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
