// <copyright file="ReferendumView.xaml.cs" company="UCOL 3rd Year Bachelor of Information and Communication Assignment">
// Copyright (c) UCOL 3rd Year Bachelor of Information and Communication Assignment. All rights reserved.
// </copyright>

namespace VotingFrontend.Views
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using VotingFrontend.DatabaseTables;
    using VotingFrontend.Services;
    using VotingFrontend.ViewModels;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media.Imaging;

    /// <summary>
    /// Displays the Referendum for the user to choose from
    /// </summary>
    public sealed partial class ReferendumView : Page
    {
        // ViewModel Object for the related ViewModel
        private ReferendumViewModel referendumVM;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferendumView"/> class.
        /// </summary>
        public ReferendumView()
        {
            this.InitializeComponent();

            this.referendumVM = new ReferendumViewModel(new NavigationService());
            this.DataContext = this.referendumVM;

            ReferendumTable referendum = this.referendumVM.Referendum;

            var details = JsonConvert.DeserializeObject<List<string>>(referendum.Detail);
            var images = JsonConvert.DeserializeObject<List<string>>(referendum.Images);

            Grid grid = new Grid();
            grid.ColumnDefinitions.Insert(0, new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Insert(0, new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            if (details.Count > images.Count)
            {
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
                            Margin = new Thickness(20)
                        };

                        if ((i - 1) < images.Count)
                        {
                            Image newImage = new Image()
                            {
                                Source = new BitmapImage(new Uri(this.BaseUri, images[i - 1])),
                                VerticalAlignment = VerticalAlignment.Center,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                Margin = new Thickness(5),
                                MaxHeight = 250,
                                MaxWidth = 250
                            };

                            if (i % 2 == 0)
                            {
                                Grid.SetColumn(newText, 0);
                                Grid.SetColumn(newImage, 1);
                            }
                            else
                            {
                                Grid.SetColumn(newText, 1);
                                Grid.SetColumn(newImage, 0);
                            }

                            Grid.SetRow(newText, i);
                            Grid.SetRow(newImage, i);

                            grid.Children.Add(newText);
                            grid.Children.Add(newImage);
                        }
                        else
                        {
                            Grid.SetColumn(newText, 0);
                            Grid.SetColumnSpan(newText, 2);

                            Grid.SetRow(newText, i);

                            grid.Children.Add(newText);
                        }
                    }
                }
            }

            this.scrollView.Content = grid;
        }
    }
}
