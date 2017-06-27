// <copyright file="LoginView.xaml.cs" company="UCOL 3rd Year Bachelor of Information and Communication Assignment">
// Copyright (c) UCOL 3rd Year Bachelor of Information and Communication Assignment. All rights reserved.
// </copyright>

namespace VotingFrontend.Views
{
    using VotingFrontend.Services;
    using VotingFrontend.ViewModels;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Displays a Login page for the user to enter their details and login
    /// </summary>
    public sealed partial class LoginView : Page
    {
        // ViewModel Object for the related ViewModel
        private LoginViewModel loginVM;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginView"/> class.
        /// </summary>
        public LoginView()
        {
            this.loginVM = new LoginViewModel(new NavigationService());

            this.InitializeComponent();
            this.DataContext = this.loginVM;
        }
    }
}
