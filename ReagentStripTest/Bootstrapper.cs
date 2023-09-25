using Caliburn.Micro;
using ControlzEx.Theming;
using MahApps.Metro.Controls;
using ReagentStripTest.Core;
using ReagentStripTest.i18N;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;
using ThreeNH.Communication;
using ThreeNH.Communication.CommDevice.Serial;
using ThreeNH.Database;
using ThreeNH.Extensions;
using ThreeNH.i18N;
using ThreeNH.IO;
using ThreeNH.Reflection;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;

namespace ReagentStripTest
{
    public class Bootstrapper : BootstrapperBase
    {
        private CompositionContainer _container;
        private ITranslater _translater;
        private Options _options;
        //初始化
        public Bootstrapper()
        {
            Initialize();
            Application.ShutdownMode = ShutdownMode.OnExplicitShutdown;
        }

        //重写Configure
        protected override void Configure()
        {
            var aggregateCatalog = new AggregateCatalog(AssemblySource.Instance.Select(x => new AssemblyCatalog(x))
     .OfType<ComposablePartCatalog>());

            _container = new CompositionContainer(aggregateCatalog);
            var batch = new CompositionBatch();

            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(_container);
            //初始化配置文件
            InitializeConfig();
            batch.AddExportedValue(_options);
            //batch.AddExportedValue<AppSettings>(_config);
            //batch.AddExportedValue<DeviceService>(new DeviceService());
            //batch.AddExportedValue<Seasail.Core.Control.Views.Dialog.MessageBoxView>
            //初始化语言
            InitializeCulture();
            DbProviderFactory factory =
            DbProviderFactories.GetFactory("System.Data.SQLite.EF6");
            var connection = factory.CreateConnection();
            connection.ConnectionString = "Data Source =Colors.db;BinaryGUID=False;";
            batch.AddExportedValue(new ColorDatabase(connection));
            batch.AddExportedValue(new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: Application.Current.MainWindow,
                    corner: Corner.TopRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(3),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = Application.Current.Dispatcher;
            }));
            batch.AddExportedValue(_translater);
            _container.Compose(batch);
        }


        protected override object GetInstance(Type service, string key)
        {
            string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(service) : key;
            var exports = _container.GetExportedValues<object>(contract);
            ThreeNH.Data.Check.NotNull(exports, $"Could not locate any instances of contract {contract}.");
            return exports.First();
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetExportedValues<object>(
                AttributedModelServices.GetContractName(service));
        }


        protected override async void OnStartup(object sender, StartupEventArgs e)
        {
            //加载启动动画
            LoadSplashScreen();

            //加载程序集
            LoadAssemblys();
            // 自定义视图、视图模型查找
            ViewLocator.LocateTypeForModelType = LocateTypeForModelType;

            // 初始化自定义的值替换
            InitSpecialValues();

            // 解决控件时间显示不是本地格式的问题
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            // 初始化显示主题
            InitializeTheme();

            //设置显示主界面
            Application.ShutdownMode = ShutdownMode.OnLastWindowClose;
            SerialPortService.PortsChanged += OnPortsChanged;
            SerialPortService.Start();
            await DisplayRootViewFor<IShell>();
        }


        private async void OnPortsChanged(object sender, PortsChangedArgs e)
        {
            var eventAggregator = IoC.Get<IEventAggregator>();
            await eventAggregator?.PublishOnUIThreadAsync(e);
        }

        private void LoadAssemblys()
        {
            var finder = new DirectoryAssemblyFinder(DirectoryHelper.RootPath());
            var assemblys = finder.FindAll();
            if (!assemblys.IsNullOrEmpty())
            {
                foreach (var assembly in assemblys?.Where(p => p.FullName.StartsWith("ThreeNH")))
                {
                    AssemblySource.Instance.AddIfNotExist(assembly);
                }
            }
            //var assembly = Assembly.LoadFrom("TNH.Control.dll");
            //AssemblySource.Instance.AddIfNotExist(assembly);
        }


        public static System.Data.Common.DbConnection CreateDbConnection(
    string providerName, string connectionString)
        {
            System.Data.Common.DbConnection connection = null;

            if (connectionString != null)
            {
                if (providerName == "System.Data.OleDb")
                {
                    providerName = "JetEntityFrameworkProvider";
                }
                else if (providerName == "System.Data.SQLite")
                {
                    providerName = "System.Data.SQLite.EF6";
                }

                if (providerName == "System.Data.SQLite.EF6")
                {
                    var regex = new Regex(@"Binary\s*GUID\s*=\s*True", RegexOptions.IgnoreCase);
                    var match = regex.Match(connectionString);
                    if (match.Success)
                    {
                        connectionString = regex.Replace(connectionString, "BinaryGUID=False");
                    }
                    else
                    {
                        if (!connectionString.EndsWith(";"))
                        {
                            connectionString += ";";
                        }
                        connectionString += "BinaryGUID=False";
                    }
                }

                try
                {
                    DbProviderFactory factory =
                        DbProviderFactories.GetFactory(providerName);

                    connection = factory.CreateConnection();
                    connection.ConnectionString = connectionString;
                }
                catch (Exception ex)
                {
                    if (connection != null)
                    {
                        connection = null;
                    }

                    Console.WriteLine(ex.Message);
                }
            }

            return connection;
        }

        /// <summary>
        /// 加载开机界面
        /// </summary>
        private void LoadSplashScreen()
        {
            ////在资源文件中定义了SplashScreen，不再需要手动启动开机动画
            //string splashScreenPngPath = "Resources/SplashScreen.png";
            //SplashScreen s = new SplashScreen(splashScreenPngPath);
            //s.Show(true,true);
            //s.Close(TimeSpan.FromSeconds(3));
        }
        private void InitializeTheme()
        {
            Theme theme = ThemeManager.Current.Themes.FirstOrDefault(p => p.Name == _options.Theme);
            if (theme != null && theme != ThemeManager.Current.DetectTheme())
                ThemeManager.Current.ChangeTheme(Application.Current, theme.Name);
        }

        /// <summary>
        /// 在这里添加我自已的Caliburn.Micro绑定变量
        /// </summary>
        private void InitSpecialValues()
        {
            MessageBinder.SpecialValues.Add("$clickedItem",
                c => (c.EventArgs as ItemClickEventArgs)?.ClickedItem);
            MessageBinder.SpecialValues.Add("$listViewItem",
                c => (c.Source.Parent as StackPanel).DataContext);
            MessageBinder.SpecialValues.Add("$true", c => true);
            MessageBinder.SpecialValues.Add("$false", c => false);
            MessageBinder.SpecialValues.Add("$selectedDataGridItems", c => (c.Source as DataGrid).SelectedItems);
            MessageBinder.SpecialValues.Add("$selectedDataGridItem", c => (c.Source as DataGrid).SelectedItem);
            MessageBinder.SpecialValues.Add("$isChecked", c => (c.Source as CheckBox).IsChecked);
            MessageBinder.SpecialValues.Add("$CancelEventArgs", c => (c.EventArgs as CancelEventArgs));
            MessageBinder.SpecialValues.Add("$tabItemClosingEventArgs", c => (c.EventArgs as BaseMetroTabControl.TabItemClosingEventArgs));
            MessageBinder.SpecialValues.Add("$treeViewItem",
                c => (c.Source.Parent as Grid).DataContext);
            MessageBinder.SpecialValues.Add("$treeViewSelectedItems", c => (c.Source as TreeView).SelectedItem);
            MessageBinder.SpecialValues.Add("$dataGridSelectedItems", c => (c.Source as DataGrid).SelectedItems);
        }

        // 定位视图类型，支持派生类继承父视图
        private static Type LocateTypeForModelType(Type modelType, DependencyObject displayLocation, object context)
        {
            var viewTypeName = modelType.FullName;

            if (Caliburn.Micro.View.InDesignMode)
            {
                viewTypeName = ViewLocator.ModifyModelTypeAtDesignTime(viewTypeName);
            }

            viewTypeName = viewTypeName.Substring(0, viewTypeName.IndexOf('`') < 0
                ? viewTypeName.Length
                : viewTypeName.IndexOf('`'));

            var viewTypeList = ViewLocator.TransformName(viewTypeName, context);
            var viewType = AssemblySource.FindTypeByNames(viewTypeList);
            if (viewType == null)
            {
                Trace.TraceWarning("View not found. Searched: {0}.", string.Join(", ", viewTypeList.ToArray()));

                if (modelType.BaseType != null)
                {
                    return ViewLocator.LocateTypeForModelType(modelType.BaseType, displayLocation, context);
                }
            }

            return viewType;
        }

        protected override void BuildUp(object instance)
        {
            _container.SatisfyImportsOnce(instance);
        }
        private void InitializeCulture()
        {
            CultureInfo ci = CultureInfo.CurrentCulture;
            if (!string.IsNullOrEmpty(_options.Culture))
            {
                ci = CultureInfo.GetCultureInfo(_options.Culture);
            }
            Utils.LocalUtil.SwitchCulture(ci);
            _translater = new Translater();
        }

        /// <summary>
        /// 初始化配置文件
        /// </summary>
        private void InitializeConfig()
        {
            _options = Options.Load();
            if (_options == null)
                _options = new Options();
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            SerialPortService.CleanUp();
            SerialPortService.PortsChanged -= OnPortsChanged;
            _options.Save();
            base.OnExit(sender, e);
        }

        protected override async void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {

            if (e.Exception != null && e.Exception is CommunicationException ex)
            {
                ThreeNH.Logging.LogManager.Error(ex);
                return;
            }
            ThreeNH.Logging.LogManager.Error(e.Exception);
            Application.Current.Shutdown(-1);
        }
    }
}
