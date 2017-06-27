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

namespace VotingFrontend.ViewModels
{
    /// <summary>
    /// The ViewModel of the ElectorateView which contains all the logic for the view
    /// </summary>
    public class ElectorateViewModel : INotifyPropertyChanged
    {
        //Private variables for the properties to store information
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
        /// Default Contructor for ElectorateViewModel
        /// </summary>
        /// <param name="navigationService">Passes the Navigation Property from the View to the ViewModel</param>
        public ElectorateViewModel(INavigationService navigationService)
        {
            this.navigation = navigationService;

            this.resource = new ResourceLoader();
            Title = resource.GetString("ElectorateTitle");
            SelectButton = resource.GetString("ElectorateSelect");
            SelectCommand = new CommandService(Next);

            ConnectionText = resource.GetString("ConnectionText");

            Electorates = new List<ElectorateTable>();
            Electorates = db.GetElectorates();
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
        /// Gets and Sets the List<ElectorateTable> property Electorates
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
        /// Saves the selected electorate to the user and navigates to the next view
        /// </summary>
        /// <param name="sender">Sender Contains the selected item from the list</param>
        internal async void Next(object sender)
        {
            int result = db.AddElectorateVote((ElectorateTable)sender);

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
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
