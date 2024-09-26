using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Protocol.Infrastructure.Data;

namespace Protocol.Infrastructure.Initializers
{
    public class DbMigrationInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ProtocolDbContext>();
                dbContext.Database.Migrate();
            }
        }
    }
}
