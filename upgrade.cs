using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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
        public upgrade(Form1 parent)
        {
            InitializeComponent();
            this.parent = parent;
            passw = parent.devicesList[parent.appSetting.deviceNum];
        }

        private void DoUG_Click(object sender, EventArgs e)
        {
            // read fpga version
            List<uint> ver = new List<uint>();
            string err = getVer(out ver);
            if (err != "")
            {
                MessageBox.Show("Error reading ODEM version: " + err, "Upgrade", MessageBoxButtons.OK);
                return;
            }

            // Load upgrade file
            upgrade_LoadFile();

            // Check upgrade file on device - might be a problem - this check should be done on the PC side
            var result = parent.ssh.CreateCommand($"swupdate -i package.swu -v -n").Execute().Trim();

            // Send upgrade command
            result = parent.ssh.CreateCommand($"swupdate -i package.swu -v").Execute().Trim();


            // read fpga version
            err = getVer(out ver);
            if (err != "")
            {
                MessageBox.Show("Error reading ODEM version: " + err, "Upgrade", MessageBoxButtons.OK);
                return;
            }

            MessageBox.Show("ODEM upgraded. Resart ODEM", "Upgrade", MessageBoxButtons.OK);
            parent.Close();
        }
        private void upgrade_LoadFile()
        {
            using (var client = new ScpClient("192.168.2.24", "root", ""))
            {
                client.Connect();
                var stream = File.OpenRead(ugFilename);
                client.Upload(stream, "root@192.168.2.24:/tmp/");
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
            }

        }
    }
}