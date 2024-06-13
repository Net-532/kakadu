using System.Drawing;
using System.Drawing.Printing;

namespace Kakadu.Backend.Services
{
    public class PrintService
    {
        public void Print(string text)
        {
            string imagePath = "./data/burger_logo.png";
            PrintDocument printDocument = new PrintDocument();
            printDocument.OriginAtMargins = true;
            printDocument.DefaultPageSettings.Margins = new Margins(35, 35, 50, 35);

            printDocument.PrintPage += (sender, e) =>
            {   
                Image img = Image.FromFile(imagePath);
                Bitmap bitmap = new Bitmap(img, new Size(100, 100));
                float centerX = (e.PageBounds.Width - bitmap.Width) - 420;
                e.Graphics.DrawImage(bitmap, centerX, 0);
                float textYPosition = bitmap.Height + 20;
                e.Graphics.DrawString(text, new Font(FontFamily.GenericMonospace, 27), Brushes.Black, new PointF(0, textYPosition));
            };

            printDocument.Print();
        }
    }
    }

