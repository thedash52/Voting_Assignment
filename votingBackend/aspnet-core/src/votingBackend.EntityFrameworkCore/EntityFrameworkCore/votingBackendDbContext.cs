using Abp.Zero.EntityFrameworkCore;
using votingBackend.Authorization.Roles;
using votingBackend.Authorization.Users;
using votingBackend.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace votingBackend.EntityFrameworkCore
{
    public class votingBackendDbContext : AbpZeroDbContext<Tenant, Role, User, votingBackendDbContext>
    {
        /* Define an IDbSet for each entity of the application */
        
        public votingBackendDbContext(DbContextOptions<votingBackendDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //...
        }
    }
}
