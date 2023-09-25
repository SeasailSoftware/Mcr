using Caliburn.Micro;
using ReagentStripTest.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ThreeNH.i18N;
using ThreeNH.Instrument;
using ToastNotifications;

namespace ReagentStripTest.ViewModels
{
    internal class ViewModelBase : Screen
    {
        public ITranslater Translater => IoC.Get<ITranslater>();
        public IWindowManager WindowManager => IoC.Get<IWindowManager>();

        public IEventAggregator EventAggregator => IoC.Get<IEventAggregator>();



        private ImageSource _icon;
        public ImageSource Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                NotifyOfPropertyChange(() => Icon);
            }
        }

        public Notifier Notifier => IoC.Get<Notifier>();
    }
}
