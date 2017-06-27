// <copyright file="INavigationService.cs" company="UCOL 3rd Year Bachelor of Information and Communication Assignment">
// Copyright (c) UCOL 3rd Year Bachelor of Information and Communication Assignment. All rights reserved.
// </copyright>

namespace VotingFrontend.Interfaces
{
    using System;

    /// <summary>
    /// Interface for controlling navigation from the view model
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Navigates to the specified view
        /// </summary>
        /// <param name="sourcePage">The view to navigate to</param>
        void Navigate(Type sourcePage);

        /// <summary>
        /// Navigates to the specified view with the data to be passed to the view
        /// </summary>
        /// <param name="sourcePage">The view to navigate to</param>
        /// <param name="parameter">The data to be passed to the view</param>
        void Navigate(Type sourcePage, object parameter);

        /// <summary>
        /// Navigates back one step in the navigation stack
        /// </summary>
        void GoBack();
    }
}
