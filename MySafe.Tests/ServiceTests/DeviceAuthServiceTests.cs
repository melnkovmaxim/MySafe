using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Moq;
using MySafe.Core;
using MySafe.Repositories.Abstractions;
using MySafe.Services;
using MySafe.Services.Abstractions;
using NUnit.Framework;
using Xamarin.Essentials.Interfaces;

namespace MySafe.Tests.ServiceTests
{
    [TestFixture]
    public class DeviceAuthServiceTests
    {
        private readonly IDeviceAuthService _deviceAuthService;
        private readonly Faker _faker;

        public DeviceAuthServiceTests()
        {
            _deviceAuthService = Ioc.Resolve<IDeviceAuthService>();
            _faker = new Faker();
            var secureStorage = Ioc.Resolve<Mock<ISecureStorage>>()
                .Setup(s => s.SetAsync(It.IsAny<string>(), It.IsAny<string>()));

            Ioc.Resolve<Mock<IAsyncDelayerService>>()
                .Setup(d => d.Delay());
        }

        [Test]
        public async Task RegisterTest()
        {

            for (var i = 0; i < 100; i++)
            {
                var password = _faker.Random.String(0, 15, '0', '9');
                var expectedResult = password.Length == MySafeApp.Resources.RequiredLengthDevicePwd;
                var isCompleteAction = false;

                await _deviceAuthService.RegisterAsync(password, () => isCompleteAction = true);

                Assert.AreEqual(expectedResult , isCompleteAction);
            }
        }

        [Test]
        public async Task TryLoginAsync()
        {
            for (var i = 0; i < 100; i++)
            {
                var password = _faker.Random.String(0, 15, '0', '9');
                var expectedResult = password.Length == MySafeApp.Resources.RequiredLengthDevicePwd;

                await _deviceAuthService.RegisterAsync(password);

                var resultOnLogin = false;
                var result = await _deviceAuthService.TryLoginAsync(password, () => resultOnLogin = true);


                Assert.AreEqual(expectedResult, result);
                Assert.AreEqual(result, resultOnLogin);
            }
        }
    }
}
