using System;
using System.ComponentModel.DataAnnotations;
using TripFlip.ViewModels.Enums;

namespace TripFlip.ViewModels.UserViewModels
{
    public class GrantApplicationRoleViewModel
    {
        public Guid UserId { get; set; }

        [Required(ErrorMessage = ErrorConstants.EmptyApplicationRoleFieldError)]
        [EnumDataType(typeof(TaskPriorityLevel), ErrorMessage = ErrorConstants.NotMatchAnyTaskPriorityLevelError)]
        public ApplicationRole ApplicationRole { get; set; }
    }
}
