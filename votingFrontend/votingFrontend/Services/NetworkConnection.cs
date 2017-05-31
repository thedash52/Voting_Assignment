using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace votingFrontend.Services
{
    class NetworkConnection : StateTriggerBase
    {
        private bool requiresInternet;

        public NetworkConnection()
        {
            NetworkInformation.NetworkStatusChanged += NetworkInformation_NetworkStatusChanged;
        }

        public bool RequiresInternet
        {
            get
            {
                return this.requiresInternet;
            }

            set
            {
                var profile = NetworkInformation.GetInternetConnectionProfile();

                if (profile != null && profile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess)
                {
                    SetActive(true);
                }
                else
                {
                    SetActive(false);
                }
            }
        }

        private async void NetworkInformation_NetworkStatusChanged(object sender)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if (NetworkInformation.GetInternetConnectionProfile() != null)
                {
                    SetActive(this.RequiresInternet);
                }
                else
                {
                    SetActive(!this.RequiresInternet);
                }
            });
        }
    }
}
