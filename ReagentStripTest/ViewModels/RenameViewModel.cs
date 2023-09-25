using Caliburn.Micro;
using ReagentStripTest.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReagentStripTest.ViewModels
{
    internal class RenameViewModel : Screen
    {
        public RenameViewModel(string recordName)
        {
            _recordName = recordName;
        }
        private string _recordName;
        public string RecordName
        {
            get => _recordName;
            set
            {
                _recordName = value;
                NotifyOfPropertyChange(() => RecordName);
            }
        }

        public RelayCommand AcceptCommand => new RelayCommand(async x =>
        {
            await TryCloseAsync(true);
        }, y => !string.IsNullOrEmpty(RecordName));
    }
}
