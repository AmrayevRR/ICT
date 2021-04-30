﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Week10
{
    public partial class Form1 : Form
    {
        Graphics graphics = default(Graphics);
        Rectangle r = default(Rectangle);
        public Form1()
        {
            InitializeComponent();
            graphics = Graphics.FromHwnd(this.Handle);
            r = new Rectangle(10, 10, 200, 200);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Red), r);

            e.Graphics.DrawRectangle(new Pen(Color.Blue), 40, 200, 300, 300);
            e.Graphics.Clear(Color.Red);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            graphics.DrawEllipse(new Pen(Color.Red), new Rectangle(10, 10, 200, 200));
            graphics.Clear(Color.Blue);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            r.Width = int.Parse(numericUpDown1.Value.ToString());
            r.Height = int.Parse(numericUpDown1.Value.ToString());
            Refresh();
        }
    }
}
