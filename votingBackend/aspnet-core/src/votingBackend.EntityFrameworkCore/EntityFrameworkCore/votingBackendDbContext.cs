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
        public virtual DbSet<UserVote> UserVoteSet { get; set; }
        public virtual DbSet<Electorate> ElectorateSet { get; set; }
        public virtual DbSet<Candidate> CandidateSet { get; set; }
        public virtual DbSet<Party> PartySet { get; set; }
        public virtual DbSet<Referendum> ReferendumSet { get; set; }

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
