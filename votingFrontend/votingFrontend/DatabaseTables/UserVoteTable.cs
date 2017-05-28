using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace votingFrontend.DatabaseTables
{
    public class UserVoteTable
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }

        public int ServerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DoB { get; set; }

        public string ElectoralId { get; set; }

        public bool VoteSaved { get; set; }

        public int ElectorateId { get; set; }

        public int CandidateId { get; set; }

        public int PartyId { get; set; }

        public bool Referendum { get; set; }
    }
}
