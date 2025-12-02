using Org.BouncyCastle.Tls.Crypto.Impl.BC;
using Renci.SshNet;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
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

        private async void ConnectToDevice()
        {
            if (isConnected)
            {
                LogMessage("Disconnecting from device...");
                isConnected = false;
                client.Close();
                ssh.Disconnect();
                connect.Text = "Connect";
                deviceState.Text = "DisConnected";
                deviceState.ForeColor = Color.Red;
                mainBox.Enabled = false;
                devices.Enabled = true;
            }
            else
            {
                client = new TcpClient();
                Task connectTask = client.ConnectAsync(_ipAddress, _port);
                if (await Task.WhenAny(connectTask, Task.Delay(10000)) == connectTask)
                {
                    // Connected successfully
                    if (client.Connected)
                    {
                        isConnected = true;
                        stream = client?.GetStream();
                        client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                        if (KeepAlive.Checked)
                        {
                            SetKeepAlive(client.Client, 5000, 1000); // 5s idle, 1s interval
                            LogMessage("Connected to device with KeepAlive");
                        }
                        else
                            LogMessage("Connected to device without KeepAlive");

                        readTemp();
                    }
                }


                // Simulate a connection attempt
                if (isConnected)
                {
                    devices.Enabled = false;
                    connect.Text = "Disconnect";
                    deviceState.Text = "Connected";
                    deviceState.ForeColor = Color.Lime;
                    mainBox.Enabled = true;
                    ssh = new SshClient("192.168.2.24", "root", "");
                    ssh.Connect();

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
        private string WriteRegWaitResp(uint add, List<uint> vals)
        {
            byte[] TxBuf = SerWriteRegBuf(add, vals);
            if (!isConnected) return "Device not connected";
            stream.ReadTimeout = 10000;
            stream.Write(TxBuf);

            try
            {
                byte[] buffer = new byte[1024];
                int count = stream.Read(buffer, 0, buffer.Length);
                if ((count >= 8) && (buffer[0] == 0) && (buffer[1] == 1))
                {
                    LogMessage("Reg write pass");
                    return "";
                }
                else
                {
                    LogMessage("Reg write fail");
                    int ml = ((int)buffer[4] << 24) + ((int)buffer[5] << 16) + ((int)buffer[6] << 8) + (int)buffer[7];
                    string s = new string(Encoding.ASCII.GetChars(buffer), 12, ml + 1);
                    if (loggingEnabled)
                        LogMessage("Reg write response: " + s);
                    return s;
                }
            }
            catch (IOException)
            {
                return "Device not reponding";
            }
        }
        private string WriteI2CWaitResp(uint ch, uint dev, uint option, uint reg, List<uint> vals)
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
            stream.ReadTimeout = 10000;
            stream.Write(TxBuf);

            try
            {
                byte[] buffer = new byte[1024];
                int count = stream.Read(buffer, 0, buffer.Length);
                if ((count >= 8) && (buffer[0] == 0) && (buffer[1] == 7))
                    return "";
                else
                {
                    int ml = ((int)buffer[4] << 24) + ((int)buffer[5] << 16) + ((int)buffer[6] << 8) + (int)buffer[7];
                    string s = new string(Encoding.ASCII.GetChars(buffer), 12, ml + 1);
                    return s;
                }
            }
            catch (IOException)
            {
                return "Device not reponding";
            }
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
                LogMessage("I2C write: " + tx);
            }

            byte[] TxBuf = data.ToArray();
            stream.ReadTimeout = 10000;
            stream.Write(TxBuf);

            try
            {
                byte[] buffer = new byte[1024];
                int count = stream.Read(buffer, 0, buffer.Length);
                if (!((count >= 8) && (buffer[0] == 0) && (buffer[1] == 11)))
                {
                    int ml = ((int)buffer[4] << 24) + ((int)buffer[5] << 16) + ((int)buffer[6] << 8) + (int)buffer[7];
                    string s = new string(Encoding.ASCII.GetChars(buffer), 12, ml + 1);
                    return s;
                }
            }
            catch (IOException)
            {
                return "Device not reponding";
            }

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
                LogMessage("I2C write: " + tx);
            }

            TxBuf = data.ToArray();
            stream.ReadTimeout = 10000;
            stream.Write(TxBuf);

            try
            {
                byte[] buffer = new byte[1024];
                int count = stream.Read(buffer, 0, buffer.Length);
                if ((count >= 8) && (buffer[0] == 0) && (buffer[1] == 10))
                    return "";
                else
                {
                    int ml = ((int)buffer[4] << 24) + ((int)buffer[5] << 16) + ((int)buffer[6] << 8) + (int)buffer[7];
                    string s = new string(Encoding.ASCII.GetChars(buffer), 12, ml + 1);
                    return s;
                }
            }
            catch (IOException)
            {
                return "Device not reponding";
            }


        }
        private string RunOpto(int mode)
        {
            if (!isConnected) return "Device not connected";

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
            stream.ReadTimeout = 100000;
            stream.Write(TxBuf);

            int stepNum = 0;
            int NumSteps = 20;

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
                return "Device not reponding";
            }
        }
        private string ReadI2C(uint ch, uint dev, uint option, uint reg, uint len, out List<uint> vals)
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
            stream.ReadTimeout = 10000;
            stream.Write(TxBuf);

            try
            {
                byte[] buffer = new byte[1024];
                int count = stream.Read(buffer, 0, buffer.Length);
                if ((count >= 8) && (buffer[0] == 0) && (buffer[1] == 8))
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
                else
                {
                    int ml = ((int)buffer[4] << 24) + ((int)buffer[5] << 16) + ((int)buffer[6] << 8) + (int)buffer[7];
                    string s = new string(Encoding.ASCII.GetChars(buffer), 12, ml + 1);
                    return s;
                }
            }
            catch (IOException)
            {
                vals = null;
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
                LogMessage("I2C write: " + tx);
            }

            byte[] TxBuf = data.ToArray();
            stream.ReadTimeout = 1000;
            stream.Write(TxBuf);

            try
            {
                byte[] buffer = new byte[1024];
                int count = stream.Read(buffer, 0, buffer.Length);
                if ((count >= 8) && (buffer[0] == 0) && (buffer[1] == 5))
                    return "";
                else
                {
                    int ml = ((int)buffer[4] << 24) + ((int)buffer[5] << 16) + ((int)buffer[6] << 8) + (int)buffer[7];
                    string s = new string(Encoding.ASCII.GetChars(buffer), 12, ml + 1);
                    return s;
                }
            }
            catch (IOException)
            {
                return "Device not reponding";
            }


            return "";
        }
    }
}
