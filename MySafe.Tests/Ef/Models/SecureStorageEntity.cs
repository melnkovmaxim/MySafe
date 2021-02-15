using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySafe.Tests.Ef.Models
{
    public class SecureStorageEntity
    {
        [Key]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
