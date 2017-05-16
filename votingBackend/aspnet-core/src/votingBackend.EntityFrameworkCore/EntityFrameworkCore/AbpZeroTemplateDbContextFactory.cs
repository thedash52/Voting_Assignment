using votingBackend.Configuration;
using votingBackend.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace votingBackend.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class votingBackendDbContextFactory : IDbContextFactory<votingBackendDbContext>
    {
        public votingBackendDbContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<votingBackendDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            votingBackendDbContextConfigurer.Configure(builder, configuration.GetConnectionString(votingBackendConsts.ConnectionStringName));
            
            return new votingBackendDbContext(builder.Options);
        }
    }
}