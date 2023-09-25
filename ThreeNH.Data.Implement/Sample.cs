using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeNH.Data.Implement
{
    public class Sample : ISample
    {

        public Sample()
        {
            Id = Guid.NewGuid();
            DateTime = DateTime.Now;
        }

        public Sample(ISample source, bool deepCopy = true)
        {
            if (deepCopy)
            {
                DeepCopyFrom(source);
            }
            else
            {
                ShallowCopyFrom(source);
            }
        }

        private Dictionary<string, IColorData> _colors = new Dictionary<string, IColorData>();
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public string Material { get; set; }
        public string Remark { get; set; }
        public string ColorFormula { get; set; }
        public IDictionary<string, object> InstrumentInformation { get; private set; } = new Dictionary<string, object>();

        public string[] Channels => _colors.Keys.ToArray();


        public object Tag { get; set; }

        public object Clone()
        {
            return new Sample(this);
        }

        public bool EqualObj(object obj1, object obj2)
        {
            throw new NotImplementedException();
        }

        public IColorData GetColorData(string channel)
        {
            return _colors.ContainsKey(channel) ? _colors[channel] : null;
        }

        public void SetColorData(string channel, IColorData colorData)
        {
            if (colorData != null)
            {
                _colors[channel] = colorData;
            }
            else if (_colors.ContainsKey(channel))
            {
                _colors.Remove(channel);
            }
        }

        protected void ShallowCopyFrom(ISample source)
        {
            Name = source.Name;
            DateTime = source.DateTime;
            InstrumentInformation = source.InstrumentInformation;
            foreach (var channel in source.Channels)
            {
                _colors.Add(channel, source.GetColorData(channel));
            }
        }

        protected void DeepCopyFrom(ISample source)
        {
            Name = source.Name;
            DateTime = source.DateTime;
            InstrumentInformation = source.InstrumentInformation;
            foreach (var channel in source.Channels)
            {
                _colors.Add(channel, new ColorData(source.GetColorData(channel)));
            }
        }
    }
}
