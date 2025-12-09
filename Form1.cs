using System;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Reflection;
using System.Text.Json;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.Design.AxImporter;

namespace OdemControl
{
    public partial class Form1 : Form
    {
        public appSettings appSetting;
        public bool isConnected = false;
        public List<string> deviceID = new List<string>();
        public Dictionary<string, scanMode> scanModes = new Dictionary<string, scanMode>();
        public List<string> modes = new List<string>();
        Dictionary<string, List<uint>> confFiles = new Dictionary<string, List<uint>>();
        Dictionary<string, List<uint>> wfFiles = new Dictionary<string, List<uint>>();
        string scanParamsJson = "";
        private List<string> devicesList = new List<string>();
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
        bool dbgMode = true;

        string version = "0.01.00";
        Dictionary<Control, Rectangle> originalRects;
        Size originalFormSize;
        float scanModeFontSize;
        float tempFontSize;
        float modeFontSize;
        bool deviceConfigured = false;

        public Form1(string mode)
        {
            InitializeComponent();

            appSetting = new appSettings();

            originalFormSize = this.Size;
            modeFontSize = scanMode.Font.Size;
            scanModeFontSize = ModeParams.Font.Size;
            tempFontSize = tempTable.Font.Size;
            originalRects = new Dictionary<Control, Rectangle>();
            foreach (Control c in this.Controls)
            {
                originalRects.Add(c, new Rectangle());
                originalRects[c] = c.Bounds;
            }
            this.Size = new Size(appSetting.width, appSetting.height);

            this.Resize += Form1_Resize;

            this.Text = "Odem Control - Version " + version;
            dbgMode |= mode.Contains("-d");
            debugMode.Visible = dbgMode;
            debugmodeEnabled = dbgMode;
            loggingEnabled = mode.Contains("-l") || dbgMode;
            dataLoggingEnabled = mode.Contains("-le") || dbgMode;
            if (loggingEnabled)
                OpenLogFile();

            bool nodv = SetVars(mode);

            if (nodv)
            {
                this.Enabled = false;
                MessageBox.Show("Wrong or missing device ID in command line\n\nUsage: OdemControl -Dev SNXXXX\nSNXXXX can be found on device", "Not recognize device", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

        }
        private bool SetVars(string mode)
        {
            //string op = Dns.GetHostEntry(Dns.GetHostName()).AddressList
            //  .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)?
            //  .ToString() ?? "No IPv4 found";

            string path = @"C:\Lidwave";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

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

            bool nodev = false;
            dbgMode = false;
            if (dbgMode)
            {
                foreach (string devName in devicesList)
                    devices.Items.Add(devName);
            }
            else
            {
                nodev = true;
                int idx = mode.IndexOf("-dev ");
                if (idx >= 0)
                {
                    string dev = mode.Substring(mode.Length - idx - 7).Substring(0, 6).ToUpper();
                    if (devicesList.Contains(dev))
                    {
                        devicesList.Clear();
                        deviceID.Clear();
                        devicesList.Add(dev);
                        deviceID.Add(dev);
                        devices.Items.Add(dev);
                        devices.Enabled = false;
                        nodev = false;
                    }
                }
            }

            if (nodev)
            {
                return true;
            }

            // Get scan modes from csv file
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OdemControl.Devices.Devices_Params.csv");
            if (stream == null)
            {
                MessageBox.Show("Failed to read scan modes paramaters file.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            StreamReader reader = new StreamReader(stream);
            string allparams = reader.ReadToEnd();
            List<string> paramsList = allparams.Split("\r\n").ToList();
            List<string> dvs = paramsList[0].Substring(paramsList[0].IndexOf("SN")).Split(',').ToList();


            // configuration files dictionaries
            confFiles.Add("badGoodIndxs_High", new List<uint>());
            confFiles.Add("badGoodIndxs_Low", new List<uint>());
            confFiles.Add("2kWin", new List<uint>());
            confFiles.Add("128Bins_Final", new List<uint>());
            confFiles.Add("blackmanHarris_DEC", new List<uint>());
            confFiles.Add("AWG", new List<uint>());
            wfFiles.Add("waveformX", new List<uint>());
            wfFiles.Add("waveformY", new List<uint>());

            // Set scan mode parameters table
            ModeParams.Rows.Clear();
            ModeParams.Rows.Add("Horizontal FOV", ".. °");
            ModeParams.Rows.Add("Vertical FOV", ".. °");
            ModeParams.Rows.Add("Horizontal Res.", ".. °");
            ModeParams.Rows.Add("Vertical Res.", ".. °");
            ModeParams.Rows.Add("Lines per frame", ".. °");
            ModeParams.Rows.Add("frame rate", ".. FPS");

            // Get scan modes from csv file
            stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OdemControl.Optotune.modes_params.csv");
            if (stream == null)
            {
                MessageBox.Show("Failed to read scan modes paramaters file.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            reader = new StreamReader(stream);
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

            if (devicesList.Count == 1)
                appSetting.deviceNum = 0;
            devices.SelectedIndex = Math.Min(appSetting.deviceNum, devicesList.Count() - 1);
            return false;
        }
        private void SensitivityNormal_CheckedChanged(object sender, EventArgs e)
        {
            if (SensitivityNormal.Checked)
                appSetting.sensitivity = 0;
            else
                appSetting.sensitivity = 1;
        }
        private void scanMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (deviceConfigured)
            {
                MessageBox.Show("Please restart device and reconnect before changing scan mode");
                DevieLost();
                return;
            }
            updateScanMode(scanMode.SelectedIndex);
            streamBox(false);
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

            string resourceName = "OdemControl.Optotune." + scanModes[modeName].folder + ".";
            List<string> files = wfFiles.Keys.ToList();
            Stream stream;
            StreamReader reader;
            stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName + "scan_parameters.json");
            if (stream == null)
            {
                MessageBox.Show("Failed to read device configuation file.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            reader = new StreamReader(stream);
            scanParamsJson = reader.ReadToEnd();

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
            ConfigNow();
        }
        private async void ConfigNow()
        {
            configuring = true;
            pingLost = 10;
            this.Cursor = Cursors.WaitCursor;
            this.Enabled = false;
            deviceState.Text = "";
            deviceState.ForeColor = Color.Black;
            appSetting.Update(true);
            //UpdateConfFiles();
            confState = (int)confStates.IDLE;
            await cofigdeviceAsync();
            if (confState == (int)confStates.DONE)
            {
                deviceState.Text = "Device ready";
                deviceState.ForeColor = Color.Lime;
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
            dataLoggingEnabled = true;
            debugMode.Enabled = false;
            db = new Debug(this, false);
            db.StartPosition = FormStartPosition.CenterParent;
            db.Show(this);
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
            if (db != null)
            {
                if (!db.IsDisposed)
                    db.UpdateMonitor(message);
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
            string err = ReadI2C(4, 0x48, 0x14, 0xD8, 1, out t);
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
            err = ReadI2C(4, 0x48, 0x14, 0x88, 1, out t);
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
            err = ReadI2C(1, 0x48, 0x14, 0, 1, out tmp);
            if (err != "")
                return err;
            tmp2.Add(tmp[0]);
            err = ReadI2C(1, 0x49, 0x14, 0, 1, out tmp);
            if (err != "")
                return err;
            tmp2.Add(tmp[0]);
            err = ReadI2C(1, 0x4A, 0x14, 0, 1, out tmp);
            if (err != "")
                return err;
            tmp2.Add(tmp[0]);
            err = ReadI2C(1, 0x4B, 0x14, 0, 1, out tmp);
            if (err != "")
                return err;
            tmp2.Add(tmp[0]);
            uint mtmp = tmp2.Max();
            mtmp >>= 4;       // 12-bit value
            if ((mtmp & 0x800) != 0) mtmp -= 4096;
            temp = (double)mtmp * 0.0625;
            return "";
        }
        private void connect_Click(object sender, EventArgs e)
        {
            ConnectToDevice();
        }
        private void sStart_Click(object sender, EventArgs e)
        {
            streaming.Visible = false;
            if (coldLaser.Visible)
            {
                MessageBox.Show("Laser temperature too low.\nPlease wait until the laser warms up.", "Laser Temperature", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            stream.ReadTimeout = 50000;
            string Error = StreamingCmd(true);
            stream.ReadTimeout = 10000;
            if (Error.Length > 0)
            {
                LogMessage("Streaming command: " + Error);
                MessageBox.Show("Error Streaning command:\n" + Error, "Command Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            //            else
            //            {
            ////                timer1.Stop();
            //            }
        }
        private void KeepAlive_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            float xRatio = (float)this.Width / originalFormSize.Width;
            float yRatio = (float)this.Height / originalFormSize.Height;

            foreach (Control c in this.Controls)
            {
                Rectangle r = originalRects[c];
                c.Bounds = new Rectangle(
                    (int)(r.X * xRatio),
                    (int)(r.Y * yRatio),
                    (int)(r.Width * xRatio),
                    (int)(r.Height * yRatio)
                );
            }

            float scale = (float)this.Width / originalFormSize.Width;
            float newSize = scanModeFontSize * scale;
            ModeParams.Font = new Font(ModeParams.Font.FontFamily, newSize);
            newSize = tempFontSize * scale;
            tempTable.Font = new Font(tempTable.Font.FontFamily, newSize);
            newSize = modeFontSize * scale;
            scanMode.Font = new Font(scanMode.Font.FontFamily, newSize);
            this.ActiveControl = null;

            appSetting.width = this.Width;
            appSetting.height = this.Height;
            appSetting.Update(true);
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
        public appSettings()
        {
            deviceNum = Properties.Settings.Default.deviceNum;
            scanModeNum = Properties.Settings.Default.scanmode;
            sensitivity = Math.Min(1, Properties.Settings.Default.sensitivity);
            range = Math.Min(2, Properties.Settings.Default.range);
            width = Properties.Settings.Default.width;
            height = Properties.Settings.Default.hight;
        }
        public void Update(bool save)
        {
            Properties.Settings.Default.deviceNum = deviceNum;
            Properties.Settings.Default.scanmode = scanModeNum;
            Properties.Settings.Default.sensitivity = sensitivity;
            Properties.Settings.Default.range = range;
            Properties.Settings.Default.width = width;
            Properties.Settings.Default.hight = height;
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
