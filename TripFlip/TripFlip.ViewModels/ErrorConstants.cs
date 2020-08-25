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
    }
}
