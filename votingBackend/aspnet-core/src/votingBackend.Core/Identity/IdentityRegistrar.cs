using votingBackend.Authorization.Roles;
using votingBackend.Authorization.Users;
using votingBackend.MultiTenancy;
using Microsoft.Extensions.DependencyInjection;

namespace votingBackend.Identity
{
    public static class IdentityRegistrar
    {
        public static void Register(ServiceCollection services)
        {
            services.AddLogging();

            services.AddAbpIdentity<Tenant, User, Role, SecurityStampValidator>(options =>
                {
                    options.Cookies.ApplicationCookie.AuthenticationScheme = "AbpZeroTemplateAuthSchema";
                    options.Cookies.ApplicationCookie.CookieName = "AbpZeroTemplateAuth";
                })
                .AddUserManager<UserManager>()
                .AddRoleManager<RoleManager>()
                .AddSignInManager<SignInManager>()
                .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory>()
                .AddDefaultTokenProviders();
        }
    }
}
