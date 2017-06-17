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
    /// <summary>
    /// The ViewModel of the CandidateView which contains all the logic for the view
    /// </summary>
    public class CandidateViewModel : INotifyPropertyChanged
    {
        //Private variables for the properties to store information
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

        /// <summary>
        /// Default Contructor for CandidateViewModel
        /// </summary>
        /// <param name="navigationService">Passes the Navigation Property from the View to the ViewModel</param>
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
        /// Gets and Sets the ObservableCollection<CandidateSelection> property Canidates
        /// </summary>
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

        /// <summary>
        /// Gets and Sets the String property SelectButton
        /// </summary>
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

        /// <summary>
        /// Gets and Sets the ICommand property SelectCommand
        /// </summary>
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
        /// Gets and Sets the List<CandidateTable> property SelectCandidates
        /// </summary>
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

        /// <summary>
        /// Gets and Sets the CandidateSelection property SelectCandidate
        /// </summary>
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

        /// <summary>
        /// Gets and Sets the String property 
        /// </summary>
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

        /// <summary>
        /// When an item is selected checks how many have been selected and if to many tells the user otherwise adds the selected item to a selected item list
        /// </summary>
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

        /// <summary>
        /// When an item is unselected removes the selected item from the selected item list
        /// </summary>
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

        /// <summary>
        /// Takes the selected item list and saves it to the user, then navigates to the next view
        /// </summary>
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
