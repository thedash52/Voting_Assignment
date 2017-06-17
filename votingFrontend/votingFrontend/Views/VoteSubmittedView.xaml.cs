using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using votingFrontend.DatabaseTables;
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
    /// Displays Vote Data the user selected
    /// </summary>
    public sealed partial class VoteSubmittedView : Page
    {
        //ViewModel Object for the related ViewModel
        private VoteSubmittedViewModel voteSubmittedVM;

        /// <summary>
        /// Default Contructor of the VoteSubmittedView
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
            //user = e.Parameter as UserVoteTable;
            this.voteSubmittedVM = new VoteSubmittedViewModel(new NavigationService(), e.Parameter as UserVoteTable);
            this.DataContext = voteSubmittedVM;

            base.OnNavigatedTo(e);
        }
    }
}
