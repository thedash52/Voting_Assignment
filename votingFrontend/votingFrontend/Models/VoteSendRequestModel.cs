// <copyright file="VoteSendRequestModel.cs" company="UCOL 3rd Year Bachelor of Information and Communication Assignment">
// Copyright (c) UCOL 3rd Year Bachelor of Information and Communication Assignment. All rights reserved.
// </copyright>

namespace VotingFrontend.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Request Model for sending vote results to the server
    /// </summary>
    public class VoteSendRequestModel
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "electorateId")]
        public int ElectorateId { get; set; }

        [JsonProperty(PropertyName = "candidateIds")]
        public string CandidateIds { get; set; }

        [JsonProperty(PropertyName = "partyId")]
        public int PartyId { get; set; }

        [JsonProperty(PropertyName = "referendum")]
        public bool Referendum { get; set; }
    }
}
