// <copyright file="CandidateTable.cs" company="UCOL 3rd Year Bachelor of Information and Communication Assignment">
// Copyright (c) UCOL 3rd Year Bachelor of Information and Communication Assignment. All rights reserved.
// </copyright>

namespace VotingFrontend.DatabaseTables
{
    using SQLite.Net.Attributes;

    /// <summary>
    /// Database Table Model for the Candidates
    /// </summary>
    public class CandidateTable
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }

        public int ServerId { get; set; }

        public string Name { get; set; }

        public string Detail { get; set; }

        public string Image { get; set; }
    }
}
