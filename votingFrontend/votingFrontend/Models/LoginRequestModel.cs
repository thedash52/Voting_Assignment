// <copyright file="LoginRequestModel.cs" company="UCOL 3rd Year Bachelor of Information and Communication Assignment">
// Copyright (c) UCOL 3rd Year Bachelor of Information and Communication Assignment. All rights reserved.
// </copyright>

namespace VotingFrontend.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Request Model for Login
    /// </summary>
    public class LoginRequestModel
    {
        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "dob")]
        public string Dob { get; set; }

        [JsonProperty(PropertyName = "electoralId")]
        public string ElectoralId { get; set; }
    }
}
