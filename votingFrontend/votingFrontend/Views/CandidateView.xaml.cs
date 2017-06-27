using VotingFrontend.Services;
using VotingFrontend.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace VotingFrontend.Views
{
    /// <summary>
    /// Displays Candidates for the user to choose from
    /// </summary>
    public sealed partial class CandidateView : Page
    {
        //ViewModel Object for the related ViewModel
        private CandidateViewModel candidateVM;

        /// <summary>
        /// Default Contructor of the CandidateView
        /// </summary>
        public CandidateView()
        {
            candidateVM = new CandidateViewModel(new NavigationService());

            this.InitializeComponent();
            this.DataContext = candidateVM;
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

        /// <summary>
        /// Links the checkboxes to the related method in the view model
        /// </summary>
        /// <param name="sender">Contains the selected item of the list</param>
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            candidateVM.SelectedCandidateChecked(sender);
        }

        /// <summary>
        /// Links the checkboxes to the related method in the view model
        /// </summary>
        /// <param name="sender">Contains the selected item of the list</param>
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            candidateVM.SelectedCandidateUnChecked(sender);
        }
    }
}
