using MySafe.Core.Models.Requests;

namespace MySafe.Core.Models.JsonRequests
{
    public class SerializedUserJsonBody : UserJsonBody, IJsonBody, ISerializedObject
    {
        protected override string _rootName => "user";
    }
}