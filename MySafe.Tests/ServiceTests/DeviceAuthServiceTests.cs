using System.Threading.Tasks;
using Bogus;
using MySafe.Core;
using MySafe.Domain.Services;
using NUnit.Framework;

namespace MySafe.Tests.ServiceTests
{
    [TestFixture]
    public class DeviceAuthServiceTests
    {
        private readonly IDeviceAuthService _deviceAuthService;

        public DeviceAuthServiceTests()
        {
            _deviceAuthService = Ioc.Resolve<IDeviceAuthService>();
        }

        [Test]
        public async Task RegisterTest()
        {
            var faker = new Faker();
            for (var i = 0; i < 10; i++)
            {
                var password = faker.Random.String(0, 10, '0', '9');
                var expectedResult = password.Length == MySafeApp.Resources.DefaultApplicationPasswordLength;
                var isCompleteAction = false;

                await _deviceAuthService.RegisterAsync(password, () => isCompleteAction = true);

                Assert.AreEqual(expectedResult, isCompleteAction);
            }
        }
    }
}