using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeNH.Data.Implement.Converters
{
    public class ToleranceItemJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ToleranceItem) || objectType == typeof(IToleranceItem);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);
            if (objectType == typeof(ToleranceItem))
                return jObject.ToObject<ToleranceItem>();
            else if (objectType == typeof(IToleranceItem))
            {
                var tolerance = new ToleranceItem();
                var items = JsonConvert.DeserializeObject<ToleranceItem>(jObject["Items"].ToString());

                return tolerance;
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
