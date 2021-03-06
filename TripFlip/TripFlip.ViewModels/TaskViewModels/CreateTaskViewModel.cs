﻿using System.ComponentModel.DataAnnotations;
using TripFlip.ViewModels.Enums;

namespace TripFlip.ViewModels.TaskViewModels
{
    public class CreateTaskViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorConstants.EmptyDescriptionFieldError)]
        [StringLength(500, ErrorMessage = ErrorConstants.DescriptionLengthError)]
        public string Description { get; set; }

        [EnumDataType(typeof(TaskPriorityLevel), ErrorMessage = ErrorConstants.NotMatchAnyTaskPriorityLevelError)]
        public TaskPriorityLevel PriorityLevel { get; set; }

        [Required(ErrorMessage = ErrorConstants.EmptyIdFieldError)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)]
        public int TaskListId { get; set; }
    }
}
