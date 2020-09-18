using System.Data;

namespace TripFlip.ViewModels
{
    public static class ErrorConstants
    {
        public const string EmptyIdFieldError = "Field 'id' cannot be empty.";

        public const string IdLessThanOneError = "Id cannot be less than 1.";

        public const string EmptyTitleFieldError = "Field 'title' cannot be empty.";

        public const string TitleLengthError = "Title length cannot be greater than 100.";

        public const string EmptyDescriptionFieldError = "Field 'description' cannot be empty.";

        public const string DescriptionLengthError = "Description length cannot be greater than 500.";

        public const string StartDateEarlierThanNowError = "Start date cannot be earlier than now.";

        public const string EndDateEarlierThanNowError = "End date cannot be earlier than now.";

        public const string EndDateEarlierThanStartDateError = "End date cannot be earlier than start date.";

        public const string NotMatchAnyTaskPriorityLevelError = "Value of 'priority level' field doesn't match any task priority level.";

        public const string CommentLengthError = "Comment length cannot be greater than 250.";

        public const string QuantityLengthError = "Quantity length cannot be greater than 50.";

        public const string ItemWithoutItemListError = "Item cannot exist without item list.";

        public const string EmptyIsCompletedFieldError = "Field 'is completed' cannot be empty.";

        public const string EmptyTaskPriorityFieldError = "Field 'priority level' cannot be empty.";

        public const string PageNumberLessThanOneError = "Page number cannot be less than 1.";

        public const string InvalidPageSizeError = "Page size cannot be less than 1 or greater than 50.";

        public const string EmptyEmailFieldError = "Field 'email' cannot be empty.";

        public const string EmptyPasswordFieldError = "Field 'password' cannot be empty.";

        public const string InvalidEmailFormatError = "Email does not suit correct email format.";

        public const string InvalidPasswordFormatError = "Password does not suit correct password format.";

        public const string EmailLengthError = "Email length must be between 6 and 320 characters.";

        public const string PasswordLengthError = "Password length must be between 8 and 100 characters.";

        public const string EmptyPasswordConfirmationFieldError = "Field 'password confirmation' cannot be empty.";

        public const string MissmatchPasswordConfirmationError = "Password confirmation doesn't match password.";

        public const string RequiredTripIdError = "Id of the trip is required.";

        public const string RequiredUserIdError = "Id of the user is required.";

        public const string TripIdLessThanOneError = "Id of the trip cannot be" +
                                                               "less than 1.";

        public const string RouteIdLessThanOneError = "Id of the route cannot be less than 1.";

        public const string RequiredRoleIdError = "Id of role of trip participant is required.";

        public const string RoleIdLessThanOneError = "Id of role cannot be" +
                                                               "less than 1.";

        public const string RequiredRolesArray = "Role identifiers array is required.";

        public const string UserFirstNameExceedsMaxLengthError = "First name exceeds max length of 50 characters.";

        public const string UserLastNameExceedsMaxLengthError = "Last name exceeds max length of 50 characters.";

        public const string UserAboutMeExceedsMaxLengthError = "\'About me\' exceeds max length of 300 characters.";

        public const string InvalidUserGenderError = "Invalid user gender value.";

        public const string InvalidUserBirthDateError = "Invalid user birthdate value.";

        public const string EmptyTaskIdFieldError = "Field 'task id' cannot be empty.";

        public const string TaskIdLessThanOneError = "Task id cannot be less than 1.";

        public const string EmptyRouteSubscriberIdsFieldError = "Field 'route subscriber ids' cannot be empty.";

        public const string InvalidRouteSubscriberIdsError = "Field 'route subscriber ids' contains invalid values.";

        public const string RouteSubscriberIdsValuesRepeatedError = "Field 'route subscriber ids' contains repeating values.";
    }
}
