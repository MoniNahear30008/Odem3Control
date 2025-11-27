using System;
using System.Net.Sockets;
using System.Reflection;

namespace OdemControl
{
    public partial class Form1 : Form
    {
        public appSettings appSetting;
        public bool isConnected = false;
        public List<string> deviceID = new List<string>();
        private Dictionary<string, scanMode> scanModes = new Dictionary<string, scanMode>()
        {
            { "88deg_0.015res_80L", new scanMode() { mirror=2000, nPoints=12329, hFOV=88, vFOV=19, hRes=0.013, vRes=0.24, lines=80, fRate=5 , folder="Mode1"} },
            { "88deg_0.030res_80L", new scanMode() { mirror=4000, nPoints=6164, hFOV=88, vFOV=19, hRes=0.03, vRes=0.24, lines=80, fRate=10, folder="Mode2" } },
            { "80deg_0.050res_160L", new scanMode() { mirror=8000, nPoints=6164, hFOV=80, vFOV=19, hRes=0.05, vRes=0.12, lines=160, fRate=10, folder="Mode3" } },
            { "60deg_0.013res_80L", new scanMode() { mirror=2500, nPoints=9106, hFOV=60, vFOV=19, hRes=0.013, vRes=0.24, lines=80, fRate=6, folder="ModeA4" } },
        };
        private List<string> modes = new List<string>();
        Dictionary<string, List<uint>> confFiles = new Dictionary<string, List<uint>>();
        Dictionary<string, List<uint>> wfFiles = new Dictionary<string, List<uint>>();
        private List<string> devicesList = new List<string>();
        int confState = (int)confStates.IDLE;
        Dictionary<string, uint> deviceParameters = new Dictionary<string, uint>()
        {
            {"Capture_Delay" ,3600},
            {"Chirp AWG gain",7000},
            {"LO",7000},
            {"TxSOA1",2050},
            {"TxSOA2",5050},
            {"Tx3_0_9",5050},
            {"Tx3_10_19",5050},
            {"Tx3_20_29",5050},
            {"Tx3_30_39",5050},
        };
        bool loggingEnabled = false;
        bool dataLoggingEnabled = false;
        private StreamWriter logFile;

        public Form1(String mode)
        {
            InitializeComponent();

            debugMode.Visible = mode.Contains("-d");
            loggingEnabled = mode.Contains("-l");
            dataLoggingEnabled = mode.Contains("-le");
            if (loggingEnabled)
                OpenLogFile();

            IPAddredd.Text = _ipAddress;
            IPPort.Text = _port.ToString();

            var assembly = Assembly.GetExecutingAssembly();
            string[] resources = assembly.GetManifestResourceNames();
            foreach (string r in resources)
            {
                if (r.Contains("badGoodIndxs_High"))
                    devicesList.Add(r.Split('.')[2]);
            }

            foreach (string d in devicesList)
                deviceID.Add(d);

            ModeParams.Rows.Clear();
            ModeParams.Rows.Add("Horizontal FOV", ".. °");
            ModeParams.Rows.Add("Vertical FOV", ".. °");
            ModeParams.Rows.Add("Horizontal Res.", ".. °");
            ModeParams.Rows.Add("Vertical Res.", ".. °");
            ModeParams.Rows.Add("Lies per frame", ".. °");
            ModeParams.Rows.Add("frame rate", ".. FPS");

            confFiles.Add("badGoodIndxs_High", new List<uint>());
            confFiles.Add("badGoodIndxs_Low", new List<uint>());
            confFiles.Add("2kWin", new List<uint>());
            confFiles.Add("128Bins_Final", new List<uint>());
            confFiles.Add("blackmanHarris_DEC", new List<uint>());
            confFiles.Add("AWG", new List<uint>());

            wfFiles.Add("waveformX", new List<uint>());
            wfFiles.Add("waveformY", new List<uint>());

            modes = scanModes.Keys.ToList();
            foreach (string m in scanModes.Keys)
                scanMode.Items.Add(m);

            foreach (string d in deviceID)
                devices.Items.Add(d);

            appSetting = new appSettings();
            scanMode.SelectedIndex = Math.Min(appSetting.scanModeNum, modes.Count() - 1);
            updateScanMode(scanMode.SelectedIndex);

            devices.SelectedIndex = Math.Min(appSetting.deviceNum, devicesList.Count() - 1);

            if (appSetting.sensitivity == 0)
                SensitivityNormal.Checked = true;
            else
                sensitivityHigh.Checked = true;

            if (appSetting.range == 0)
                RangeNormal.Checked = true;
            else if (appSetting.range == 1)
                RangeExt.Checked = true;
            else
                rangeMax.Checked = true;
        }

