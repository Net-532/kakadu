using System.Drawing;
using System.Drawing.Printing;

namespace Kakadu.Backend.Services
{
    public class PrintService
    {
        public void Print(string text)
        {
            string imagePath = "burger.png";
            PrintDocument printDocument = new PrintDocument();
            printDocument.OriginAtMargins = true;
            printDocument.DefaultPageSettings.Margins = new Margins(35, 35, 50, 35);

            printDocument.PrintPage += (sender, e) =>
            {   
                Image img = Image.FromFile(imagePath);
                float centerX = (e.PageBounds.Width - img.Width) - 420;
                e.Graphics.DrawImage(img, centerX, 0);
                float textYPosition = img.Height + 20;
                e.Graphics.DrawString(text, new Font(FontFamily.GenericMonospace, 27), Brushes.Black, new PointF(0, textYPosition));
            };

            printDocument.Print();
        }
    }
    }

