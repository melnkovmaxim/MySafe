using System.IO;

namespace MySafe.Droid.Services
{
    public class StorageService
    {
        public void SaveFileToStorage(byte[] bytes, string filename, string extension)
        {            
            var downloadsPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDownloads);

            var filePath = Path.Combine(downloadsPath, filename + extension);
            File.WriteAllBytes(filePath, bytes);
        }
    }
}
