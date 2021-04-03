using System.IO;
using Android.OS;

namespace MySafe.Droid.Services
{
    public class StorageService
    {
        public void SaveFileToStorage(byte[] bytes, string filename, string extension)
        {
            var downloadsPath = Path.Combine(Environment.ExternalStorageDirectory.AbsolutePath,
                Environment.DirectoryDownloads);

            var filePath = Path.Combine(downloadsPath, filename + extension);
            File.WriteAllBytes(filePath, bytes);
        }
    }
}