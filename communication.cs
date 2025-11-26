using System.Net.Sockets;
using System.Text;

namespace OdemControl
{
    partial class Form1
    {
        private string _ipAddress = "192.168.2.24";
        private int _port = 24871;
        NetworkStream stream;
        TcpClient client;

        public event Action<string> OnMessageReceived;

        private async void connect_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                isConnected = false;
                client.Close();
                connect.Text = "Connect";
                connectState.Text = "DisConnected";
                connectState.ForeColor = Color.Red;
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
                        SetKeepAlive(client.Client, 5000, 1000); // 5s idle, 1s interval
                        _ = Task.Run(ReceiveListener);
                    }
                }


                // Simulate a connection attempt
                if (isConnected)
                {
                    devices.Enabled = false;
                    connect.Text = "Disconnect";
                    connectState.Text = "Connected";
                    connectState.ForeColor = Color.Lime;
                    mainBox.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Failed to connect to the device.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    connect.Text = "Connect";
                    connectState.Text = "Disconnected";
                    connectState.ForeColor = Color.Red;
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
        private bool SendAndReceive(string command)
        {
            using var client = new TcpClient(_ipAddress, 24871);
            NetworkStream stream = client.GetStream();
            stream.ReadTimeout = 3000; // 3 seconds

            try
            {
                byte[] buffer = new byte[256];
                int count = stream.Read(buffer, 0, buffer.Length);  // times out
            }
            catch (IOException)
            {
                MessageBox.Show("No response from device.", "Communication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
    }
}
