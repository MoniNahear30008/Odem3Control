using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OdemControl
{
    public partial class Debug : Form
    {
        Form1 mainfrm;
        public Debug(Form1 mainfrm)
        {
            InitializeComponent();
            this.mainfrm = mainfrm;
        }
    }
}
