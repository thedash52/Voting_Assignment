using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using votingBackend.EntityFrameworkCore;
using votingBackend.EntityFrameworkCore.Repositories;
using votingBackend.VoteData.Dto;

namespace votingBackend.VoteData
{
    public class VoteDataAppService : votingBackendAppServiceBase, IVoteDataAppService
    {
        private IVoteDataRepository _voteRepository;

        public VoteDataAppService()
        {
            _voteRepository = new VoteDataRepository();
        }

        public Task<Tuple<string, bool>> NewReferendum(string name, string detail, string images)
        {
            Tuple<string, bool> result = _voteRepository.NewReferendum(name, detail, images);

            return Task.FromResult(result);
        }

        public Task<Tuple<string, bool>> AddParty(string name, string detail, string image)
        {
            Tuple<string, bool> result = _voteRepository.AddParty(name, detail, image);

            return Task.FromResult(result);
        }

        public Task<Tuple<string, bool>> AddCandidate(string name, string detail, string image)
        {
            Tuple<string, bool> result = _voteRepository.AddCandidate(name, detail, image);

            return Task.FromResult(result);
        }

        public Task<Tuple<string, bool>> AddElectorate(string name, string detail, string image)
        {
            Tuple<string, bool> result = _voteRepository.AddElectorate(name, detail, image);

            return Task.FromResult(result);
        }

        public Task<Tuple<List<ElectorateDto>, string, bool>> GetElectorates()
        {
            Tuple<List<Electorate>, string, bool> result = _voteRepository.GetElectorates();

            List<ElectorateDto> newList = new List<ElectorateDto>();
            newList = Mapper.Map<List<Electorate>, List<ElectorateDto>>(result.Item1);

            return Task.FromResult(Tuple.Create(newList, result.Item2, result.Item3));
        }

        public Task<Tuple<List<CandidateDto>, string, bool>> GetCandidates()
        {
            Tuple<List<Candidate>, string, bool> result = _voteRepository.GetCandidates();

            List<CandidateDto> newList = new List<CandidateDto>();
            newList = Mapper.Map<List<Candidate>, List<CandidateDto>>(result.Item1);

            return Task.FromResult(Tuple.Create(newList, result.Item2, result.Item3));
        }

        public Task<Tuple<List<PartyDto>, string, bool>> GetParties()
        {
            Tuple<List<Party>, string, bool> result = _voteRepository.GetParties();

            List<PartyDto> newList = new List<PartyDto>();
            newList = Mapper.Map<List<Party>, List<PartyDto>>(result.Item1);

            return Task.FromResult(Tuple.Create(newList, result.Item2, result.Item3));
        }

        public Task<Tuple<ReferendumDto, string, bool>> GetReferendum()
        {
            Tuple<Referendum, string, bool> result = _voteRepository.GetReferendum();

            ReferendumDto newList = new ReferendumDto();
            newList = Mapper.Map<Referendum, ReferendumDto>(result.Item1);

            return Task.FromResult(Tuple.Create(newList, result.Item2, result.Item3));
        }
    }
}
