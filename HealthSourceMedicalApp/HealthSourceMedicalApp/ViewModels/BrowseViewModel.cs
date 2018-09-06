using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace HealthSourceMedicalApp.ViewModels
{
    public class BrowseViewModel : BaseViewModel
    {

        public String webappurl = "http://eapmobile.healthsource.co.za/";
        public BrowseViewModel()
        {
            Title = "Employee Assistance Program";
        }
        public string WebAppURL
        {
            get { return webappurl; }
            set
            {
                webappurl = value;
            }
        }
        private bool _isBusy = true;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }
    }
}