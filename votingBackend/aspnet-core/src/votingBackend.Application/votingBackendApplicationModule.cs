using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using votingBackend.Authorization;

namespace votingBackend
{
    [DependsOn(
        typeof(votingBackendCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class votingBackendApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<votingBackendAuthorizationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(votingBackendApplicationModule).GetAssembly());
        }
    }
}