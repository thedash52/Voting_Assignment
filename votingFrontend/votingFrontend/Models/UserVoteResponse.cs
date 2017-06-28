// <copyright file="UserVoteResponse.cs" company="UCOL 3rd Year Bachelor of Information and Communication Assignment">
// Copyright (c) UCOL 3rd Year Bachelor of Information and Communication Assignment. All rights reserved.
// </copyright>

namespace VotingFrontend.Models
{
    /// <summary>
    /// User data response model
    /// </summary>
    public class UserVoteResponse
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DoB { get; set; }

        public string ElectoralId { get; set; }

        public int ElectorateId { get; set; }

        public string CandidateIds { get; set; }

        public int PartyId { get; set; }

        public bool Referendum { get; set; }

        public bool VoteSaved { get; set; } = false;
    }
}
