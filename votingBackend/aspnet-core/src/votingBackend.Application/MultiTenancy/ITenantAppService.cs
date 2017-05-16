using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using votingBackend.MultiTenancy.Dto;

namespace votingBackend.MultiTenancy
{
    public interface ITenantAppService : IApplicationService
    {
        ListResultDto<TenantListDto> GetTenants();

        Task CreateTenant(CreateTenantInput input);
    }
}
