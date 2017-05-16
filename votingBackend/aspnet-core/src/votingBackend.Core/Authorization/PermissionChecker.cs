using Abp.Authorization;
using votingBackend.Authorization.Roles;
using votingBackend.Authorization.Users;
using votingBackend.MultiTenancy;

namespace votingBackend.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
