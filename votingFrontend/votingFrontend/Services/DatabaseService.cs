using Newtonsoft.Json;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using votingFrontend.DatabaseTables;
using votingFrontend.Models;

namespace votingFrontend.Services
{
    public class DatabaseService
    {
        private SQLiteConnection db;

        public DatabaseService()
        {
            var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            db = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);

            db.CreateTable<UserVoteTable>();
            db.CreateTable<ElectorateTable>();
            db.CreateTable<CandidateTable>();
            db.CreateTable<PartyTable>();
            db.CreateTable<ReferendumTable>();
        }

        internal void VoterLoggedIn(UserVoteTable user)
        {
            db.Query<UserVoteTable>("UPDATE UserVoteTable SET Active = 0");

            db.Insert(user);
        }

        internal UserVoteTable CheckVoter(string firstName, string lastName, DateTime doB, string electoralId)
        {
            List<UserVoteTable> userDB = new List<UserVoteTable>();
            userDB = db.Query<UserVoteTable>("SELECT * FROM UserVoteTable WHERE FirstName = '" + firstName + "' AND LastName = '" + lastName + "' AND DoB LIKE '" + doB.Date.ToString("d") + "%' AND ElectoralId = '" + electoralId + "'");

            if (userDB.Count != 1)
            {
                return null;
            }

            return userDB[0];
        }

        internal ObservableCollection<CandidateSelection> GetCandidates()
        {
            List<CandidateTable> dbcandidates = new List<CandidateTable>();
            dbcandidates = db.Query<CandidateTable>("SELECT * FROM CandidateTable");

            ObservableCollection<CandidateSelection> candidates = new ObservableCollection<CandidateSelection>();

            foreach (CandidateTable item in dbcandidates)
            {
                CandidateSelection candidate = new CandidateSelection()
                {
                    Id = item.Id,
                    ServerId = item.ServerId,
                    Name = item.Name,
                    Detail = item.Detail,
                    Image = item.Image,
                    Selected = false
                };

                candidates.Add(candidate);
            }

            return candidates;
        }

        internal List<ElectorateTable> GetElectorates()
        {
            List<ElectorateTable> electorates = new List<ElectorateTable>();
            electorates = db.Query<ElectorateTable>("SELECT * FROM ElectorateTable");

            return electorates;
        }

        internal UserVoteTable SwitchActive(UserVoteTable user)
        {
            db.Query<UserVoteTable>("UPDATE UserVoteTable SET Active = '" + !user.Active + "' WHERE Id = '" + user.Id + "'");

            return user;
        }

        internal Task UpdateReferendum(ReferendumTable referendum)
        {
            db.DropTable<ReferendumTable>();
            db.CreateTable<ReferendumTable>();

            db.Insert(referendum);

            return Task.FromResult(-1);
        }

        internal int AddCandidateVote(List<CandidateTable> sender)
        {
            List<UserVoteTable> users = new List<UserVoteTable>();
            users = db.Query<UserVoteTable>("SELECT * FROM UserVoteTable WHERE Active = 1");

            if (users.Count != 1)
            {
                return -1;
            }

            List<int> choosenCandidates = new List<int>();

            foreach (CandidateTable item in sender)
            {
                choosenCandidates.Add(item.Id);
            }

            string json = JsonConvert.SerializeObject(choosenCandidates);

            UserVoteTable user = new UserVoteTable();
            user = users[0];
            user.CandidateIds = json;

            db.Update(user);

            return 1;
        }

        internal int AddElectorateVote(ElectorateTable sender)
        {
            List<UserVoteTable> users = new List<UserVoteTable>();
            users = db.Query<UserVoteTable>("SELECT * FROM UserVoteTable WHERE Active = 1");

            if (users.Count != 1)
            {
                return -1;
            }

            UserVoteTable user = new UserVoteTable();
            user = users[0];
            user.ElectorateId = sender.Id;

            db.Update(user);

            return 1;
        }

        internal Task UpdateParties(List<PartyTable> parties)
        {
            db.DropTable<PartyTable>();
            db.CreateTable<PartyTable>();

            db.InsertAll(parties);

            return Task.FromResult(-1);
        }

        internal Task UpdateCandidates(List<CandidateTable> candidates)
        {
            db.DropTable<CandidateTable>();
            db.CreateTable<CandidateTable>();

            db.InsertAll(candidates);

            return Task.FromResult(-1);
        }

        internal Task UpdateElectorates(List<ElectorateTable> electorates)
        {
            db.DropTable<ElectorateTable>();
            db.CreateTable<ElectorateTable>();

            db.InsertAll(electorates);

            return Task.FromResult(-1);
        }
    }
}
