using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using votingFrontend.DatabaseTables;
using votingFrontend.Interfaces;
using votingFrontend.Models;
using votingFrontend.Services;
using votingFrontend.Views;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Controls;

namespace votingFrontend.ViewModels
{
    public class CandidateViewModel : INotifyPropertyChanged
    {
        private string title;
        private ObservableCollection<CandidateSelection> candidates;
        private List<CandidateTable> selectedCandidates;
        private CandidateSelection selectedCandidate;
        private string selectButton;

        private string connectionText;

        private bool canExecute;

        private ICommand selectCommand;
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

            ConnectionText = resource.GetString("ConnectionText");

            CanExecute = false;

            SelectedCandidates = new List<CandidateTable>();

            Candidates = new ObservableCollection<CandidateSelection>();
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

        public ObservableCollection<CandidateSelection> Candidates
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

        public CandidateSelection SelectedCandidate
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

        public bool CanExecute
        {
            get
            {
                return this.canExecute;
            }

            set
            {
                this.canExecute = value;
                OnPropertyChanged();
            }
        }

        internal async void SelectedCandidateChecked(object sender)
        {
            if (SelectedCandidates.Count > 3)
            {
                ContentDialog error = new ContentDialog()
                {
                    Title = "To Many Selected",
                    Content = "You have choosen to many Candidates, Maximum allowed is 3",
                    PrimaryButtonText = "OK"
                };

                CheckBox chkbox = (CheckBox)sender;
                chkbox.IsChecked = false;

                await error.ShowAsync();
            }
            else
            {
                int idx = Candidates.IndexOf(SelectedCandidate);

                Candidates[idx].Selected = true;

                CandidateTable candidate = new CandidateTable()
                {
                    Id = SelectedCandidate.Id,
                    ServerId = SelectedCandidate.ServerId,
                    Name = SelectedCandidate.Name,
                    Detail = SelectedCandidate.Detail,
                    Image = SelectedCandidate.Image
                };

                if (!SelectedCandidates.Any(i => i.Id == candidate.Id && i.ServerId == candidate.ServerId))
                {
                    SelectedCandidates.Add(candidate);
                }

                CanExecute = true;
            }
        }

        internal void SelectedCandidateUnChecked(object sender)
        {
            CandidateTable candidate = new CandidateTable()
            {
                Id = SelectedCandidate.Id,
                ServerId = SelectedCandidate.ServerId,
                Name = SelectedCandidate.Name,
                Detail = SelectedCandidate.Detail,
                Image = SelectedCandidate.Image
            };

            if (SelectedCandidates.Any(i => i.Id == candidate.Id && i.ServerId == candidate.ServerId))
            {
                int idx = Candidates.IndexOf(SelectedCandidate);

                Candidates[idx].Selected = false;

                int rmIdx = 0;

                for (int i = 0; i < SelectedCandidates.Count; i++)
                {
                    if (SelectedCandidates[i].Id == candidate.Id && SelectedCandidates[i].ServerId == candidate.ServerId)
                    {
                        rmIdx = i;
                    }
                }

                SelectedCandidates.RemoveAt(rmIdx);

                if (SelectedCandidates.Count == 0)
                {
                    CanExecute = false;
                }
            }
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
