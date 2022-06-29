using Microsoft.EntityFrameworkCore;

namespace IMgzavri.FileStore.Api.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void InitializeDatabase<TContext>(this IServiceCollection services) where TContext : DbContext
        {
            var sp = services.BuildServiceProvider();
            var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                using (var context = sp.GetService(typeof(TContext)) as TContext)
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
