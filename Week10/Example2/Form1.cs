using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Example2
{
    enum Tool
    {
        Line,
        Rectangle, 
        Pen,
        Triangle,
        Eraser,
        Fill,
        Fill2
    }
    public partial class Form1 : Form
    {
        Bitmap bitmap = default(Bitmap);
        Graphics graphics = default(Graphics);
        Pen pen = new Pen(Color.Black);
        Point prevPoint = default(Point);
        Point currentPoint = default(Point);
        bool isMousePressed = false;
        Tool currentTool = Tool.Pen;
        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            
            pictureBox1.Image = bitmap;
            graphics.Clear(Color.White);

            button3.Select();

            numericUpDown1.Value = (decimal)pen.Width;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Line;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Rectangle;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Pen;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Triangle;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Eraser;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Fill;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Fill2;
        }

        Rectangle GetMRectangle(Point pPoint, Point cPoint)
        {
            return new Rectangle 
            { 
                X = Math.Min(pPoint.X, cPoint.X), 
                Y = Math.Min(pPoint.Y, cPoint.Y), 
                Width = Math.Abs(pPoint.X - cPoint.X), 
                Height = Math.Abs(pPoint.Y - cPoint.Y) 
            };
        }

        void DrawTriangle(Rectangle r, Graphics g)
        {
            Point A = new Point(r.X, r.Y + r.Height);
            Point B = new Point(r.X + r.Width / 2, r.Y);
            Point C = new Point(r.X + r.Width, r.Y + r.Height);
            g.DrawLine(pen, A, B);
            g.DrawLine(pen, B, C);
            g.DrawLine(pen, C, A);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = e.Location.ToString();
            
            if (isMousePressed)
            {
                switch (currentTool)
                {
                    case Tool.Line:
                    case Tool.Rectangle:
                    case Tool.Triangle:
                        currentPoint = e.Location;
                        break;
                    case Tool.Pen:
                        prevPoint = currentPoint;
                        currentPoint = e.Location;
                        graphics.DrawLine(pen, prevPoint, currentPoint);
                        break;
                    case Tool.Eraser:
                        prevPoint = currentPoint;
                        currentPoint = e.Location;
                        graphics.DrawLine(pen, prevPoint, currentPoint);
                        break;
                    case Tool.Fill:
                        break;
                    case Tool.Fill2:
                        break;
                    default:
                        break;
                }

                pictureBox1.Refresh();
            }
            
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            prevPoint = e.Location;
            isMousePressed = true;
            currentPoint = e.Location;

            if (currentTool == Tool.Fill)
            {
                bitmap = Utils.Fill(bitmap, currentPoint, bitmap.GetPixel(e.X, e.Y), pen.Color);
                graphics = Graphics.FromImage(bitmap);
                pictureBox1.Image = bitmap;
                pictureBox1.Refresh();
            } else if (currentTool == Tool.Fill2)
            {
                MapFill mf = new MapFill();
                mf.Fill(graphics, currentPoint, pen.Color, ref bitmap);
                graphics = Graphics.FromImage(bitmap);
                pictureBox1.Image = bitmap;
                pictureBox1.Refresh();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMousePressed = false;

            switch (currentTool)
            {
                case Tool.Line:
                    graphics.DrawLine(pen, prevPoint, currentPoint);
                    break;
                case Tool.Rectangle:
                    graphics.DrawRectangle(pen, GetMRectangle(prevPoint, currentPoint));
                    break;
                case Tool.Pen:
                    break;
                case Tool.Triangle:
                    DrawTriangle(GetMRectangle(prevPoint, currentPoint), graphics);
                    break;
                case Tool.Fill:
                    break;
                case Tool.Fill2:
                    break;
                default:
                    break;
            }
            prevPoint = e.Location;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            switch (currentTool)
            {
                case Tool.Line:
                    e.Graphics.DrawLine(pen, prevPoint, currentPoint);
                    break;
                case Tool.Rectangle:
                    e.Graphics.DrawRectangle(pen, GetMRectangle(prevPoint, currentPoint));
                    break;
                case Tool.Pen:
                    break;
                case Tool.Triangle:
                    DrawTriangle(GetMRectangle(prevPoint, currentPoint), e.Graphics);
                    break;
                case Tool.Fill:
                    break;
                case Tool.Fill2:
                    break;
                default:
                    break;
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                bitmap = Bitmap.FromFile(openFileDialog1.FileName) as Bitmap;
                pictureBox1.Image = bitmap;
                graphics = Graphics.FromImage(bitmap);
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the Image
            // assigned to Button2.
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs =
                    (System.IO.FileStream)saveFileDialog1.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the
                // File type selected in the dialog box.
                // NOTE that the FilterIndex property is one-based.
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        bitmap.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        bitmap.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        bitmap.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                }

                fs.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            pen.Width = (float)numericUpDown1.Value;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            //Color change
            Label l = (Label)sender;
            pen.Color = l.BackColor;
        }
    }
}
