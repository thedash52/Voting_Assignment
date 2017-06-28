// <copyright file="RestService.cs" company="UCOL 3rd Year Bachelor of Information and Communication Assignment">
// Copyright (c) UCOL 3rd Year Bachelor of Information and Communication Assignment. All rights reserved.
// </copyright>

namespace VotingFrontend.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using VotingFrontend.DatabaseTables;
    using VotingFrontend.Models;
    using Windows.ApplicationModel.Resources;

    /// <summary>
    /// Service controlling Restful server calls
    /// </summary>
    internal class RestService
    {
        // private objects
        private DatabaseService db = new DatabaseService();
        private ResourceLoader resource;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestService"/> class.
        /// </summary>
        public RestService()
        {
            this.resource = new ResourceLoader();
        }

        /// <summary>
        /// Sends login details to server for login
        /// </summary>
        /// <param name="firstName">String containing first name</param>
        /// <param name="lastName">String containing last name</param>
        /// <param name="dob">DateTime object containing date of birth</param>
        /// <param name="electoralId">String containing electora id</param>
        /// <returns>Returns User details from server or a null result if it fails</returns>
        internal async Task<UserVoteTable> Login(string firstName, string lastName, DateTime dob, string electoralId)
        {
            string url = this.resource.GetString("BaseUrl") + "/api/UserContoller/LoginAsync";
            string contentType = this.resource.GetString("ContentType");

            LoginRequestModel loginModel = new LoginRequestModel()
            {
                FirstName = firstName,
                LastName = lastName,
                Dob = dob.Date.ToString("D"),
                ElectoralId = electoralId
            };

            string json = JsonConvert.SerializeObject(loginModel);
            HttpContent content = new StringContent(json, Encoding.UTF8, contentType);
            HttpResponseMessage response;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    response = await client.PostAsync(url, content);
                }
                catch
                {
                    return null;
                }
            }

            if (response.IsSuccessStatusCode)
            {
                LoginUserResponse result = JsonConvert.DeserializeObject<LoginUserResponse>(response.Content.ReadAsStringAsync().Result);

                if (result.Success)
                {
                    UserVoteTable newUser = new UserVoteTable()
                    {
                        ServerId = result.Result.Id,
                        FirstName = result.Result.FirstName,
                        LastName = result.Result.LastName,
                        DoB = result.Result.DoB,
                        ElectoralId = result.Result.ElectoralId,
                        ElectorateId = result.Result.ElectorateId,
                        CandidateIds = result.Result.CandidateIds,
                        PartyId = result.Result.PartyId,
                        Referendum = result.Result.Referendum,
                        VoteSaved = result.Result.VoteSaved
                    };

                    return newUser;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Contacts the server for all electorates
        /// </summary>
        /// <returns>Returns the electorate data received from the server or a null result if it fails</returns>
        internal async Task<List<ElectorateTable>> GetElectorates()
        {
            string url = this.resource.GetString("BaseUrl") + "/api/VoteData/GetElectoratesAsync";

            var get = new HttpRequestMessage(HttpMethod.Get, url);
            HttpResponseMessage response;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    response = await client.SendAsync(get);
                }
                catch
                {
                    return null;
                }
            }

            if (response.IsSuccessStatusCode)
            {
                ElectorateResponseModel result = JsonConvert.DeserializeObject<ElectorateResponseModel>(response.Content.ReadAsStringAsync().Result);

                if (result.Success)
                {
                    return result.Result;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Contacts the server for all candidates
        /// </summary>
        /// <returns>Returns the candidate data received from the server or a null result if it fails</returns>
        internal async Task<List<CandidateTable>> GetCandidates()
        {
            string url = this.resource.GetString("BaseUrl") + "/api/VoteData/GetCandidatesAsync";

            var get = new HttpRequestMessage(HttpMethod.Get, url);
            HttpResponseMessage response;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    response = await client.SendAsync(get);
                }
                catch
                {
                    return null;
                }
            }

            if (response.IsSuccessStatusCode)
            {
                CandidateResponseModel result = JsonConvert.DeserializeObject<CandidateResponseModel>(response.Content.ReadAsStringAsync().Result);

                if (result.Success)
                {
                    return result.Result;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Sends the vote results to the server
        /// </summary>
        /// <returns>Returns the updated user details or a null result if it fails</returns>
        internal async Task<UserVoteTable> SendVote()
        {
            string url = this.resource.GetString("BaseUrl") + "/api/UserContoller/SaveVoteAsync";
            string contentType = this.resource.GetString("ContentType");

            UserVoteTable voteToSend = new UserVoteTable();
            voteToSend = this.db.GetVoteToSend();

            VoteSendRequestModel sendRequest = new VoteSendRequestModel()
            {
                Id = voteToSend.ServerId,
                ElectorateId = voteToSend.ElectorateId,
                CandidateIds = voteToSend.CandidateIds,
                PartyId = voteToSend.PartyId,
                Referendum = voteToSend.Referendum
            };

            string json = JsonConvert.SerializeObject(sendRequest);
            HttpContent content = new StringContent(json, Encoding.UTF8, contentType);
            HttpResponseMessage response;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    response = await client.PostAsync(url, content);
                }
                catch
                {
                    return voteToSend;
                }
            }

            if (response.IsSuccessStatusCode)
            {
                VoteSentResponseModel result = JsonConvert.DeserializeObject<VoteSentResponseModel>(response.Content.ReadAsStringAsync().Result);

                if (result.Success)
                {
                    voteToSend = this.db.VoteSent(voteToSend);

                    return voteToSend;
                }
                else
                {
                    return voteToSend;
                }
            }
            else
            {
                return voteToSend;
            }
        }

        /// <summary>
        /// Contacts the server for all parties
        /// </summary>
        /// <returns>Returns the party data received from the server or a null result if it fails</returns>
        internal async Task<List<PartyTable>> GetParties()
        {
            string url = this.resource.GetString("BaseUrl") + "/api/VoteData/GetPartiesAsync";

            var get = new HttpRequestMessage(HttpMethod.Get, url);
            HttpResponseMessage response;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    response = await client.SendAsync(get);
                }
                catch
                {
                    return null;
                }
            }

            if (response.IsSuccessStatusCode)
            {
                PartyResponseModel result = JsonConvert.DeserializeObject<PartyResponseModel>(response.Content.ReadAsStringAsync().Result);

                if (result.Success)
                {
                    return result.Result;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Contacts the server for the referendum
        /// </summary>
        /// <returns>Returns the referendum data received from the server or a null result if it fails</returns>
        internal async Task<ReferendumTable> GetReferendum()
        {
            string url = this.resource.GetString("BaseUrl") + "/api/VoteData/GetReferendumAsync";

            var get = new HttpRequestMessage(HttpMethod.Get, url);
            HttpResponseMessage response;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    response = await client.SendAsync(get);
                }
                catch
                {
                    return null;
                }
            }

            if (response.IsSuccessStatusCode)
            {
                ReferendumResponseModel result = JsonConvert.DeserializeObject<ReferendumResponseModel>(response.Content.ReadAsStringAsync().Result);

                if (result.Success)
                {
                    return result.Result;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
