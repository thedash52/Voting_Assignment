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
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CandidateView : Page
    {
        private CandidateViewModel candidateVM;
        public CandidateView()
        {
            candidateVM = new CandidateViewModel(new NavigationService());

            this.InitializeComponent();
            this.DataContext = candidateVM;
        }

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

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            candidateVM.SelectedCandidateChecked(sender);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            candidateVM.SelectedCandidateUnChecked(sender);
        }
    }
}
