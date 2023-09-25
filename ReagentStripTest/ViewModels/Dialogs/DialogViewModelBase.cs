using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeNH.i18N;

namespace ReagentStripTest.ViewModels.Dialogs
{
    public class DialogViewModelBase : Screen
    {
        public static ITranslater Translater => IoC.Get<ITranslater>();
    }
}
