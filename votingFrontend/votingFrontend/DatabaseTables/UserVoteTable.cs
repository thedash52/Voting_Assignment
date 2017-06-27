// <copyright file="UserVoteTable.cs" company="UCOL 3rd Year Bachelor of Information and Communication Assignment">
// Copyright (c) UCOL 3rd Year Bachelor of Information and Communication Assignment. All rights reserved.
// </copyright>

namespace VotingFrontend.DatabaseTables
{
    using SQLite.Net.Attributes;

    /// <summary>
    /// Database Table Model for User Details and Vote Storage
    /// </summary>
    public class UserVoteTable
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }

        public int ServerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DoB { get; set; }

        public string ElectoralId { get; set; }

        public int ElectorateId { get; set; }

        public string CandidateIds { get; set; }

        public int PartyId { get; set; }

        public bool Referendum { get; set; }

        public bool Active { get; set; } = false;

        public bool VoteSaved { get; set; } = false;
    }
}
