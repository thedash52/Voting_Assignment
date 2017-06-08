using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using votingBackend.CustomUser;
using votingBackend.CustomUser.Dto;
using votingBackend.Models;

namespace votingBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserContoller : votingBackendControllerBase
    {
        private CustomUserAppService _userAppService;

        public UserContoller()
        {
            _userAppService = new CustomUserAppService();
        }

        [HttpPost]
        public async Task<AjaxResponse> LoginAsync([FromBody] LoginModel user)
        {
            return await Task<AjaxResponse>.Run(() => Login(user));
        }

        private async Task<AjaxResponse> Login(LoginModel user)
        {
            Tuple<LoginDto, string, bool> loginResponse = await _userAppService.Login(user.FirstName, user.LastName, user.Dob, user.ElectoralId);

            AjaxResponse ar = new AjaxResponse();

            if (loginResponse.Item3)
            {
                ar.Success = true;
                ar.Result = loginResponse.Item1;
            }
            else
            {
                ar.Success = false;
                ar.Error = new ErrorInfo(loginResponse.Item2);
            }

            return ar;
        }

        [HttpPost]
        public async Task<AjaxResponse> RegisterAsync([FromBody] RegisterModel user)
        {
            return await Task<AjaxResponse>.Run(() => Register(user));
        }

        private async Task<AjaxResponse> Register(RegisterModel user)
        {
            Tuple<string, bool> registerResponse = await _userAppService.Register(user.FirstName, user.LastName, user.Dob, user.ElectoralId);

            AjaxResponse ar = new AjaxResponse();

            if (registerResponse.Item2)
            {
                ar.Success = true;
                ar.Result = registerResponse.Item1;
            }
            else
            {
                ar.Success = false;
                ar.Error = new ErrorInfo(registerResponse.Item1);
            }

            return ar;
        }

        [HttpPost]
        public async Task<AjaxResponse> SaveVote([FromBody] SaveVoteModel voteResult)
        {
            throw new NotImplementedException();
        }
    }
}
