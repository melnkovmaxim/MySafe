using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySafe.Business.Services.Abstractions
{
    public interface IPrintService
    {
        void PrintImage(Stream imgStream);
        bool TryPrintPdfFile(Stream pdfFileStream);
        void ShowPrinterWebView();
    }
}
