using System.Reflection;
using Abp.Modules;
using Abp.Reflection.Extensions;
using votingBackend.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace votingBackend.Web.Host.Startup
{
    [DependsOn(
       typeof(votingBackendWebCoreModule))]
    public class votingBackendWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public votingBackendWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(votingBackendWebHostModule).GetAssembly());
        }
    }
}
