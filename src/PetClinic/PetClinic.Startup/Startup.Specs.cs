namespace PetClinic.Startup
{
    using Application.Common.Contracts;
    using Infrastructure.Persistence.Identity;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using MyTested.AspNetCore.Mvc;
    using Specs;

    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration)
            : base(configuration)
        {
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            ValidateServices(services);

            services
                .ReplaceTransient<UserManager<User>>(_ => IdentityFakes.FakeUserManager)
                .ReplaceTransient<ICurrentUser>(_ => Mocks.CurrentUser)
                .ReplaceTransient<IDateTime>(_ => Mocks.DateTime)
                .ReplaceTransient<IJwtTokenGenerator>(_ => JwtTokenGeneratorFakes.FakeJwtTokenGenerator);
        }

        private static void ValidateServices(IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IMediator>();
            provider.GetRequiredService<IControllerFactory>();
        }
    }
}
