using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using votingFrontend.Interfaces;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace votingFrontend.Services
{
    public class NavigationService : INavigationService
    {
        public void GoBack()
        {
            var frame = (Frame)Window.Current.Content;
            frame.GoBack();
        }

        public void Navigate(Type sourcePage)
        {
            var frame = (Frame)Window.Current.Content;
            frame.Navigate(sourcePage);
        }

        public void Navigate(Type sourcePage, object Parameter)
        {
            var frame = (Frame)Window.Current.Content;
            frame.Navigate(sourcePage, Parameter);
        }
    }
}
