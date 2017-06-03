using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using votingFrontend.DatabaseTables;
using Newtonsoft.Json;

namespace votingFrontend.Services
{
    internal class RestService
    {
        public RestService()
        {

        }

        internal async Task<bool> Login(string firstName, string lastName, DateTime dob, string electoralId)
        {
            string first = "Bob";
            string last = "Smith";
            DateTime birth = DateTime.Parse("1993-08-10");
            string id = "ABC123456";

            if (first != firstName || last != lastName || birth.Date != dob.Date || id != electoralId)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        internal async Task<List<ElectorateTable>> GetElectorates()
        {
            List<ElectorateTable> items = new List<ElectorateTable>()
            {
                new ElectorateTable() { ServerId = 1, Name = "Palmerston North", Detail = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut a sollicitudin urna. Sed sollicitudin suscipit est, eget euismod tellus lobortis id. Sed maximus cursus pellentesque. Morbi quis dolor pretium, sodales orci quis, dignissim neque. Proin ante tellus, lobortis non dui auctor, tincidunt mollis enim. Donec eget congue nisi, a tincidunt felis. Mauris feugiat, orci in lacinia consequat, lorem dui cursus odio, iaculis rutrum velit nisl in nisl. Fusce aliquet lacus vitae turpis scelerisque, a fermentum ex luctus. Etiam eleifend dolor vel ex sodales imperdiet. Praesent non nunc in orci sollicitudin imperdiet.", Image = "/Assets/placeholder120x120.png" },
                new ElectorateTable() { ServerId = 2, Name = "Rangitikei", Detail = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut a sollicitudin urna. Sed sollicitudin suscipit est, eget euismod tellus lobortis id. Sed maximus cursus pellentesque. Morbi quis dolor pretium, sodales orci quis, dignissim neque. Proin ante tellus, lobortis non dui auctor, tincidunt mollis enim. Donec eget congue nisi, a tincidunt felis. Mauris feugiat, orci in lacinia consequat, lorem dui cursus odio, iaculis rutrum velit nisl in nisl. Fusce aliquet lacus vitae turpis scelerisque, a fermentum ex luctus. Etiam eleifend dolor vel ex sodales imperdiet. Praesent non nunc in orci sollicitudin imperdiet.", Image = "/Assets/placeholder120x120.png" },
                new ElectorateTable() { ServerId = 3, Name = "Te Tai Hauauru", Detail = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut a sollicitudin urna. Sed sollicitudin suscipit est, eget euismod tellus lobortis id. Sed maximus cursus pellentesque. Morbi quis dolor pretium, sodales orci quis, dignissim neque. Proin ante tellus, lobortis non dui auctor, tincidunt mollis enim. Donec eget congue nisi, a tincidunt felis. Mauris feugiat, orci in lacinia consequat, lorem dui cursus odio, iaculis rutrum velit nisl in nisl. Fusce aliquet lacus vitae turpis scelerisque, a fermentum ex luctus. Etiam eleifend dolor vel ex sodales imperdiet. Praesent non nunc in orci sollicitudin imperdiet.", Image = "/Assets/placeholder120x120.png" }
            };

            return items;
        }

        internal async Task<List<CandidateTable>> GetCandidates()
        {
            List<CandidateTable> items = new List<CandidateTable>()
            {
                new CandidateTable() { ServerId = 1, Name = "Daniel Nash", Detail = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut a sollicitudin urna. Sed sollicitudin suscipit est, eget euismod tellus lobortis id. Sed maximus cursus pellentesque. Morbi quis dolor pretium, sodales orci quis, dignissim neque. Proin ante tellus, lobortis non dui auctor, tincidunt mollis enim. Donec eget congue nisi, a tincidunt felis. Mauris feugiat, orci in lacinia consequat, lorem dui cursus odio, iaculis rutrum velit nisl in nisl. Fusce aliquet lacus vitae turpis scelerisque, a fermentum ex luctus. Etiam eleifend dolor vel ex sodales imperdiet. Praesent non nunc in orci sollicitudin imperdiet.", Image = "/Assets/placeholder120x120.png" },
                new CandidateTable() { ServerId = 2, Name = "Lynne Nash", Detail = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut a sollicitudin urna. Sed sollicitudin suscipit est, eget euismod tellus lobortis id. Sed maximus cursus pellentesque. Morbi quis dolor pretium, sodales orci quis, dignissim neque. Proin ante tellus, lobortis non dui auctor, tincidunt mollis enim. Donec eget congue nisi, a tincidunt felis. Mauris feugiat, orci in lacinia consequat, lorem dui cursus odio, iaculis rutrum velit nisl in nisl. Fusce aliquet lacus vitae turpis scelerisque, a fermentum ex luctus. Etiam eleifend dolor vel ex sodales imperdiet. Praesent non nunc in orci sollicitudin imperdiet.", Image = "/Assets/placeholder120x120.png" },
                new CandidateTable() { ServerId = 3, Name = "Paul Nash", Detail = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut a sollicitudin urna. Sed sollicitudin suscipit est, eget euismod tellus lobortis id. Sed maximus cursus pellentesque. Morbi quis dolor pretium, sodales orci quis, dignissim neque. Proin ante tellus, lobortis non dui auctor, tincidunt mollis enim. Donec eget congue nisi, a tincidunt felis. Mauris feugiat, orci in lacinia consequat, lorem dui cursus odio, iaculis rutrum velit nisl in nisl. Fusce aliquet lacus vitae turpis scelerisque, a fermentum ex luctus. Etiam eleifend dolor vel ex sodales imperdiet. Praesent non nunc in orci sollicitudin imperdiet.", Image = "/Assets/placeholder120x120.png" }
            };

            return items;
        }

        internal async Task<List<PartyTable>> GetParties()
        {
            List<PartyTable> items = new List<PartyTable>()
            {
                new PartyTable() { ServerId = 1, Name = "National", Detail = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut a sollicitudin urna. Sed sollicitudin suscipit est, eget euismod tellus lobortis id. Sed maximus cursus pellentesque. Morbi quis dolor pretium, sodales orci quis, dignissim neque. Proin ante tellus, lobortis non dui auctor, tincidunt mollis enim. Donec eget congue nisi, a tincidunt felis. Mauris feugiat, orci in lacinia consequat, lorem dui cursus odio, iaculis rutrum velit nisl in nisl. Fusce aliquet lacus vitae turpis scelerisque, a fermentum ex luctus. Etiam eleifend dolor vel ex sodales imperdiet. Praesent non nunc in orci sollicitudin imperdiet.", Image = "/Assets/placeholder120x120.png" },
                new PartyTable() { ServerId = 2, Name = "Labour", Detail = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut a sollicitudin urna. Sed sollicitudin suscipit est, eget euismod tellus lobortis id. Sed maximus cursus pellentesque. Morbi quis dolor pretium, sodales orci quis, dignissim neque. Proin ante tellus, lobortis non dui auctor, tincidunt mollis enim. Donec eget congue nisi, a tincidunt felis. Mauris feugiat, orci in lacinia consequat, lorem dui cursus odio, iaculis rutrum velit nisl in nisl. Fusce aliquet lacus vitae turpis scelerisque, a fermentum ex luctus. Etiam eleifend dolor vel ex sodales imperdiet. Praesent non nunc in orci sollicitudin imperdiet.", Image = "/Assets/placeholder120x120.png" },
                new PartyTable() { ServerId = 3, Name = "Green", Detail = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut a sollicitudin urna. Sed sollicitudin suscipit est, eget euismod tellus lobortis id. Sed maximus cursus pellentesque. Morbi quis dolor pretium, sodales orci quis, dignissim neque. Proin ante tellus, lobortis non dui auctor, tincidunt mollis enim. Donec eget congue nisi, a tincidunt felis. Mauris feugiat, orci in lacinia consequat, lorem dui cursus odio, iaculis rutrum velit nisl in nisl. Fusce aliquet lacus vitae turpis scelerisque, a fermentum ex luctus. Etiam eleifend dolor vel ex sodales imperdiet. Praesent non nunc in orci sollicitudin imperdiet.", Image = "/Assets/placeholder120x120.png" }
            };

            return items;
        }

        internal async Task<ReferendumTable> GetReferendum()
        {
            List<string> images = new List<string>()
            {
                "/Assets/placeholder120x120.png",
                "/Assets/placeholder120x120.png",
                "/Assets/placeholder120x120.png"
            };

            string imageJson = JsonConvert.SerializeObject(images);

            ReferendumTable items = new ReferendumTable()
            {
                ServerId = 1,
                Name = "Palmerston North",
                Detail = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut a sollicitudin urna. Sed sollicitudin suscipit est, eget euismod tellus lobortis id. Sed maximus cursus pellentesque. Morbi quis dolor pretium, sodales orci quis, dignissim neque. Proin ante tellus, lobortis non dui auctor, tincidunt mollis enim. Donec eget congue nisi, a tincidunt felis. Mauris feugiat, orci in lacinia consequat, lorem dui cursus odio, iaculis rutrum velit nisl in nisl. Fusce aliquet lacus vitae turpis scelerisque, a fermentum ex luctus. Etiam eleifend dolor vel ex sodales imperdiet. Praesent non nunc in orci sollicitudin imperdiet.",
                Images = imageJson 
            };

            return items;
        }
    }
}
