// <copyright file="NavigationService.cs" company="UCOL 3rd Year Bachelor of Information and Communication Assignment">
// Copyright (c) UCOL 3rd Year Bachelor of Information and Communication Assignment. All rights reserved.
// </copyright>

namespace VotingFrontend.Services
{
    using System;
    using VotingFrontend.Interfaces;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Service that allows navigation control from the view model
    /// </summary>
    public class NavigationService : INavigationService
    {
        /// <summary>
        /// Navigates back one step in the navigation stack
        /// </summary>
        public void GoBack()
        {
            var frame = (Frame)Window.Current.Content;
            frame.GoBack();
        }

        /// <summary>
        /// Navigates to the specified view
        /// </summary>
        /// <param name="sourcePage">The view to navigate to</param>
        public void Navigate(Type sourcePage)
        {
            var frame = (Frame)Window.Current.Content;
            frame.Navigate(sourcePage);
        }

        /// <summary>
        /// Navigates to the specified view with the data to be passed to the view
        /// </summary>
        /// <param name="sourcePage">The view to navigate to</param>
        /// <param name="parameter">The data to be passed to the view</param>
        public void Navigate(Type sourcePage, object parameter)
        {
            var frame = (Frame)Window.Current.Content;
            frame.Navigate(sourcePage, parameter);
        }
    }
}
