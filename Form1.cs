using System.Reflection;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OdemControl
{
    public partial class Form1 : Form
    {
        string version = "2.50.00";

        public bool forceDbgMode = false;
        bool noDevice = false;
        public appSettings appSetting;
        public bool isConnected = false;
        public List<string> deviceID = new List<string>();
        public Dictionary<string, scanMode> scanModes = new Dictionary<string, scanMode>();
        public List<string> modes = new List<string>();
        public Dictionary<string, List<uint>> confFiles = new Dictionary<string, List<uint>>();
        public List<string> devicesList = new List<string>();
        int confState = (int)confStates.IDLE;
        public Dictionary<string, int> deviceParameters = new Dictionary<string, int>()
        {
            {"Capture_Delay" ,3600},
            {"Chirp_AWG_gain",7000},
            {"LO",7000},
            {"TxSOA1",2050},
            {"TxSOA2",5050},
            {"Tx3_0_9",5050},
            {"Tx3_10_19",5050},
            {"Tx3_20_29",5050},
            {"Tx3_30_39",5050},
            {"MainBoard",1 },
            {"DriverBoard",1 },
        };
        public Dictionary<string, int> GeneralParameters = new Dictionary<string, int>()
        {
            {"Sensitivity" ,0},
            {"CFAR",0},
            {"Spurs",0},
            {"Retro",10000},
            {"PM1",0 },
            {"PM2",0 },
            {"SOA",0 },
            {"OTD", 0 }
        };
        bool loggingEnabled = false;
        bool debugmodeEnabled = false;
        bool dataLoggingEnabled = false;
        private StreamWriter logFile;
        int readTempCounter = 0;
        bool configuring = false;
        Debug db = null;
        bool EnablePing = true;
        Dictionary<string, object> OT_Delay = new Dictionary<string, object>();
        Dictionary<string, object> Devices_Params = new Dictionary<string, object>();
        int pingLost = 0;
        bool dbgMode = false;
        bool deviceConfigured = false;
        string iniDev = "";
        int connectCnt = 0;
        public Form1()
        {
            InitializeComponent();

            splitContainer3.Panel2Collapsed = true;
            splitContainer4.Panel2Collapsed = true;
            this.Width = 750;

            string path = @"C:\Lidwave";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            this.Text = "ODEM Control by Lidwave. Version: " + version;

            appSetting = new appSettings();

            // Generate exection
            //int y = 0;
            //int x = 1 / y;

            Getini();
            dbgMode |= forceDbgMode;
            debugMode.Visible = dbgMode;
            debugmodeEnabled = dbgMode;
            loggingEnabled |= dbgMode;
            dataLoggingEnabled |= dbgMode;
            if (loggingEnabled)
                OpenLogFile();

            ConnectedTo.Visible = !dbgMode;
            devices.Visible = dbgMode;

            noDevice = SetVars();

            this.Refresh();
            timer2.Start();
        }
        private void Getini()
        {
            if (!File.Exists("C:\\Lidwave\\Odem.ini"))
                File.Create("C:\\Lidwave\\Odem.ini");

            long size = new FileInfo("C:\\Lidwave\\Odem.ini").Length;
            if (size == 0)
                return;

            string[] lines = File.ReadAllLines("C:\\Lidwave\\Odem.ini");
            if (lines.Length == 0)
                return;
            foreach (string l in lines)
            {
                if (l == "-forcedbg")
                    forceDbgMode = true;
                else if (l == "-dbg")
                    dbgMode = true;
                else if (l == "-le")
                {
                    dataLoggingEnabled = true;
                    loggingEnabled = true;
                }
                else if (l == "-l")
                    loggingEnabled = true;
                else if (l.StartsWith("SN"))
                    iniDev = l;
            }

        }
        private bool SetVars()
        {
            //string op = Dns.GetHostEntry(Dns.GetHostName()).AddressList
            //  .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)?
            //  .ToString() ?? "No IPv4 found";

            tempTable.Rows.Add("Optical chip", "");
            tempTable.Rows.Add("Scanner", "");
            tempTable.Rows.Add("Main Board", "");
            tempTable.Rows.Add("Laser", "");
            tempTable.ClearSelection();

            IPAddredd.Text = _ipAddress;
            IPPort.Text = _port.ToString();

            // Set devices lists
            var assembly = Assembly.GetExecutingAssembly();
            string[] resources = assembly.GetManifestResourceNames();
            foreach (string r in resources)
            {
                if (r.Contains("badGoodIndxs_High"))
                {
                    string devName = r.Split('.')[2];
                    devicesList.Add(devName);
                    deviceID.Add(devName);
                }
            }

            if (dbgMode)
            {
                foreach (string devName in devicesList)
                    devices.Items.Add(devName);
            }

            // configuration files dictionaries
            confFiles.Add("badGoodIndxs_High", new List<uint>());
            confFiles.Add("badGoodIndxs_Low", new List<uint>());
            confFiles.Add("2kWin", new List<uint>());
            confFiles.Add("128Bins_Final", new List<uint>());
            confFiles.Add("blackmanHarris_DEC", new List<uint>());
            confFiles.Add("AWG", new List<uint>());

            // Set scan mode parameters table
            ModeParams.Rows.Clear();
            ModeParams.Rows.Add("Horizontal FOV", ".. °");
            ModeParams.Rows.Add("Vertical FOV", ".. °");
            ModeParams.Rows.Add("Horizontal Res.", ".. °");
            ModeParams.Rows.Add("Vertical Res.", ".. °");
            ModeParams.Rows.Add("Lines per frame", ".. °");
            ModeParams.Rows.Add("frame rate", ".. FPS");

            // Get scan modes from csv file
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OdemControl.Optotune.modes_params.csv");
            if (stream == null)
            {
                MessageBox.Show("Failed to read scan modes paramaters file.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            StreamReader reader = new StreamReader(stream);
            string allmodes = reader.ReadToEnd();
            List<string> allmodesList = allmodes.Split("\r\n").ToList();
            modes = allmodesList[0].Split(",").ToList();
            modes.RemoveRange(0, 2);
            int mn = 1;
            foreach (string m in modes)
            {
                scanModes.Add(m, new scanMode());
                scanModes[m].modeNum = mn;
                scanModes[m].folder = "Mode" + mn.ToString();
                mn++;
            }
            List<string> modes1 = scanModes.Keys.ToList();
            for (int l = 1; l < allmodesList.Count; l++)
            {
                if (allmodesList[l].Contains(",,")) continue;
                if (allmodesList[l].Contains("Mirror"))
                {
                    List<string> nl = allmodesList[l].Split(',').ToList();
                    int np = 2;
                    foreach (string m in modes1)
                        scanModes[m].mirror = int.Parse(nl[np++]);

                }
                else if (allmodesList[l].Contains("points"))
                {
                    List<string> nl = allmodesList[l].Split(',').ToList();
                    int np = 2;
                    foreach (string m in modes1)
                        scanModes[m].nPoints = int.Parse(nl[np++]);

                }
                else if (allmodesList[l].Contains("Horizontal FOV"))
                {
                    List<string> nl = allmodesList[l].Split(',').ToList();
                    int np = 2;
                    foreach (string m in modes1)
                        scanModes[m].hFOV = int.Parse(nl[np++].Split(" ")[0]);

                }
                else if (allmodesList[l].Contains("Horizontal res"))
                {
                    List<string> nl = allmodesList[l].Split(',').ToList();
                    int np = 2;
                    foreach (string m in modes1)
                        scanModes[m].hRes = double.Parse(nl[np++].Split(" ")[0]);
                }
                else if (allmodesList[l].Contains("Vertical FOV"))
                {
                    List<string> nl = allmodesList[l].Split(',').ToList();
                    int np = 2;
                    foreach (string m in modes1)
                        scanModes[m].vFOV = int.Parse(nl[np++].Split(" ")[0]);

                }
                else if (allmodesList[l].Contains("Vertical res"))
                {
                    List<string> nl = allmodesList[l].Split(',').ToList();
                    int np = 2;
                    foreach (string m in modes1)
                        scanModes[m].vRes = double.Parse(nl[np++].Split(" ")[0]);
                }
                else if (allmodesList[l].Contains("Lines per frame"))
                {
                    List<string> nl = allmodesList[l].Split(',').ToList();
                    int np = 2;
                    foreach (string m in modes1)
                        scanModes[m].lines = int.Parse(nl[np++].Split(" ")[0]);

                }
                else if (allmodesList[l].Contains("Frame rate"))
                {
                    List<string> nl = allmodesList[l].Split(',').ToList();
                    int np = 2;
                    foreach (string m in modes1)
                        scanModes[m].fRate = int.Parse(nl[np++].Split(" ")[0]);

                }
            }

            foreach (string m in scanModes.Keys)
            {
                scanMode.Items.Add(m);
                deviceParameters.Add(m, 0);
            }
            scanMode.SelectedIndex = Math.Min(appSetting.scanModeNum, modes.Count() - 1);

            // Get OT delay
            stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OdemControl.Optotune.OT_Delay.csv");
            if (stream == null)
            {
                MessageBox.Show("Failed to read scan modes paramaters file.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            reader = new StreamReader(stream);
            string otd = reader.ReadToEnd();
            List<string> otdl = otd.Split("\r\n").ToList();
            List<string> dv = otdl[0].Split(',').ToList();
            dv.RemoveAt(0);
            foreach (string d in dv)
                OT_Delay.Add(d, null);

            Dictionary<string, List<int>> ot = new Dictionary<string, List<int>>();
            for (int l = 1; l < otdl.Count(); l++)
            {
                List<string> parts = otdl[l].Split(',').ToList();
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

            if (appSetting.sensitivity == 0)
                SensitivityNormal.Checked = true;
            else
                sensitivityHigh.Checked = true;

            if (dbgMode)
            {
                if (devicesList.Count == 1)
                    appSetting.deviceNum = 0;
                devices.SelectedIndex = Math.Min(appSetting.deviceNum, devicesList.Count() - 1);
            }
            return false;
        }
        private void SensitivityNormal_CheckedChanged(object sender, EventArgs e)
        {
            if (deviceConfigured)
            {
                MessageBox.Show("Please restart device and reconnect before changing scan mode");
                DevieLost();
                return;
            }

            if (SensitivityNormal.Checked)
                appSetting.sensitivity = 0;
            else
                appSetting.sensitivity = 1;
        }
        private void scanMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateScanMode(scanMode.SelectedIndex);
            streamBox(false);
            if (deviceConfigured)
            {
                MessageBox.Show("Please restart device and reconnect before changing scan mode");
                DevieLost();
                return;
            }
        }
        private void streamBox(bool enable)
        {
            sStart.Enabled = enable;
            sStop.Enabled = enable;
        }
        private void updateScanMode(int mode)
        {
            appSetting.scanModeNum = mode;
            string modeName = modes[mode];
            ModeParams.Rows[0].Cells[1].Value = scanModes[modeName].hFOV.ToString() + " °";
            ModeParams.Rows[1].Cells[1].Value = scanModes[modeName].vFOV.ToString() + " °";
            ModeParams.Rows[2].Cells[1].Value = scanModes[modeName].hRes.ToString() + " °";
            ModeParams.Rows[3].Cells[1].Value = scanModes[modeName].vRes.ToString() + " °";
            ModeParams.Rows[4].Cells[1].Value = scanModes[modeName].lines.ToString();
            ModeParams.Rows[5].Cells[1].Value = scanModes[modeName].fRate.ToString() + " FPS";
            ModeParams.ClearSelection();
            return;

            Dictionary<string, List<uint>> wfFiles = new Dictionary<string, List<uint>>();
            wfFiles.Add("waveformX", new List<uint>());
            wfFiles.Add("waveformY", new List<uint>());

            string resourceName = "OdemControl.Optotune." + scanModes[modeName].folder + ".";
            List<string> files = wfFiles.Keys.ToList();
            Stream stream;
            StreamReader reader;

            foreach (string f in files)
            {
                wfFiles[f].Clear();
                string fname = resourceName + f + ".csv";
                stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fname);
                if (stream == null)
                {
                    MessageBox.Show("Failed to read device configuation file.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                reader = new StreamReader(stream);
                string content = reader.ReadToEnd();
                List<string> lc;
                if (content.Contains("\r\n"))
                    lc = content.Split("\r\n").ToList();
                else
                    lc = content.Split("\n").ToList();
                lc.RemoveAt(lc.Count() - 1);

                foreach (string n in lc)
                    wfFiles[f].Add(BitConverter.SingleToUInt32Bits(float.Parse(n)));
            }
        }
        private void confDev_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                MessageBox.Show("Device not connected");
                return;
            }
            GeneralParameters["Sensitivity"] = (int)sensitivity[appSetting.sensitivity];
            if (appSetting.sensitivity == 1)
            {
                GeneralParameters["CFAR"] = 0x00000101;
                GeneralParameters["Spurs"] = 0x00003C78;
            }
            else
            {
                GeneralParameters["CFAR"] = 0x00000303;
                GeneralParameters["Spurs"] = 0x70033C78;
            }

            GeneralParameters["Retro"] = 10000;
            GeneralParameters["PM1"] = 0;
            GeneralParameters["PM2"] = 0;
            GeneralParameters["SOA"] = 2;

            GeneralParameters["OTD"] = deviceParameters[modes[appSetting.scanModeNum]];
            ConfigNow("");
        }
        public async void ConfigNow(string wfPath)
        {
            configuring = true;
            pingLost = 10;
            this.Cursor = Cursors.WaitCursor;
            this.Enabled = false;
            deviceState.Text = "";
            deviceState.ForeColor = Color.Black;
            appSetting.Update(true);
            confState = (int)confStates.IDLE;
            await cofigdeviceAsync(wfPath);
            if (confState == (int)confStates.DONE)
            {
                deviceState.Text = "Device ready";
                deviceState.ForeColor = Color.Green;
                LogMessage("Configuring: Done");
                streamBox(true);
            }
            else
            {
                deviceState.Text = "Device configuration error";
                deviceState.ForeColor = Color.Red;
                LogMessage("Configuring: Error");
                streamBox(false);
            }
            deviceConfigured = true;
            this.Cursor = Cursors.Default;
            this.Enabled = true;
            configuring = false;
        }
        public static byte[] GetBytesBigEndian(uint value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            return bytes;
        }
        private void debugMode_Click(object sender, EventArgs e)
        {
            StartDbg();
        }
        private void StartDbg()
        {
            splitContainer4.Panel2Collapsed = false;
            this.Width = 1500;
            debugMode.Visible = false;
            return;

            db = new Debug();
            db.StartPosition = FormStartPosition.CenterParent;
            db.Show();
            db.SetDebugForm(this);

            Rectangle screen = Screen.PrimaryScreen.WorkingArea;

            int totalWidth = this.Width + db.Width;
            int startX = (screen.Width - totalWidth) / 2;
            int y = (screen.Height - this.Height) / 2;

            this.Location = new Point(startX, y);
            db.Location = new Point(startX + this.Width, y);
            //db.StartPosition = FormStartPosition.Manual;
            //db.Location = new Point(
            //    this.Location.X + this.Width,
            //    this.Location.Y
            //);
        }
        private void devices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (deviceConfigured)
            {
                MessageBox.Show("Please restart device and reconnect before changing device");
                DevieLost();
                return;
            }
            appSetting.deviceNum = devices.SelectedIndex;
            UpdateConfFiles();
        }
        private void UpdateConfFiles()
        {
            string resourceName = "OdemControl.Devices." + devicesList[appSetting.deviceNum] + ".";
            List<string> files = confFiles.Keys.ToList();
            Stream stream;
            StreamReader reader;
            List<string> lc;
            // Get general parameters
            string fln = resourceName + "General_Params.csv";
            LogMessage("Update file: " + fln);
            stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fln);
            if (stream == null)
            {
                MessageBox.Show("Failed to read device configuation file.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            reader = new StreamReader(stream);
            string content = reader.ReadToEnd();
            lc = content.Split("\r\n").ToList();
            foreach (string n in lc)
            {
                string[] parts = n.Split(',');
                if (parts.Length != 2)
                    continue;
                string pname = parts[0].Trim();
                uint pval = 0;
                if (parts[1].Contains("0x"))
                    pval = uint.Parse(parts[1].Replace("0x", ""), System.Globalization.NumberStyles.HexNumber);
                else
                    pval = uint.Parse(parts[1]);
                if (deviceParameters.ContainsKey(pname))
                    deviceParameters[pname] = (int)pval;
                else
                {
                    MessageBox.Show("Unknown parameter in general parameters file.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            LogMessage("Update device " + devicesList[appSetting.deviceNum] + " Main Board version: " + deviceParameters["MainBoard"].ToString()
                + "; Driver Board version: " + deviceParameters["DriverBoard"].ToString());

            Dictionary<string, int> dp = OT_Delay[devicesList[appSetting.deviceNum]] as Dictionary<string, int>;
            foreach (string m in dp.Keys)
            {
                if (deviceParameters.ContainsKey(m))
                    deviceParameters[m] = dp[m];
            }

            // Get common file
            string commonFile = "blackmanHarris_DEC";
            foreach (string f in files)
            {
                confFiles[f].Clear();
                if (f == commonFile)
                {
                    fln = "OdemControl.Devices." + f + ".txt";
                    stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fln);
                }
                else
                {
                    fln = resourceName + f + ".txt";
                    stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fln);
                }
                LogMessage("Update file: " + fln);
                if (stream == null)
                {
                    MessageBox.Show("Failed to read device configuation file.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                reader = new StreamReader(stream);
                content = reader.ReadToEnd();
                if (content.Contains("\r\n"))
                    lc = content.Split("\r\n").ToList();
                else
                    lc = content.Split("\n").ToList();
                lc.RemoveAt(lc.Count() - 1);
                foreach (string n in lc)
                    confFiles[f].Add(uint.Parse(n));
            }
        }
        private void OpenLogFile()
        {
            if (loggingEnabled)
            {
                string path = @"C:\Lidwave";
                string logFileName = path + "\\OdemLog_.txt";
                try
                {
                    logFile = new StreamWriter(logFileName, false);
                    logFile.WriteLine("App Start @" + DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                    logFile.AutoFlush = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to create log file: " + ex.Message, "Logging Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    loggingEnabled = false;
                }
            }
        }
        public void LogMessage(string message)
        {
            if (showCom.Checked)
            {
                MonitorView.AppendText(message + Environment.NewLine);
                if (AutoScroll.Checked)
                {
                    MonitorView.SelectionStart = MonitorView.Text.Length;
                    MonitorView.ScrollToCaret();
                }
            }
            if (loggingEnabled && logFile != null)
            {
                if (message.StartsWith("Reg write") & (message.Length > 80))
                {
                    logFile.WriteLine(DateTime.Now.ToString("yyyyMMdd_HHmmss") + " - Reg Write:");
                    message = message.Substring(11);
                    while (message.Length > 96)
                    {
                        logFile.WriteLine("                  " + message.Substring(0, 96));
                        message = message.Substring(96);
                    }
                    if (message.Length > 0)
                        logFile.WriteLine("                  " + message);

                }
                else
                    logFile.WriteLine(DateTime.Now.ToString("yyyyMMdd_HHmmss") + " - " + message);
            }
        }
        private void ReadAllTemp()
        {
            if (!isConnected) return;
            //return;
            bool tooHat = false;
            bool picHat = false;
            bool lasercold = false;
            string msg = "Temperature readings: ";
            string err = "";
            double t = 0;

            string not = "";
            foreach (DataGridViewRow row in tempTable.Rows)
            {
                row.Cells[1].Value = "";
                string sensor = row.Cells[0].Value.ToString();
                row.Cells[0].Style.ForeColor = Color.Black;
                switch (sensor)
                {
                    case "Optical chip":
                        err = ReadPICtemp(out t);
                        if (err == "")
                        {
                            row.Cells[1].Value = t.ToString("0.00") + " °c";
                            msg += sensor + ": " + t.ToString("0.00") + " °c; ";
                            if ((t >= 47) && (t <= 59))
                                row.Cells[1].Style.ForeColor = Color.Green;
                            else
                            {
                                row.Cells[1].Style.ForeColor = Color.Red;
                                picHat = true;
                            }
                        }
                        else
                        {
                            row.Cells[0].Style.ForeColor = Color.Orange;
                            not += "Optical chip, ";
                        }
                        break;

                    case "Laser":
                        err = readLaserTemp(out t);
                        if (err == "")
                        {
                            row.Cells[1].Value = t.ToString("0.00") + " °c";
                            msg += sensor + ": " + t.ToString("0.00") + " °c; ";
                            if (t > 58)
                            {
                                row.Cells[1].Style.ForeColor = Color.Red;
                                tooHat = true;
                            }
                            else if (t >= 52)
                                row.Cells[1].Style.ForeColor = Color.Green;
                            else
                            {
                                row.Cells[1].Style.ForeColor = Color.Orange;
                                lasercold = true;
                            }
                        }
                        else
                        {
                            row.Cells[0].Style.ForeColor = Color.Orange;
                            not += "Laser, ";
                        }
                        break;

                    case "Scanner":
                        double ottemp = 0;
                        string oterr = readOMTemp(out ottemp);
                        if (oterr != "")
                        {
                            row.Cells[0].Style.ForeColor = Color.Orange;
                            not += "Scanner, ";
                        }
                        else
                        {
                            msg += sensor + ": " + ottemp.ToString("0.00") + " °c; ";
                            row.Cells[1].Value = ottemp.ToString("0.00") + " °c";
                            if (ottemp < 88)
                                row.Cells[1].Style.ForeColor = Color.Green;
                            else
                            {
                                row.Cells[1].Style.ForeColor = Color.Red;
                                tooHat = true;
                            }

                        }
                        break;

                    case "Main Board":
                        double temp = 0;
                        err = readMBTemp(out temp);
                        if (err != "")
                        {
                            row.Cells[0].Style.ForeColor = Color.Orange;
                            not += "Main board, ";
                        }
                        else
                        {
                            msg += sensor + ": " + temp.ToString("0.00") + " °c; ";
                            row.Cells[1].Value = temp.ToString("0.00") + " °c";
                            if (temp < 72)
                                row.Cells[1].Style.ForeColor = Color.Green;
                            else
                            {
                                row.Cells[1].Style.ForeColor = Color.Red;
                                tooHat = true;
                            }
                        }
                        break;
                }
            }
            if (not.Length > 0)
            {
                MessageBox.Show("Fail to read " + not, "Temperature", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            tempTable.ClearSelection();
            LogMessage(msg);
            if (tooHat || picHat)
            {
                System.Media.SystemSounds.Beep.Play();
                string Error = SPISOAControl(0);
                if (Error.Length > 0)
                    picHat = true;
                if (picHat)
                    MessageBox.Show("Please power off and restart the system" + Error, "Over temperature", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("System overheating\n Please power off, connect the top cooling unit, and try again" + Error, "Over temperature", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            coldLaser.Visible = lasercold;
        }
        private void checkT_Click(object sender, EventArgs e)
        {
            ReadAllTemp();
        }
        private string ReadPICtemp(out double temp)
        {
            temp = 0;
            List<uint> t;
            string err = ReadI2C(0, 0x70, 4, 0x48, 0x14, 0xD8, 1, out t);
            if (err != "")
                return err;
            double vref = 2.45;
            double r1 = 5.6;
            double r2 = 2.0;
            double r3 = 47.0;
            double vtempout = ((double)t[0] / 4095 * 2.5);
            double term1 = 2 * r1 * r2 * vtempout;
            double term2_inner = r1 * r2 - r2 * r3 + r1 * r3;
            double term2 = vref * term2_inner;
            double numerator = term1 - term2;

            double term3_inner = r1 - r3;
            double term3 = vref * term3_inner;
            double term4 = 2 * r1 * vtempout;
            double denominator = term3 - term4;

            double rth = numerator / denominator;
            double T0 = 298.15;
            double B = 3380;
            double R0 = 10;
            double ratio = rth / R0;
            double ln_ratio = Math.Log(ratio);
            double inv_t0 = 1 / T0;
            double ln_term = (1 / B) * ln_ratio;
            double inv_temp_kelvin = inv_t0 + ln_term;
            double temp_kelvin = 1 / inv_temp_kelvin;
            temp = temp_kelvin - 273.15;
            return "";
        }
        private string readLaserTemp(out double temp)
        {
            temp = 0;
            string err = "";
            List<uint> t;
            err = ReadI2C(0, 0x70, 4, 0x48, 0x14, 0x88, 1, out t);
            if (err == "")
            {
                double r1 = 5.6;
                double r2 = 2.0;
                double r3 = 47;
                double vref = 2.45;
                double vtempout = ((double)t[0] / 4095.0) * 2.5;
                double term1 = 2 * r1 * r2 * vtempout;
                double term2_inner = r1 * r2 - r2 * r3 + r1 * r3;
                double term2 = vref * term2_inner;
                double numerator = term1 - term2;

                double term3_inner = r1 - r3;
                double term3 = vref * term3_inner;
                double term4 = 2 * r1 * vtempout;
                double denominator = term3 - term4;

                double rth = numerator / denominator;
                double t_ref = 298.15;
                double beta = 3380;
                double r_ref = 10;

                double T0 = t_ref;
                double B = beta;
                double R0 = r_ref;

                double ratio = rth / R0;
                double ln_ratio = Math.Log(ratio);
                double inv_t0 = 1 / T0;
                double ln_term = (1 / B) * ln_ratio;
                double inv_temp_kelvin = inv_t0 + ln_term;
                double temp_kelvin = 1 / inv_temp_kelvin;

                temp = temp_kelvin - 273.15;
            }
            return err;
        }
        private string readOMTemp(out double temp)
        {
            List<int> regs = new List<int>() { 0, 2, 6 };
            temp = 0;
            double tmp;
            List<double> tmp2 = new List<double>();
            foreach (int r in regs)
            {
                string err = ReadRegFromOT(0x22, (uint)r, out tmp);
                if (err != "")
                    return err;
                tmp2.Add(tmp);
            }
            temp = tmp2.Max();
            return "";
        }
        private string readMBTemp(out double temp)
        {
            temp = 0;
            string err = "";
            List<uint> tmp;
            List<uint> tmp2 = new List<uint>();
            err = ReadI2C(0, 0x70, 1, 0x48, 0x14, 0, 1, out tmp);
            if (err != "")
                return err;
            tmp2.Add(tmp[0]);
            err = ReadI2C(0, 0x70, 1, 0x49, 0x14, 0, 1, out tmp);
            if (err != "")
                return err;
            tmp2.Add(tmp[0]);
            err = ReadI2C(0, 0x70, 1, 0x4A, 0x14, 0, 1, out tmp);
            if (err != "")
                return err;
            tmp2.Add(tmp[0]);
            err = ReadI2C(0, 0x70, 1, 0x4B, 0x14, 0, 1, out tmp);
            if (err != "")
                return err;
            tmp2.Add(tmp[0]);
            uint mtmp = tmp2.Max();
            mtmp >>= 4;       // 12-bit value
            if ((mtmp & 0x800) != 0) mtmp -= 4096;
            temp = (double)mtmp * 0.0625;
            return "";
        }
        private async void connect_Click(object sender, EventArgs e)
        {
            oVer.Text = "";
            bool tryconnect = !isConnected;
            this.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            await ConnectToDevice();
            this.Cursor = Cursors.Default;
            this.Enabled = true;
            if (!isConnected && tryconnect)
            {
                connectCnt++;
                if (connectCnt == 5)
                {
                    MessageBox.Show("Fail to connect.\n\nRestart the App and/or ODEM\nand try again");
                }
            }
            else
            {
                connectCnt = 0;
                if (!dbgMode)
                {
                    bool noSN = false;
                    List<uint> t = ReadEEPROM(0, 1);
                    if (t == null)
                        noSN = true;
                    else if (t[0] == 0xFFFF)
                        noSN = true;
                    else
                    {
                        string rSN = "SN" + t[0].ToString("D4");
                        if (devicesList.Contains(rSN))
                        {
                            devicesList.Clear();
                            deviceID.Clear();
                            devicesList.Add(rSN);
                            deviceID.Add(rSN);
                            devices.Items.Add(rSN);
                            devices.Enabled = false;
                            ConnectedTo.Text = rSN;
                            appSetting.deviceNum = 0;
                            UpdateConfFiles();
                        }
                    }

                    if (noSN)
                    {
                        if (devicesList.Contains(iniDev))
                        {
                            devicesList.Clear();
                            deviceID.Clear();
                            devicesList.Add(iniDev);
                            deviceID.Add(iniDev);
                            devices.Items.Add(iniDev);
                            devices.Enabled = false;
                            ConnectedTo.Text = iniDev;
                            appSetting.deviceNum = 0;
                            UpdateConfFiles();
                        }
                        else
                        {
                            this.Enabled = false;
                            MessageBox.Show("Wrong or missing device SN\n\nUpdate your device SN in \"C:\\Lidwave\\Odem.ini\"", "Not recognize device", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Close();
                        }

                    }
                }
            }
        }
        private void sStart_Click(object sender, EventArgs e)
        {
            streaming.Visible = false;
            if (coldLaser.Visible)
            {
                if (dbgMode)
                {
                    if (MessageBox.Show("Laser temperature too low. Start streaming anyways?", "Laser Temperature", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        return;
                }
                else
                {
                    MessageBox.Show("Laser temperature too low.\nPlease wait until the laser warms up.", "Laser Temperature", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            stream.ReadTimeout = 50000;
            string Error = StreamingCmd(true);
            stream.ReadTimeout = 10000;
            if (Error.Length > 0)
            {
                LogMessage("Streaming command: " + Error);
                string smsg = Error.Replace("\0", "") + "\nRestart ODEM and App";
                MessageBox.Show(smsg, "Start Streaming Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                streaming.Visible = true;
                sStart.Enabled = false;
                sStop.Enabled = true;
                deviceState.Text = "Steaming";
                deviceState.ForeColor = Color.Green;
                scanMode.Enabled = false;
                DisableConf(false);
            }
        }
        private void DisableConf(bool enable)
        {
            scanMode.Enabled = enable;
            SensitivityNormal.Enabled = enable;
            sensitivityHigh.Enabled = enable;
            confDev.Enabled = enable;
            connect.Enabled = enable;
        }
        private void sStop_Click(object sender, EventArgs e)
        {
            string Error = StreamingCmd(false);
            if (Error.Length > 0)
            {
                LogMessage("Streaming command: " + Error);
                MessageBox.Show("Error Streaning command:\n" + Error, "Command Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                streaming.Visible = false;
                sStart.Enabled = true;
                sStop.Enabled = false;
                deviceState.Text = "Steaming stopped";
                deviceState.ForeColor = Color.Orange;
                scanMode.Enabled = true;
                DisableConf(true);
            }

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (configuring || !isConnected) return;

            if (autoTemp.Checked)
            {
                readTempCounter -= 10;
                ReadIntProg.Value = Math.Max(0, ReadIntProg.Value - 10);
                if (readTempCounter <= 0)
                {
                    ReadIntProg.Value = (int)ReadInt.Value * 60;
                    readTempCounter = 60 * (int)ReadInt.Value;
                    ReadAllTemp();
                }
                else
                    PingDevice();
            }
            else
                PingDevice();

        }
        private void autoTemp_CheckedChanged(object sender, EventArgs e)
        {
            autoTempControl();
        }
        private void autoTempControl()
        {
            checkT.Visible = !autoTemp.Checked;
            ReadInt.Visible = autoTemp.Checked;
            ReadIntText.Visible = autoTemp.Checked;
            ReadIntProg.Visible = autoTemp.Checked;
            if (autoTemp.Checked)
            {
                ReadIntProg.Value = (int)ReadInt.Value * 60;
                ReadIntProg.Maximum = (int)ReadInt.Value * 60;
                timer1.Interval = 10000;
                readTempCounter = 60 * (int)ReadInt.Value;
                ReadAllTemp();
            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();

            //if (noDevice)
            //{
            //    this.Enabled = false;
            //    MessageBox.Show("Wrong or missing device SN\n\nUpdate your device SN in \"C:\\Lidwave\\Odem.ini\"", "Not recognize device", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    this.Close();

            //}
            //else
            {
                splitContainer3.Panel2Collapsed = !dbgMode;
                SetDebugView();
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

        private void pw_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (pw.Text == appSetting.dbgPW)
                {
                    groupBox5.Visible = false;
                    tabControl1.Visible = true;
                }
                else
                {
                    pw.Text = "";
                    MessageBox.Show("Incorrect Password");
                }
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            timer3.Stop();
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
                case "stopOT":
                    stopOT.BackColor = SystemColors.Control;
                    break;
                case "getVer":
                    getVer.BackColor = SystemColors.Control;
                    break;
                case "setSN":
                    setSN.BackColor = SystemColors.Control;
                    break;
                case "readUID":
                    readUID.BackColor = SystemColors.Control;
                    break;
            }
        }
        private void resetDSP_Click(object sender, EventArgs e)
        {
            string Error = WriteRegWaitResp(WriteRegs[(int)confStates.RESET_DSP], new List<uint> { 0x4100004 });
            if (Error.Length > 0)
            {
                LogMessage("Configuring Error: " + Error);
                resetDSP.BackColor = Color.Red;
            }
            else
                resetDSP.BackColor = Color.Lime;
            pushed = "resetDSP";
            timer3.Start();
        }
        private void wrOTDelay_Click(object sender, EventArgs e)
        {
            if (!isConnected) return;

            LogMessage("Configuring: Set OT Delay");
            string mode = modes[appSetting.scanModeNum];
            int nPoints = scanModes[mode].nPoints;
            int otd = (int)OTDelay.Value;
            uint iotd = (uint)Math.Abs(otd);
            if (otd < 0)
                iotd = (uint)(nPoints - iotd);
            lastOTdelay = otd;
            string Error = WriteRegWaitResp(WriteRegs[(int)confStates.SET_OT_DELAY], new List<uint> { iotd });
            if (Error.Length > 0)
            {
                LogMessage("Configuring Error: " + Error);
                wrOTDelay.BackColor = Color.Red;
            }
            else
                wrOTDelay.BackColor = Color.Lime;
            pushed = "OTDelay";
            timer3.Start();
        }
        private void WriteReg_Click(object sender, EventArgs e)
        {
            if (!isConnected) return;
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

            string Error = WriteRegWaitResp(add, new List<uint>() { val });
            if (Error.Length > 0)
            {
                LogMessage("Configuring Error: " + Error);
                WriteReg.BackColor = Color.Red;
            }
            else
                WriteReg.BackColor = Color.Lime;
            pushed = "WriteReg";
            timer3.Start();
        }
        private void WrVec_Click(object sender, EventArgs e)
        {
            if (VecData.Count == 0)
            {
                MessageBox.Show("Vector not loaded");
                return;
            }

            string Error = WriteRegWaitResp(VecDest, VecData);
            if (Error.Length > 0)
            {
                LogMessage("Configuring Error: " + Error);
                WrVec.BackColor = Color.Red;
            }
            else
                WrVec.BackColor = Color.Lime;
            pushed = "WrVec";
            timer3.Start();
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

            string Error = WriteI2CWaitResp(I2C_ch, I2C_dev, 0x14, I2C_reg, new List<uint> { val });
            if (Error.Length > 0)
            {
                LogMessage("Configuring Error: " + Error);
                WriteI2C.BackColor = Color.Red;
            }
            else
                WriteI2C.BackColor = Color.Lime;
            pushed = "WriteI2C";
            timer3.Start();

        }
        private void RegsNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            string rn = RegsNames.SelectedItem as string;
            regAdd.Text = WriteRegsAdd[rn].ToString("X08");
        }
        private void I2CsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string rn = I2CsList.SelectedItem as string;
            I2C_ch = I2CConf[rn].ch;
            I2C_dev = I2CConf[rn].dev;
            I2C_reg = I2CConf[rn].reg;
            I2C_val = I2CConf[rn].val;
            I2Cdest.Text = "Ch: " + I2C_ch.ToString() + " ;  Dev: 0x" + I2C_dev.ToString("X02") +
                "; Reg: 0x" + I2C_reg.ToString("X02");
            I2Cval.Text = "0x" + I2C_val.ToString("X04");
        }
        private void getFromFolder_Click(object sender, EventArgs e)
        {
            GetFIles();
        }
        private void saveSetting_Click(object sender, EventArgs e)
        {
            SaveToFile();
        }
        private void loadSetting_Click(object sender, EventArgs e)
        {
            LoadFromFile();
        }
        private void customMode_CheckedChanged(object sender, EventArgs e)
        {
            cModeParams.Enabled = customMode.Checked;
            cWaveForm.Enabled = customMode.Checked;
        }
        private void impSM_Click(object sender, EventArgs e)
        {
            string modeName = modes[appSetting.scanModeNum];
            cModeParams.Rows[0].Cells[1].Value = scanModes[modeName].mirror.ToString();
            cModeParams.Rows[1].Cells[1].Value = scanModes[modeName].nPoints.ToString();
            cModeParams.Rows[3].Cells[1].Value = scanModes[modeName].hFOV.ToString();
            cModeParams.Rows[4].Cells[1].Value = scanModes[modeName].vFOV.ToString();
            cModeParams.Rows[5].Cells[1].Value = scanModes[modeName].hRes.ToString("0.00");
            cModeParams.Rows[6].Cells[1].Value = scanModes[modeName].vRes.ToString("0.00");
            cModeParams.Rows[7].Cells[1].Value = scanModes[modeName].lines.ToString();
            cModeParams.Rows[8].Cells[1].Value = scanModes[modeName].fRate.ToString();
        }
        private void cModeParams_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            genJSON.ForeColor = Color.Red;
            JsonReady = false;
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
        private void genJSON_Click(object sender, EventArgs e)
        {
            GenJson();
            genJSON.ForeColor = Color.LimeGreen;
        }
        private void selWF_Click(object sender, EventArgs e)
        {
            GetWfFiles();
        }
        private void customConfig_Click(object sender, EventArgs e)
        {
            CustomCofig();
        }

        private void stopOT_Click(object sender, EventArgs e)
        {
            string Error = SendStopCmd();
            if (Error.Length > 0)
                stopOT.BackColor = Color.Red;
            else
                stopOT.BackColor = Color.Lime;
            pushed = "stopOT";
            timer3.Start();
        }

        private void getVer_Click(object sender, EventArgs e)
        {
            fpgaVer.Text = "FPGA Version: ";
            List<uint> ver = new List<uint>();
            string err = ReadReg(0xFF200018, 1, out ver);
            if (err.Length > 0)
                getVer.BackColor = Color.Red;
            else
            {
                getVer.BackColor = Color.Lime;
                string f = "FPGA Version: 0x" + ver[3].ToString("X02") + " " + ver[0].ToString() + " Channels";
                if (ver[2] == 1)
                    f += " (Debug)";
                fpgaVer.Text = f;
            }
            pushed = "getVer";
            timer3.Start();
        }

        private void upgradeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                MessageBox.Show("Device not connected");
                return;
            }

            if (deviceConfigured)
            {
                MessageBox.Show("ODEM in running\nRestart ODEM and just connect");
                return;
            }

            upgrade ug = new upgrade(this);
            ug.ShowDialog();
            for (int i = 0; i < tempTable.Rows.Count; i++)
                tempTable.Rows[i].Cells[1].Value = "";
            DevieLost();
        }

        private void genEncypt_Click(object sender, EventArgs e)
        {
            GenerateEncryptedFile();
        }

        private void getVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                oVer.Text = "";
                return;
            }

            List<uint> ver = new List<uint>();
            string err = ReadReg(0xFF200018, 1, out ver);
            if (err.Length > 0)
                MessageBox.Show("Error reading version: " + err, "Read Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                oVer.Text = "Version: 0x" + ver[3].ToString("X02");
        }

        private void getEncyptedFile_Click(object sender, EventArgs e)
        {
            GetEncryptedFile();
        }

        private void readUID_Click(object sender, EventArgs e)
        {
            devUID.Text = "UID: ";
            devSN.Text = "SN: ";
            List<uint> t = ReadEEPROM(0x7F7A, 4);
            if (t == null)
            {
                MessageBox.Show("Fail to read device UID");
                readUID.BackColor = Color.Red;
                pushed = "readUID";
                timer3.Start();
                return;
            }
            else
            {
                ulong id = (ulong)t[0] + ((ulong)t[1] << 16) + ((ulong)t[2] << 32);
                devUID.Text = "EUI-48: " + id.ToString("X12");
            }

            t = ReadEEPROM(0, 1);
            if (t == null)
            {
                MessageBox.Show("Fail to read device SN");
                readUID.BackColor = Color.Red;
                pushed = "readUID";
                timer3.Start();
                return;
            }
            else if (t[0] == 0xFFFF)
            {
                devSN.Text = "Device SN not programmed";
            }
            else
            {
                devSN.Text = "SN" + t[0].ToString("D4");
            }

            readUID.BackColor = Color.Lime;
            pushed = "readUID";
            timer3.Start();
        }
        private void setSN_Click(object sender, EventArgs e)
        {
            uint sn = uint.Parse(devicesList[appSetting.deviceNum].Replace("SN", ""));
            string err = WriteEEPROM(1, 0x50, 0x24, 0, new List<uint>() { sn });
            if (err.Length > 0)
            {
                MessageBox.Show("Error set SN: " + err, "Write Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                setSN.BackColor = Color.Red;
            }
            else
                setSN.BackColor = Color.Lime;

            pushed = "setSN";
            timer3.Start();
        }
    }

    public class appSettings
    {
        public int deviceNum;
        public int scanModeNum;
        public int sensitivity;
        public int range;
        public int width;
        public int height;
        public string dbgPW;
        public appSettings()
        {
            deviceNum = Properties.Settings.Default.deviceNum;
            scanModeNum = Properties.Settings.Default.scanmode;
            sensitivity = Math.Min(1, Properties.Settings.Default.sensitivity);
            range = Math.Min(2, Properties.Settings.Default.range);
            width = Properties.Settings.Default.width;
            height = Properties.Settings.Default.hight;
            dbgPW = Properties.Settings.Default.dbgPW;
        }
        public void Update(bool save)
        {
            Properties.Settings.Default.deviceNum = deviceNum;
            Properties.Settings.Default.scanmode = scanModeNum;
            Properties.Settings.Default.sensitivity = sensitivity;
            Properties.Settings.Default.range = range;
            Properties.Settings.Default.width = width;
            Properties.Settings.Default.hight = height;
            Properties.Settings.Default.dbgPW = dbgPW;
            if (save)
                Properties.Settings.Default.Save();
        }
    }
    public class deviceParameters
    {
        public uint Capture_Delay { get; set; }
        public uint Chirp_AWG_gain { get; set; }
        public uint LO { get; set; }
        public uint TxSOA1 { get; set; }
        public uint TxSOA2 { get; set; }
        public uint Tx3_0_9 { get; set; }
        public uint Tx3_10_19 { get; set; }
        public uint Tx3_20_29 { get; set; }
        public uint Tx3_30_39 { get; set; }
        public deviceParameters()
        {
            Capture_Delay = 3600;
            Chirp_AWG_gain = 7000;
            TxSOA1 = 2050;
            TxSOA2 = 5050;
            Tx3_0_9 = 5050;
            Tx3_10_19 = 5050;
            Tx3_20_29 = 5050;
            Tx3_30_39 = 5050;
        }
    }
    public class scanMode
    {
        public int mirror { get; set;}
        public int nPoints { get; set; }
        public int hFOV { get; set; }
        public int vFOV { get; set; }
        public double hRes { get; set; }
        public double vRes { get; set; }
        public int lines { get; set; }
        public int fRate { get; set; }
        public string folder { get; set; }
        public int modeNum { get; set; }

        public scanMode()
        {
            mirror = 1;
            nPoints = 1000;
            hFOV = 100;
            vFOV = 20;
            hRes = 1;
            vRes = 1;
            lines = 1;
            fRate = 10;
            folder = "";
            modeNum = 0;
        }
    }
}
