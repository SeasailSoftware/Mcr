using ReagentStripTest.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ReagentStripTest.ViewModels.Instruments
{
    internal class AddInstrumentViewModel : ViewModelBase
    {

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
                NotifyOfPropertyChange(nameof(AcceptCommand));
            }
        }

        private string _sn;
        public string SN
        {
            get => _sn;
            set
            {
                _sn = value;
                NotifyOfPropertyChange(() => SN);
                NotifyOfPropertyChange(() => AcceptCommand);
            }
        }

        public ICommand AcceptCommand => new RelayCommand(async x =>
        {
            await TryCloseAsync(true);
        }, y => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(SN));
    }
}
