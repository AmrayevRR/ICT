using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Example1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void BtnPressedEvent(object sender, EventArgs args)
        {
            MessageBox.Show("Hello");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hello2");
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show(e.Location.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MyMessageBox box = new MyMessageBox();
            
            switch (box.ShowDialog())
            {
                case DialogResult.OK:
                    label1.Text = "OK";
                    break;
                case DialogResult.Cancel:
                    label1.Text = "Cancel";
                    break;
                default:
                    break;
            }
        } 
    }
}
