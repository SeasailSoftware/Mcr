using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeNH.Color.Model;

namespace ThreeNH.Data.Implement.Converters
{
    public class ColorDataJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ColorData) || objectType == typeof(IColorData);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);
            if (objectType == typeof(ColorData))
                return jObject.ToObject<ColorData>();
            else if (objectType == typeof(IColorData))
            {
                var colorData = new ColorData();
                colorData.Lab = JsonConvert.DeserializeObject<CIELab>(jObject["Lab"].ToString());
                colorData.Xyz = JsonConvert.DeserializeObject<CIEXYZ>(jObject["Xyz"].ToString());
                colorData.PseudoColor = JsonConvert.DeserializeObject<sRGB>(jObject["PseudoColor"].ToString());
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new SpectralDataJsonConverter());
                colorData.SpectralData = JsonConvert.DeserializeObject<SpectralData>(jObject["SpectralData"].ToString(), settings);
                return colorData;
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
