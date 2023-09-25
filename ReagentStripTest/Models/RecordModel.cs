using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeNH.Color.Enums;

namespace ReagentStripTest.Models
{
    public class RecordModel : Caliburn.Micro.PropertyChangedBase
    {

        private int _index;
        public int Index
        {
            get => _index;
            set
            {
                _index = value;
                NotifyOfPropertyChange(() => Index);
            }
        }
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        private DateTime _dateTime;
        public DateTime DateTime
        {
            get => _dateTime;
            set
            {
                _dateTime = value;
                NotifyOfPropertyChange(() => DateTime);
            }
        }


        public ObservableCollection<SampleModel> Samples { get; set; } = new ObservableCollection<SampleModel>();
    }
}
