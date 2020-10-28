namespace TripFlip.Services
{
    class ErrorConstants
    {
        public static readonly string TripNotFound = "Trip is not found.";

        public static readonly string TaskListNotFound = "Task list is not found.";

        public static readonly string TaskNotFound = "Task is not found.";

        public static readonly string ItemListNotFound = "Item list is not found.";

        public static readonly string ItemNotFound = "Item is not found.";

        public static readonly string RouteNotFound = "Route is not found.";

        public static readonly string UserNotFound = "User is not found.";

        public static readonly string AddingTaskToNotExistingTaskList = "Trying to add task to not existing task list.";

        public static readonly string AddingTaskListToNotExistingRoute = "Trying to add task list to not existing route.";

        public static readonly string PasswordNotVerified = "Password is not verified.";

        public static readonly string EmailIsTaken = "This Email is already taken.";

        public static readonly string TripRoleNotFound = "Trip role is not found.";

        public static readonly string NotSubscriberOfTheTrip = "User is not a subscriber of this trip.";

        public static readonly string NotSubscriberOfTheRoute = "User is not a subscriber of this route.";

        public static readonly string NoGrantAdminRolePermission = "You have no permission to " +
                                                                   "grant admin role to other users.";

        public static readonly string AlreadyRoleSet = "This role for this user is already set.";

        public static readonly string NotAuthorized = "Not authorized access.";

        public static readonly string IsAlreadyTripSubscriber = "You already subscribed to this trip.";

        public static readonly string IsAlreadyRouteSubscriber = "You already subscribed to this route.";

        public static readonly string TripSubscriberNotFound = "Trip subscriber is not found.";

        public static readonly string NotTripAdmin = "Current user is not trip admin.";

        public static readonly string NotTripEditor = "Current user is not trip editor.";

        public static readonly string NotSuperAdmin = "Current user is not application super admin.";

        public static readonly string GrantingSuperAdminRole = "Trying to grant application super admin role.";

        public static readonly string SingleAdminTryToUnsubscribeFromTrip =
            "Current user is the only one admin of this trip, he cannot unsubscribe from it.";

        public static readonly string RouteSubscribersNotFound = "At least one of given route subscribers' ids is not found.";

        public static readonly string NotRouteEditor = "Current user is not granted with route editor role.";

        public static readonly string NotRouteAdmin = "Current user is not granted with route admin role.";

        public static readonly string NotRouteSubscriber = "Current user is not a subscriber of a specified route.";

        public static readonly string GoogleFailedToReturnOpenIdConfig = "Google failed to return it's OpenID configuration values.";

        public static readonly string GoogleInvalidAuthResponseType = "Invalid OpenID configuration object, or it no longer supports " +
            "\"authorization code\" response type for authentication.";

        public static readonly string GoogleInvalidAuthorizationCode = "Invalid authorization code.";

        public static readonly string GoogleFailedToExchangeAuthCodeForTokens = "Failed to exchange given authorizationCode for " +
                    "Google's access and id tokens.";

        public static readonly string GoogleNoEmailInIdToken = "Google's Id token does not contain email.";

        public static readonly string CannotSendMailMessage = "Cannot send mail message.";

        public static readonly string BirthdayCongratulationTemplateFileNotFound = 
            "Birthday congratulation template html file is not found.";

        public static readonly string BirthdayCongratulationTemplateIsEmpty =
            "Birthday congratulation template is null or empty.";
    }
}
