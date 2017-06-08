using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace votingBackend
{
    public class UserVote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DoB { get; set; }
        public string ElectoralId { get; set; }
        public int? ElectorateId { get; set; }
        public string CandidateIds { get; set; }
        public int? PartyId { get; set; }
        public bool? Referendum { get; set; }
        public bool VoteSaved { get; set; }
    }
}
