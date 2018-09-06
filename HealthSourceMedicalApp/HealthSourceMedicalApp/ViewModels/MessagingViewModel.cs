using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace HealthSourceMedicalApp.ViewModels
{
    public class MessagingViewModel : BaseViewModel
    {
        public MessagingViewModel()
        {
            Title = "Messages";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));
        }

        public ICommand OpenWebCommand { get; }
    }
}