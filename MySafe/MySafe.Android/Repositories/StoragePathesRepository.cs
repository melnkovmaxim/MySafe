using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Repositories.Abstractions;

namespace MySafe.Droid.Repositories
{
    public class StoragePathesRepository: IStoragePathesRepository
    {
        public string DownloadPath => 
            Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDownloads);
    }
}
