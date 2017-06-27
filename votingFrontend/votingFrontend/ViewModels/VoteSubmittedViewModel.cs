// <copyright file="VoteSubmittedViewModel.cs" company="UCOL 3rd Year Bachelor of Information and Communication Assignment">
// Copyright (c) UCOL 3rd Year Bachelor of Information and Communication Assignment. All rights reserved.
// </copyright>

namespace VotingFrontend.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;
    using Newtonsoft.Json;
    using VotingFrontend.DatabaseTables;
    using VotingFrontend.Interfaces;
    using VotingFrontend.Services;
    using VotingFrontend.Views;
    using Windows.ApplicationModel.Resources;

    /// <summary>
    /// The ViewModel of the VoteSubmittedView which contains all the logic for the view
    /// </summary>
    public class VoteSubmittedViewModel : INotifyPropertyChanged
    {
        // Private variables for the properties to store information
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
        /// Initializes a new instance of the <see cref="VoteSubmittedViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">Passes the Navigation Property from the View to the ViewModel</param>
        /// <param name="user">Receives the user data from the view</param>
        public VoteSubmittedViewModel(INavigationService navigationService, UserVoteTable user)
        {
            this.navigation = navigationService;
            this.user = user;

            this.resource = new ResourceLoader();
            this.Title = this.resource.GetString("SubmittedTitle");
            this.VoteText = this.resource.GetString("VoteText");
            this.ElectorateText = this.resource.GetString("ElectorateText");
            this.CandidateText = this.resource.GetString("CandidateText");
            this.PartyText = this.resource.GetString("PartyText");
            this.ReferendumText = this.resource.GetString("ReferendumText");
            this.SubmittedText = this.resource.GetString("SubmittedText");
            this.LoginButton = this.resource.GetString("VoteButton");
            this.ConnectionText = this.resource.GetString("ConnectionText");

            this.LoginCommand = new CommandService(this.Login);

            ElectorateTable electorateData = new ElectorateTable();
            List<CandidateTable> candidateData = new List<CandidateTable>();
            PartyTable partyData = new PartyTable();

            List<string> candidateList = JsonConvert.DeserializeObject<List<string>>(this.user.CandidateIds);

            electorateData = this.db.GetElectorateFromId(this.user.ElectorateId);

            foreach (string item in candidateList)
            {
                int id = int.Parse(item);

                CandidateTable candidate = new CandidateTable();
                candidate = this.db.GetCandidateFromId(id);

                candidateData.Add(candidate);
            }

            partyData = this.db.GetPartyFromId(this.user.PartyId);

            this.Electorate = electorateData.Name;

            string candidateNames = string.Empty;

            foreach (CandidateTable item in candidateData)
            {
                if (candidateNames == string.Empty)
                {
                    candidateNames = item.Name;
                }
                else
                {
                    candidateNames = string.Join(Environment.NewLine, candidateNames, item.Name);
                }
            }

            this.Candidates = candidateNames;
            this.Party = partyData.Name;
            this.Referendum = this.user.Referendum.ToString();
        }

        /// <summary>
        /// Event relating to and controlling property changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the String property Title
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the String property VoteText
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the String property ElectorateText
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the String property Electorate
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the String property CandidateText
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the String property Candidates
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the String property PartyText
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the String property Party
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the String property ReferendumText
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the String property Referendum
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the String property SubmittedText
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the String property LoginButton
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the String property ConnectionText
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
        /// Navigates the user back to the LoginView
        /// </summary>
        /// <param name="obj">Object not used</param>
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
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
