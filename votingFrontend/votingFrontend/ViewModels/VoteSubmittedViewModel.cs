using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using VotingFrontend.DatabaseTables;
using VotingFrontend.Interfaces;
using VotingFrontend.Services;
using VotingFrontend.Views;
using Windows.ApplicationModel.Resources;

namespace VotingFrontend.ViewModels
{
    /// <summary>
    /// The ViewModel of the VoteSubmittedView which contains all the logic for the view
    /// </summary>
    public class VoteSubmittedViewModel : INotifyPropertyChanged
    {
        //Private variables for the properties to store information
        private string title;
        private string voteText;
        private string electorateText;
        private string candidateText;
        private string partyText;
        private string referendumText;
        private string submittedText;
        private string loginButton;

        private string electorate;
        private string candidates;
        private string party;
        private string referendum;

        private string connectionText;

        private ICommand loginCommand;
        private INavigationService navigation;

        private UserVoteTable user;

        private ResourceLoader resource;
        private RestService restAPI = new RestService();
        private DatabaseService db = new DatabaseService();

        /// <summary>
        /// Default Contructor for LoginViewModel
        /// </summary>
        /// <param name="navigationService">Passes the Navigation Property from the View to the ViewModel</param>
        public VoteSubmittedViewModel(INavigationService navigationService, UserVoteTable user)
        {
            this.navigation = navigationService;
            this.user = user;

            this.resource = new ResourceLoader();
            Title = resource.GetString("SubmittedTitle");
            VoteText = resource.GetString("VoteText");
            ElectorateText = resource.GetString("ElectorateText");
            CandidateText = resource.GetString("CandidateText");
            PartyText = resource.GetString("PartyText");
            ReferendumText = resource.GetString("ReferendumText");
            SubmittedText = resource.GetString("SubmittedText");
            LoginButton = resource.GetString("VoteButton");
            ConnectionText = resource.GetString("ConnectionText");

            LoginCommand = new CommandService(Login);

            ElectorateTable electorateData = new ElectorateTable();
            List<CandidateTable> candidateData = new List<CandidateTable>();
            PartyTable partyData = new PartyTable();

            List<string> candidateList = JsonConvert.DeserializeObject<List<string>>(this.user.CandidateIds);

            electorateData = db.GetElectorateFromId(this.user.ElectorateId);

            foreach (string item in candidateList)
            {
                int id = int.Parse(item);

                CandidateTable candidate = new CandidateTable();
                candidate = db.GetCandidateFromId(id);

                candidateData.Add(candidate);
            }

            partyData = db.GetPartyFromId(this.user.PartyId);

            Electorate = electorateData.Name;

            string candidateNames = String.Empty;

            foreach (CandidateTable item in candidateData)
            {
                if (candidateNames == String.Empty)
                {
                    candidateNames = item.Name;
                }
                else
                {
                    candidateNames = String.Join(Environment.NewLine, candidateNames, item.Name);
                }
            }

            Candidates = candidateNames;
            Party = partyData.Name;
            Referendum = this.user.Referendum.ToString();
        }

        /// <summary>
        /// Event relating to and controlling property changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets and Sets the String property Title
        /// </summary>
        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                this.title = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and Sets the String property VoteText
        /// </summary>
        public string VoteText
        {
            get
            {
                return this.voteText;
            }

            set
            {
                this.voteText = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and Sets the String property ElectorateText
        /// </summary>
        public string ElectorateText
        {
            get
            {
                return this.electorateText;
            }

            set
            {
                this.electorateText = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and Sets the String property Electorate
        /// </summary>
        public string Electorate
        {
            get
            {
                return this.electorate;
            }

            set
            {
                this.electorate = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and Sets the String property CandidateText
        /// </summary>
        public string CandidateText
        {
            get
            {
                return this.candidateText;
            }

            set
            {
                this.candidateText = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and Sets the String property Candidates
        /// </summary>
        public string Candidates
        {
            get
            {
                return this.candidates;
            }

            set
            {
                this.candidates = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and Sets the String property PartyText
        /// </summary>
        public string PartyText
        {
            get
            {
                return this.partyText;
            }

            set
            {
                this.partyText = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and Sets the String property Party
        /// </summary>
        public string Party
        {
            get
            {
                return this.party;
            }

            set
            {
                this.party = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and Sets the String property ReferendumText
        /// </summary>
        public string ReferendumText
        {
            get
            {
                return this.referendumText;
            }

            set
            {
                this.referendumText = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and Sets the String property Referendum
        /// </summary>
        public string Referendum
        {
            get
            {
                return this.referendum;
            }

            set
            {
                this.referendum = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and Sets the String property SubmittedText
        /// </summary>
        public string SubmittedText
        {
            get
            {
                return this.submittedText;
            }

            set
            {
                this.submittedText = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and Sets the String property LoginButton
        /// </summary>
        public string LoginButton
        {
            get
            {
                return this.loginButton;
            }

            set
            {
                this.loginButton = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and Sets the String property ConnectionText
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
        /// Navigates the user back to the LoginView
        /// </summary>
        internal void Login(object obj)
        {
            this.navigation.Navigate(typeof(LoginView));
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
