using System;
using System.Windows.Input;
using System.ComponentModel;
using Xamarin.Forms;
using System.Diagnostics;
using HealthSourceMedicalApp.Common;
using Newtonsoft.Json.Linq;
using Com.OneSignal;
using Com.OneSignal.Abstractions;

namespace HealthSourceMedicalApp.ViewModels
{
    public class StartupViewModel : BaseViewModel
    {


        public Action RegisterUser;
        public Action DisplayRegisterFailedPrompt;
        public Action NavigateToHomeScreen;
        public ICommand RegisterCommand { protected set; get; }
        public ICommand GetStartedCommand { protected set; get; }
        public StartupViewModel()
        {
            Title = "Registration";
            RegisterCommand = new Command(OnRegister);
            GetStartedCommand = new Command(OnGetStarted);
        }


        private string firstname;
        public string FirstName
        {
            get { return firstname; }
            set
            {
                firstname = value;
            }
        }
        private string lastname;
        public string LastName
        {
            get { return lastname; }
            set
            {
                lastname = value;
            }
        }
        private string idnumber;
        public string IDNumber
        {
            get { return idnumber; }
            set
            {
                idnumber = value;
            }
        }
        private string phonenumber;
        public string PhoneNumber
        {
            get { return phonenumber; }
            set
            {
                phonenumber = value;
            }
        }
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
            }
        }

        private string employer;
        public string Employer
        {
            get { return employer; }
            set
            {
                employer = value;
            }
        }
        private string maxdob = DateTime.Now.ToString("MMMM dd, yyyy");
        public string MaxDOB
        {
            get { return maxdob; }
            set
            {
                maxdob = value;
             }
        }
        private string dob = DateTime.Now.ToString("MMMM dd, yyyy");
        public string DOB
        {
            get { return dob; }
            set
            {
                dob = DateTime.Parse(value).ToString("dd-MM-yyyy");
            }
        }
        private string gender = "Female";
        public string Gender
        {
            get { return gender; }
            set
            {
                gender = value;
            }
        }

        bool consentStatus;
        public bool ConsentStatus
        {
            get
            {
                return consentStatus;
            }
            set
            {
                consentStatus = value;
            }
        }

        public String registrationStatus;
        public String RegistrationStatus
        {
            get { return registrationStatus; }
            set
            {
                registrationStatus = value;
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
        private bool _showForm = true;
        public bool ShowForm
        {
            get { return _showForm; }
            set
            {
                _showForm = value;
                OnPropertyChanged();
            }
        }
        private bool _showRegistrationProgress = false;
        public bool ShowRegistrationProgress
        {
            get { return _showRegistrationProgress; }
            set
            {
                _showRegistrationProgress = value;
                OnPropertyChanged();
            }
        }
        private bool showGetStarted = false;
        public bool ShowGetStarted
        {
            get { return showGetStarted; }
            set
            {
                showGetStarted = value;
                OnPropertyChanged();
            }
        }



        /*void SomeMethod()
        {
            OneSignal.Current.IdsAvailable(IdsAvailable);
        }

        private void IdsAvailable(string userID, string pushToken)
        {
            //print("UserID:" + userID);
            //print("pushToken:" + pushToken);
        }*/
        public async void OnRegister()
        {
            ShowForm = false;
            IsBusy = true;
            ShowRegistrationProgress = true;

            RestService service = new RestService();
            JObject user = new JObject(
                 new JProperty("firstname", firstname),
                 new JProperty("lastname", lastname),
                 new JProperty("idnumber", idnumber),
                 new JProperty("phonenumber", phonenumber),
                 new JProperty("email", email),
                 new JProperty("employer", employer),
                 new JProperty("dob", dob),
                 new JProperty("gender", gender),
                 new JProperty("action", "create")
            );
            if (Application.Current.Properties.ContainsKey("playerID"))
            {
                user.Add("player_id", (String)Application.Current.Properties["playerID"]);
                user.Add("push_token", (String)Application.Current.Properties["pushToken"]);
            }


            Application.Current.Properties["is_registered"] = false;
            Application.Current.Properties["user"] = user.ToString();
            await Application.Current.SavePropertiesAsync();
            try
            {
                var result = await service.RegisterUser(user);
                JObject json = JObject.Parse(result);
                if ((int)json.GetValue("success") == 1)
                {
                    Application.Current.Properties["is_registered"] = true;
                    Application.Current.Properties["user"] = user.ToString(); ;
                    await Application.Current.SavePropertiesAsync();

                    //GET STARTED
                    IsBusy = false;
                    ShowGetStarted = true;

                }
                else {
                    DisplayRegisterFailedPrompt();
                    ShowForm = true;
                    IsBusy = false;
                    ShowRegistrationProgress = false;
                }
                Console.WriteLine("API Response:........................" + result);
            }
            catch (Exception e) {
                System.Diagnostics.Debug.WriteLine(e);
                DisplayRegisterFailedPrompt();
                ShowForm = true;
                IsBusy = false;
                ShowRegistrationProgress = false;
            }

            /*if (App.Current.Properties.Exists("ScrambledEggsRecipe"))
                App.Current.Properties["ScrambledEggsRecipe"] = fullRecipe;
            else
                App.Current.Properties.Add("ScrambledEggsRecipe", fullRecipe);
            var recipeJson = JsonConvert.SerializeObject(recipe);


            string fullRecipe;

            if (App.Current.Properties.Exists("ScrambledEggsRecipe"))
            {
                fullRecipe = App.Current.Properties["ScrambledEggsRecipe"] as string;
            }*/
            //registrationStatus = isSuccess ? "Registered Successfully" : "Retry Later";

            //if (email != "macoratti@yahoo.com" || password != "secret")
            // {
            //DisplayInvalidLoginPrompt();
            //NavigateToHomeScreen();
            // }
        }
        public void OnGetStarted() {
            NavigateToHomeScreen();
        }
    }

}