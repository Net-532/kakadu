using System.Printing;
using System.Drawing.Printing;
using Brushes = System.Drawing.Brushes;


namespace Kakadu.Backend.Services
{
    public class PrintService
    {
        public void Print(string text)
        {
            PrintDocument printDocument = new PrintDocument();

            printDocument.PrintPage += (sender, e) =>
            {
                e.Graphics.DrawString(text, new System.Drawing.Font("Arial", 28), Brushes.Black, 0, 0);
            };

       
            PrintQueue printQueue = LocalPrintServer.GetDefaultPrintQueue();


            printDocument.PrinterSettings.PrinterName = printQueue.Name;

   
            printDocument.Print();
        }
       
        }
    }

