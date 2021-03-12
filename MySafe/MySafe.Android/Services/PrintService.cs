using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;
using Android.Print;
using Android.Print.Pdf;
using Android.Support.V4.Print;
using Android.Webkit;
using MySafe.Business.Services.Abstractions;
using Plugin.CurrentActivity;

namespace MySafe.Droid.Services
{
    public class PrintService: IPrintService
    {
        public void ShowPrinterWebView()
        {
            var printManager = (PrintManager) CrossCurrentActivity.Current.Activity.GetSystemService(Context.PrintService);
            printManager.Print("Razor HTML Hybrid", new WebView(CrossCurrentActivity.Current.Activity).CreatePrintDocumentAdapter(), null);
        }

        public void PrintImage(Stream imgStream)
        {
            var imageName = "Test image name";
            var photoPrinter = new PrintHelper(CrossCurrentActivity.Current.Activity) { ScaleMode = PrintHelper.ScaleModeFit};
            var bitmap = BitmapFactory.DecodeStream(imgStream);

            photoPrinter.PrintBitmap(imageName, bitmap);
        }

        public bool TryPrintPdfFile(Stream pdfFileStream)
        {
            throw new NotImplementedException();
        }
    }
}
