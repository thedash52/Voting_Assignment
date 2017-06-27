// <copyright file="PartyViewModel.cs" company="UCOL 3rd Year Bachelor of Information and Communication Assignment">
// Copyright (c) UCOL 3rd Year Bachelor of Information and Communication Assignment. All rights reserved.
// </copyright>

namespace VotingFrontend.ViewModels
{
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
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// The ViewModel of the PartyView which contains all the logic for the view
    /// </summary>
    public class PartyViewModel : INotifyPropertyChanged
    {
        // Private variables for the properties to store information
        private string title;
        private List<PartyTable> parties;
        private PartyTable selectedParty;
        private string selectButton;

        private string connectionText;

        private ICommand selectCommand;
        private INavigationService navigation;

        private ResourceLoader resource;
        private RestService restAPT = new RestService();
        private DatabaseService db = new DatabaseService();

        /// <summary>
        /// Initializes a new instance of the <see cref="PartyViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">Passes the Navigation Property from the View to the ViewModel</param>
        public PartyViewModel(INavigationService navigationService)
        {
            this.navigation = navigationService;

            this.resource = new ResourceLoader();
            this.Title = this.resource.GetString("PartyTitle");
            this.SelectButton = this.resource.GetString("PartySelect");

            this.SelectCommand = new CommandService(this.Next);

            this.ConnectionText = this.resource.GetString("ConnectionText");

            this.Parties = new List<PartyTable>();
            this.Parties = this.db.GetParties();
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
        /// Gets or sets the <list type="PartyTable"/> property Parties
        /// </summary>
        public List<PartyTable> Parties
        {
            get
            {
                return this.parties;
            }

            set
            {
                this.parties = value;
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
        /// Gets or sets the PartyTable property SelectParty
        /// </summary>
        public PartyTable SelectedParty
        {
            get
            {
                return this.selectedParty;
            }

            set
            {
                this.selectedParty = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Takes the selected item and saves it to the user, then navigates to the next view
        /// </summary>
        /// <param name="sender">Contains the selected item from the list</param>
        internal async void Next(object sender)
        {
            int result = this.db.AddPartyVote((PartyTable)sender);

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
                this.navigation.Navigate(typeof(ReferendumView));
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
