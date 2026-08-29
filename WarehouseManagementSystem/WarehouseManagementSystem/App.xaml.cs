using Microsoft.Extensions.Configuration;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Windows;
using WarehouseManagementSystem.Services.ADS;
using WarehouseManagementSystem.ViewModels;
using WarehouseManagementSystem.Views;

namespace WarehouseManagementSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Properties
        public AdsClientService AdsClient { get; set; }
        public static IConfiguration Configuration { get; private set; } = null!;
        #endregion

        #region Events

        #endregion

        #region Constructors
        public App()
        {
            AdsClient = new AdsClientService();
        }

        #endregion

        #region Command-Methods

        #endregion

        #region Methods
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile(
                path: "appsettings.json",
                optional: false,
                reloadOnChange: true)
            .Build();

            AdsClient.ClientConnect("199.4.42.250.1.1",851);
            MainWindow mainWindow = new MainWindow();
            MainWindow = mainWindow;
            mainWindow.Show();
        }
        protected override void OnExit(ExitEventArgs e)
        {
            AdsClient.Dispose();

            base.OnExit(e);
        }

        #endregion

        #region Interface-Methods

        #endregion
    }

}
