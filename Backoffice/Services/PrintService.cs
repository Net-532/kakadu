using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Printing;
using System.Drawing.Printing;
using Brushes = System.Drawing.Brushes;

namespace Backoffice.Services
{
    internal class PrintService
    {
        public void Print(string text)
        {
          
            string result = text;

            
            PrintDocument printDocument = new PrintDocument();

        
            printDocument.PrintPage += (sender, e) =>
            {
           
                e.Graphics.DrawString(result, new System.Drawing.Font("Arial", 28), Brushes.Black, 0, 0);
            };

       
            PrintQueue printQueue = LocalPrintServer.GetDefaultPrintQueue();


            printDocument.PrinterSettings.PrinterName = printQueue.Name;

   
            printDocument.Print();
        }
       
        }
    }

