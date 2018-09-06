using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using HealthSourceMedicalApp.Models;
using HealthSourceMedicalApp.Views;
using System.Diagnostics;
using HealthSourceMedicalApp.ViewModels;

namespace HealthSourceMedicalApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BrowsePage : ContentPage
	{
        BrowseViewModel viewModel;

        public BrowsePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);  // Hide nav bar
            BindingContext = viewModel = new BrowseViewModel();

            Browser.Source = viewModel.WebAppURL;
            //Browser.
        }
        void WebOnNavigating(object sender, WebNavigatingEventArgs e)
        {
            //LoadingLabel.IsVisible = true;
            viewModel.IsBusy = true;
            Debug.WriteLine("WebOnNavigating: ...................................  ");
        }

        void WebOnEndNavigating(object sender, WebNavigatedEventArgs e)
        {
            //LoadingLabel.IsVisible = false;
            viewModel.IsBusy = false; ;
            Debug.WriteLine("WebOnEndNavigating: ...................................  ");
        }

        /*async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }*/
        private void RefreshClicked(object sender, EventArgs e)
        {
            // Check to see if there is anywhere to go back to
                 Browser.Source = viewModel.WebAppURL;
        }
        private void BackClicked(object sender, EventArgs e)
        {
            // Check to see if there is anywhere to go back to
            if (Browser.CanGoBack)
            {
                Browser.GoBack();
            }
            else
            { // If not, leave the view
                Navigation.PopAsync();
            }
        }

        private void ForwardClicked(object sender, EventArgs e)
        {
            if (Browser.CanGoForward)
            {
                Browser.GoForward();
            }
        }
        /*async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }*/
    }
}