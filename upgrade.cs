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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace OdemControl
{
    public partial class upgrade : Form
    {
        Form1 parent;
        string passw;
        string ugFilename;
        StreamWriter logFile;
        public upgrade(Form1 parent)
        {
            InitializeComponent();
            this.parent = parent;
            passw = parent.devicesList[parent.appSetting.deviceNum];
            string path = @"C:\Lidwave";
            string logFileName = path + "\\upgrade.log";
            logFile = new StreamWriter(logFileName, false);
            logFile.WriteLine("Ugrade start " + DateTime.Now.ToString("yyyyMMdd_HHmmss"));
        }

        private void DoUG_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.Enabled = false;
            // Load upgrade file
            logFile.WriteLine("Load package to device - " + ugFilename + "to /tmp/package.swu");
            upgrade_LoadFile();

            // Check upgrade file on device - might be a problem - this check should be done on the PC side
            logFile.WriteLine("Send ssh command: swupdate -i package.swu -v -n");
            string err = "";
            var result = parent.ssh.CreateCommand($"swupdate -i package.swu -v -n").Execute().Trim();
            if (result.Contains("ERROR"))
                err += " ERROR";
            if (result.Contains(" handler "))
                err += " handler";
            if (result.Contains("parse"))
                err += " parse";

            logFile.WriteLine("Package test result:");
            logFile.WriteLine(result);

            if (err != "")
            {
                logFile.WriteLine("Package test failed - " + err);
                if (MessageBox.Show("Upgrade package might be corrupted.\nPlease consult lidwave support\nIf approved click Yes to contiue", "Upgrade warning",                   // Window title
                MessageBoxButtons.YesNo,MessageBoxIcon.Warning) != DialogResult.Yes)
                {
                    this.Cursor = Cursors.Default;
                    this.Enabled = true;
                    return;
                }
            }

            // Send upgrade command
            waitRes.Visible = true;
            logFile.WriteLine("Send ssh command: swupdate -i package.swu -v");
            result = parent.ssh.CreateCommand($"swupdate -i package.swu -v").Execute().Trim();
            Thread.Sleep(20000); // wait for command to start
            logFile.WriteLine("Upgrade result:");
            logFile.WriteLine(result);
            logFile.Close();

            this.Cursor = Cursors.Default;
            this.Enabled = true;

            MessageBox.Show("ODEM upgraded:\n- Resart ODEM\n- Wait for 1 minute and try to connect\n- Read version", "Upgrade", MessageBoxButtons.OK);
            parent.Close();
        }
        private void upgrade_LoadFile()
        {
            using (var client = new ScpClient("192.168.2.24", "root", ""))
            {
                client.Connect();
                var stream = File.OpenRead(ugFilename);
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
                    logFile.WriteLine("ODEM version: 0x" + ver[0].ToString("X02") +
                        ver[1].ToString("X02") + ver[2].ToString("X02") + ver[3].ToString("X02"));

                    selFile.Visible = true;
                    DoUG.Visible = true;
                    ugFln.Visible = true;
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