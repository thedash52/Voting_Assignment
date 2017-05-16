using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace votingBackend.Controllers
{
    public abstract class votingBackendControllerBase: AbpController
    {
        protected votingBackendControllerBase()
        {
            LocalizationSourceName = votingBackendConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}