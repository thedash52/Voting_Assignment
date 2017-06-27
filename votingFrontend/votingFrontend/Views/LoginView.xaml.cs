using VotingFrontend.Services;
using VotingFrontend.ViewModels;
using Windows.UI.Xaml.Controls;

namespace VotingFrontend.Views
{
    /// <summary>
    /// Displays a Login page for the user to enter their details and login
    /// </summary>
    public sealed partial class LoginView : Page
    {
        //ViewModel Object for the related ViewModel
        private LoginViewModel loginVM;

        /// <summary>
        /// Default Contructor of the LoginView
        /// </summary>
        public LoginView()
        {
            loginVM = new LoginViewModel(new NavigationService());

            this.InitializeComponent();
            this.DataContext = loginVM;
        }
    }
}
