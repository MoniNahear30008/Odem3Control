using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Tls.Crypto.Impl.BC;
using Renci.SshNet;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace OdemControl
{
    public partial class Form1
    {
        private string _ipAddress = "192.168.2.24";
        private int _port = 24871;
        NetworkStream stream;
        TcpClient client;
        SshClient ssh;

        public event Action<string> OnMessageReceived;

        private void DevieLost()
        {
            LogMessage("Disconnecting from device...");
            isConnected = false;
            client.Close();
            if (ssh != null)
                ssh.Disconnect();
            connect.Text = "Connect";
            deviceState.Text = "DisConnected";
            deviceState.ForeColor = Color.Red;
            streamBox(false);
            mainBoxEnable(false);
            devices.Enabled = true;
            foreach (DataGridViewRow row in tempTable.Rows)
                row.Cells[1].Value = "";

        }
        private void mainBoxEnable(bool enable)
        {
            scanMode.Enabled = enable;
            ModeParams.Enabled = enable;
        }
        private async void ConnectToDevice()
        {
            timer1.Stop();
            if (isConnected)
            {
                LogMessage("Disconnecting from device...");
                isConnected = false;
                client.Close();
                ssh.Disconnect();
                connect.Text = "Connect";
                deviceState.Text = "DisConnected";
                deviceState.ForeColor = Color.Red;
                mainBoxEnable(false);
                devices.Enabled = true;
            }
            else
            {
                await ConnectNow();

                if (isConnected)
                {
                    devices.Enabled = false;
                    connect.Text = "Disconnect";
                    deviceState.Text = "Connected";
                    deviceState.ForeColor = Color.Green;
                    mainBoxEnable(true);
                    autoTempControl();
                    timer1.Start();
                }
                else
                {
                    MessageBox.Show("Failed to connect to the device.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    connect.Text = "Connect";
                    deviceState.Text = "Disconnected";
                    deviceState.ForeColor = Color.Red;
                }
            }
        }
        private async Task ConnectNow()
        {
            if (client != null)
                client.Close();
            client = new TcpClient();
            Task connectTask = client.ConnectAsync(_ipAddress, _port);
            if (await Task.WhenAny(connectTask, Task.Delay(10000)) == connectTask)
            {
                if (client.Connected)
                {
                    isConnected = true;
                    stream = client?.GetStream();
//                    client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                    stream.ReadTimeout = 10000;
                    ssh = new SshClient("192.168.2.24", "root", "");
                    ssh.Connect();
                }
            }
        }
        private void SetKeepAlive(Socket socket, uint keepAliveTime, uint keepAliveInterval)
        {
            // Structure: 3 x 4-byte unsigned int
            byte[] inOptionValues = new byte[12];
            BitConverter.GetBytes((uint)1).CopyTo(inOptionValues, 0);               // enable
            BitConverter.GetBytes(keepAliveTime).CopyTo(inOptionValues, 4);         // time in ms
            BitConverter.GetBytes(keepAliveInterval).CopyTo(inOptionValues, 8);     // interval in ms

            socket.IOControl(IOControlCode.KeepAliveValues, inOptionValues, null);
        }
        private async Task ReceiveListener()
        {
            byte[] buffer = new byte[1024];

            try
            {
                while (true)
                {
                    int bytes = await stream.ReadAsync(buffer, 0, buffer.Length);

                    if (bytes <= 0) break;  // disconnected

                    string msg = Encoding.UTF8.GetString(buffer, 0, bytes);

                    if (HandleMessage != null)
                    {
                        msg = HandleMessage(msg);
                    }

                    // Trigger event
                    OnMessageReceived?.Invoke(msg);
                }
            }
            catch
            {
                // disconnected
            }
        }
        private string HandleMessage(string msg)
        {
            // Handle incoming messages from the device
            Console.WriteLine("Received message: " + msg);
            return msg;
        }
        private bool SendCommand(byte[] TxBuf)
        {
            if (!isConnected) return false;
            stream.Write(TxBuf);
            return true;

        }
        private void PingDevice()
        {
//            return;
            List<byte> data = new List<byte>();
            data.Add(0x09);         // Write command
            data.Add(0x00);         // Reserved
            data.AddRange(new List<byte>() { 0, 0, 0, 0 });
            data.AddRange(new List<byte>() { 0, 0, 0, 0 });
            LogMessage("Ping device");

            if (stream.CanWrite == false)
            {
                DevieLost();
            }

            try
            {
                byte[] TxBuf = data.ToArray();
                stream.Write(TxBuf);
            }
            catch
            {
                pingLost--;
                if (pingLost == 0)
                    DevieLost();
            }

            string res = WaitWriteRespose(9, false);
            if (res != "")
            {
                pingLost--;
                if (pingLost == 0)
                    DevieLost();
            }


        }
        private byte[] SerWriteRegBuf(uint add, List<uint> vals)
        {
            List<byte> data = new List<byte>();
            data.Add(0x01);         // Write command
            data.Add(0x00);         // Reserved
            data.AddRange(GetBytesBigEndian(add));
            data.AddRange(GetBytesBigEndian((uint)vals.Count));
            foreach (uint v in vals)
                data.AddRange(GetBytesBigEndian(v));
            if (dataLoggingEnabled)
            {
                string tx = "01 00 ";
                foreach (byte b in data.Skip(2))
                    tx += b.ToString("X2") + " ";
                LogMessage("Reg write: " + tx);
            }
            return data.ToArray();
        }
        public string WriteRegWaitResp(uint add, List<uint> vals)
        {
            byte[] TxBuf = SerWriteRegBuf(add, vals);
            if (!isConnected) return "Device not connected";
            if (stream.CanWrite == false)
            {
                DevieLost();
                return "Device not reponding";
            }

            try
            {
                stream.Write(TxBuf);
            }
            catch (IOException)
            {
                DevieLost();
                return "Device not reponding";
            }

            string res = WaitWriteRespose(1);
            return res;
        }
        public string WriteI2CWaitResp(uint ch, uint dev, uint option, uint reg, List<uint> vals)
        {
            if (!isConnected) return "Device not connected";

            byte[] regadd = GetBytesBigEndian(reg).ToArray();
            List<byte> data = new List<byte>();
            data.Add(0x07);         // command ID
            data.Add(0x00);         // Bus
            data.Add(0x70);         // Mux
            data.Add((byte)ch);     // Mux channel
            data.Add((byte)dev);    // Device
            data.Add((byte)option); // I2C device register
            data.AddRange(GetBytesBigEndian((uint)vals.Count));  // Number of values
            switch (option & 0x30)
            {
                case 0x10:                  // 8 bits register address
                    data.Add(regadd[3]);    
                    break;

                case 0x20:                  // 16 bits register address
                    data.Add(regadd[2]);
                    data.Add(regadd[3]);
                    break;

                default:
                    MessageBox.Show("Invalid I2C register address option");
                    return "Invalid I2C register address option";
            }
            switch (option & 0xC)
            {
                case 0x0:                  // 8 bits value
                    foreach (uint v in vals)
                        data.Add((byte)v);
                    break;

                case 0x4:                  // 16 bits value
                    foreach (uint v in vals)
                    {
                        data.Add((byte)(v>> 8));
                        data.Add((byte)v);
                    }
                    break;

                default:
                    MessageBox.Show("Invalid I2C data size option");
                    return "Invalid I2C data size option";
            }

            if (dataLoggingEnabled)
            {
                string tx = "";
                foreach (byte b in data)
                    tx += b.ToString("X2") + " ";
                LogMessage("I2C write: " + tx);
            }

            byte[] TxBuf = data.ToArray();
            if (stream.CanWrite == false)
            {
                DevieLost();
                return "Device not reponding";
            }
            try
            {
                stream.Write(TxBuf);
            }
            catch (IOException)
            {
                DevieLost();
                return "Device not reponding";
            }
            string res = WaitWriteRespose(7);
            return res;
        }
        public string SPISOAControl(uint val)
        {
            if (!isConnected) return "Device not connected";
            List<byte> data = new List<byte>();
            data.Add(0x0A);         // command
            data.Add(0x01);         // Sub command
            data.AddRange(new List<byte>() { 0, 0, 0, 0 });
            data.AddRange(GetBytesBigEndian((uint)16));
            data.AddRange(GetBytesBigEndian(val));
            if (dataLoggingEnabled)
            {
                string tx = "";
                foreach (byte b in data)
                    tx += b.ToString("X2") + " ";
                LogMessage("SOA write: " + tx);
            }
            byte[] TxBuf = data.ToArray();
            if (stream.CanWrite == false)
            {
                DevieLost();
                return "Device not reponding";
            }
            try
            {
                stream.Write(TxBuf);
            }
            catch (IOException)
            {
                DevieLost();
                return "Device not reponding";
            }

            string res = WaitWriteRespose(10);
            return res;
        }
        private string SPIWriteAWGWaitResp(List<uint> vals)
        {
            if (!isConnected) return "Device not connected";

            List<byte> data = new List<byte>();
            data.Add(0x0B);         // command
            data.Add(0x00);         // Sub command
            data.AddRange(new List<byte>() { 0, 0, 0, 0 });
            data.AddRange(GetBytesBigEndian((uint)vals.Count()));   // length
            foreach (uint val in vals)
                data.AddRange(new List<byte>() { (byte)(val >> 8), (byte)(val & 0xff)});  // Number of values

            if (dataLoggingEnabled)
            {
                string tx = "";
                foreach (byte b in data)
                    tx += b.ToString("X2") + " ";
                LogMessage("AWG write: " + tx);
            }

            byte[] TxBuf = data.ToArray();
            if (stream.CanWrite == false)
            {
                DevieLost();
                return "Device not reponding";
            }
            try
            {
                stream.Write(TxBuf);
            }
            catch (IOException)
            {
                DevieLost();
                return "Device not reponding";
            }

            string res = WaitWriteRespose(11);
            if (res != "")
                return res;

            data.Clear();
            data.Add(0x0A);         // command
            data.Add(0x02);         // Sub command
            data.AddRange(new List<byte>() { 0, 0, 0, 0 });
            data.AddRange(new List<byte>() { 0, 0, 0, 0 });
            data.AddRange(GetBytesBigEndian((uint)vals.Count()));   // length
            if (dataLoggingEnabled)
            {
                string tx = "";
                foreach (byte b in data)
                    tx += b.ToString("X2") + " ";
                LogMessage("AWG gain write: " + tx);
            }

            TxBuf = data.ToArray();
            if (stream.CanWrite == false)
            {
                DevieLost();
                return "Device not reponding";
            }
            try
            {
                stream.Write(TxBuf);
            }
            catch (IOException)
            {
                DevieLost();
                return "Device not reponding";
            }
            res = WaitWriteRespose(10);
            return res;

        }
        private string SendStopCmd()
        {
            List<byte> data = new List<byte>();
            data.Add(0x04);         // command
            data.Add(0x03);         // Sub command
            data.AddRange(new List<byte>() { 0, 0, 0, 0 });   // Address
            data.AddRange(new List<byte>() { 0, 0, 0, 0 });   // length

            if (dataLoggingEnabled)
            {
                string tx = "";
                foreach (byte b in data)
                    tx += b.ToString("X2") + " ";
                LogMessage("SPI Write: " + tx);
            }

            byte[] TxBuf = data.ToArray();
            if (stream.CanWrite == false)
            {
                LogMessage("Streamer write error");
                DevieLost();
                return "Device not reponding";
            }

            try
            {
                stream.Write(TxBuf);
            }
            catch (IOException)
            {
                LogMessage("Streamer write error");
                DevieLost();
                return"Device not reponding";
            }

            string res = WaitWriteRespose(4);
            return res;
        }
        private string SendRunCmd(int mode)
        {
            List<byte> data = new List<byte>();
            data.Add(0x04);         // command
            data.Add(0x09);         // Sub command
            data.AddRange(new List<byte>() { 0, 0, 0, 0 });   // Address
            data.AddRange(new List<byte>() { 0, 0, 0, 2 });   // length
            data.Add((byte)(0x30 + mode));
            data.Add(0x00);

            if (dataLoggingEnabled)
            {
                string tx = "";
                foreach (byte b in data)
                    tx += b.ToString("X2") + " ";
                LogMessage("SPI Write: " + tx);
            }

            byte[] TxBuf = data.ToArray();
            if (stream.CanWrite == false)
            {
                DevieLost();
                return "Device not reponding";
            }
            try
            {
                stream.Write(TxBuf);
            }
            catch (IOException)
            {
                DevieLost();
                return "Device not reponding";
            }

            try
            {
                stream.Write(TxBuf);
            }
            catch (IOException)
            {
                DevieLost();
                return "Device not reponding";
            }

            string res = WaitWriteRespose(4);
            return res;

        }
        private string RunOpto(int mode)
        {
            if (!isConnected) return "Device not connected";
            optoStat.Maximum = 7;
            optoStat.Value = 1;
            optoStat.Visible = true;
            stream.ReadTimeout = 500000;
            // Send command and wait progress
            string res = SendRunCmd(mode);
            stream.ReadTimeout = 10000;
            optoStat.Visible = false;
            return res;
        }
        private string WaitWriteRespose(int cmd, bool waitmsg = true)
        { 
            int stepNum = 0;
            int NumSteps = 7;

            List<byte> resp = new List<byte>();
            try
            {
                while (true)
                {
                    while (true)
                    {
                        this.Refresh();
                        byte[] buffer = new byte[1024];
                        int count = stream.Read(buffer, 0, buffer.Length);
                        resp.AddRange(new List<byte>(buffer.Take(count)));
                        // ACK/NACK respons
                        if (resp.Count >= 8)
                        {
                            if ((resp[0] == 0) && (resp[1] == cmd))
                            {
                                LogMessage("Command pass");
                                return "";
                            }
                            else if (resp[0] == 1)
                            {
                                LogMessage("Command fail");
                                int ml = ((int)buffer[4] << 24) + ((int)buffer[5] << 16) + ((int)buffer[6] << 8) + (int)buffer[7];
                                string s = new string(Encoding.ASCII.GetChars(buffer), 12, ml + 1);
                                if (loggingEnabled)
                                    LogMessage("Reg write response: " + s);
                                return s;

                            }
                        }
                        while (resp.Count > 17)
                        {
                            int ml = ((int)resp[4] << 24) + ((int)resp[5] << 16) + ((int)resp[6] << 8) + (int)resp[7];
                            if (resp.Count < (ml + 12))
                                continue;
                            if ((resp[0] == 2) && (resp[1] == 9))
                            {
                                stepNum = (int)resp[11];
                                NumSteps = (int)resp[15];
                                int sl = ((int)resp[16] << 24) + ((int)resp[17] << 16) + ((int)resp[18] << 8) + (int)resp[19];
                                string msg = new string(Encoding.ASCII.GetChars(buffer), 20, sl);
                                LogMessage("Running: Step " + stepNum.ToString() + " / " + NumSteps.ToString() + " ==> " + msg);
                                resp.RemoveRange(0, ml + 12);

                                optoStat.Value = stepNum;
                                this.Refresh();
                                if (stepNum == NumSteps)
                                {
                                    return "";
                                }
                            }
                            else
                            {
                                string s = new string(Encoding.ASCII.GetChars(buffer), 12, ml);
                                return s;
                            }
                        }
                    }
                }
            }
            catch (IOException)
            {
                if (waitmsg)
                      DevieLost();
                return "Device not reponding";
    
            }
        }
        private string ReadI2C(uint ch, uint dev, uint option, uint reg, uint len, out List<uint> vals, bool waitmsg = true)
        {
            vals = null;
            if (!isConnected)
                return "Device not connected";
 
            byte[] regadd = GetBytesBigEndian(reg).ToArray();
            List<byte> data = new List<byte>();
            data.Add(0x08);         // command ID
            data.Add(0x00);         // Bus
            data.Add(0x70);         // Mux
            data.Add((byte)ch);     // Mux channel
            data.Add((byte)dev);    // Device
            data.Add((byte)option); // I2C device register
            data.AddRange(GetBytesBigEndian(len));  // Number of values

            switch (option & 0x30)
            {
                case 0x10:                  // 8 bits register address
                    data.Add(regadd[3]);
                    break;

                case 0x20:                  // 16 bits register address
                    data.Add(regadd[2]);
                    data.Add(regadd[3]);
                    break;

                default:
                    MessageBox.Show("Invalid I2C register address option");
                    return "Invalid I2C register address option";
            }
            if (dataLoggingEnabled)
            {
                string tx = "";
                foreach (byte b in data)
                    tx += b.ToString("X2") + " ";
                LogMessage("I2C read command: " + tx);
            }

            byte[] TxBuf = data.ToArray();
            if (stream.CanWrite == false)
            {
                DevieLost();
                return "Device not reponding";
            }
            try
            {
                stream.Write(TxBuf);
            }
            catch (IOException)
            {
                vals = null;
                DevieLost();
                return "Device not reponding";
            }
            
            string res = WaitReadRespose(8, option, out vals, waitmsg);
            return res;
        }
        private string WaitReadRespose(int cmd, uint option, out List<uint> vals, bool waitmsg = true)
        {
            vals = null;
            int stepNum = 0;
            int NumSteps = 7;

            List<byte> resp = new List<byte>();
            try
            {
                while (true)
                {
                    while (true)
                    {
                        this.Refresh();
                        byte[] buffer = new byte[1024];
                        int count = stream.Read(buffer, 0, buffer.Length);
                        resp.AddRange(new List<byte>(buffer.Take(count)));
                        // ACK/NACK respons
                        if (resp.Count >= 10)
                        {
                            if ((resp[0] == 0) && (resp[1] == cmd))
                            {
                                vals = new List<uint>();
                                int nvals = ((int)buffer[4] << 24) + ((int)buffer[5] << 16) + ((int)buffer[6] << 8) + (int)buffer[7];
                                int idx = 8;
                                if ((option & 0xC) == 0)
                                {
                                    for (int n = 0; n < nvals; n++)
                                        vals.Add((uint)buffer[idx + n]);
                                }
                                else
                                {
                                    for (int n = 0; n < nvals; n += 2)
                                    {
                                        vals.Add(((uint)buffer[idx + n] << 8) + (uint)buffer[idx + n + 1]);
                                    }

                                }
                                return "";
                            }
                            else if (resp[0] == 1)
                            {
                                LogMessage("Command fail");
                                int ml = ((int)buffer[4] << 24) + ((int)buffer[5] << 16) + ((int)buffer[6] << 8) + (int)buffer[7];
                                string s = new string(Encoding.ASCII.GetChars(buffer), 12, ml + 1);
                                if (loggingEnabled)
                                    LogMessage("Reg write response: " + s);
                                return s;

                            }
                        }

                        while (resp.Count > 17)
                        {
                            int ml = ((int)resp[4] << 24) + ((int)resp[5] << 16) + ((int)resp[6] << 8) + (int)resp[7];
                            if (resp.Count < (ml + 12))
                                continue;
                            if ((resp[0] == 2) && (resp[1] == 9))
                            {
                                stepNum = (int)resp[11];
                                NumSteps = (int)resp[15];
                                int sl = ((int)resp[16] << 24) + ((int)resp[17] << 16) + ((int)resp[18] << 8) + (int)resp[19];
                                string msg = new string(Encoding.ASCII.GetChars(buffer), 20, sl);
                                LogMessage("Running: Step " + stepNum.ToString() + " / " + NumSteps.ToString() + " ==> " + msg);
                                resp.RemoveRange(0, ml + 12);

                                optoStat.Value = stepNum;
                                this.Refresh();
                                if (stepNum == NumSteps)
                                {
                                    return "";
                                }
                            }
                            else
                            {
                                string s = new string(Encoding.ASCII.GetChars(buffer), 12, ml);
                                return s;
                            }
                        }
                    }
                }
            }
            catch (IOException)
            {
                if (waitmsg)
                    DevieLost();
                return "Device not reponding";
            }
        }
        private string LoadHHSDriver()
        {
            var result = ssh.CreateCommand($"lsmod").Execute().Trim();
            if (!result.Contains("altera_msgdma_st"))
            {
                string command = $"insmod /lib/modules/$(uname -r)/extra/altera_msgdma_st.ko udp_forwarding=1 udp_dest_ip=\"192.168.2.20\" udp_dest_port=10003 transfer_size=704";
                var reult = ssh.CreateCommand(command).Execute().Trim();  //cmd.Execute();
                result = ssh.CreateCommand($"lsmod").Execute().Trim();  //cmd.Execute();
            }
            return "";
        }
        public void RemoteDirectoryExists()
        {
            string path = "/var/lib/odem/patterns";
            string command = $"[ -d \"{path}\" ] && echo exists || echo missing";
            var result = ssh.CreateCommand(command).Execute().Trim();
            if (result != "exists")
                ssh.CreateCommand($"mkdir -p \"{path}\"").Execute();
        }
        private string LoadFiles()
        {
            string modeName = modes[appSetting.scanModeNum];
            int modeIndex = scanModes[modeName].modeNum;
            var assembly = Assembly.GetExecutingAssembly();


            RemoteDirectoryExists();

            using (var client = new ScpClient("192.168.2.24", "root", ""))
            {
                client.Connect();

                string resourceName = "OdemControl.Optotune." + scanModes[modeName].folder + ".scan_parameters.json";
                Stream resourceStream = assembly.GetManifestResourceStream(resourceName);
                if (resourceStream == null)
                {
                    return "Failed to read device configuation file";
                }

                client.Upload(resourceStream, "/var/lib/odem/patterns/" + modeIndex.ToString() + "_scan_parameters.json");

                resourceName = "OdemControl.Optotune." + scanModes[modeName].folder + ".waveformX.csv";
                resourceStream = assembly.GetManifestResourceStream(resourceName);
                if (resourceStream == null)
                {
                    return "Failed to read device configuation file";
                }
                client.Upload(resourceStream, "/var/lib/odem/patterns/" + modeIndex.ToString() + "_waveformX.csv");

                resourceName = "OdemControl.Optotune." + scanModes[modeName].folder + ".waveformY.csv";
                resourceStream = assembly.GetManifestResourceStream(resourceName);
                if (resourceStream == null)
                {
                    return "Failed to read device configuation file";
                }
                client.Upload(resourceStream, "/var/lib/odem/patterns/" + modeIndex.ToString() + "_waveformY.csv");

                client.Disconnect();
            }

            return "";
        }
        private string StreamingCmd(bool start)
        {
            if (!isConnected) return "Device not connected";

            List<byte> data = new List<byte>();
            data.Add(0x05);         // command
            if (start)
                data.Add(0x01);         // Sub command
            else
                data.Add(0x02);         // Sub command
            data.AddRange(new List<byte>() { 0, 0, 0, 0 });   // Address
            data.AddRange(new List<byte>() { 0, 0, 0, 0 });   // length

            if (dataLoggingEnabled)
            {
                string tx = "";
                foreach (byte b in data)
                    tx += b.ToString("X2") + " ";
                LogMessage("Straem command write: " + tx);
            }

            if (stream.CanWrite == false)
            {
                DevieLost();
                return "Device not reponding";
            }
            try
            {
                byte[] TxBuf = data.ToArray();
                stream.Write(TxBuf);
            }
            catch (Exception ex)
            {
                DevieLost();
                return "Device not reponding";
            }

            string res = WaitWriteRespose(5);
            return res;
        }
        private string ReadRegFromOT(uint sys, uint reg, out double val)
        {
            val = 0;
            List<byte> data = new List<byte>();
            data.Add(0x04);         // command
            data.Add(0x07);         // Sub command
            data.AddRange(new List<byte>() { 0, 0, 0, 0 });   // Address
            data.AddRange(new List<byte>() { 0, 0, 0, 2 });   // length
            data.Add((byte)sys);
            data.Add((byte)reg);

            if (dataLoggingEnabled)
            {
                string tx = "";
                foreach (byte b in data)
                    tx += b.ToString("X2") + " ";
                LogMessage("OT Reg read: " + tx);
            }

            byte[] TxBuf = data.ToArray();
            if (stream.CanWrite == false)
            {
                DevieLost();
                return "Device not reponding";
            }

            try
            {
                stream.Write(TxBuf);

                byte[] buffer = new byte[1024];
                int count = stream.Read(buffer, 0, buffer.Length);
                if (dataLoggingEnabled)
                {
                    string tx = "";
                    for (int i = 0; i < count; i++)
                        tx += buffer[i].ToString("X2") + " ";
                    LogMessage("SOA write response: " + tx);
                }
                if (!((count >= 8) && (buffer[0] == 0) && (buffer[1] == 4)))
                {
                    int ml = ((int)buffer[4] << 24) + ((int)buffer[5] << 16) + ((int)buffer[6] << 8) + (int)buffer[7];
                    string s = new string(Encoding.ASCII.GetChars(buffer), 12, ml + 1);
                    return s;
                }
                else
                {
                    uint v = ((uint)buffer[8] << 24) + ((uint)buffer[9] << 16) + ((uint)buffer[10] << 8) + (uint)buffer[11];
                    val = BitConverter.UInt32BitsToSingle(v);
                }
            }
            catch (IOException)
            {
                DevieLost();
                return "Device not reponding";
            }


            return "";
        }
    }
}
