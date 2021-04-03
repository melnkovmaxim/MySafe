using Newtonsoft.Json;

namespace MySafe.Core.Models
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public abstract class JsonObjectBase
    {
        //  Получаем сериализованный объект вместе с корневым названием
        public virtual string SerializeWithRoot()
        {
            var serializedObject = JsonConvert.SerializeObject(this);

            return $"{{\"{_rootName}\":{serializedObject}}}";
        }

        protected virtual string _rootName => GetType().Name.ToLower();
    }
}