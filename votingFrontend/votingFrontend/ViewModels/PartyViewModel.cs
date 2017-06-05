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
    public class PartyViewModel : INotifyPropertyChanged
    {
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

        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
