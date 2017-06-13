using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using votingBackend.Models.VoteData;
using votingBackend.VoteData;
using votingBackend.VoteData.Dto;

namespace votingBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    public class VoteDataController : votingBackendControllerBase
    {
        protected IVoteDataAppService _voteDataAppService;

        public VoteDataController()
        {
            _voteDataAppService = new VoteDataAppService();
        }

        [HttpGet]
        public async Task<AjaxResponse> GetElectoratesAsync()
        {
            return await Task<AjaxResponse>.Run(() => GetElectorates());
        }

        private async Task<AjaxResponse> GetElectorates()
        {
            Tuple<List<ElectorateDto>, string, bool> response = await _voteDataAppService.GetElectorates();

            AjaxResponse ar = new AjaxResponse();

            if (response.Item3)
            {
                ar.Success = true;
                ar.Result = response.Item1;
            }
            else
            {
                ar.Success = false;
                ar.Error = new ErrorInfo(response.Item2);
            }

            return ar;
        }

        [HttpGet]
        public async Task<AjaxResponse> GetCandidatesAsync()
        {
            return await Task<AjaxResponse>.Run(() => GetCandidates());
        }

        private async Task<AjaxResponse> GetCandidates()
        {
            Tuple<List<CandidateDto>, string, bool> response = await _voteDataAppService.GetCandidates();

            AjaxResponse ar = new AjaxResponse();

            if (response.Item3)
            {
                ar.Success = true;
                ar.Result = response.Item1;
            }
            else
            {
                ar.Success = false;
                ar.Error = new ErrorInfo(response.Item2);
            }

            return ar;
        }

        [HttpGet]
        public async Task<AjaxResponse> GetPartiesAsync()
        {
            return await Task<AjaxResponse>.Run(() => GetParties());
        }

        private async Task<AjaxResponse> GetParties()
        {
            Tuple<List<PartyDto>, string, bool> response = await _voteDataAppService.GetParties();

            AjaxResponse ar = new AjaxResponse();

            if (response.Item3)
            {
                ar.Success = true;
                ar.Result = response.Item1;
            }
            else
            {
                ar.Success = false;
                ar.Error = new ErrorInfo(response.Item2);
            }

            return ar;
        }

        [HttpGet]
        public async Task<AjaxResponse> GetReferendumAsync()
        {
            return await Task<AjaxResponse>.Run(() => GetReferendum());
        }

        private async Task<AjaxResponse> GetReferendum()
        {
            Tuple<ReferendumDto, string, bool> response = await _voteDataAppService.GetReferendum();

            AjaxResponse ar = new AjaxResponse();

            if (response.Item3)
            {
                ar.Success = true;
                ar.Result = response.Item1;
            }
            else
            {
                ar.Success = false;
                ar.Error = new ErrorInfo(response.Item2);
            }

            return ar;
        }

        [HttpPost]
        public async Task<AjaxResponse> AddElectorateAsync([FromBody] AddElectorateModel newElectorate)
        {
            return await Task<AjaxResponse>.Run(() => AddElectorate(newElectorate));
        }

        private async Task<AjaxResponse> AddElectorate(AddElectorateModel newElectorate)
        {
            Tuple<string, bool> response = await _voteDataAppService.AddElectorate(newElectorate.Name, newElectorate.Detail, newElectorate.Image);

            AjaxResponse ar = new AjaxResponse();

            if (response.Item2)
            {
                ar.Success = true;
                ar.Result = response.Item1;
            }
            else
            {
                ar.Success = false;
                ar.Error = new ErrorInfo(response.Item1);
            }

            return ar;
        }

        [HttpPost]
        public async Task<AjaxResponse> AddCandidateAsync([FromBody] AddCandidateModel newCandidate)
        {
            return await Task<AjaxResponse>.Run(() => AddCandidate(newCandidate));
        }

        private async Task<AjaxResponse> AddCandidate(AddCandidateModel newCandidate)
        {
            Tuple<string, bool> response = await _voteDataAppService.AddCandidate(newCandidate.Name, newCandidate.Detail, newCandidate.Image);

            AjaxResponse ar = new AjaxResponse();

            if (response.Item2)
            {
                ar.Success = true;
                ar.Result = response.Item1;
            }
            else
            {
                ar.Success = false;
                ar.Error = new ErrorInfo(response.Item1);
            }

            return ar;
        }

        [HttpPost]
        public async Task<AjaxResponse> AddPartyAsync([FromBody] AddPartyModel newParty)
        {
            return await Task<AjaxResponse>.Run(() => AddParty(newParty));
        }

        private async Task<AjaxResponse> AddParty(AddPartyModel newParty)
        {
            Tuple<string, bool> response = await _voteDataAppService.AddParty(newParty.Name, newParty.Detail, newParty.Image);

            AjaxResponse ar = new AjaxResponse();

            if (response.Item2)
            {
                ar.Success = true;
                ar.Result = response.Item1;
            }
            else
            {
                ar.Success = false;
                ar.Error = new ErrorInfo(response.Item1);
            }

            return ar;
        }

        [HttpPost]
        public async Task<AjaxResponse> NewReferendumAsync([FromBody] NewReferendumModel newReferendum)
        {
            return await Task<AjaxResponse>.Run(() => NewReferendum(newReferendum));
        }

        private async Task<AjaxResponse> NewReferendum(NewReferendumModel newReferendum)
        {
            Tuple<string, bool> response = await _voteDataAppService.NewReferendum(newReferendum.Name, newReferendum.Detail, newReferendum.Images);

            AjaxResponse ar = new AjaxResponse();

            if (response.Item2)
            {
                ar.Success = true;
                ar.Result = response.Item1;
            }
            else
            {
                ar.Success = false;
                ar.Error = new ErrorInfo(response.Item1);
            }

            return ar;
        }
    }
}
