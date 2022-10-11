using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lapes_triusiai
{
    public partial class Form1 : Form
    {

        private Simuliator simuliator;
        private Bitmap bitmap;
        private int width = 200;
        private int height = 200;
        public Form1()
        {
            InitializeComponent();
            simuliator = new Simuliator(width, height);
            simuliator.StepDone += Simuliator_StepDone;
            bitmap = new Bitmap(width, height);
        }

        private void Simuliator_StepDone(Field field)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    var actor = field.GetActorAt(i, j);
                    var color = Color.White;
                    if (actor is Fox)
                    {
                        color = Color.Blue;
                    }
                    else if (actor is Rabbit)
                    {
                        color = Color.Orange;
                    }
                    bitmap.SetPixel(i, j, color);
                }
            }
            panel1.CreateGraphics().DrawImage(ResizeImage(bitmap, panel1.Width, panel1.Height),new Point(0,0));
        }
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

                using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics.PixelOffsetMode = PixelOffsetMode.None;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
                }
            }
            return destImage;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            simuliator.RunOneStep();
        }
    }
}
