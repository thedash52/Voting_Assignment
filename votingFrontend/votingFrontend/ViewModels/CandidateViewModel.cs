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
using Windows.UI.Xaml.Controls;

namespace votingFrontend.ViewModels
{
    public class CandidateViewModel : INotifyPropertyChanged
    {
        private string title;
        private List<CandidateTable> candidates;
        private List<CandidateTable> selectedCandidates;
        private CandidateTable selectedCandidate;
        private string selectButton;

        private string connectionText;

        private ICommand selectCommand;
        private ICommand checkedCommand;
        private ICommand unCheckedCommand;
        private INavigationService navigation;

        private ResourceLoader resource;
        private RestService resAPI = new RestService();
        private DatabaseService db = new DatabaseService();

        public CandidateViewModel(INavigationService navigationService)
        {
            this.navigation = navigationService;

            this.resource = new ResourceLoader();
            Title = resource.GetString("CandidateTitle");
            SelectButton = resource.GetString("CandidateSelect");

            SelectCommand = new CommandService(Next);
            CheckedCommand = new CommandService(SelectedCandidateChecked);
            UnCheckedCommand = new CommandService(SelectedCandidateUnChecked);

            ConnectionText = resource.GetString("ConnectionText");

            Candidates = db.GetCandidates();
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

        public List<CandidateTable> Candidates
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

        public string SelectButton
        {
            get
            {
                return this.selectButton;
            }

            set
            {
                this.selectButton = value;
                OnPropertyChanged();
            }
        }

        public ICommand SelectCommand
        {
            get
            {
                return this.selectCommand;
            }

            set
            {
                this.selectCommand = value;
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

        public List<CandidateTable> SelectedCandidates
        {
            get
            {
                return this.selectedCandidates;
            }

            set
            {
                this.selectedCandidates = value;
                OnPropertyChanged();
            }
        }

        public ICommand CheckedCommand
        {
            get
            {
                return this.checkedCommand;
            }

            set
            {
                this.checkedCommand = value;
                OnPropertyChanged();
            }
        }

        public ICommand UnCheckedCommand
        {
            get
            {
                return this.unCheckedCommand;
            }

            set
            {
                this.unCheckedCommand = value;
                OnPropertyChanged();
            }
        }

        public CandidateTable SelectedCandidate
        {
            get
            {
                return this.selectedCandidate;
            }

            set
            {
                this.selectedCandidate = value;
                OnPropertyChanged();
            }
        }

        internal void SelectedCandidateChecked(object sender)
        {

        }

        internal void SelectedCandidateUnChecked(object sender)
        {

        }

        internal async void Next(object sender)
        {
            int result = db.AddCandidateVote(SelectedCandidates);

            if (result == -1)
            {
                ContentDialog error = new ContentDialog()
                {
                    Title = "Error Getting User",
                    Content = "Unable to get current user, returning to login",
                    PrimaryButtonText = "OK"
                };

                await error.ShowAsync();
                this.navigation.Navigate(typeof(LoginView));
            }
            else
            {
                this.navigation.Navigate(typeof(PartyView));
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
