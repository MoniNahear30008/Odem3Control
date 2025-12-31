using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace OdemControl
{
    public partial class upgrade : Form
    {
        Form1 parent;
        string passw;
        string ugFilename;
        StreamWriter logFile;
        uint oVer = 0;
        public upgrade(Form1 parent)
        {
            InitializeComponent();
            this.parent = parent;
            passw = parent.deviceID[parent.appSetting.deviceNum];
        }

        private void DoUG_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            string path = @"C:\Lidwave";
            string logFileName = path + "\\upgrade.log";
            logFile = new StreamWriter(logFileName, false);
            logFile.WriteLine("Ugrade start " + DateTime.Now.ToString("yyyyMMdd_HHmmss"));
            logFile.WriteLine("Current ODEM version: 0x" + oVer.ToString("X08"));

            this.Cursor = Cursors.WaitCursor;
            this.Enabled = false;
            // Load upgrade file
            logFile.WriteLine("Load package to device - " + ugFilename + "to /tmp/package.swu");
            upgrade_LoadFile();

            // Check upgrade file on device - might be a problem - this check should be done on the PC side
            logFile.WriteLine("Send ssh command: swupdate -i /tmp/package.swu -v -n");
            var result = parent.ssh.CreateCommand($"swupdate -i /tmp/package.swu -v -n").Execute().Trim();
            Thread.Sleep(1000); // wait for command to start
            progressBar1.Value = 1;
            this.Refresh();
            bool pass = result.Contains("SWUpdate was successful");

            logFile.WriteLine("Package test result:");
            logFile.WriteLine(result);

            if (!pass)
            {
                logFile.WriteLine("Package test failed");
                MessageBox.Show("Upgrade package might be corrupted.\n\nPlease consult Lidwave support", "Upgrade error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Cursor = Cursors.Default;
                this.Enabled = true;
                logFile.Close();
                return;
            }

            // Send upgrade command
            logFile.WriteLine("Send ssh command: swupdate -i /tmp/package.swu -v");
            result = parent.ssh.CreateCommand($"swupdate -i /tmp/package.swu -v").Execute().Trim();
            for (int i = 0; i < 4; i++)
            {
                Thread.Sleep(1000); // wait for command to start
                progressBar1.Value = i + 2;
                this.Refresh();
            }
            logFile.WriteLine("Upgrade result:");
            logFile.WriteLine(result);
            logFile.Close();

            this.Cursor = Cursors.Default;
            this.Enabled = true;

            MessageBox.Show("ODEM upgraded:\n- Resart ODEM\n- Wait for 5 minute and try to connect\n- Read version", "Upgrade", MessageBoxButtons.OK);
            this.Close();
        }
        private void upgrade_LoadFile()
        {
            using (var client = new ScpClient("192.168.2.24", "root", ""))
            {
                client.Connect();
                var stream = System.IO.File.OpenRead(ugFilename);
                client.Upload(stream, "/tmp/package.swu");
                client.Disconnect();
            }
        }
        private string getVer(out List<uint> ver)
        {
            ver = new List<uint>();
            string err = parent.ReadReg(0xFF200018, 1, out ver);
            return err;
        }
        private void pw_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (pw.Text == passw)
                {
                    pwBox.Visible = false;
                    List<uint> ver = new List<uint>();
                    string err = getVer(out ver);
                    if (err != "")
                    {
                        logFile.WriteLine("Fail to read ODEM version: " + err);
                        MessageBox.Show("Error reading ODEM version: " + err, "Upgrade", MessageBoxButtons.OK);
                        return;
                    }
                    oVer = (ver[0] << 24) | (ver[1] << 16) | (ver[2] << 8) | ver[3];

                    selFile.Visible = true;
                    DoUG.Visible = true;
                    ugFln.Visible = true;
                    progressBar1.Visible = true;
                }
                else
                {
                    pw.Text = "";
                    MessageBox.Show("Incorrect Password");
                }
            }
        }
        private void selFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Upgrade Files (*.swu)|*.swu";
            ofd.Title = "Select a Upgrade File";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ugFilename = ofd.FileName;
                ugFln.Text = ugFilename;
                DoUG.Enabled = true;
            }

        }
    }
}