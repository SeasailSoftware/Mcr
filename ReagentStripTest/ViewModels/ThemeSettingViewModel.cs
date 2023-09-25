using Caliburn.Micro;
using ControlzEx.Theming;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ReagentStripTest.ViewModels
{
    public class ThemeChangedMessage
    {
        public SolidColorBrush Brush { get; set; }

        public ThemeChangedMessage(SolidColorBrush brush)
        {
            Brush = brush;
        }
    }
    class ThemeSettingViewModel : ViewModelBase
    {

        private Options _options;
        public ThemeSettingViewModel()
        {
            DisplayName = Translater.Trans("s_Theme");
            Themes = ThemeManager.Current.Themes.Where(x => x.Name.Contains("Light")).ToList();
            _options = IoC.Get<Options>();
            CurrentTheme = Themes.FirstOrDefault(x => x.Name == _options.Theme);
            Icon = Utils.MahAppsPackIconHelper.CreateImageSource(MahApps.Metro.IconPacks.PackIconMaterialKind.TshirtCrew, Brushes.LightGray);
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            Icon = Utils.MahAppsPackIconHelper.CreateImageSource(MahApps.Metro.IconPacks.PackIconMaterialKind.TshirtCrew, Brushes.LightGray);
            return base.OnActivateAsync(cancellationToken);
        }


        public List<Theme> Themes { get; set; }

        private Theme _currentTheme;
        public Theme CurrentTheme
        {
            get => _currentTheme;
            set
            {
                _currentTheme = value;
                _options.Theme = value.Name;
                ThemeManager.Current.ChangeTheme(Application.Current, value);
                NotifyOfPropertyChange(() => CurrentTheme);
            }
        }
    }
}
