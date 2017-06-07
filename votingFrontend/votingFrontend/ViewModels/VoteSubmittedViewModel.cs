using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using votingFrontend.DatabaseTables;
using votingFrontend.Interfaces;
using votingFrontend.Services;
using votingFrontend.Views;
using Windows.ApplicationModel.Resources;

namespace votingFrontend.ViewModels
{
    public class VoteSubmittedViewModel : INotifyPropertyChanged
    {
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

        public event PropertyChangedEventHandler PropertyChanged;

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

        internal void Login(object obj)
        {
            this.navigation.Navigate(typeof(LoginView));
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
