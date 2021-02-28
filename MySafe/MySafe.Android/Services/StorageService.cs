using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Services.Abstractions;
using Xamarin.Essentials;

namespace MySafe.Droid.Services
{
    public class StorageService: IStorageService
    {
        public void SaveFileToStorage(byte[] bytes, string filename, string extension)
        {            
            var downloadsPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDownloads);

            var filePath = Path.Combine(downloadsPath, filename + extension);
            File.WriteAllBytes(filePath, bytes);
        }
    }
}
