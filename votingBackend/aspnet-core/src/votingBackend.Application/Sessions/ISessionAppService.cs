using System.Threading.Tasks;
using Abp.Application.Services;
using votingBackend.Sessions.Dto;

namespace votingBackend.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
