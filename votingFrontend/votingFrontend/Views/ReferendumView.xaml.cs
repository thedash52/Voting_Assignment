using Newtonsoft.Json;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace votingFrontend.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ReferendumView : Page
    {
        private ReferendumViewModel referendumVM;

        public ReferendumView()
        {
            this.InitializeComponent();

            this.referendumVM = new ReferendumViewModel(new NavigationService());
            this.DataContext = referendumVM;

            ReferendumTable referendum = this.referendumVM.Referendum;

            var details = JsonConvert.DeserializeObject<List<string>>(referendum.Detail);
            var images = JsonConvert.DeserializeObject<List<string>>(referendum.Images);

            Grid grid = new Grid();
            grid.ColumnDefinitions.Insert(0, new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Insert(0, new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            for (int i = 0; i < details.Count; i++)
            {
                grid.RowDefinitions.Insert(i, new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });

                if (i == 0)
                {
                    TextBlock newText = new TextBlock()
                    {
                        Text = details[i],
                        VerticalAlignment = VerticalAlignment.Center, 
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        TextWrapping = TextWrapping.Wrap,
                        Margin = new Thickness(5)
                    };

                    Grid.SetColumn(newText, 0);
                    Grid.SetColumnSpan(newText, 2);
                    Grid.SetRow(newText, i);

                    grid.Children.Add(newText);
                }
                else
                {
                    TextBlock newText = new TextBlock()
                    {
                        Text = details[i],
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        TextWrapping = TextWrapping.Wrap,
                        Margin = new Thickness(5)
                    };

                    if (i % 2 == 0)
                    {
                        Grid.SetColumn(newText, 0);
                    }
                    else
                    {
                        Grid.SetColumn(newText, 1);
                    }

                    Grid.SetRow(newText, i);

                    grid.Children.Add(newText);
                }
            }

            scrollView.Content = grid;
        }
    }
}
