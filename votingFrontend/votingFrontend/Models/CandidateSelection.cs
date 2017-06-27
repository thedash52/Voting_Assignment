// <copyright file="CandidateSelection.cs" company="UCOL 3rd Year Bachelor of Information and Communication Assignment">
// Copyright (c) UCOL 3rd Year Bachelor of Information and Communication Assignment. All rights reserved.
// </copyright>

namespace VotingFrontend.Models
{
    /// <summary>
    /// Model for the acordian list on the select candidate view
    /// </summary>
    public class CandidateSelection
    {
        public int Id { get; set; }

        public int ServerId { get; set; }

        public string Name { get; set; }

        public string Detail { get; set; }

        public string Image { get; set; }

        public bool Selected { get; set; }
    }
}
