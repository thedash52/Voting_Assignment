// <copyright file="DatabaseService.cs" company="UCOL 3rd Year Bachelor of Information and Communication Assignment">
// Copyright (c) UCOL 3rd Year Bachelor of Information and Communication Assignment. All rights reserved.
// </copyright>

namespace VotingFrontend.Services
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using SQLite.Net;
    using VotingFrontend.DatabaseTables;
    using VotingFrontend.Models;

    /// <summary>
    /// Service that handles all data manipulation
    /// </summary>
    public class DatabaseService
    {
        // private database object
        private SQLiteConnection db;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseService"/> class.
        /// </summary>
        public DatabaseService()
        {
            var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "this.db.sqlite");
            this.db = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);

            this.db.CreateTable<UserVoteTable>();
            this.db.CreateTable<ElectorateTable>();
            this.db.CreateTable<CandidateTable>();
            this.db.CreateTable<PartyTable>();
            this.db.CreateTable<ReferendumTable>();
        }

        /// <summary>
        /// Sets login status of user to active
        /// </summary>
        /// <param name="user">Model containing current user data</param>
        internal void VoterLoggedIn(UserVoteTable user)
        {
            this.db.Query<UserVoteTable>("UPDATE UserVoteTable SET Active = 0");

            this.db.Insert(user);
        }

        /// <summary>
        /// Checks if the login details match current records
        /// </summary>
        /// <param name="firstName">string containing first name</param>
        /// <param name="lastName">string containing last name</param>
        /// <param name="doB">DateTime object containing date of birth</param>
        /// <param name="electoralId">String containing electoral id</param>
        /// <returns>Returns the user details if found, if not returns null</returns>
        internal UserVoteTable CheckVoter(string firstName, string lastName, DateTime doB, string electoralId)
        {
            List<UserVoteTable> userDB = new List<UserVoteTable>();
            userDB = this.db.Query<UserVoteTable>("SELECT * FROM UserVoteTable WHERE FirstName = '" + firstName + "' AND LastName = '" + lastName + "' AND DoB LIKE '" + doB.Date.ToString("d") + "%' AND ElectoralId = '" + electoralId + "'");

            if (userDB.Count != 1)
            {
                return null;
            }

            return userDB[0];
        }

        /// <summary>
        /// Get the referendum from the database
        /// </summary>
        /// <returns>Returns the referendum data</returns>
        internal ReferendumTable GetReferendum()
        {
            ReferendumTable referendum = new ReferendumTable();
            referendum = this.db.Query<ReferendumTable>("SELECT * FROM ReferendumTable")[0];

            return referendum;
        }

        /// <summary>
        /// Gets all the candidates from the database
        /// </summary>
        /// <returns>Returns the candidate data</returns>
        internal ObservableCollection<CandidateSelection> GetCandidates()
        {
            List<CandidateTable> dbcandidates = new List<CandidateTable>();
            dbcandidates = this.db.Query<CandidateTable>("SELECT * FROM CandidateTable");

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

        /// <summary>
        /// Gets the party from the database using the party id
        /// </summary>
        /// <param name="partyId">Int containing the party id</param>
        /// <returns>Returns the party data relating to the id if found, otherwise returns null</returns>
        internal PartyTable GetPartyFromId(int partyId)
        {
            List<PartyTable> parties = new List<PartyTable>();
            parties = this.db.Query<PartyTable>("SELECT * FROM PartyTable WHERE Id = " + partyId);

            if (parties.Count != 1)
            {
                return null;
            }

            return parties[0];
        }

        /// <summary>
        /// Gets the candidate from the database using the canididate id
        /// </summary>
        /// <param name="id">Int containing the candidate id</param>
        /// <returns>Returns the candidate data relating to the id if found, otherwise returns null</returns>
        internal CandidateTable GetCandidateFromId(int id)
        {
            List<CandidateTable> candidates = new List<CandidateTable>();
            candidates = this.db.Query<CandidateTable>("SELECT * FROM CandidateTable WHERE Id = " + id);

            if (candidates.Count != 1)
            {
                return null;
            }

            return candidates[0];
        }

        /// <summary>
        /// Gets the electorate from the database using the electorate id
        /// </summary>
        /// <param name="electorateId">Int containing the electorate id</param>
        /// <returns>Returns the electorate data relating to the id if found, otherwise returns null</returns>
        internal ElectorateTable GetElectorateFromId(int electorateId)
        {
            List<ElectorateTable> electorates = new List<ElectorateTable>();
            electorates = this.db.Query<ElectorateTable>("SELECT * FROM ElectorateTable WHERE Id = " + electorateId);

            if (electorates.Count != 1)
            {
                return null;
            }

            return electorates[0];
        }

        /// <summary>
        /// Gets all the parties from the database
        /// </summary>
        /// <returns>Returns the party data</returns>
        internal List<PartyTable> GetParties()
        {
            List<PartyTable> parties = new List<PartyTable>();
            parties = this.db.Query<PartyTable>("SELECT * FROM PartyTable");

            return parties;
        }

        /// <summary>
        /// Gets all the electorates from the database
        /// </summary>
        /// <returns>Returns the electorate data</returns>
        internal List<ElectorateTable> GetElectorates()
        {
            List<ElectorateTable> electorates = new List<ElectorateTable>();
            electorates = this.db.Query<ElectorateTable>("SELECT * FROM ElectorateTable");

            return electorates;
        }

        /// <summary>
        /// Changes the active state of the user
        /// </summary>
        /// <param name="user">Model containing the users data</param>
        /// <returns>Returns the updated user data</returns>
        internal UserVoteTable SwitchActive(UserVoteTable user)
        {
            this.db.Query<UserVoteTable>("UPDATE UserVoteTable SET Active = '" + !user.Active + "' WHERE Id = '" + user.Id + "'");

            return user;
        }

        /// <summary>
        /// Saves the referendum result to the user
        /// </summary>
        /// <param name="result">Bool containing the referendum result</param>
        /// <returns>Returns an int that is either -1 for a fail and a 1 for a success</returns>
        internal int AddReferendumVote(bool result)
        {
            List<UserVoteTable> users = new List<UserVoteTable>();
            users = this.db.Query<UserVoteTable>("SELECT * FROM UserVoteTable WHERE Active = 1");

            if (users.Count != 1)
            {
                return -1;
            }

            UserVoteTable user = new UserVoteTable();
            user = users[0];
            user.Referendum = result;

            this.db.Update(user);

            return 1;
        }

        /// <summary>
        /// Updates the referendum in the database
        /// </summary>
        /// <param name="referendum">Model containing the referendum data to be updated</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        internal Task UpdateReferendum(ReferendumTable referendum)
        {
            this.db.DropTable<ReferendumTable>();
            this.db.CreateTable<ReferendumTable>();

            this.db.Insert(referendum);

            return Task.FromResult(-1);
        }

        /// <summary>
        /// Saves the candidates chosen to the user
        /// </summary>
        /// <param name="sender">Model list containing the list of candidate vote result</param>
        /// <returns>Returns a -1 for a fail result and a 1 for a success result</returns>
        internal int AddCandidateVote(List<CandidateTable> sender)
        {
            List<UserVoteTable> users = new List<UserVoteTable>();
            users = this.db.Query<UserVoteTable>("SELECT * FROM UserVoteTable WHERE Active = 1");

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

            this.db.Update(user);

            return 1;
        }

        /// <summary>
        /// Saves the electorate chosen to the user
        /// </summary>
        /// <param name="sender">Model containing the electorate vote result</param>
        /// <returns>Returns a -1 for a fail result and a 1 for a success result</returns>
        internal int AddElectorateVote(ElectorateTable sender)
        {
            List<UserVoteTable> users = new List<UserVoteTable>();
            users = this.db.Query<UserVoteTable>("SELECT * FROM UserVoteTable WHERE Active = 1");

            if (users.Count != 1)
            {
                return -1;
            }

            UserVoteTable user = new UserVoteTable();
            user = users[0];
            user.ElectorateId = sender.Id;

            this.db.Update(user);

            return 1;
        }

        /// <summary>
        /// Updates the parties in the database
        /// </summary>
        /// <param name="parties">Model list containing a list of the party data to be updated</param>
        /// /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        internal Task UpdateParties(List<PartyTable> parties)
        {
            this.db.DropTable<PartyTable>();
            this.db.CreateTable<PartyTable>();

            this.db.InsertAll(parties);

            return Task.FromResult(-1);
        }

        /// <summary>
        /// Updates the candidates in the database
        /// </summary>
        /// <param name="candidates">Model list containing a list of the candidate data to be updated</param>
        /// /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        internal Task UpdateCandidates(List<CandidateTable> candidates)
        {
            this.db.DropTable<CandidateTable>();
            this.db.CreateTable<CandidateTable>();

            this.db.InsertAll(candidates);

            return Task.FromResult(-1);
        }

        /// <summary>
        /// Updates the user to having the vote being sent to the server and to deactive
        /// </summary>
        /// <param name="voteToSend">Model containing the user details for status update</param>
        /// <returns>Returns the updated user details</returns>
        internal UserVoteTable VoteSent(UserVoteTable voteToSend)
        {
            voteToSend.VoteSaved = true;
            voteToSend.Active = false;

            this.db.Update(voteToSend);

            return voteToSend;
        }

        /// <summary>
        /// Gets the current user details that contain the vote results
        /// </summary>
        /// <returns>Returns the user details if found otherwise returns null</returns>
        internal UserVoteTable GetVoteToSend()
        {
            List<UserVoteTable> users = new List<UserVoteTable>();
            users = this.db.Query<UserVoteTable>("SELECT * FROM UserVoteTable WHERE Active = 1");

            if (users.Count != 1)
            {
                return null;
            }

            return users[0];
        }

        /// <summary>
        /// Updates the electorates in the database
        /// </summary>
        /// <param name="electorates">Model list containing a list of the electorate data to be updated</param>
        /// /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        internal Task UpdateElectorates(List<ElectorateTable> electorates)
        {
            this.db.DropTable<ElectorateTable>();
            this.db.CreateTable<ElectorateTable>();

            this.db.InsertAll(electorates);

            return Task.FromResult(-1);
        }

        /// <summary>
        /// Saves the party chosen to the user
        /// </summary>
        /// <param name="sender">Model containing the party vote result</param>
        /// <returns>Returns a -1 for a fail result and a 1 for a success result</returns>
        internal int AddPartyVote(PartyTable sender)
        {
            List<UserVoteTable> users = new List<UserVoteTable>();
            users = this.db.Query<UserVoteTable>("SELECT * FROM UserVoteTable WHERE Active = 1");

            if (users.Count != 1)
            {
                return -1;
            }

            UserVoteTable user = new UserVoteTable();
            user = users[0];
            user.PartyId = sender.Id;

            this.db.Update(user);

            return 1;
        }

        /// <summary>
        /// Sets active status of all users to inactive
        /// </summary>
        internal void DeactivateUsers()
        {
            this.db.Query<UserVoteTable>("UPDATE UserVoteTable SET Active = false");
        }

        /// <summary>
        /// Checks the database to see if it has data in the Electorate, Candidate, Party, and Referendum tables
        /// </summary>
        /// <returns>Returns true if all the tables contain data otherwise returns false</returns>
        internal bool CheckData()
        {
            List<ElectorateTable> electorates = new List<ElectorateTable>();
            List<CandidateTable> candidates = new List<CandidateTable>();
            List<PartyTable> parties = new List<PartyTable>();
            List<ReferendumTable> referendum = new List<ReferendumTable>();

            electorates = this.db.Query<ElectorateTable>("SELECT * FROM ElectorateTable");
            candidates = this.db.Query<CandidateTable>("SELECT * FROM CandidateTable");
            parties = this.db.Query<PartyTable>("SELECT * FROM PartyTable");
            referendum = this.db.Query<ReferendumTable>("SELECT * FROM ReferendumTable");

            if (electorates.Count == 0 || candidates.Count == 0 || parties.Count == 0 || referendum.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}