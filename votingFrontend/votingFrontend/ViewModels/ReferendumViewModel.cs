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
    /// <summary>
    /// The ViewModel of the ReferendumView which contains all the logic for the view
    /// </summary>
    public class ReferendumViewModel : INotifyPropertyChanged
    {
        //Private variables for the properties to store information
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

        /// <summary>
        /// Default Contructor for ReferendumViewModel
        /// </summary>
        /// <param name="navigationService">Passes the Navigation Property from the View to the ViewModel</param>
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
        /// Gets and Sets the ReferendumTable property Referendum
        /// </summary>
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

        /// <summary>
        /// Gets and Sets the String property YesButton
        /// </summary>
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

        /// <summary>
        /// Gets and Sets the String property NoButton
        /// </summary>
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

        /// <summary>
        /// Gets and Sets the ICommand property AnswerCommand
        /// </summary>
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
        /// Takes the result passed from the button and saves it to the user, then navigates to the next view
        /// </summary>
        /// <param name="obj">Contains a Yes or No value depending on what button was pressed</param>
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
