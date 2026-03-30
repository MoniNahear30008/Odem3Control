using System.Drawing.Design;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace OdemControl
{
    public partial class Form1 : Form
    {
        public bool forceDbgMode = false;
        bool noDevice = false;
        public appSettings appSetting;
        public List<string> deviceID = new List<string>();
        public Dictionary<string, scanMode> scanModes = new Dictionary<string, scanMode>();
        public List<string> modes = new List<string>();
        public Dictionary<string, List<uint>> confFiles = new Dictionary<string, List<uint>>();
        public Dictionary<string, object> AllConfFiles = new Dictionary<string, object>();
        public Dictionary<string, object> AllConfParams = new Dictionary<string, object>();
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
        bool loggingEnabled = true;
        bool debugmodeEnabled = false;
        bool dataLoggingEnabled = false;
        private StreamWriter logFile;
        Cursor previousCursor = Cursors.Default;
        public bool isConnected = false;
        string pushed = "";
        List<uint> awgData = new List<uint>();
        uint awgSize = 0;
        int rowNum = 0;
        bool doSetAll = false;

        public Form1(string version)
        {
            InitializeComponent();


            string path = @"C:\Lidwave";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            this.Text = "ODEM3 Control";

            tempTable.Rows.Add("Optical chip", "");
            tempTable.Rows.Add("Scanner", "");
            tempTable.Rows.Add("Main Board", "");
            tempTable.Rows.Add("Laser", "");
            tempTable.ClearSelection();

            foreach (KeyValuePair<string, string> p in ParamsList)
            {
                paramTable.Rows.Add(p.Key, p.Value);
            }

            List<string> ports = SerialPort.GetPortNames().ToList();
            if (ports.Count == 0)
            {
                MessageBox.Show("No COM port found. Please connect the device and restart the application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                noDevice = true;
                connect.Enabled = false;
            }
            else
            {
                ports.Sort();
                foreach (string p in ports)
                    comports.Items.Add(p);
                comports.SelectedIndex = 0;
            }
            appSetting = new appSettings();

            this.Refresh();
        }

        public static byte[] GetBytesBigEndian(uint value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            return bytes;
        }
        public void LogMessage(string message, bool flush = false)
        {
            // Check if invoke is required
            if (MonitorView.InvokeRequired)
            {
                MonitorView.Invoke(new Action(() =>
                {
                    Add2Monitor(message);
                }));
            }
            else
            {
                Add2Monitor(message);
            }
            //if (loggingEnabled && logFile != null)
            //{
            //    if (message.StartsWith("Reg write") & (message.Length > 80))
            //    {
            //        logFile.WriteLine("Reg Write:");
            //        //                    logFile.WriteLine(DateTime.Now.ToString("yyyyMMdd_HHmmss") + " - Reg Write:");
            //        message = message.Substring(11);
            //        while (message.Length > 96)
            //        {
            //            logFile.WriteLine("                  " + message.Substring(0, 96));
            //            message = message.Substring(96);
            //        }
            //        if (message.Length > 0)
            //            logFile.WriteLine("                  " + message);

            //    }
            //    else
            //    {
            //        logFile.WriteLine(message);
            //        //                    logFile.WriteLine(DateTime.Now.ToString("yyyyMMdd_HHmmss") + " - " + message);
            //    }

            //    if (flush)
            //        logFile.Flush();
            //}
        }
        private void Add2Monitor(string message)
        {
            MonitorView.Text += message + Environment.NewLine;
            if (AutoScroll.Checked)
            {
                MonitorView.SelectionStart = MonitorView.TextLength;
                MonitorView.ScrollToCaret();
            }
        }
        private void ShowTemp()
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
                MessageBox.Show("Please power off and restart the system", "Over temperature", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void checkT_Click(object sender, EventArgs e)
        {
            ReadAllTemp();
        }
        private string ReadPICtemp(out double temp)
        {
            temp = 0;
            List<uint> t = new List<uint> { 100 };
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
            List<uint> t = new List<uint>() { 100 };
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
            //string err = "";
            //List<uint> tmp;
            //List<uint> tmp2 = new List<uint>();
            //err = ReadI2C(0, 0x70, 1, 0x48, 0x14, 0, 1, out tmp);
            //if (err != "")
            //    return err;
            //tmp2.Add(tmp[0]);
            //err = ReadI2C(0, 0x70, 1, 0x49, 0x14, 0, 1, out tmp);
            //if (err != "")
            //    return err;
            //tmp2.Add(tmp[0]);
            //err = ReadI2C(0, 0x70, 1, 0x4A, 0x14, 0, 1, out tmp);
            //if (err != "")
            //    return err;
            //tmp2.Add(tmp[0]);
            //err = ReadI2C(0, 0x70, 1, 0x4B, 0x14, 0, 1, out tmp);
            //if (err != "")
            //    return err;
            //tmp2.Add(tmp[0]);
            //uint mtmp = tmp2.Max();
            //mtmp >>= 4;       // 12-bit value
            //if ((mtmp & 0x800) != 0) mtmp -= 4096;
            //temp = (double)mtmp * 0.0625;
            return "";
        }
        private void connect_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            if (isConnected)
            {
                LogMessage("Disconnecting from device...");
                isConnected = false;
                connect.Text = "Connect";
                deviceState.Text = "DisConnected";
                deviceState.ForeColor = Color.Red;
                _port.Close();
            }
            else
            {
                OpenComPort();

                PingDevice();

                Thread.Sleep(100);

                timer1.Start();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            MessageBox.Show("Connection lost", "Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void clr_Click(object sender, EventArgs e)
        {
            MonitorView.Clear();
        }
        private void loadAwg_Click(object sender, EventArgs e)
        {
            awgData = Enumerable.Range(0, 4096).Select(i => (uint)(i)).ToList();
            awgSize = (uint)awgData.Count;
            awglen.Text = "AWG vector length = " + awgSize.ToString();
        }

        private void sendAWG_Click(object sender, EventArgs e)
        {
            sendAWGtoDevice();
        }

        private void progAWG_Click(object sender, EventArgs e)
        {
            confAWGcmd();
        }

        private void paramTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                setParamInRow(e.RowIndex);
            }
            else
                paramTable.Rows[e.RowIndex].Cells[0].Style.BackColor = System.Drawing.SystemColors.Control;

        }
        private void setParamInRow(int row)
        {
            string val = paramTable.Rows[row].Cells[1].Value.ToString();
            uint iVal = 0;
            bool valid = false;
            if (val.StartsWith("0x"))
                valid = uint.TryParse(val.Substring(2), System.Globalization.NumberStyles.HexNumber, null, out iVal);
            else
                valid = uint.TryParse(val, out iVal);

            if (!valid)
            {
                MessageBox.Show("Invalid value: " + val, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                paramTable.Rows[row].Cells[1].Value = "";
                return;
            }
            rowNum = row;
            setParam(row, iVal, paramTable.Rows[row].Cells[0].Value.ToString());
        }
        private void paintPar(uint pass)
        {
            Color color = Color.Lime;
            if (pass == 1)
                color = Color.Red;

            paramTable.Invoke(new Action(() =>
            {
                paramTable.Rows[rowNum].Cells[0].Style.BackColor = color;
                paramTable.ClearSelection();
                paramTable.CurrentCell = null;
            }));
            if (doSetAll)
            {
                Thread.Sleep(100);
                setAllCtrl();
            }
        }
        private void runAWG_Click(object sender, EventArgs e)
        {
            if (runAWG.Text == "Run AWG")
            {
                runAWG.Text = "Stop AWG";
                AwgControl(true);
            }
            else
            {
                runAWG.Text = "Run AWG";
                AwgControl(false);
            }
        }

        private void setAll_Click(object sender, EventArgs e)
        {
            bool valid = false;
            for (int i = 0; i < paramTable.Rows.Count; i++)
            {
                string val = paramTable.Rows[i].Cells[1].Value.ToString();
                uint iVal = 0;
               
                if (val.StartsWith("0x"))
                    valid = uint.TryParse(val.Substring(2), System.Globalization.NumberStyles.HexNumber, null, out iVal);
                else
                    valid = uint.TryParse(val, out iVal);
                if (!valid)
                {
                    MessageBox.Show("Invalid value in row " + (i+1).ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    paramTable.Rows[i].Cells[1].Value = "";
                    break;
                }
            }
            if (!valid)
                return;

            doSetAll = true;
            setParamInRow(0);
        }
        private void setAllCtrl()
        {
            rowNum++;
            if (rowNum >= paramTable.Rows.Count)
            {
                doSetAll = false;
                return;
            }
            setParamInRow(rowNum);

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
