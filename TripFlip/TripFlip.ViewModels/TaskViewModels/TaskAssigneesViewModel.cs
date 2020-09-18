using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TripFlip.ViewModels.Attributes;

namespace TripFlip.ViewModels.TaskViewModels
{
    public class TaskAssigneesViewModel
    {
        [Required(ErrorMessage = ErrorConstants.EmptyTaskIdFieldError)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.TaskIdLessThanOneError)]
        public int TaskId { get; set; }

        [Required(ErrorMessage = ErrorConstants.EmptyRouteSubscriberIdsFieldError)]
        [UniqueIntArrayValues(
            ErrorMessage = ErrorConstants.RouteSubscriberIdsValuesRepeatedError)]
        [IntArrayRange(1, int.MaxValue,
            ErrorMessage = ErrorConstants.InvalidRouteSubscriberIdsError)]
        public IEnumerable<int> RouteSubscriberIds { get; set; }
    }
}
