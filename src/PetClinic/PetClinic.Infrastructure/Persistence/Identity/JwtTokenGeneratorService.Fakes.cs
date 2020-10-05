namespace PetClinic.Infrastructure.Persistence.Identity
{
    using FakeItEasy;
    using System.Collections.Generic;
    using System.Security.Claims;

    public class JwtTokenGeneratorFakes
    {
        public const string ValidToken = "ValidToken";

        public static IJwtTokenGenerator FakeJwtTokenGenerator
        {
            get
            {
                var jwtTokenGenerator = A.Fake<IJwtTokenGenerator>();

                A
                    .CallTo(() => jwtTokenGenerator.GenerateToken(A<User>.Ignored, A<List<Claim>>.Ignored))
                    .Returns(ValidToken);

                return jwtTokenGenerator;
            }
        }
    }
}
