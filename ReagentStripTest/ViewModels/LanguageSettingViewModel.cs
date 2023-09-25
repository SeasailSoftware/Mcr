﻿using Caliburn.Micro;
using ReagentStripTest.Core;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ReagentStripTest.ViewModels
{
    class LanguageSettingViewModel : ViewModelBase
    {

        private Options _options;
        public LanguageSettingViewModel()
        {
            DisplayName = Translater.Trans("s_Language");
            Cultures = new Dictionary<CultureInfo, string>();
            foreach (var culture in Utils.LocalUtil.Languages)
            {
                Cultures.Add(culture, GetName(culture));
            }
            string GetName(CultureInfo culture)
            {
                if (culture.Name == "zh-CN")
                    return "中文(简体)";
                else if (culture.Name == "zh-TW")
                    return "中文(繁体)";
                return culture.NativeName;
                //return culture.NativeName.Replace("中国","简体").Replace("台灣", "繁体");
            }

        }



        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            _options = IoC.Get<Options>();
            Culture = Cultures.FirstOrDefault(x => x.Key.Name == _options.Culture);
            return base.OnActivateAsync(cancellationToken);
        }


        public Dictionary<CultureInfo, string> Cultures { get; set; }

        private KeyValuePair<CultureInfo, string> _culture;
        public KeyValuePair<CultureInfo, string> Culture
        {
            get => _culture;
            set
            {
                _culture = value;
                NotifyOfPropertyChange(() => Culture);
            }
        }

        public RelayCommand AcceptCommand => new RelayCommand(async x =>
        {
            _options.Culture = Culture.Key.Name;
            Utils.LocalUtil.SwitchCulture(Culture.Key);
            await TryCloseAsync(true);
        });
    }

}
