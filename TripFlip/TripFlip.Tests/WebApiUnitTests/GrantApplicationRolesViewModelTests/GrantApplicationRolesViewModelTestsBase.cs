using System;
using System.Collections.Generic;
using TripFlip.ViewModels.UserViewModels;

namespace WebApiUnitTests.GrantApplicationRolesViewModelTests
{
    public abstract class GrantApplicationRolesViewModelTestsBase
    {
        /// <summary>
        /// Creates GrantApplicationRolesViewModel object
        /// with given user id and application roles' ids.
        /// </summary>
        /// <param name="applicationRoleIds">Application roles' ids.</param>
        /// <param name="userId">User id.</param>
        /// <returns></returns>
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
