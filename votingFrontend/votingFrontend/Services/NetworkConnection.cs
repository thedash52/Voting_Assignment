// <copyright file="NetworkConnection.cs" company="UCOL 3rd Year Bachelor of Information and Communication Assignment">
// Copyright (c) UCOL 3rd Year Bachelor of Information and Communication Assignment. All rights reserved.
// </copyright>

namespace VotingFrontend.Services
{
    using System;
    using Windows.Networking.Connectivity;
    using Windows.UI.Core;
    using Windows.UI.Xaml;

    /// <summary>
    /// Service that allows the internet status to be checked
    /// </summary>
    internal class NetworkConnection : StateTriggerBase
    {
        private bool requiresInternet;

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkConnection"/> class.
        /// </summary>
        public NetworkConnection()
        {
            NetworkInformation.NetworkStatusChanged += this.NetworkInformation_NetworkStatusChanged;
        }

        /// <summary>
        /// Gets or sets a value indicating whether internet is required
        /// </summary>
        public bool RequiresInternet
        {
            get
            {
                return this.requiresInternet;
            }

            set
            {
                this.requiresInternet = value;
                var profile = NetworkInformation.GetInternetConnectionProfile();

                if (profile != null && profile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess)
                {
                    this.SetActive(value);
                }
                else
                {
                    this.SetActive(!value);
                }
            }
        }

        private async void NetworkInformation_NetworkStatusChanged(object sender)
        {
            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if (NetworkInformation.GetInternetConnectionProfile() != null)
                {
                    this.SetActive(this.RequiresInternet);
                }
                else
                {
                    this.SetActive(!this.RequiresInternet);
                }
            });
        }
    }
}
