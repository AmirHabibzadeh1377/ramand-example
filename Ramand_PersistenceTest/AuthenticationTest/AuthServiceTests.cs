using Microsoft.Extensions.Options;
using Moq;
using Ramand_Application.Model;
using Ramand_Persistence.Repositories;

namespace Ramand_PersistenceTest.AuthenticationTest
{
    [TestFixture]
    public class AuthServiceTests
    {
        #region Fields

        private JWTSettings _jwtSettings;
        private Mock<IOptions<JWTSettings>> _mockJwtSettings;
        private AuthenticationRespository _authenticationRepo;

        #endregion

        #region Setup

        [SetUp]
        public void Setup()
        {
            ////یه توضیخ بابت این کلید ها بدم این ها برای پروژه خودم هستش.حوصله نداشتم برم کلید با طول الگوریتم درست بسازم.چون اینها کار میکردند از همین ها استفاده کردم
            _mockJwtSettings = new Mock<IOptions<JWTSettings>>();
            _authenticationRepo = new AuthenticationRespository(_mockJwtSettings.Object);
        }

        #endregion

        #region TestMethods

        [Test]
        public void AuthenticateAsync_ValidCredentials()
        {
            var authRequest = new AuthRequest{ Username = "amirhabibzadeh", Password = "123456789" };
            var result =  _authenticationRepo.Login(authRequest);

            Assert.IsNotNull(result,"خروجی متد null می باشد");
        }

        [Test]
        public void GetUser_ValidCredentials()
        {
            _jwtSettings = new JWTSettings { Audience = "GhertyBeautyUser", DurationInMinutes = 60, IsSure = "GhertyBeauty", Key = "GhertyBeauty_IdentityGhertyBeauty" };
            _mockJwtSettings.Setup(x => x.Value).Returns(_jwtSettings);
            var result = _authenticationRepo.GetUsers();

            Assert.IsNotNull(result, "خروجی متد null می باشد");
        }
        
        #endregion
    }
}