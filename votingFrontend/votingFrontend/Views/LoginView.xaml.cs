using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using votingFrontend.Services;
using votingFrontend.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace votingFrontend.Views
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
