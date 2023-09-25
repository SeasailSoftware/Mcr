using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ThreeNH.Data.Implement
{
    [DataContract]
    public class Tolerance : ITolerance
    {
        [DataMember(Name = "Items")]
        private Dictionary<string, ToleranceItem> _items = new Dictionary<string, ToleranceItem>();

        [DataMember(Name = "Factors")]
        private Dictionary<string, double> _factors;

        public Tolerance()
        {
        }

        public Tolerance(IDictionary<string, ToleranceItem> items, IDictionary<string, double> factors = null)
        {
            _items = new Dictionary<string, ToleranceItem>(items);
            if (factors != null)
                _factors = new Dictionary<string, double>(factors);
        }

        public Tolerance(Tolerance other)
        {
            foreach (var item in other._items)
            {
                _items[item.Key] = new ToleranceItem
                {
                    Lower = item.Value.Lower,
                    Upper = item.Value.Upper,
                };
            }

            if (other._factors != null)
            {
                _factors = new Dictionary<string, double>(other._factors);
            }
        }


        /// <summary>
        /// 判定方法，如果不为空则使用该方法判定是否合格，否则只有当所有数据容差项的值都在指定范围内时才合格
        /// </summary>
        public Func<Dictionary<string, ToleranceItem>, IDictionary<string, double>, bool?> JudgeFunc { get; set; }

        public double GetFactor(string factorName)
        {
            if (ContainsFactor(factorName)) return _factors[factorName];
            throw new ArgumentException($"Cannot find factor '{factorName}'", nameof(factorName));
        }

        public Dictionary<string, ToleranceItem> Items => _items;

        public Dictionary<string, double> Factors
        {
            get
            {
                lock (this)
                {
                    return _factors ?? (_factors = new Dictionary<string, double>());
                }
            }
        }

        public bool? IsPass(IDictionary<string, double> values)
        {
            if (JudgeFunc != null)
            {
                return JudgeFunc(Items, values);
            }

            foreach (var item in Items)
            {
                if (!values.ContainsKey(item.Key))
                    return null;

                if (double.IsNaN(item.Value.Lower))
                {
                    if (values[item.Key] > item.Value.Upper)
                        return false;
                }
                else if (double.IsNaN(item.Value.Upper))
                {
                    if (values[item.Key] < item.Value.Lower)
                        return false;
                }
                else
                {
                    if (values[item.Key] < item.Value.Lower || values[item.Key] > item.Value.Upper)
                        return false;
                }
            }

            return true;
        }


        public bool? IsPass(string itemName, double value)
        {
            if (!Items.ContainsKey(itemName)) return null;
            var item = Items[itemName];
            return value >= item.Lower && value <= item.Upper;
        }
        public ToleranceItem this[string name]
        {
            get => _items[name];
            set => _items[name] = value;
        }

        #region ITolerance

        public object Clone()
        {
            return new Tolerance(this);
        }

        IToleranceItem ITolerance.this[string name]
        {
            get => _items[name];
            set
            {
                if (value is ToleranceItem item)
                {
                    _items[name] = item;
                }
                throw new ArgumentException();
            }
        }

        public bool Contains(string name)
        {
            return _items.ContainsKey(name);
        }

        public bool ContainsFactor(string factorName)
        {
            return _factors?.ContainsKey(factorName) == true;
        }

        public IToleranceItem GetItem(string name, IToleranceItem defaultValue = null)
        {
            if (_items.ContainsKey(name)) return _items[name];
            return defaultValue;
        }


        IReadOnlyList<KeyValuePair<string, IToleranceItem>> ITolerance.Items =>
         _items.ToArray().Select(x => new KeyValuePair<string, IToleranceItem>(x.Key, x.Value)).ToArray();

        IReadOnlyList<KeyValuePair<string, double>> ITolerance.Factors =>
            _factors?.ToArray();

        #endregion
    }
}
