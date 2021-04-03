using Newtonsoft.Json;

namespace MySafe.Core.Entities
{
    public abstract class JsonObjectBase
    {
        //  Получаем сериализованный объект вместе с корневым названием
        public virtual string SerializeWithRoot()
        {
            var rootName = GetType().Name.ToLower();
            var serializedObject = JsonConvert.SerializeObject(this);

            return $"{{\"{rootName}\":{serializedObject}}}";
        }
    }
}