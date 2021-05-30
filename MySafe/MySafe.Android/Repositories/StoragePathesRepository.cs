using System.IO;
using Android.OS;
using MySafe.Core.Interfaces.Repositories;

namespace MySafe.Droid.Repositories
{
    public class StoragePathesRepository : IStoragePathesRepository
    {
        public string DownloadPath =>
            Path.Combine(Environment.ExternalStorageDirectory.AbsolutePath, Environment.DirectoryDownloads);
    }
}