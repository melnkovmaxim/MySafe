using System.IO;

namespace MySafe.Domain.Services
{
    public interface IPrintService
    {
        void PrintImage(Stream imgStream);
        bool TryPrintPdfFile(Stream pdfFileStream);
        bool TryPrint(string filePath);
    }
}