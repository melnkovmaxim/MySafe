using System.IO;

namespace MySafe.Core.Interfaces.Services
{
    public interface IPrintService
    {
        void PrintImage(Stream imgStream);
        bool TryPrintPdfFile(Stream pdfFileStream);
        bool TryPrint(string filePath);
    }
}