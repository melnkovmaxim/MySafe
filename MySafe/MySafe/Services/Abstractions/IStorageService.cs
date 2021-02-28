using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySafe.Services.Abstractions
{
    public interface IStorageService
    {
        void SaveFileToStorage(byte[] bytes, string filename, string extension);
    }
}
