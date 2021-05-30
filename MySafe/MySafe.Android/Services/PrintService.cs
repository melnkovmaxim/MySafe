using System;
using System.IO;
using Android.Content;
using Android.Graphics;
using Android.Print;
using Android.Support.V4.Print;
using MySafe.Core.Interfaces.Services;
using Plugin.CurrentActivity;

namespace MySafe.Droid.Services
{
    public class PrintService : IPrintService
    {
        public bool TryPrint(string filePath)
        {
            var printManager =
                (PrintManager) CrossCurrentActivity.Current.Activity.GetSystemService(Context.PrintService);
            var result = printManager.Print($"Printing {filePath}", new CustomPrintDocumentAdapter(filePath), null);

            return result.IsCompleted;
        }

        public void PrintImage(Stream imgStream)
        {
            var imageName = "Test image name";
            var photoPrinter = new PrintHelper(CrossCurrentActivity.Current.Activity)
                {ScaleMode = PrintHelper.ScaleModeFit};
            var bitmap = BitmapFactory.DecodeStream(imgStream);

            photoPrinter.PrintBitmap(imageName, bitmap);
        }

        public bool TryPrintPdfFile(Stream pdfFileStream)
        {
            throw new NotImplementedException();
        }
    }
}