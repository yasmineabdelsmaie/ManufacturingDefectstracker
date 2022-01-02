using ManufacturingDefectsTraking.Models;
using ManufacturingDefectsTraking.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManufacturingDefectsTraking.ViewModels
{
    public class VisualNoteItemsPageViewModel : BindableBase, INavigationAware
    {
        INavigationService NavigationService;
        private List<VisualNoteItem> itemsList;
        public List<VisualNoteItem> ItemsList
        {
            get
            {
                return itemsList;
            }
            set
            {
                SetProperty(ref itemsList, value);
            }
        }
        public DelegateCommand NavigatetoAddNewVisualItemPageCommand { get; set; }
        public DelegateCommand GetAllItemsCommand { get;  set; }
        public DelegateCommand<object> UpdateItemCommand { get; set; }
        public DelegateCommand<object> DeleteItemCommand { get; set; }
        
        public VisualNoteItemsPageViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            NavigatetoAddNewVisualItemPageCommand = new DelegateCommand(async ()=> await NavigatetoAddNewVisualItemPageCommandExecute());
            GetAllItemsCommand = new DelegateCommand(async () => await GetAllItemsCommandExecute());
            UpdateItemCommand = new DelegateCommand<object>(async (obj)=> await UpdateItemCommandExecute(obj));
            DeleteItemCommand = new DelegateCommand<object>(async (obj) => await DeleteItemCommandExecute(obj));           
        }

        private async Task DeleteItemCommandExecute(object obj)
        {
            VisualNoteItem VisualItem = obj as VisualNoteItem;
            await App.Database.DeleteItemAsync(VisualItem);
            ItemsList = await App.Database.GetItemsAsync();
        }

        private async Task UpdateItemCommandExecute(object obj)
        {
            NavigationParameters Parameters = new NavigationParameters() { { "ObjToBeUpdated", obj } };        
            await NavigationService.NavigateAsync(nameof(UpdateCurrentVisualItem), Parameters);
        }

        private async Task GetAllItemsCommandExecute()
        {
            ItemsList = await App.Database.GetItemsAsync();
        }
        private async Task NavigatetoAddNewVisualItemPageCommandExecute()
        {
            await NavigationService.NavigateAsync(nameof(AddNewVisualItemPage));
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
                GetAllItemsCommand.Execute();   
        }
    }
}
