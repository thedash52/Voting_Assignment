using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using votingBackend.CustomUser.Dto;
using votingBackend.EntityFrameworkCore;
using votingBackend.EntityFrameworkCore.Models;
using votingBackend.EntityFrameworkCore.Repositories;

namespace votingBackend.CustomUser
{
    public class CustomUserAppService : votingBackendAppServiceBase, ICustomUserAppService
    {
        private IUserRespository _userRepository;

        public CustomUserAppService()
        {
            _userRepository = new UserRepository();
        }

        public Task<Tuple<LoginDto, string, bool>> Login(string first, string last, string dob, string electoral)
        {
            Tuple<AuthenticationModel, string, bool> result = _userRepository.Authenticate(first, last, dob, electoral);

            LoginDto loginDto = new LoginDto();
            loginDto = Mapper.Map<AuthenticationModel, LoginDto>(result.Item1);

            return Task.FromResult(Tuple.Create(loginDto, result.Item2, result.Item3));
        }

        public Task<Tuple<string, bool>> Register(string firstName, string lastName, string dob, string electoralId)
        {
            Tuple<string, bool> result = _userRepository.Register(firstName, lastName, dob, electoralId);

            return Task.FromResult(result);
        }

        public Task<Tuple<string, bool>> SaveVote(int id, int electorateId, string candidateIds, int partyId, bool referendum) 
        {
            Tuple<string, bool> result = _userRepository.SaveVote(id, electorateId, candidateIds, partyId, referendum);

            return Task.FromResult(result);
        }
    }
}
