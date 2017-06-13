using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using votingBackend.CustomUser.Dto;

namespace votingBackend.CustomUser
{
    public interface ICustomUserAppService : IApplicationService
    {
        Task<Tuple<LoginDto, string, bool>> Login(string first, string last, string dob, string electoral);

        Task<Tuple<string, bool>> Register(string firstName, string lastName, string dob, string electoralId);

        Task<Tuple<string, bool>> SaveVote(int id, int electorateId, string candidateIds, int partyId, bool referendum);
    }
}
