using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using votingBackend.EntityFrameworkCore.Models;

namespace votingBackend.EntityFrameworkCore.Repositories
{
    public interface IUserRespository : IRepository
    {
        Tuple<AuthenticationModel, string, bool> Authenticate(string first, string last, string dob, string electoral);

        Tuple<string, bool> Register(string first, string last, string dob, string electoralId);

        Tuple<string, bool> SaveVote(int id, int electorateId, string candidateIds, int partyId, bool referendum);
    }
}
