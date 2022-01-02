using ManufacturingDefectsTraking.Enums;
using ManufacturingDefectsTraking.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ManufacturingDefectsTraking.ViewModels
{
    public class AddNewVisualItemPageViewModel : BindableBase
    {
        INavigationService NavigationService;
        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                SetProperty(ref title, value);
            }
        }
        private string description;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                SetProperty(ref description, value);
            }
        }
        private DateTime date = DateTime.Now;
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                SetProperty(ref date, value);
            }
        }
        private ImageSource  picture;
        public ImageSource Picture
        {
            get
            {
                return picture;
            }
            set
            {
                SetProperty(ref picture, value);
            }
        }
        private string imagePath;
        public string ImagePath
        {
            get
            {
                return imagePath;
            }
            set
            {
                SetProperty(ref imagePath, value);
            }
        }
        private bool isCameraIconVisible;
        public bool IsCameraIconVisible
        {
            get { return isCameraIconVisible; }
            set { SetProperty(ref isCameraIconVisible, value); }
        }
        public DelegateCommand AddNewVisualItemCommand { get; set; }
        public DelegateCommand TakeImageByCameraCommand { get; set; }
        public AddNewVisualItemPageViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            AddNewVisualItemCommand = new DelegateCommand(async () =>await AddNewVisualItemCommandExecute());
            TakeImageByCameraCommand = new DelegateCommand(async () => await TakeImageByCameraCommandExecute());
            IsCameraIconVisible = true;
        }

        private async Task TakeImageByCameraCommandExecute()
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });         
            if (photo != null)
            {
                ImagePath = photo.Path;
                Picture = ImageSource.FromStream(() => { return photo.GetStream(); });
                IsCameraIconVisible = false;
            }
        }
     
        private async Task AddNewVisualItemCommandExecute()
        {
            
            VisualNoteItem VisualItem = new VisualNoteItem()
            { Title = Title, Date = Date, Description = Description, Status = (int)StatusEnum.Open, Picture = imagePath };
            await App.Database.SaveItemAsync(VisualItem);
            await App.Current.MainPage.DisplayAlert("Added", "The item has been added successfully", "Ok");
            await NavigationService.GoBackAsync();
        }
    }
}
