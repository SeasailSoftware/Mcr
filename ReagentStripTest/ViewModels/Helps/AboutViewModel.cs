using System.Reflection;
using System.Windows.Media;

namespace ReagentStripTest.ViewModels.Helps
{
    class AboutViewModel : ViewModelBase
    {
        public AboutViewModel()
        {
            AppName = $"试剂条测试工具";
        }
        public override string DisplayName => Translater.Trans(nameof(AboutViewModel));

        public string AppName { get; set; }

        public string Version
        {
            get
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                return $"Version：{version}";
            }
        }

    }
}
