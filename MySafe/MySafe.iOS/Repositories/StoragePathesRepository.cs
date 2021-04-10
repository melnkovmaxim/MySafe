using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Domain.Repositories;

namespace MySafe.iOS.Repositories
{
    public class StoragePathesRepository: IStoragePathesRepository
    {
        public string DownloadPath => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    }
}
