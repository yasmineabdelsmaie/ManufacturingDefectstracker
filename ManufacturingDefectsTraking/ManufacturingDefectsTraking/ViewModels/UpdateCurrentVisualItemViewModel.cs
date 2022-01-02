using ManufacturingDefectsTraking.Enums;
using ManufacturingDefectsTraking.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ManufacturingDefectsTraking.ViewModels
{
    public class UpdateCurrentVisualItemViewModel : BindableBase, INavigationAware
    {
        INavigationService NavigationService;
        private List<StatusEnum> itemStatusList;
        public List<StatusEnum> ItemStatusList
        {
            get
            {
                return itemStatusList;
            }
            set
            {
                SetProperty(ref itemStatusList, value);
            }
        }
        
        private VisualNoteItem itemToBeUpdated;
        public VisualNoteItem ItemToBeUpdated
        {
            get
            {
                return itemToBeUpdated;
            }
            set
            {
                SetProperty(ref itemToBeUpdated, value);
            }
        }
        private bool isCameraIconVisible;
        public bool IsCameraIconVisible
        {
            get
            {
                return isCameraIconVisible;
            }
            set
            {
                SetProperty(ref isCameraIconVisible, value);
            }
        }
        private ImageSource picture;
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
       
        public DelegateCommand UpdateItemCommand { get; set; }
        public DelegateCommand TakeImageByCameraCommand { get; set; }
        public DelegateCommand DeleteImageCommand { get; set; }
        public UpdateCurrentVisualItemViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            ItemStatusList = new List<StatusEnum>() { StatusEnum.Open, StatusEnum.Closed };
            UpdateItemCommand = new DelegateCommand(async () => await UpdateItemCommandExecute());
            TakeImageByCameraCommand = new DelegateCommand(async () => await TakeImageByCameraCommandExecute());
            DeleteImageCommand = new DelegateCommand (DeleteImageCommandExecute);
        }

        private void DeleteImageCommandExecute()
        {
            itemToBeUpdated.Picture = null;
            Picture = null;
            IsCameraIconVisible = true; 
        }

        private async Task TakeImageByCameraCommandExecute()
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });
            if (photo != null)
            {
                ItemToBeUpdated.Picture = photo.Path;
                Picture = ImageSource.FromStream(() => { return photo.GetStream(); });
                IsCameraIconVisible = false; 
            }
        }

        private async Task UpdateItemCommandExecute()
        { 
            await App.Database.UpdateItemAsync(ItemToBeUpdated);
            await App.Current.MainPage.DisplayAlert("Updated", "The item has been updated successfully", "Ok");
            await NavigationService.GoBackAsync();
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
          
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters != null && parameters.ContainsKey("ObjToBeUpdated"))
            {
                ItemToBeUpdated = parameters["ObjToBeUpdated"] as VisualNoteItem;
                IsCameraIconVisible = string.IsNullOrEmpty(ItemToBeUpdated.Picture) ? true : false;
                Picture = ItemToBeUpdated.ItemImageSource;

            }


        }
    }
}
