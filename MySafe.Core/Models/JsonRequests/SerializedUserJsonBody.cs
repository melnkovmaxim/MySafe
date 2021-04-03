using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Core.Models.Requests;

namespace MySafe.Core.Models.JsonRequests
{
    public class SerializedUserJsonBody : UserJsonBody, IJsonBody, ISerializedObject
    {
        protected override string _rootName => "user";
    }
}
