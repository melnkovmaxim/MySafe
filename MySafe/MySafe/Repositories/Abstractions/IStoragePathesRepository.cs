using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySafe.Repositories.Abstractions
{
    public interface IStoragePathesRepository
    {
        string DownloadPath { get; }
    }
}
