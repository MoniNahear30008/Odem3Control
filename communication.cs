using DocumentFormat.OpenXml.Vml;
using System.IO.Ports;
using System.Text;

namespace OdemControl
{
    public partial class Form1
    {
        public event Action<string> OnMessageReceived;
        private SerialPort _port;
        private List<byte> _buffer = new List<byte>();
        private int _frameLength;
        private bool waitMsg = true;
        private int rxMsgLen = 0;
        private int awgPacketNum = 0;
        uint nPackets = 0;

        // User callback for complete frames
        public Action<byte[]> OnFrameReceived;

        public void setParam(int pID, uint pVal, string pName)
        {
            LogMessage("Set " + pName + " = " + pVal.ToString() + " (0x" + pVal.ToString("X08") + ")");
            byte[] data = new byte[] { 0x55, 0x55, 0x00, 0x0B, 0, 4, 0, 0, 0, 0, 0 };
            data[6] = (byte)(pID & 0xFF);           
            data[7] = (byte)((pVal >> 24) & 0xFF);
            data[7] = (byte)((pVal >> 16) & 0xFF);
            data[9] = (byte)((pVal >> 8) & 0xFF);
            data[10] = (byte)(pVal & 0xFF);
            waitMsg = true;
            _port.Write(data, 0, 11);
        }
        public void AwgControl(bool run)
        {
            byte[] data = new byte[] { 0x55, 0x55, 0x00, 0x07, 0, 5, 0 };
            if (run)
            {
                LogMessage("Run AWG");
                data[6] = 1;
            }
            else
                LogMessage("Stop AWG");
            waitMsg = true;
            _port.Write(data, 0, 7);
        }
        public void confAWGcmd()
        {
            LogMessage("Send config AWG");
            byte[] data = new byte[] { 0x55, 0x55, 0x00, 0x08, 0, 3, 0, 0 };
            data[6] = (byte)((awgSize >> 8) & 0xFF); // MSB
            data[7] = (byte)(awgSize & 0xFF);        // LSB
            waitMsg = true;
            _port.Write(data, 0, 8);
        }
        public void sendAWGtoDevice()
        {
            nPackets = (uint)Math.Ceiling((double)awgSize / 64.0);
            awgPacketNum = 0;
            sendNextPacket(-1, 0);
        }
        public void sendNextPacket(int packet, uint err)
        {
            if (err != 0)
            {
                LogMessage("Error response received for AWG packet number " + packet.ToString() + ": error code " + err.ToString());
                return;
            }
            if (packet != awgPacketNum - 1)
            {
                LogMessage("Unexpected AWG packet number received: " + packet.ToString() + " expected: " + (awgPacketNum-1).ToString());
                return;
            }

            if (awgPacketNum >= nPackets)
            {
                LogMessage("All AWG data sent");
                return;
            }
            LogMessage("Send AWG packet number " + awgPacketNum.ToString());
            List<byte> data = new List<byte>() { 0x55, 0x55, 0x00, 0x88, 0, 2 };
            data.Add((byte)((awgPacketNum >> 8) & 0xFF)); // MSB
            data.Add((byte)(awgPacketNum & 0xFF));        // LSB
            for (int i = 0; i < 64; i++)
            {
                data.Add((byte)((awgPacketNum * 64 + i) & 0xFF));        // LSB
                data.Add((byte)(((awgPacketNum * 64 + i) >> 8) & 0xFF)); // MSB
            }
            waitMsg = true;
            _port.Write(data.ToArray(), 0, data.Count);
            awgPacketNum++;
        }
        private void HandleDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int bytesToRead = _port.BytesToRead;
            if (bytesToRead == 0) return;

            byte[] temp = new byte[bytesToRead];
            _port.Read(temp, 0, bytesToRead);

