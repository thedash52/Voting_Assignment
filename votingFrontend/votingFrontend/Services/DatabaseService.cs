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

        internal ReferendumTable GetReferendum()
        {
            ReferendumTable referendum = new ReferendumTable();
            referendum = db.Query<ReferendumTable>("SELECT * FROM ReferendumTable")[0];

            return referendum;
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

        internal PartyTable GetPartyFromId(int partyId)
        {
            List<PartyTable> parties = new List<PartyTable>();
            parties = db.Query<PartyTable>("SELECT * FROM PartyTable WHERE Id = " + partyId);

            if (parties.Count != 1)
            {
                return null;
            }

            return parties[0];
        }

        internal CandidateTable GetCandidateFromId(int id)
        {
            List<CandidateTable> candidates = new List<CandidateTable>();
            candidates = db.Query<CandidateTable>("SELECT * FROM CandidateTable WHERE Id = " + id);

            if (candidates.Count != 1)
            {
                return null;
            }

            return candidates[0];
        }

        internal ElectorateTable GetElectorateFromId(int electorateId)
        {
            List<ElectorateTable> electorates = new List<ElectorateTable>();
            electorates = db.Query<ElectorateTable>("SELECT * FROM ElectorateTable WHERE Id = " + electorateId);

            if (electorates.Count != 1)
            {
                return null;
            }

            return electorates[0];
        }

        internal List<PartyTable> GetParties()
        {
            List<PartyTable> parties = new List<PartyTable>();
            parties = db.Query<PartyTable>("SELECT * FROM PartyTable");

            return parties;
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

        internal int AddReferendumVote(bool v)
        {
            List<UserVoteTable> users = new List<UserVoteTable>();
            users = db.Query<UserVoteTable>("SELECT * FROM UserVoteTable WHERE Active = 1");

            if (users.Count != 1)
            {
                return -1;
            }

            UserVoteTable user = new UserVoteTable();
            user = users[0];
            user.Referendum = v;

            db.Update(user);

            return 1;
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

        internal UserVoteTable VoteSent(UserVoteTable voteToSend)
        {
            voteToSend.VoteSaved = true;
            voteToSend.Active = false;

            db.Update(voteToSend);

            return voteToSend;
        }

        internal UserVoteTable GetVoteToSend()
        {
            List<UserVoteTable> users = new List<UserVoteTable>();
            users = db.Query<UserVoteTable>("SELECT * FROM UserVoteTable WHERE Active = 1");

            if (users.Count != 1)
            {
                return null;
            }

            return users[0];
        }

        internal Task UpdateElectorates(List<ElectorateTable> electorates)
        {
            db.DropTable<ElectorateTable>();
            db.CreateTable<ElectorateTable>();

            db.InsertAll(electorates);

            return Task.FromResult(-1);
        }

        internal int AddPartyVote(PartyTable sender)
        {
            List<UserVoteTable> users = new List<UserVoteTable>();
            users = db.Query<UserVoteTable>("SELECT * FROM UserVoteTable WHERE Active = 1");

            if (users.Count != 1)
            {
                return -1;
            }

            UserVoteTable user = new UserVoteTable();
            user = users[0];
            user.PartyId = sender.Id;

            db.Update(user);

            return 1;
        }

        internal void DeactivateUsers()
        {
            db.Query<UserVoteTable>("UPDATE UserVoteTable SET Active = false");
        }
    }
}
