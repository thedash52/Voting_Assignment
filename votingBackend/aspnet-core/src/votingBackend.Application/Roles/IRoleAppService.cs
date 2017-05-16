using System.Threading.Tasks;
using Abp.Application.Services;
using votingBackend.Roles.Dto;

namespace votingBackend.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
