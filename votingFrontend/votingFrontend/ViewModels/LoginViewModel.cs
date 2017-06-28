// <copyright file="LoginViewModel.cs" company="UCOL 3rd Year Bachelor of Information and Communication Assignment">
// Copyright (c) UCOL 3rd Year Bachelor of Information and Communication Assignment. All rights reserved.
// </copyright>

namespace VotingFrontend.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using VotingFrontend.DatabaseTables;
    using VotingFrontend.Interfaces;
    using VotingFrontend.Services;
    using VotingFrontend.Views;
    using Windows.ApplicationModel.Resources;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// The ViewModel of the LoginView which contains all the logic for the view
    /// </summary>
    public class LoginViewModel : INotifyPropertyChanged
    {
        // Private variables for the properties to store information
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

        private DateTime openDateTime = DateTime.Parse("28 June 2017 1:45PM");
        private DispatcherTimer countdown;

        private RestService restAPI = new RestService();
        private DatabaseService db = new DatabaseService();
        private INavigationService navigation;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">Passes the Navigation Property from the View to the ViewModel</param>
        public LoginViewModel(INavigationService navigationService)
        {
            this.navigation = navigationService;

            this.resource = new ResourceLoader();
            this.TitleText = this.resource.GetString("LoginTitle");
            this.FirstNameText = this.resource.GetString("FirstName");
            this.LastNameText = this.resource.GetString("LastName");
            this.DoBText = this.resource.GetString("DateOfBirth");
            this.ElectoralIdText = this.resource.GetString("ElectoralID");
            this.LoginText = this.resource.GetString("LoginButton");
            this.LoginCommand = new CommandService(this.Login);

            this.FirstNamePlaceHolder = this.resource.GetString("FirstNamePlaceHolder");
            this.LastNamePlaceHolder = this.resource.GetString("LastNamePlaceHolder");
            this.ElectoralIdPlaceHolder = this.resource.GetString("ElectoralIdPlaceHolder");
            this.ConnectionText = this.resource.GetString("ConnectionText");

            this.LoggingIn = false;

            this.DoB = DateTime.Now;
            this.DoB = this.DoB.AddYears(-18);

            if (DateTime.Now <= this.openDateTime)
            {
                this.VotingClosed = Visibility.Visible;

                this.countdown = new DispatcherTimer()
                {
                    Interval = new TimeSpan(0, 0, 1)
                };
                this.countdown.Tick += this.Countdown_Tick;

                this.TimeTillOpenText = this.resource.GetString("TimeTillOpen");
                this.TimeTillOpen = (this.openDateTime - DateTime.Now).TotalSeconds;

                this.countdown.Start();
            }
            else if (DateTime.Now >= this.openDateTime.AddHours(9).AddMinutes(50))
            {
                this.VotingClosed = Visibility.Visible;

                this.TimeTillOpenText = this.resource.GetString("VotingFinished");
            }
            else
            {
                this.VotingClosed = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Event relating to and controlling property changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the ICommand property LoginCommand
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the String property TitleText
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the String property FirstNameText
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the String property FirstName
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the String property LastNameText
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the String property LastName
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the String property DoBText
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the DateTime property DoB
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the String property ElectoralIdText
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the String property ElectoralId
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the String property LoginText
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the Visibility property VotingClosed
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the String property TimeTillOpenText
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the String property TimeTillOpen
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the String property FistNamePlaceHolder
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the String property LastNamePlaceHolder
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the String property ElectoralIdPlaceHolder
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Boolean property LoggingIn
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the String property Connection Text
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Validates the user entry and if the entry is valid sends the data to the Rest Service to check if user details are correct and logs in the user.
        /// When the user has been logged in and has received the user details navigates user to appropriate view
        /// </summary>
        /// <param name="sender">Empty Object</param>
        internal async void Login(object sender)
        {
            this.LoggingIn = true;

            if (string.IsNullOrEmpty(this.FirstName) || string.IsNullOrEmpty(this.LastName) || string.IsNullOrEmpty(this.ElectoralId))
            {
                ContentDialog emptyEntry = new ContentDialog()
                {
                    Title = "No Login Information",
                    Content = "No information has been entered. Please enter your firstname, last name, date of birth and electoral ID.",
                    PrimaryButtonText = "OK"
                };

                await emptyEntry.ShowAsync();
                this.LoggingIn = false;
                return;
            }

            MatchCollection firstNameResult = Regex.Matches(this.FirstName, @"^[a-zA-Z]+$");
            MatchCollection lastNameResult = Regex.Matches(this.LastName, @"^[a-zA-Z]+$");
            MatchCollection electoralIdResult = Regex.Matches(this.ElectoralId, @"^[A-Z]{3}[0-9]{6}$");

            if (firstNameResult.Count == 0)
            {
                ContentDialog invalidEntry = new ContentDialog()
                {
                    Title = "Invalid Entry",
                    Content = this.FirstName + " is not a valid name, Please enter your correct first name",
                    PrimaryButtonText = "OK"
                };

                await invalidEntry.ShowAsync();
                this.LoggingIn = false;
                return;
            }

            if (lastNameResult.Count == 0)
            {
                ContentDialog invalidEntry = new ContentDialog()
                {
                    Title = "Invalid Entry",
                    Content = this.LastName + " is not a valid name, Please enter your correct last name",
                    PrimaryButtonText = "OK"
                };

                await invalidEntry.ShowAsync();
                this.LoggingIn = false;
                return;
            }

            DateTime validationDate = DateTime.Now;
            validationDate = validationDate.AddYears(-18);

            if (this.DoB.Date > validationDate.Date)
            {
                ContentDialog invalidEntry = new ContentDialog()
                {
                    Title = "Invalid Entry",
                    Content = "Only 18 years and older are allowed to vote",
                    PrimaryButtonText = "OK"
                };

                await invalidEntry.ShowAsync();
                this.LoggingIn = false;
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
                this.LoggingIn = false;
                return;
            }

            if (!(await this.UpdateVoteData()))
            {
                ContentDialog noData = new ContentDialog()
                {
                    Title = "No Data",
                    Content = "Unable to get Voting Data, Please Try Again Later",
                    PrimaryButtonText = "OK"
                };

                await noData.ShowAsync();
                this.LoggingIn = false;
                return;
            }

            UserVoteTable user = this.db.CheckVoter(this.FirstName, this.LastName, this.DoB, this.ElectoralId);

            if (user != null)
            {
                if (user.VoteSaved)
                {
                    this.navigation.Navigate(typeof(VoteSubmittedView), user);
                    return;
                }

                if (!user.Active)
                {
                    this.db.DeactivateUsers();
                    user = this.db.SwitchActive(user);
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

            UserVoteTable loggedIn = await this.restAPI.Login(this.FirstName, this.LastName, this.DoB, this.ElectoralId);

            if (loggedIn != null)
            {
                loggedIn.Active = true;

                this.db.VoterLoggedIn(loggedIn);

                this.LoggingIn = false;

                if (loggedIn.VoteSaved)
                {
                    this.navigation.Navigate(typeof(VoteSubmittedView), loggedIn);
                    return;
                }

                if (!loggedIn.Active)
                {
                    this.db.DeactivateUsers();
                    loggedIn = this.db.SwitchActive(loggedIn);
                }

                this.navigation.Navigate(typeof(ElectorateView));
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
                this.LoggingIn = false;
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

            electorates = await this.restAPI.GetElectorates();
            candidates = await this.restAPI.GetCandidates();
            parties = await this.restAPI.GetParties();
            referendum = await this.restAPI.GetReferendum();

            if (electorates != null || candidates != null || parties != null || referendum != null)
            {
                await this.db.UpdateElectorates(electorates);
                await this.db.UpdateCandidates(candidates);
                await this.db.UpdateParties(parties);
                await this.db.UpdateReferendum(referendum);

                return true;
            }
            else
            {
                if (this.db.CheckData())
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
        /// <param name="sender">Empty Object</param>
        /// <param name="e">Object is not used</param>
        private void Countdown_Tick(object sender, object e)
        {
            this.TimeTillOpen--;

            if (this.TimeTillOpen < 0)
            {
                this.VotingClosed = Visibility.Collapsed;
                this.TimeTillOpen = 0;
                this.countdown.Stop();
            }
        }

        /// <summary>
        /// Handles what happens whenever the property data changes
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed</param>
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
