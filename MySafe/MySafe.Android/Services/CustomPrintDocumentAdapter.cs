using System;
using Android.OS;
using Android.Print;
using Java.IO;
using Debug = System.Diagnostics.Debug;
using FileNotFoundException = System.IO.FileNotFoundException;

namespace MySafe.Droid.Services
{
    internal class CustomPrintDocumentAdapter : PrintDocumentAdapter
    {
        private readonly string filePath;

        public CustomPrintDocumentAdapter(string filePath)
        {
            this.filePath = filePath;
        }

        public override void OnLayout(PrintAttributes oldAttributes, PrintAttributes newAttributes,
            CancellationSignal cancellationSignal, LayoutResultCallback callback, Bundle extras)
        {
            if (cancellationSignal.IsCanceled)
            {
                callback.OnLayoutCancelled();
                return;
            }

            callback.OnLayoutFinished(new PrintDocumentInfo.Builder(filePath)
                .SetContentType(PrintContentType.Document)
                .Build(), true);
        }

        public override void OnWrite(PageRange[] pages, ParcelFileDescriptor destination,
            CancellationSignal cancellationSignal, WriteResultCallback callback)
        {
            try
            {
                using (InputStream input = new FileInputStream(filePath))
                {
                    using (OutputStream output = new FileOutputStream(destination.FileDescriptor))
                    {
                        var buf = new byte[1024];
                        int bytesRead;
                        while ((bytesRead = input.Read(buf)) > 0) output.Write(buf, 0, bytesRead);
                    }
                }

                callback.OnWriteFinished(new[] {PageRange.AllPages});
            }
            catch (FileNotFoundException fileNotFoundException)
            {
                Debug.WriteLine(fileNotFoundException);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }
    }
}