            lock (_buffer) // ensure thread-safety
            {
                _buffer.AddRange(temp);

                if (waitMsg)
                {
                    if (_buffer.Count > 3)
                    {
                        if ((_buffer[0] == 0x55) && (_buffer[1] == 0x55))
                        {
                            rxMsgLen = (int)((uint)_buffer[2] << 8) | _buffer[3];
                            waitMsg = false;
                        }
                        else
                        {
                            _buffer.Clear();
                        }
                    }
                }

                if (!waitMsg && (_buffer.Count >= rxMsgLen))
                {
                    byte[] frame = _buffer.GetRange(0, rxMsgLen).ToArray();
                    _buffer.RemoveRange(0, rxMsgLen);

                    // Invoke callback safely
                    OnFrameReceived?.Invoke(frame);
                }
            }
        }
        private void deviceMsgHandler(byte[] msg)
        {
            timer1.Stop();
            uint msgCode = ((uint)msg[4] << 8) | msg[5];
            string response = "";

            switch (msgCode)
            {
                case 0x0000:    // Ping response
                    LogMessage("Device ping response received");
                    deviceState.Text = "Connected";
                    deviceState.ForeColor = Color.Green;
                    if (connect.InvokeRequired)
                    {
                        connect.Invoke(new Action(() =>
                        {
                            connect.Text = "Disconnect";
                        }));
                    }
                    else
                    {
                        connect.Text = "Disconnect";
                    }

                    isConnected = true;
                    setControlsEnabled(true);

                    break;

                case 0x0001:    // Temperature response
                    int tempA = (int)(((uint)msg[6] << 8) | msg[7]);
                    int tempB = (int)(((uint)msg[8] << 8) | msg[9]);
                    int tempC = (int)(((uint)msg[10] << 8) | msg[11]);
                    int tempD = (int)(((uint)msg[12] << 8) | msg[13]);
                    tempTable.Rows[0].Cells[1].Value = ((double)tempA / 100).ToString("0.00") + " °c";
                    tempTable.Rows[1].Cells[1].Value = ((double)tempB / 100).ToString("0.00") + " °c";
                    tempTable.Rows[2].Cells[1].Value = ((double)tempC / 100).ToString("0.00") + " °c";
                    tempTable.Rows[3].Cells[1].Value = ((double)tempD / 100).ToString("0.00") + " °c";
                    break;

                case 0x0002:    // AWG packet response
                    int packetNum = ((int)msg[7] << 8) | msg[8];
                    LogMessage("AWG response for packet number " + packetNum.ToString());
                    sendNextPacket(packetNum, (uint)msg[6]);
                    break;

                case 0x0003:    // AWG packet response
                    response = "AWG configuration ";
                    if (msg[6] == 0)
                        response += "done";
                    else
                        response += "failed";
                    LogMessage(response);
                    break;

                case 0x0004:    // Set parameter response
                    response = "Set parameter ";
                    if (msg[6] == 0)
                        response += "done";
                    else
                        response += "failed";
                    LogMessage(response);
                    paintPar(msg[6]);
                    break;

                case 0x0005:    // Set parameter response
                    response = "AWG Control ";
                    if (msg[6] == 0)
                        response += "done";
                    else
                        response += "failed";
                    LogMessage(response);
                    break;

                default:
                    LogMessage("Unknown message received: " + msgCode.ToString("X4"));
                    break;
            }

        }
        private void OpenComPort()
        {
            if (_port != null)
            {
                try
                {
                    _port.Close();
                }
                catch { }
            }

            string cp = comports.SelectedItem?.ToString();
            _port = new SerialPort(cp, 115200, Parity.None, 8, StopBits.One);

            // Assign callback
            OnFrameReceived = frame =>
            {
                deviceMsgHandler(frame);
            };
            _port.DataReceived += HandleDataReceived;
            _port.Open();

        }
        private void PingDevice()
        {
            waitMsg = true;
            _port.Write(new byte[] { 0x55, 0x55, 0x00, 0x06, 0, 0 }, 0, 6);
            LogMessage("Ping device");
        }
        private void ReadAllTemp()
        {
            waitMsg = true;
            _port.Write(new byte[] { 0x55, 0x55, 0x00, 0x06, 0, 1 }, 0, 6);
            LogMessage("Read temerature");
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
            try
            {

                byte[] buffer = new byte[1024];
                int count = 0;
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
                return "Device not reponding";
            }


            return "";
        }
    }
}
