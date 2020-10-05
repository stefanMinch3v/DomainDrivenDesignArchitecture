namespace PetClinic.Startup.Specs
{
    using Application.Common.Contracts;
    using Moq;
    using MyTested.AspNetCore.Mvc;

    public class Mocks
    {
        public static ICurrentUser CurrentUser
        {
            get
            {
                var currentUserMock = new Mock<ICurrentUser>();

                currentUserMock
                    .SetupGet(u => u.UserId)
                    .Returns(TestUser.Identifier);

                currentUserMock
                    .SetupGet(u => u.Role)
                    .Returns(TestData.ClientRole);

                return currentUserMock.Object;
            }
        }

        public static IDateTime DateTime
        {
            get
            {
                var currentUserMock = new Mock<IDateTime>();

                currentUserMock
                    .SetupGet(u => u.Now)
                    .Returns(TestData.TestDateNow);

                return currentUserMock.Object;
            }
        }
    }
}
