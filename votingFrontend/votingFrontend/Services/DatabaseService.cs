using Newtonsoft.Json;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using votingFrontend.DatabaseTables;

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

        internal List<CandidateTable> GetCandidates()
        {
            List<CandidateTable> candidates = new List<CandidateTable>();
            candidates = db.Query<CandidateTable>("SELECT * FROM CandidateTable");

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

        internal Task UpdateReferendum(List<ReferendumTable> referendum)
        {
            foreach (ReferendumTable item in referendum)
            {
                List<ReferendumTable> dbreferendum = new List<ReferendumTable>();
                dbreferendum = db.Query<ReferendumTable>("SELECT * FROM ReferendumTable WHERE ServerId = " + item.ServerId);

                if (dbreferendum.Count == 1)
                {
                    ReferendumTable single = new ReferendumTable();
                    single = dbreferendum[0];

                    bool updateRecord = false;

                    if (item.Name != single.Name)
                    {
                        updateRecord = true;
                    }

                    if (item.Detail != single.Detail)
                    {
                        updateRecord = true;
                    }

                    if (item.Images != single.Images)
                    {
                        updateRecord = true;
                    }

                    if (updateRecord)
                    {
                        single = new ReferendumTable()
                        {
                            Id = dbreferendum[0].Id,
                            ServerId = item.ServerId,
                            Name = item.Name,
                            Detail = item.Detail,
                            Images = item.Images
                        };

                        db.Update(single);
                    }
                }
                else if (dbreferendum.Count == 0)
                {
                    db.Insert(item);
                }
            }

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
            foreach (PartyTable item in parties)
            {
                List<PartyTable> dbParties = new List<PartyTable>();
                dbParties = db.Query<PartyTable>("SELECT * FROM PartyTable WHERE ServerId = " + item.ServerId);

                if (dbParties.Count == 1)
                {
                    PartyTable single = new PartyTable();
                    single = dbParties[0];

                    bool updateRecord = false;

                    if (item.Name != single.Name)
                    {
                        updateRecord = true;
                    }

                    if (item.Detail != single.Detail)
                    {
                        updateRecord = true;
                    }

                    if (item.Image != single.Image)
                    {
                        updateRecord = true;
                    }

                    if (updateRecord)
                    {
                        single = new PartyTable()
                        {
                            Id = dbParties[0].Id,
                            ServerId = item.ServerId,
                            Name = item.Name,
                            Detail = item.Detail,
                            Image = item.Image
                        };

                        db.Update(single);
                    }
                }
                else if (dbParties.Count == 0)
                {
                    db.Insert(item);
                }
            }

            return Task.FromResult(-1);
        }

        internal Task UpdateCandidates(List<CandidateTable> candidates)
        {
            foreach (CandidateTable item in candidates)
            {
                List<CandidateTable> dbCandidates = new List<CandidateTable>();
                dbCandidates = db.Query<CandidateTable>("SELECT * FROM CandidateTable WHERE ServerId = " + item.ServerId);

                if (dbCandidates.Count == 1)
                {
                    CandidateTable single = new CandidateTable();
                    single = dbCandidates[0];

                    bool updateRecord = false;

                    if (item.Name != single.Name)
                    {
                        updateRecord = true;
                    }

                    if (item.Detail != single.Detail)
                    {
                        updateRecord = true;
                    }

                    if (item.Image != single.Image)
                    {
                        updateRecord = true;
                    }

                    if (updateRecord)
                    {
                        single = new CandidateTable()
                        {
                            Id = dbCandidates[0].Id,
                            ServerId = item.ServerId,
                            Name = item.Name,
                            Detail = item.Detail,
                            Image = item.Image
                        };

                        db.Insert(single);
                    }
                }
                else if (dbCandidates.Count == 0)
                {
                    db.Insert(item);
                }
            }

            return Task.FromResult(-1);
        }

        internal Task UpdateElectorates(List<ElectorateTable> electorates)
        {
            foreach (ElectorateTable item in electorates)
            {
                List<ElectorateTable> dbelectorates = new List<ElectorateTable>();
                dbelectorates = db.Query<ElectorateTable>("SELECT * FROM ElectorateTable WHERE ServerId = " + item.ServerId);

                if (dbelectorates.Count == 1)
                {
                    ElectorateTable single = new ElectorateTable();
                    single = dbelectorates[0];

                    bool updateRecord = false;

                    if (item.Name != single.Name)
                    {
                        updateRecord = true;
                    }

                    if (item.Detail != single.Detail)
                    {
                        updateRecord = true;
                    }

                    if (item.Image != single.Image)
                    {
                        updateRecord = true;
                    }

                    if (updateRecord)
                    {
                        single = new ElectorateTable()
                        {
                            Id = dbelectorates[0].Id,
                            ServerId = item.ServerId,
                            Name = item.Name,
                            Detail = item.Detail,
                            Image = item.Image
                        };

                        db.Update(single);
                    }
                }
                else if (dbelectorates.Count == 0)
                {
                    db.Insert(item);
                }
            }

            return Task.FromResult(-1);
        }
    }
}
