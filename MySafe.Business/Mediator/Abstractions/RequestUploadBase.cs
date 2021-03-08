using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySafe.Business.Mediator.Abstractions
{
    public abstract class RequestUploadBase<T>: BearerRequestBase<T>
    {
        public abstract string FileName { get; }
        public abstract string ContentType { get; }
        public abstract byte[] FileBytes { get; }

        protected RequestUploadBase(string jwtToken) : base(jwtToken)
        {
        }
    }
}
