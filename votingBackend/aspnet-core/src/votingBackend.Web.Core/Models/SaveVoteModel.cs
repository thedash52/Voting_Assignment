using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace votingBackend.Models
{
    public class SaveVoteModel
    {
        public int Id { get; set; }
        public int ElectorateId { get; set; }
        public string CandidateIds { get; set; }
        public int PartyId { get; set; }
        public bool Referendum { get; set; }
    }
}
