using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeNH.Data.Implement.Converters
{
    public class ToleranceJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Tolerance) || objectType == typeof(ITolerance);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);
            if (objectType == typeof(Tolerance))
                return jObject.ToObject<Tolerance>();
            else if (objectType == typeof(ITolerance))
            {
                var tolerance = new Tolerance();
                var items = JsonConvert.DeserializeObject<Dictionary<string, ToleranceItem>>(jObject["Items"].ToString());
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        tolerance.Items.Add(item.Key, item.Value);
                    }
                }
                var factors = JsonConvert.DeserializeObject<Dictionary<string, double>>(jObject["Factors"].ToString());
                if (factors != null)
                {
                    foreach (var factor in factors)
                    {
                        tolerance.Factors.Add(factor.Key, factor.Value);
                    }
                }
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
