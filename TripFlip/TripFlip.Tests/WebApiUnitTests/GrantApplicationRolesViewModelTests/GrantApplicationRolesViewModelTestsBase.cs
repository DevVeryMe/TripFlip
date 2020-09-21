using System;
using System.Collections.Generic;
using TripFlip.ViewModels.UserViewModels;

namespace WebApiUnitTests.GrantApplicationRolesViewModelTests
{
    public abstract class GrantApplicationRolesViewModelTestsBase
    {
        protected static GrantApplicationRolesViewModel BuildGrantApplicationRolesViewModel(
            IEnumerable<int> applicationRoleIds,
            Guid userId = default)
        {
            var grantApplicationRolesViewModel = new GrantApplicationRolesViewModel()
            {
                UserId = userId,
                ApplicationRoleIds = applicationRoleIds
            };

            return grantApplicationRolesViewModel;
        }
    }
}
