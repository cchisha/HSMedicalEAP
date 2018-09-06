using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HealthSourceMedicalApp.ViewModels;
using System.Diagnostics;
using HealthSourceMedicalApp.LoadingIndicator;
using System.Text;
using System.Threading.Tasks;
namespace HealthSourceMedicalApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StartupPage : ContentPage
	{

        StartupViewModel viewModel;
        public StartupPage()
		{
            if (Application.Current.Properties.ContainsKey("is_registered"))
            {
                Boolean registered = (Boolean)Application.Current.Properties["is_registered"]; //Application.Current.Properties["is_registered"] as String;
                if (registered)
                {
                    GotoHomeScreen();
                }
                    
            }
            if (Application.Current.Properties.ContainsKey("user"))
            {
                String user = (String)Application.Current.Properties["user"];
                Boolean registered = (Boolean)Application.Current.Properties["is_registered"];
                Console.WriteLine("Saved User:........................" + user);
                Console.WriteLine("Registered:........................" + registered.ToString());
            }
            InitializeComponent ();
            BindingContext = viewModel = new StartupViewModel();

            viewModel.DisplayRegisterFailedPrompt += () => DisplayAlert("Error", "Failed To Register, Try Again", "OK");
            viewModel.NavigateToHomeScreen += () => GotoHomeScreen();

            // show the loading page...
            //DependencyService.Get<ILodingPageService>().InitLoadingPage(new LoadingIndicatorPage());
            //DependencyService.Get<ILodingPageService>().ShowLoadingPage();


            //viewModel.ConsentStatus = false;
            //                        <Picker ItemsSource="{Binding SomeCollection}" SelectedItem="{Binding AnotherItem}" />
            //GenderPicker.Items.Add("Male");
            //GenderPicker.Items.Add("Female");
            //Email.Completed += (object sender, EventArgs e) =>
            //{
            //Password.Focus();
            //};

            //Password.Completed += (object sender, EventArgs e) =>
            //{
            //    viewModel.SubmitCommand.Execute(null);
            //};


        }

        private void ConsentStatus_Toggled(object sender, EventArgs e)
        {
            // Check to see if there is anywhere to go back to
            //Browser.Source = viewModel.WebAppURL;
            Debug.WriteLine("ConsentStatus: " + viewModel.ConsentStatus);


            // navigate to next page...
            GotoHomeScreen();
        }
        async void GotoHomeScreen()
        {
            //DependencyService.Get<ILodingPageService>().ShowLoadingPage();
            // just to showcase a delay...
            //await Task.Delay(2000);
            await Navigation.PushModalAsync(new NavigationPage(new BrowsePage()));
        }
       /*public async Task RegisterUserAsync(string email, string password, string confirmPassword)
        {
            var model = new RegisterBindingModel
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };
            var json = JsonConvert.SerializeObject(model);
            HttpContent httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var client = new HttpClient();
            var response = await client.PostAsync("http://localhost:49563/api/Account/Register", httpContent);
        }*/
    }
}