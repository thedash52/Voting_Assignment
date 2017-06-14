using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace votingFrontend.Models
{
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
