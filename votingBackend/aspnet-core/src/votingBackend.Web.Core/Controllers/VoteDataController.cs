using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace votingBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    public class VoteDataController : votingBackendControllerBase
    {


        public VoteDataController()
        {

        }

        [HttpGet]
        public async Task<AjaxResponse> GetElectorates()
        {
            throw new NotImplementedException();

        }

        [HttpGet]
        public async Task<AjaxResponse> GetCandidates()
        {
            throw new NotImplementedException();

        }

        [HttpGet]
        public async Task<AjaxResponse> GetParties()
        {
            throw new NotImplementedException();

        }

        [HttpGet]
        public async Task<AjaxResponse> GetReferendum()
        {
            throw new NotImplementedException();

        }
    }
}
