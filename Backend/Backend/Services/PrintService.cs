using System.Drawing;
using System.Drawing.Printing;

namespace Kakadu.Backend.Services
{
    public class PrintService
    {
        public void Print(string text)
        {
            PrintDocument printDocument = new PrintDocument();

            printDocument.PrintPage += (sender, e) =>
            {
                e.Graphics.DrawString(text, new System.Drawing.Font("Arial", 12), Brushes.Black, 0, 0);
            };
   
            printDocument.Print();
        }
       
        }
    }

