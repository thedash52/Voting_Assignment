using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Input;
using votingFrontend.Services;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace votingFrontend.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string titleText;
        private string firstNameText;
        private string firstName;
        private string lastNameText;
        private string lastName;
        private string dobText;
        private DateTime dob;
        private string electoralIdText;
        private string electoralId;
        private string loginText;
        private Visibility votingClosed;
        private ICommand loginCommand;

        private string timeTillOpenText;
        private double timeTillOpen;

        private ResourceLoader resource;

        private DateTime openDateTime = DateTime.Parse("22 May 2017 3:49PM");
        private DispatcherTimer countdown;

        public LoginViewModel()
        {
            resource = new ResourceLoader();
            TitleText = resource.GetString("LoginTitle");
            FirstNameText = resource.GetString("FirstName");
            LastNameText = resource.GetString("LastName");
            DoBText = resource.GetString("DateOfBirth");
            ElectoralIdText = resource.GetString("ElectoralID");
            LoginText = resource.GetString("LoginButton");
            LoginCommand = new CommandService(Login);

            DoB = DateTime.Now;

            if (DateTime.Now >= openDateTime)
            {
                VotingClosed = Visibility.Collapsed;
            }
            else
            {
                VotingClosed = Visibility.Visible;

                countdown = new DispatcherTimer()
                {
                    Interval = new TimeSpan(0, 0, 1)
                };
                countdown.Tick += Countdown_Tick;

                TimeTillOpenText = resource.GetString("TimeTillOpen");
                TimeTillOpen = (openDateTime - DateTime.Now).TotalSeconds;

                countdown.Start();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public ICommand LoginCommand
        {
            get
            {
                return this.loginCommand;
            }

            set
            {
                this.loginCommand = value;
                OnPropertyChanged();
            }
        }

        public string TitleText
        {
            get
            {
                return this.titleText;
            }

            set
            {
                this.titleText = value;
                OnPropertyChanged();
            }
        }

        public string FirstNameText
        {
            get
            {
                return this.firstNameText;
            }

            set
            {
                this.firstNameText = value;
            }
        }

        public string FirstName
        {
            get
            {
                return this.firstName;
            }

            set
            {
                this.firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastNameText
        {
            get
            {
                return this.lastNameText;
            }

            set
            {
                this.lastNameText = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get
            {
                return this.lastName;
            }

            set
            {
                this.lastName = value;
                OnPropertyChanged();
            }
        }

        public string DoBText
        {
            get
            {
                return this.dobText;
            }

            set
            {
                this.dobText = value;
                OnPropertyChanged();
            }
        }

        public DateTime DoB
        {
            get
            {
                return this.dob;
            }

            set
            {
                this.dob = value;
                OnPropertyChanged();
            }
        }

        public string ElectoralIdText
        {
            get
            {
                return this.electoralIdText;
            }

            set
            {
                this.electoralIdText = value;
                OnPropertyChanged();
            }
        }

        public string ElectoralId
        {
            get
            {
                return this.electoralId;
            }

            set
            {
                this.electoralId = value;
                OnPropertyChanged();
            }
        }

        public string LoginText
        {
            get
            {
                return this.loginText;
            }

            set
            {
                this.loginText = value;
                OnPropertyChanged();
            }
        }

        public Visibility VotingClosed
        {
            get
            {
                return this.votingClosed;
            }

            set
            {
                this.votingClosed = value;
                OnPropertyChanged();
            }
        }

        public string TimeTillOpenText
        {
            get
            {
                return this.timeTillOpenText;
            }

            set
            {
                this.timeTillOpenText = value;
                OnPropertyChanged();
            }
        }

        public double TimeTillOpen
        {
            get
            {
                return this.timeTillOpen;
            }

            set
            {
                this.timeTillOpen = value;
                OnPropertyChanged();
            }
        }

        internal async void Login(object sender)
        {
            if (FirstName == null || LastName == null || ElectoralId == null)
            {
                ContentDialog emptyEntry = new ContentDialog()
                {
                    Title = "No Login Information",
                    Content = "No information has been entered. Please enter your firstname, last name, date of birth and electoral ID.",
                    PrimaryButtonText = "OK"
                };

                await emptyEntry.ShowAsync();
                return;
            }

            MatchCollection firstNameResult = Regex.Matches(FirstName, @"^[a-zA-Z]+$");
            MatchCollection lastNameResult = Regex.Matches(LastName, @"^[a-zA-Z]+$");
            MatchCollection electoralIdResult = Regex.Matches(ElectoralId, @"^[A-Z]{3}[0-9]+$");

            if (electoralIdResult.Count == 0)
            {
                ContentDialog invalidEntry = new ContentDialog()
                {
                    Title = "Invalid Entry",
                    Content = "Please enter a valid Electoral ID",
                    PrimaryButtonText = "OK"
                };

                await invalidEntry.ShowAsync();
                return;
            }
        }

        private void Countdown_Tick(object sender, object e)
        {
            TimeTillOpen--;

            if (TimeTillOpen < 0)
            {
                VotingClosed = Visibility.Collapsed;
                TimeTillOpen = 0;
                countdown.Stop();
            }
        }

        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
