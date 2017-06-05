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
using votingFrontend.Services;
using votingFrontend.Views;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Controls;

namespace votingFrontend.ViewModels
{
    public class ElectorateViewModel : INotifyPropertyChanged
    {
        private string title;
        private List<ElectorateTable> electorates;
        private string selectButton;

        private string connectionText;

        private ICommand selectCommand;
        private INavigationService navigation;

        private ResourceLoader resource;
        private RestService restAPI = new RestService();
        private DatabaseService db = new DatabaseService();

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

        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
