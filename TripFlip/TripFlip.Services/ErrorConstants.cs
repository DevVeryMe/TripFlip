﻿namespace TripFlip.Services
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

        public static readonly string IsAlreadySubscriber = "You already subscribed this trip.";

        public static readonly string TripSubscriberNotFound = "Trip subscriber is not found.";

        public static readonly string NotTripAdmin = "Current user is not trip admin.";

        public static readonly string NotSuperAdmin = "Current user is not application super admin.";

        public static readonly string GrantingSuperAdminRole = "Trying to grant application super admin role.";

        public static readonly string SingleAdminTryToUnsubscribeTrip =
            "Current user is the only one admin of this trip, he cannot unsubscribe it.";
    }
}
