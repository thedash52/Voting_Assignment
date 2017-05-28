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
            
        }

        internal UserVoteTable CheckVoter(string firstName, string lastName, DateTime doB, string electoralId)
        {
            throw new NotImplementedException();
        }
    }
}
