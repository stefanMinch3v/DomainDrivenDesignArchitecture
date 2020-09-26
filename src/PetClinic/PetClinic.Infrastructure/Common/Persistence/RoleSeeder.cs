namespace PetClinic.Infrastructure.Common.Persistence
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using System.Threading.Tasks;

    public static class RoleSeeder
    {
        private const string Client = "Client";
        private const string Doctor = "Doctor";

        public static IApplicationBuilder AddDefaultRoles(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var roleManager = serviceScope
                    .ServiceProvider
                    .GetService<RoleManager<IdentityRole>>();

                Task.Run(async () =>
                {
                    var roles = new[]
                    {
                        Client,
                        Doctor
                    };

                    foreach (var role in roles)
                    {
                        var existingRole = await roleManager.RoleExistsAsync(role);

                        if (!existingRole)
                        {
                            await roleManager.CreateAsync(new IdentityRole
                            {
                                Name = role
                            });
                        }
                    }
                })
                .GetAwaiter()
                .GetResult();
            }

            return app;
        }
    }
}
