using System.Threading.Tasks;
using Abp.Application.Services;
using votingBackend.Authorization.Accounts.Dto;

namespace votingBackend.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
