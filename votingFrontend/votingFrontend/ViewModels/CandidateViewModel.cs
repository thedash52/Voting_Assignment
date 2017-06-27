// <copyright file="CandidateViewModel.cs" company="UCOL 3rd Year Bachelor of Information and Communication Assignment">
// Copyright (c) UCOL 3rd Year Bachelor of Information and Communication Assignment. All rights reserved.
// </copyright>

namespace VotingFrontend.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;
    using VotingFrontend.DatabaseTables;
    using VotingFrontend.Interfaces;
    using VotingFrontend.Models;
    using VotingFrontend.Services;
    using VotingFrontend.Views;
    using Windows.ApplicationModel.Resources;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// The ViewModel of the CandidateView which contains all the logic for the view
    /// </summary>
    public class CandidateViewModel : INotifyPropertyChanged
    {
        // Private variables for the properties to store information
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
        /// Initializes a new instance of the <see cref="CandidateViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">Passes the Navigation Property from the View to the ViewModel</param>
        public CandidateViewModel(INavigationService navigationService)
        {
            this.navigation = navigationService;

            this.resource = new ResourceLoader();
            this.Title = this.resource.GetString("CandidateTitle");
            this.SelectButton = this.resource.GetString("CandidateSelect");

            this.SelectCommand = new CommandService(this.Next);

            this.ConnectionText = this.resource.GetString("ConnectionText");

            this.CanExecute = false;

            this.SelectedCandidates = new List<CandidateTable>();

            this.Candidates = new ObservableCollection<CandidateSelection>();
            this.Candidates = this.db.GetCandidates();
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
        /// Gets or sets the <list type="ObservableCollection-CandidateSelection"/> property Canidates
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the String property SelectButton
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the ICommand property SelectCommand
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
        /// Gets or sets the <list type="CandidateTable"/> property SelectCandidates
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

                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the CandidateSelection property SelectCandidate
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the command can execute
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
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// When an item is selected checks how many have been selected and if to many tells the user otherwise adds the selected item to a selected item list
        /// </summary>
        /// <param name="sender">Object containing checkbox</param>
        internal async void SelectedCandidateChecked(object sender)
        {
            if (this.SelectedCandidates.Count > 3)
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
                int idx = this.Candidates.IndexOf(this.SelectedCandidate);

                this.Candidates[idx].Selected = true;

                CandidateTable candidate = new CandidateTable()
                {
                    Id = this.SelectedCandidate.Id,
                    ServerId = this.SelectedCandidate.ServerId,
                    Name = this.SelectedCandidate.Name,
                    Detail = this.SelectedCandidate.Detail,
                    Image = this.SelectedCandidate.Image
                };

                if (!this.SelectedCandidates.Any(i => i.Id == candidate.Id && i.ServerId == candidate.ServerId))
                {
                    this.SelectedCandidates.Add(candidate);
                }

                this.CanExecute = true;
            }
        }

        /// <summary>
        /// When an item is unselected removes the selected item from the selected item list
        /// </summary>
        /// <param name="sender">Empty Object</param>
        internal void SelectedCandidateUnChecked(object sender)
        {
            CandidateTable candidate = new CandidateTable()
            {
                Id = this.SelectedCandidate.Id,
                ServerId = this.SelectedCandidate.ServerId,
                Name = this.SelectedCandidate.Name,
                Detail = this.SelectedCandidate.Detail,
                Image = this.SelectedCandidate.Image
            };

            if (this.SelectedCandidates.Any(i => i.Id == candidate.Id && i.ServerId == candidate.ServerId))
            {
                int idx = this.Candidates.IndexOf(this.SelectedCandidate);

                this.Candidates[idx].Selected = false;

                int rmIdx = 0;

                for (int i = 0; i < this.SelectedCandidates.Count; i++)
                {
                    if (this.SelectedCandidates[i].Id == candidate.Id && this.SelectedCandidates[i].ServerId == candidate.ServerId)
                    {
                        rmIdx = i;
                    }
                }

                this.SelectedCandidates.RemoveAt(rmIdx);

                if (this.SelectedCandidates.Count == 0)
                {
                    this.CanExecute = false;
                }
            }
        }

        /// <summary>
        /// Takes the selected item list and saves it to the user, then navigates to the next view
        /// </summary>
        /// <param name="sender">Empty object for command</param>
        internal async void Next(object sender)
        {
            int result = this.db.AddCandidateVote(this.SelectedCandidates);

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
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
