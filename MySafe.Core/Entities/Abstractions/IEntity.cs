using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySafe.Core.Entities.Abstractions
{
    public interface IEntity
    {
        string Error { get; set; }
        public bool HasError { get; }
        byte[] FileBytes { get; set; }
    }
}
