using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySafe.Core.Dto
{
    public class FileResultDto
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public Task<Stream> FileStream { get; set; }
    }
}
