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
    /// The ViewModel of the PartyView which contains all the logic for the view
    /// </summary>
    public class PartyViewModel : INotifyPropertyChanged
    {
        //Private variables for the properties to store information
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
        /// Default Contructor for PartyViewModel
        /// </summary>
        /// <param name="navigationService">Passes the Navigation Property from the View to the ViewModel</param>
        public PartyViewModel(INavigationService navigationService)
        {
            this.navigation = navigationService;

            this.resource = new ResourceLoader();
            Title = resource.GetString("PartyTitle");
            SelectButton = resource.GetString("PartySelect");

            SelectCommand = new CommandService(Next);

            ConnectionText = resource.GetString("ConnectionText");

            Parties = new List<PartyTable>();
            Parties = db.GetParties();
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
        /// Gets and Sets the List<PartyTable> property Parties
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
        /// Gets and Sets the PartyTable property SelectParty
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
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Takes the selected item and saves it to the user, then navigates to the next view
        /// </summary>
        /// <param name="sender">Contains the selected item from the list</param>
        internal async void Next(object sender)
        {
            int result = db.AddPartyVote((PartyTable)sender);

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
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
