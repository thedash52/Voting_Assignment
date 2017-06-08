using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using votingBackend.EntityFrameworkCore.Models;

namespace votingBackend.CustomUser.Dto
{
    [AutoMap(typeof(AuthenticationModel))]
    public class LoginDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Dob { get; set; }
        public string ElectoralId { get; set; }
        public bool VoteSaved { get; set; }
    }
}
