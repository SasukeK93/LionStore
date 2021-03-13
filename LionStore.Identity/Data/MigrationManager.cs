using LionStore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LionStore.Identity.Data
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var _logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                using (var appContext = scope.ServiceProvider.GetRequiredService<LionStoreDbContext>())
                {
                    try
                    {
                        appContext.Database.Migrate();
                    }
                    catch (Exception ex)
                    {
#if DEBUG
                        _logger.LogCritical(ex.ToString());
#else
                        _logger.LogCritical(ex.Message);
#endif
                        throw;
                    }
                }
            }

            return host;
        }
    }
}
