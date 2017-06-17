using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using votingFrontend.DatabaseTables;
using votingFrontend.Interfaces;
using votingFrontend.Services;
using votingFrontend.Views;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace votingFrontend.ViewModels
{
    /// <summary>
    /// The ViewModel of the LoginView which contains all the logic for the view
    /// </summary>
    public class LoginViewModel : INotifyPropertyChanged
    {
        //Private variables for the properties to store information
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

        private string firstNamePlaceholder;
        private string lastNamePlaceholder;
        private string electoralIdPlaceholder;
        private string connectionText;

        private bool loggingIn;

        private ResourceLoader resource;

        private DateTime openDateTime = DateTime.Parse("17 June 2017 9:45AM");
        private DispatcherTimer countdown;

        private RestService restAPI = new RestService();
        private DatabaseService db = new DatabaseService();
        private INavigationService navigation;

        /// <summary>
        /// Default Contructor for LoginViewModel
        /// </summary>
        /// <param name="navigationService">Passes the Navigation Property from the View to the ViewModel</param>
        public LoginViewModel(INavigationService navigationService)
        {
            this.navigation = navigationService;

            resource = new ResourceLoader();
            TitleText = resource.GetString("LoginTitle");
            FirstNameText = resource.GetString("FirstName");
            LastNameText = resource.GetString("LastName");
            DoBText = resource.GetString("DateOfBirth");
            ElectoralIdText = resource.GetString("ElectoralID");
            LoginText = resource.GetString("LoginButton");
            LoginCommand = new CommandService(Login);

            FirstNamePlaceHolder = resource.GetString("FirstNamePlaceHolder");
            LastNamePlaceHolder = resource.GetString("LastNamePlaceHolder");
            ElectoralIdPlaceHolder = resource.GetString("ElectoralIdPlaceHolder");
            ConnectionText = resource.GetString("ConnectionText");

            LoggingIn = false;

            DoB = DateTime.Now;
            DoB = DoB.AddYears(-18);

            if (DateTime.Now <= openDateTime)
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
            else if (DateTime.Now >= (openDateTime.AddHours(9).AddMinutes(50)))
            {
                VotingClosed = Visibility.Visible;

                TimeTillOpenText = resource.GetString("VotingFinished");
            }
            else
            {
                VotingClosed = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Event relating to and controlling property changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// Gets and Sets the ICommand property LoginCommand
        /// </summary>
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

        /// <summary>
        /// Gets and Sets the String property TitleText
        /// </summary>
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

        /// <summary>
        /// Gets and Sets the String property FirstNameText
        /// </summary>
        public string FirstNameText
        {
            get
            {
                return this.firstNameText;
            }

            set
            {
                this.firstNameText = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and Sets the String property FirstName
        /// </summary>
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

        /// <summary>
        /// Gets and Sets the String property LastNameText
        /// </summary>
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

        /// <summary>
        /// Gets and Sets the String property LastName
        /// </summary>
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

        /// <summary>
        /// Gets and Sets the String property DoBText
        /// </summary>
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

        /// <summary>
        /// Gets and Sets the DateTime property DoB
        /// </summary>
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

        /// <summary>
        /// Gets and Sets the String property ElectoralIdText
        /// </summary>
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

        /// <summary>
        /// Gets and Sets the String property ElectoralId
        /// </summary>
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

        /// <summary>
        /// Gets and Sets the String property LoginText
        /// </summary>
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

        /// <summary>
        /// Gets and Sets the Visibility property VotingClosed
        /// </summary>
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

        /// <summary>
        /// Gets and Sets the String property TimeTillOpenText
        /// </summary>
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

        /// <summary>
        /// Gets and Sets the String property TimeTillOpen
        /// </summary>
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

        /// <summary>
        /// Gets and Sets the String property FistNamePlaceHolder
        /// </summary>
        public string FirstNamePlaceHolder
        {
            get
            {
                return this.firstNamePlaceholder;
            }

            set
            {
                this.firstNamePlaceholder = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and Sets the String property LastNamePlaceHolder
        /// </summary>
        public string LastNamePlaceHolder
        {
            get
            {
                return this.lastNamePlaceholder;
            }

            set
            {
                this.lastNamePlaceholder = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and Sets the String property ElectoralIdPlaceHolder
        /// </summary>
        public string ElectoralIdPlaceHolder
        {
            get
            {
                return this.electoralIdPlaceholder;
            }

            set
            {
                this.electoralIdPlaceholder = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and Sets the Boolean property LoggingIn
        /// </summary>
        public bool LoggingIn
        {
            get
            {
                return this.loggingIn;
            }

            set
            {
                this.loggingIn = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and Sets the String property Connection Text
        /// </summary>
        public string ConnectionText
        {
            get
            {
                return this.connectionText;
            }

            set
            {
                this.connectionText = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Validates the user entry and if the entry is valid sends the data to the Rest Service to check if user details are correct and logs in the user.
        /// When the user has been logged in and has received the user details navigates user to appropriate view 
        /// </summary>
        internal async void Login(object sender)
        {
            LoggingIn = true;

            if (String.IsNullOrEmpty(FirstName) || String.IsNullOrEmpty(LastName) || String.IsNullOrEmpty(ElectoralId))
            {
                ContentDialog emptyEntry = new ContentDialog()
                {
                    Title = "No Login Information",
                    Content = "No information has been entered. Please enter your firstname, last name, date of birth and electoral ID.",
                    PrimaryButtonText = "OK"
                };

                await emptyEntry.ShowAsync();
                LoggingIn = false;
                return;
            }

            MatchCollection firstNameResult = Regex.Matches(FirstName, @"^[a-zA-Z]+$");
            MatchCollection lastNameResult = Regex.Matches(LastName, @"^[a-zA-Z]+$");
            MatchCollection electoralIdResult = Regex.Matches(ElectoralId, @"^[A-Z]{3}[0-9]{6}$");

            if (firstNameResult.Count == 0)
            {
                ContentDialog invalidEntry = new ContentDialog()
                {
                    Title = "Invalid Entry",
                    Content = FirstName + " is not a valid name, Please enter your correct first name",
                    PrimaryButtonText = "OK"
                };

                await invalidEntry.ShowAsync();
                LoggingIn = false;
                return;
            }

            if (lastNameResult.Count == 0)
            {
                ContentDialog invalidEntry = new ContentDialog()
                {
                    Title = "Invalid Entry",
                    Content = LastName + " is not a valid name, Please enter your correct last name",
                    PrimaryButtonText = "OK"
                };

                await invalidEntry.ShowAsync();
                LoggingIn = false;
                return;
            }

            DateTime validationDate = DateTime.Now;
            validationDate = validationDate.AddYears(-18);

            if (DoB.Date > validationDate.Date)
            {
                ContentDialog invalidEntry = new ContentDialog()
                {
                    Title = "Invalid Entry",
                    Content = "Only 18 years and older are allowed to vote",
                    PrimaryButtonText = "OK"
                };

                await invalidEntry.ShowAsync();
                LoggingIn = false;
                return;
            }

            if (electoralIdResult.Count == 0)
            {
                ContentDialog invalidEntry = new ContentDialog()
                {
                    Title = "Invalid Entry",
                    Content = "Please enter a valid Electoral ID",
                    PrimaryButtonText = "OK"
                };

                await invalidEntry.ShowAsync();
                LoggingIn = false;
                return;
            }

            if (!(await UpdateVoteData()))
            {
                ContentDialog noData = new ContentDialog()
                {
                    Title = "No Data",
                    Content = "Unable to get Voting Data, Please Try Again Later",
                    PrimaryButtonText = "OK"
                };

                await noData.ShowAsync();
                LoggingIn = false;
                return;
            }

            UserVoteTable user = db.CheckVoter(FirstName, LastName, DoB, ElectoralId);

            if (user != null)
            {
                if (user.VoteSaved)
                {
                    this.navigation.Navigate(typeof(VoteSubmittedView), user);
                    return;
                }

                if (!user.Active)
                {
                    db.DeactivateUsers();
                    user = db.SwitchActive(user);
                }

                if (user.ElectorateId == default(int))
                {
                    this.navigation.Navigate(typeof(ElectorateView));
                }
                else if (user.CandidateIds == null)
                {
                    this.navigation.Navigate(typeof(CandidateView));
                }
                else if (user.PartyId == default(int))
                {
                    this.navigation.Navigate(typeof(PartyView));
                }
                else
                {
                    this.navigation.Navigate(typeof(ReferendumView));
                }

                return;
            }

            UserVoteTable loggedIn = await restAPI.Login(FirstName, LastName, DoB, ElectoralId);

            if (loggedIn != null)
            {
                loggedIn.Active = true;

                db.VoterLoggedIn(loggedIn);

                LoggingIn = false;

                if (loggedIn.VoteSaved)
                {
                    this.navigation.Navigate(typeof(VoteSubmittedView), loggedIn);
                    return;
                }

                if (!loggedIn.Active)
                {
                    db.DeactivateUsers();
                    loggedIn = db.SwitchActive(loggedIn);
                }
            }
            else
            {
                ContentDialog invalidLogin = new ContentDialog()
                {
                    Title = "Incorrect Login",
                    Content = "Incorrect Login Details",
                    PrimaryButtonText = "OK"
                };

                await invalidLogin.ShowAsync();
                LoggingIn = false;
            }
        }

        /// <summary>
        /// Updates the Voting Data in the local database from the server database
        /// </summary>
        /// <returns>A bool weather the local database was updated</returns>
        private async Task<bool> UpdateVoteData()
        {
            List<ElectorateTable> electorates = new List<ElectorateTable>();
            List<CandidateTable> candidates = new List<CandidateTable>();
            List<PartyTable> parties = new List<PartyTable>();
            ReferendumTable referendum = new ReferendumTable();

            electorates = await restAPI.GetElectorates();
            candidates = await restAPI.GetCandidates();
            parties = await restAPI.GetParties();
            referendum = await restAPI.GetReferendum();

            if (electorates != null || candidates != null || parties != null || referendum != null)
            {
                await db.UpdateElectorates(electorates);
                await db.UpdateCandidates(candidates);
                await db.UpdateParties(parties);
                await db.UpdateReferendum(referendum);

                return true;
            }
            else
            {
                if (db.CheckData())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Logic behind each tick of the countdown timer
        /// </summary>
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

        /// <summary>
        /// Handles what happens whenever the property data changes
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed</param>
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
