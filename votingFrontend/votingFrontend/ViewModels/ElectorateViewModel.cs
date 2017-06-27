// <copyright file="ElectorateViewModel.cs" company="UCOL 3rd Year Bachelor of Information and Communication Assignment">
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
    /// The ViewModel of the ElectorateView which contains all the logic for the view
    /// </summary>
    public class ElectorateViewModel : INotifyPropertyChanged
    {
        // Private variables for the properties to store information
        private string title;
        private List<ElectorateTable> electorates;
        private string selectButton;

        private string connectionText;

        private ICommand selectCommand;
        private INavigationService navigation;

        private ResourceLoader resource;
        private RestService restAPI = new RestService();
        private DatabaseService db = new DatabaseService();

        /// <summary>
        /// Initializes a new instance of the <see cref="ElectorateViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">Passes the Navigation Property from the View to the ViewModel</param>
        public ElectorateViewModel(INavigationService navigationService)
        {
            this.navigation = navigationService;

            this.resource = new ResourceLoader();
            this.Title = this.resource.GetString("ElectorateTitle");
            this.SelectButton = this.resource.GetString("ElectorateSelect");
            this.SelectCommand = new CommandService(this.Next);

            this.ConnectionText = this.resource.GetString("ConnectionText");

            this.Electorates = new List<ElectorateTable>();
            this.Electorates = this.db.GetElectorates();
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
        /// Gets or sets the <list type="ElectorateTable"/> property Electorates
        /// </summary>
        public List<ElectorateTable> Electorates
        {
            get
            {
                return this.electorates;
            }

            set
            {
                this.electorates = value;
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
        /// Saves the selected electorate to the user and navigates to the next view
        /// </summary>
        /// <param name="sender">Sender Contains the selected item from the list</param>
        internal async void Next(object sender)
        {
            int result = this.db.AddElectorateVote((ElectorateTable)sender);

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
                this.navigation.Navigate(typeof(CandidateView));
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
