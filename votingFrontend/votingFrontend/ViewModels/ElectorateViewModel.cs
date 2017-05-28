using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using votingFrontend.DatabaseTables;
using votingFrontend.Interfaces;
using votingFrontend.Services;
using Windows.ApplicationModel.Resources;

namespace votingFrontend.ViewModels
{
    public class ElectorateViewModel : INotifyPropertyChanged
    {
        private string title;
        private List<ElectorateTable> electorates;
        private string selectButton;

        private ICommand selectCommand;
        private INavigationService navigation;

        private ResourceLoader resource;
        private RestService restAPI = new RestService();

        public ElectorateViewModel(INavigationService navigationService)
        {
            this.navigation = navigationService;

            this.resource = new ResourceLoader();
            Title = resource.GetString("ElectorateTitle");
            SelectButton = resource.GetString("ElectorateSelect");

            Electorates = new List<ElectorateTable>()
            {
                new ElectorateTable() { Name = "Palmerston North", Detail = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut a sollicitudin urna. Sed sollicitudin suscipit est, eget euismod tellus lobortis id. Sed maximus cursus pellentesque. Morbi quis dolor pretium, sodales orci quis, dignissim neque. Proin ante tellus, lobortis non dui auctor, tincidunt mollis enim. Donec eget congue nisi, a tincidunt felis. Mauris feugiat, orci in lacinia consequat, lorem dui cursus odio, iaculis rutrum velit nisl in nisl. Fusce aliquet lacus vitae turpis scelerisque, a fermentum ex luctus. Etiam eleifend dolor vel ex sodales imperdiet. Praesent non nunc in orci sollicitudin imperdiet.", Image = "/Assets/placeholder120x120.png" },
                new ElectorateTable() { Name = "Rangitikei", Detail = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut a sollicitudin urna. Sed sollicitudin suscipit est, eget euismod tellus lobortis id. Sed maximus cursus pellentesque. Morbi quis dolor pretium, sodales orci quis, dignissim neque. Proin ante tellus, lobortis non dui auctor, tincidunt mollis enim. Donec eget congue nisi, a tincidunt felis. Mauris feugiat, orci in lacinia consequat, lorem dui cursus odio, iaculis rutrum velit nisl in nisl. Fusce aliquet lacus vitae turpis scelerisque, a fermentum ex luctus. Etiam eleifend dolor vel ex sodales imperdiet. Praesent non nunc in orci sollicitudin imperdiet.", Image = "/Assets/placeholder120x120.png" },
                new ElectorateTable() { Name = "Te Tai Hauauru", Detail = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut a sollicitudin urna. Sed sollicitudin suscipit est, eget euismod tellus lobortis id. Sed maximus cursus pellentesque. Morbi quis dolor pretium, sodales orci quis, dignissim neque. Proin ante tellus, lobortis non dui auctor, tincidunt mollis enim. Donec eget congue nisi, a tincidunt felis. Mauris feugiat, orci in lacinia consequat, lorem dui cursus odio, iaculis rutrum velit nisl in nisl. Fusce aliquet lacus vitae turpis scelerisque, a fermentum ex luctus. Etiam eleifend dolor vel ex sodales imperdiet. Praesent non nunc in orci sollicitudin imperdiet.", Image = "/Assets/placeholder120x120.png" }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                this.title = value;
                OnPropertyChanged();
            }
        }

        public List<ElectorateTable> Electorates
        {
            get
            {
                return this.electorates;
            }

            set
            {
                this.electorates = value;
                OnPropertyChanged();
            }
        }

        public string SelectButton
        {
            get
            {
                return this.selectButton;
            }

            set
            {
                this.selectButton = value;
                OnPropertyChanged();
            }
        }

        public ICommand SelectCommand
        {
            get
            {
                return this.selectCommand;
            }

            set
            {
                this.selectCommand = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
