using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TripFlip.ViewModels.Attributes;

namespace TripFlip.ViewModels.ItemViewModels
{
    public class ItemAssigneesViewModel
    {
        [Required(ErrorMessage = ErrorConstants.RequiredItemIdError)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)]
        public int ItemId { get; set; }

        [Required(ErrorMessage = ErrorConstants.EmptyRouteSubscriberIdsFieldError)]
        [UniqueIntArrayValues(ErrorMessage = ErrorConstants.RouteSubscriberIdsValuesRepeatedError)]
        [IntArrayRange(1, int.MaxValue, ErrorMessage = ErrorConstants.InvalidRouteSubscriberIdsError)]
        public IEnumerable<int> RouteSubscriberIds { get; set; }
    }
}
