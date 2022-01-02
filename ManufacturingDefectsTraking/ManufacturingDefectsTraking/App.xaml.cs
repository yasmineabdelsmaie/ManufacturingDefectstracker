using ManufacturingDefectsTraking.LocalDB;
using ManufacturingDefectsTraking.ViewModels;
using ManufacturingDefectsTraking.Views;
using Prism;
using Prism.Ioc;
using System;
using System.IO;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
namespace ManufacturingDefectsTraking
{
    public partial class App
    {
        static Database database;

        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/" + nameof(VisualNoteItemsPage));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<VisualNoteItemsPage, VisualNoteItemsPageViewModel>();
            containerRegistry.RegisterForNavigation<AddNewVisualItemPage, AddNewVisualItemPageViewModel>();
            containerRegistry.RegisterForNavigation<UpdateCurrentVisualItem, UpdateCurrentVisualItemViewModel>();
        }

        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "VisualNoteItems.db3"));
                }
                return database;
            }
        }
    }
}
