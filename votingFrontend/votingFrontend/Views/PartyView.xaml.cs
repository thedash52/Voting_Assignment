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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace votingFrontend.Views
{
    /// <summary>
    /// Displays Parties for the user to choose from
    /// </summary>
    public sealed partial class PartyView : Page
    {
        //ViewModel Object for the related ViewModel
        private PartyViewModel partyVM;

        /// <summary>
        /// Default Contructor of the PartyView
        /// </summary>
        public PartyView()
        {
            this.InitializeComponent();

            this.partyVM = new PartyViewModel(new NavigationService());
            this.DataContext = partyVM;
        }

        /// <summary>
        /// When an item is selected the DataTemplate for the selected item is changed to a more detailed view and any other items DataTemplate is changed to the detault view
        /// </summary>
        /// <param name="sender">The Object that called this method</param>
        /// <param name="e">Event arguments for the object that called this method</param>
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in e.AddedItems)
            {
                ListViewItem lvi = (sender as ListView).ContainerFromItem(item) as ListViewItem;
                lvi.ContentTemplate = (DataTemplate)this.Resources["Detailed"];
            }

            foreach (var item in e.RemovedItems)
            {
                ListViewItem lvi = (sender as ListView).ContainerFromItem(item) as ListViewItem;
                lvi.ContentTemplate = (DataTemplate)this.Resources["Normal"];
            }
        }
    }
}
