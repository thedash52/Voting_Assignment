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
    public class ReferendumViewModel : INotifyPropertyChanged
    {
        private string title;
        private ReferendumTable referendum;
        private string yesButton;
        private string noButton;

        private string connectionText;

        private ICommand answerCommand;
        private INavigationService navigation;

        private ResourceLoader resource;
        private RestService restAPI = new RestService();
        private DatabaseService db = new DatabaseService();

        public ReferendumViewModel(INavigationService navigationService)
        {
            this.navigation = navigationService;

            this.resource = new ResourceLoader();
            Title = resource.GetString("ReferendumTitle");
            YesButton = resource.GetString("Yes");
            NoButton = resource.GetString("No");

            AnswerCommand = new CommandService(Next);

            ConnectionText = resource.GetString("ConnectionText");

            Referendum = new ReferendumTable();
            Referendum = db.GetReferendum();
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

        public ReferendumTable Referendum
        {
            get
            {
                return this.referendum;
            }

            set
            {
                this.referendum = value;
                OnPropertyChanged();
            }
        }

        public string YesButton
        {
            get
            {
                return this.yesButton;
            }

            set
            {
                this.yesButton = value;
                OnPropertyChanged();
            }
        }

        public string NoButton
        {
            get
            {
                return this.noButton;
            }

            set
            {
                this.noButton = value;
                OnPropertyChanged();
            }
        }

        public ICommand AnswerCommand
        {
            get
            {
                return this.answerCommand;
            }

            set
            {
                this.answerCommand = value;
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

        internal async void Next(object obj)
        {
            int result = -1;

            switch ((string)obj)
            {
                case "yes":
                    result = db.AddReferendumVote(true);
                    break;
                case "no":
                    result = db.AddReferendumVote(false);
                    break;
            }

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
                UserVoteTable voteSent = await restAPI.SendVote();

                if (voteSent == null)
                {
                    ContentDialog connectionError = new ContentDialog()
                    {
                        Title = "Unable to Send Vote",
                        Content = "Problem sending vote to ElectionsNZ, will try again later",
                        PrimaryButtonText = "OK"
                    };

                    await connectionError.ShowAsync();
                }

                this.navigation.Navigate(typeof(VoteSubmittedView), voteSent);
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
