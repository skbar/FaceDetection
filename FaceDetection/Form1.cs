using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FaceDetection
{
    public partial class Form1 : Form
    {
        static readonly CascadeClassifier cascadeClassifier = new CascadeClassifier("haarcascade_frontalface_alt_tree.xml");

        public Form1()
        {
            InitializeComponent();
        }

        private void btnDetect_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog()
            { Multiselect = false, Filter = "JPEG|*.jpg" })
            {
                if(ofd.ShowDialog()==DialogResult.OK)
                {
                    pic.Image = Image.FromFile(ofd.FileName);
                    Bitmap bitmap = new Bitmap(pic.Image);
                    Image<Bgr, byte> grayImage = new Image<Bgr, byte>(bitmap);
                    Rectangle[] rectangles = cascadeClassifier.DetectMultiScale(grayImage, 1.4, 0);
                    foreach(Rectangle rectangle in rectangles)
                    {
                        using(Graphics graphics = Graphics.FromImage(bitmap))
                        {
                            using(Pen pen=new Pen(Color.Red, 1))
                            {
                                graphics.DrawRectangle(pen, rectangle);
                            }
                        }
                    }
                    pic.Image = bitmap;
                }
            }
        }
    }
}
