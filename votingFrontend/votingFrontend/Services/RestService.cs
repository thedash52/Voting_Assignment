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
        private DatabaseService db = new DatabaseService();
        public RestService()
        {

        }

        internal async Task<UserVoteTable> Login(string firstName, string lastName, DateTime dob, string electoralId)
        {
            string first = "Bob";
            string last = "Smith";
            DateTime birth = DateTime.Parse("1993-08-10");
            string id = "ABC123456";

            if (first != firstName || last != lastName || birth.Date != dob.Date || id != electoralId)
            {
                return null;
            }
            else
            {
                UserVoteTable user = new UserVoteTable()
                {
                    ServerId = 1,
                    FirstName = firstName,
                    LastName = lastName,
                    DoB = dob.Date.ToString(),
                    ElectoralId = electoralId
                };

                return user;
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

        internal UserVoteTable SendVote()
        {
            UserVoteTable voteToSend = new UserVoteTable();
            voteToSend = db.GetVoteToSend();

            voteToSend = db.VoteSent(voteToSend);

            return voteToSend;
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

            List<string> details = new List<string>()
            {
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse at libero interdum, porttitor augue in, blandit nisl. Mauris ultricies mattis quam, vel porttitor orci sodales sed. Integer eleifend diam sagittis, tincidunt mauris et, luctus dolor. Praesent fringilla convallis efficitur. Nam at augue nisi. Sed odio lectus, vehicula at euismod et, aliquam vel nibh. Aliquam tristique magna massa, a auctor massa viverra quis. Morbi in lacus quis nisl auctor mattis. Sed at ante ante.",
                "Quisque fermentum tellus nec eros fermentum sagittis. Phasellus tempor magna dictum suscipit fringilla. Cras vitae elementum enim, in hendrerit felis. Sed congue elit eget erat pretium, ut pellentesque nisi molestie. In a finibus arcu. Ut mollis elit at odio dictum, id efficitur lorem luctus. Fusce luctus et tellus id feugiat. Proin velit risus, bibendum non lorem in, rutrum lobortis elit. Quisque mattis auctor nisl eu pellentesque. Proin commodo erat sit amet scelerisque viverra. Integer est tortor, dignissim nec diam eget, tristique dignissim orci. Donec auctor ante id leo commodo, non accumsan tortor gravida. Sed euismod ligula at orci consequat, vel aliquam justo interdum.",
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer ut elementum purus, at aliquet dui. Aliquam vel neque lorem. Cras fermentum interdum condimentum. Duis venenatis, quam nec hendrerit dapibus, erat dui mollis tortor, sed condimentum enim justo nec odio. Maecenas venenatis nulla eu tortor pretium venenatis. Nunc porttitor molestie augue, vel mollis leo. Donec eleifend non tellus ut mattis. Nulla facilisi.",
                "Nulla facilisi. Suspendisse quam ex, finibus tristique lorem sit amet, vestibulum tempor odio. Proin venenatis nisi ac sapien consequat sodales. Donec nec semper leo, sit amet rutrum mauris. Donec in iaculis risus. Sed eget interdum quam. In tincidunt arcu non purus efficitur, sit amet porta purus consectetur. Fusce pellentesque, dui nec pellentesque cursus, augue odio facilisis ligula, in mattis turpis nisl vel massa. Integer consectetur nisl id pharetra accumsan. Nunc non laoreet nunc. Praesent eu porttitor tortor. Curabitur bibendum pharetra vestibulum. Aliquam dolor urna, aliquam sed posuere eu, mattis in lectus. Pellentesque quis pellentesque leo.",
                "In finibus eros urna, vitae cursus arcu ultricies imperdiet. Pellentesque accumsan viverra mattis. Phasellus lacus justo, ornare in neque id, accumsan venenatis tortor. Quisque dictum mauris in convallis finibus. Morbi luctus ipsum vitae ligula volutpat, sit amet placerat ex iaculis. Duis consectetur consequat sapien vitae tempor. Duis tincidunt magna et augue volutpat consequat. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nullam purus nulla, sollicitudin a sapien in, elementum bibendum massa. Morbi laoreet cursus lorem ac tincidunt. Donec convallis malesuada nibh, quis blandit lectus sagittis id. Pellentesque volutpat libero nec tellus varius eleifend."
            };

            string imageJson = JsonConvert.SerializeObject(images);
            string detailJson = JsonConvert.SerializeObject(details);

            ReferendumTable items = new ReferendumTable()
            {
                ServerId = 1,
                Name = "Palmerston North",
                Detail = detailJson,
                Images = imageJson 
            };

            return items;
        }
    }
}
