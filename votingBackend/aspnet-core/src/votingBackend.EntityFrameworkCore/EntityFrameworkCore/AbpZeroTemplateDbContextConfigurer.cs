using Microsoft.EntityFrameworkCore;

namespace votingBackend.EntityFrameworkCore
{
    public static class votingBackendDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<votingBackendDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }
    }
}