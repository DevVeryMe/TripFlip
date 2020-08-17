﻿using System;
using System.ComponentModel.DataAnnotations;
using TripFlip.ViewModels.Attributes;
using TripFlip.ViewModels.Enums;

namespace TripFlip.ViewModels.TaskViewModels
{
    public class UpdateTaskViewModel
    {
        [Required(ErrorMessage = ErrorConstants.RequiredIdError)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorConstants.EmptyDescriptionError)]
        [StringLength(500, ErrorMessage = ErrorConstants.DescriptionLengthError)]
        public string Description { get; set; }

        [ValidateOptionalEnumField(typeof(TaskPriorityLevel), ErrorMessage = ErrorConstants.NotMatchAnyTaskPriorityLevelError)]
        public TaskPriorityLevel PriorityLevel { get; set; }

        public bool isCompleted { get; set; }
    }
}
