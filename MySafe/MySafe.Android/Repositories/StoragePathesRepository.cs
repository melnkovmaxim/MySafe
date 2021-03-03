using MySafe.Presentation.Repositories.Abstractions;
using System.IO;

namespace MySafe.Droid.Repositories
{
    public class StoragePathesRepository: IStoragePathesRepository
    {
        public string DownloadPath => 
            Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDownloads);
    }
}