        private void SensitivityNormal_CheckedChanged(object sender, EventArgs e)
        {
            if (SensitivityNormal.Checked)
                appSetting.sensitivity = 0;
            else
                appSetting.sensitivity = 1;
        }

        private void rangeChanged(object sender, EventArgs e)
        {
            if (RangeNormal.Checked)
                appSetting.range = 0;
            else if (RangeExt.Checked)
                appSetting.range = 1;
            else
                appSetting.range = 2;
        }
        private void scanMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateScanMode(scanMode.SelectedIndex);
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

            string resourceName = "OdemControl.Optotune." + scanModes[modeName].folder + ".";
            List<string> files = wfFiles.Keys.ToList();
            Stream stream;
            StreamReader reader;
            foreach (string f in files)
            {
                wfFiles[f].Clear();
                stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName + f + ".csv");
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
            appSetting.Update(true);
            UpdateConfFiles();
            confState = (int)confStates.IDLE;
            cofigdevice();
            if (confState == (int)confStates.DONE)
            {
                LogMessage("Configuring: Done");
                streamBox.Enabled = true;
            }
            else
            {
                LogMessage("Configuring: Error");
                streamBox.Enabled = false;
            }
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
            Debug db = new Debug(this);
            db.StartPosition = FormStartPosition.CenterParent;
            db.ShowDialog(this);
        }
        private void devices_SelectedIndexChanged(object sender, EventArgs e)
        {
            appSetting.deviceNum = devices.SelectedIndex;
        }
        private void UpdateConfFiles()
        {
            int dNum = devices.SelectedIndex;

            string resourceName = "OdemControl.Devices." + devicesList[dNum] + ".";
            List<string> files = confFiles.Keys.ToList();
            Stream stream;
            StreamReader reader;
            List<string> lc;
            // Get general parameters
            stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName + "General_Params.csv");
            if (stream == null)
            {
                MessageBox.Show("Failed to read device configuation file.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            reader = new StreamReader(stream);
            string content = reader.ReadToEnd();
            lc = content.Split("\r\n").ToList();
            lc.RemoveAt(lc.Count() - 1);
            foreach (string n in lc)
            {
                string[] parts = n.Split(',');
                if (parts.Length != 2)
                {
                    MessageBox.Show("Failed to read general parameters file.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string pname = parts[0].Trim();
                uint pval = uint.Parse(parts[1]);
                if (deviceParameters.ContainsKey(pname))
                    deviceParameters[pname] = pval;
                else
                {
                    MessageBox.Show("Unknown parameter in general parameters file.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }

            // Get common file
            string commonFile = "blackmanHarris_DEC";

            foreach (string f in files)
            {
                confFiles[f].Clear();
                if (f == commonFile)
                    stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OdemControl.Devices." + f + ".txt");
                else
                    stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName + f + ".txt");
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

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                //                string logFileName = path + "\\OdemLog_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
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
        private void LogMessage(string message)
        {
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

        private void checkT_Click(object sender, EventArgs e)
        {
            readTemp();
        }
        private void readTemp()
        {
            double R1 = 5.6;
            double R2 = 2;
            double R3 = 47;
            string err = "";
            List<uint> temp;
            if (tLaser.Checked)
                err = ReadI2C(4, 0x48, 0x14, 0x88, 1, out temp);
            else
                err = ReadI2C(4, 0x48, 0x14, 0xD8, 1, out temp);
            if (err == "")
            {
                if (tLaser.Checked)
                {
                    double Vtempout = ((double)temp[0] / 4095 * 2.5);
                    double Rth = (2 * R1 * R2 * Vtempout - 2.45 * (R1 * R2 - R2 * R3 + R1 * R3)) / (2.45 * (R1 - R3) - 2 * R1 * Vtempout);
                    double t = (1 / ((0.001129) + (0.0002341) * Math.Log(Rth * 1000) + (0.00000008775) * (Math.Pow(Math.Log(Rth * 1000), 3)))) - 273.15;
                    cTemp.Text = t.ToString("0.00") + " °c";
                    cTemp.ForeColor = Color.Black;
                }
                else
                {
                    R1 = 6.81;
                    R2 = 2.7;
                    R3 = 68;
                    double Vtempout = ((double)temp[0] / 4095 * 2.5);
                    double Rth = (2 * R1 * R2 * Vtempout - 2.45 * (R1 * R2 - R2 * R3 + R1 * R3)) / (2.45 * (R1 - R3) - 2 * R1 * Vtempout);
                    double t = 1 / ((1 / 298.15) + (1 / 3380) * Math.Log(Rth / 10)) - 273.15;
                    cTemp.Text = t.ToString("0.00") + " °c";

                    if ((t >= 47) && (t <= 59))
                        cTemp.ForeColor = Color.Lime;
                    else
                        cTemp.ForeColor = Color.Red;

                }

            }

        }

        private void connect_Click(object sender, EventArgs e)
        {
            ConnectToDevice();
        }
    }

    public class appSettings
    {
        public int deviceNum;
        public int scanModeNum;
        public int sensitivity;
        public int range;
        public appSettings()
        {
            deviceNum = Properties.Settings.Default.deviceNum;
            scanModeNum = Properties.Settings.Default.scanmode;
            sensitivity = Math.Min(1, Properties.Settings.Default.sensitivity);
            range = Math.Min(2, Properties.Settings.Default.range);
        }
        public void Update(bool save)
        {
            Properties.Settings.Default.deviceNum = deviceNum;
            Properties.Settings.Default.scanmode = scanModeNum;
            Properties.Settings.Default.sensitivity = sensitivity;
            Properties.Settings.Default.range = range;
            if (save)
                Properties.Settings.Default.Save();
        }
    }

    public class scanMode
    {
        public int mirror;
        public int nPoints;
        public int hFOV;
        public int vFOV;
        public double hRes;
        public double vRes;
        public int lines;
        public int fRate;
        public string folder;

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
        }
    }

    public enum confStates
    {
        IDLE,
        SEND_CAPTURE_DELAY,
        RESET_DSP,
        SET_SENSITIVITY,
        SET_RANGE,
        SET_RETRO_LEVEL,
        SET_CHIRP_WAVEFORM,
        SET_CHIRP_GAIN,
        SET_PM_CONTROL,
        LOAD_SSH_DRIVER,
        SET_LO,
        SET_TX_SOA1,
        SET_TX_SOA2,
        SET_TX3_0_9,
        SET_TX3_10_19,
        SET_TX3_20_29,
        SET_TX3_30_39,
        SET_VECTOR_1,
        SET_VECTOR_2,
        SET_VECTOR_3,
        SET_VECTOR_4,
        SET_VECTOR_5,
        SET_VECTOR_6,
        SET_OPTOTUNE_X,
        SET_OPTOTUNE_Y,
        SET_MIRROR_FREQUENCY,
        SET_NUMBER_OF_POINTS,
        RUN_OPTOTUNE_CALIBRATION,
        DONE
    }
}
