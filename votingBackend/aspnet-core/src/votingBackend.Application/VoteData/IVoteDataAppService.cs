using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using votingBackend.VoteData.Dto;

namespace votingBackend.VoteData
{
    public interface IVoteDataAppService : IApplicationService
    {
        Task<Tuple<string, bool>> NewReferendum(string name, string detail, string images);

        Task<Tuple<string, bool>> AddParty(string name, string detail, string image);

        Task<Tuple<string, bool>> AddCandidate(string name, string detail, string image);

        Task<Tuple<string, bool>> AddElectorate(string name, string detail, string image);

        Task<Tuple<List<ElectorateDto>, string, bool>> GetElectorates();

        Task<Tuple<List<CandidateDto>, string, bool>> GetCandidates();

        Task<Tuple<List<PartyDto>, string, bool>> GetParties();

        Task<Tuple<ReferendumDto, string, bool>> GetReferendum();
    }
}
