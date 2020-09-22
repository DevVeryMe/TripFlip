using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApiUnitTests.Helpers;

namespace WebApiUnitTests.GrantApplicationRolesViewModelTests
{
    [TestClass]
    public class GrantApplicationRolesViewModelNegativeTests
        : GrantApplicationRolesViewModelTestsBase
    {
        [TestMethod]
        public void ApplicationRoleIds_AreNotValid_ExceptionThrown()
        {
            // Arrange.
            var grantApplicationRolesViewModel =
                BuildGrantApplicationRolesViewModel(applicationRoleIds: null);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(grantApplicationRolesViewModel);

            // Assert.
            Assert.IsFalse(modelIsValid);
        }
    }
}
