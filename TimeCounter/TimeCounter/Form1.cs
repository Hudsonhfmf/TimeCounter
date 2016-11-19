using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeCounter
{
    public partial class Form1 : Form
    {
        private int seg = 0;
        int min = 0, hr =0;

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            seg++;
                if (seg == 60)
                {
                    min++;
                    seg = 0;
                }else if (min==60)
                {
                    hr++;
                    min = 0;

                }
            lblContador.Text = hr.ToString().PadLeft(2, '0') + ":" + min.ToString().PadLeft(2, '0') + ":" +
                               seg.ToString().PadLeft(2, '0');

        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            lblContador.Text = "00:00:00";
            seg = 0;
            min = 0;
            hr = 0;
        }
    }
}
