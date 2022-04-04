using CollectIt.Database.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace CollectIt.API.Tests.Integration;

// Internal access used because of autogenerated class 'Program' has interval visibility
internal class CollectItWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<PostgresqlCollectItDbContext>();
            services.AddDbContext<PostgresqlCollectItDbContext>(config =>
            {
                config.UseInMemoryDatabase("CollectItDB.Tests", optionsBuilder =>
                {
                    optionsBuilder.EnableNullChecks();
                });
            });
        });
        return base.CreateHost(builder);
    }
}