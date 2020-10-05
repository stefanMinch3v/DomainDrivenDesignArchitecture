namespace PetClinic.Startup.Specs
{
    using Application.Identity.Commands.LoginUser;
    using Application.Identity.Commands.RegisterUser;
    using FluentAssertions;
    using Infrastructure.Persistence.Identity;
    using MyTested.AspNetCore.Mvc;
    using Web.Controllers;
    using Xunit;

    public class IdentityControllerSpecs
    {
        [Theory]
        [InlineData(
            IdentityFakes.TestEmail,
            IdentityFakes.ValidPassword,
            JwtTokenGeneratorFakes.ValidToken)]
        public void LoginShouldReturnToken(string email, string password, string token)
            => MyPipeline
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Identity/Login")
                    .WithMethod(HttpMethod.Post)
                    .WithJsonBody(new
                    {
                        Email = email,
                        Password = password
                    }))

                .To<IdentityController>(c => c.Login(new LoginUserCommand(email, password)))

                .Which()
                .ShouldReturn()
                .ActionResult<LoginOutputModel>(result => result
                    .Passing(model => model.Token.Should().Be(token)));

        [Theory]
        [InlineData(
            IdentityFakes.TestEmail,
            IdentityFakes.ValidPassword,
            IdentityFakes.Name)]
        public void RegisterShouldReturnOkResult(string email, string password, string name)
            => MyPipeline
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Identity/Register")
                    .WithMethod(HttpMethod.Post)
                    .WithJsonBody(new
                    {
                        Email = email,
                        Password = password,
                        UserName = name
                    }))

                .To<IdentityController>(c => c.Register(new RegisterUserCommand(email, password, name)))

                .Which()
                .ShouldReturn()
                .ActionResult();

        [Fact]
        public void GetLoginShouldHaveCorrectAttributes()
            => MyController<IdentityController>
                .Calling(c => c.Login(new LoginUserCommand(IdentityFakes.TestEmail, IdentityFakes.ValidPassword)))

                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post));

        [Fact]
        public void GetRegisterShouldHaveCorrectAttributes()
            => MyController<IdentityController>
                .Calling(c => c.Register(new RegisterUserCommand(
                    IdentityFakes.TestEmail,
                    IdentityFakes.ValidPassword,
                    IdentityFakes.Name)))

                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post));
    }
}
