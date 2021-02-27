using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MySafe.Models
{
    public abstract class JsonObjectBase
    {
        //  Получаем сериализованный объект вместе с корневым названием
        public virtual string SerializeWithRoot()
        {
            var rootName = this.GetType().Name.ToLower();
            var serializedObject = JsonConvert.SerializeObject(this);

            return $"{{\"{rootName}\":{serializedObject}}}";
        }
    }
}
