// <copyright file="VoteSubmittedView.xaml.cs" company="UCOL 3rd Year Bachelor of Information and Communication Assignment">
// Copyright (c) UCOL 3rd Year Bachelor of Information and Communication Assignment. All rights reserved.
// </copyright>

namespace VotingFrontend.Views
{
    using VotingFrontend.DatabaseTables;
    using VotingFrontend.Services;
    using VotingFrontend.ViewModels;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    /// <summary>
    /// Displays Vote Data the user selected
    /// </summary>
    public sealed partial class VoteSubmittedView : Page
    {
        // ViewModel Object for the related ViewModel
        private VoteSubmittedViewModel voteSubmittedVM;

        /// <summary>
        /// Initializes a new instance of the <see cref="VoteSubmittedView"/> class.
        /// </summary>
        public VoteSubmittedView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Overrides the OnNavigatedaTo method to set the DataContext for the view
        /// </summary>
        /// <param name="e">Contains the user data passed from the previous view</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // user = e.Parameter as UserVoteTable;
            this.voteSubmittedVM = new VoteSubmittedViewModel(new NavigationService(), e.Parameter as UserVoteTable);
            this.DataContext = this.voteSubmittedVM;

            base.OnNavigatedTo(e);
        }
    }
}
