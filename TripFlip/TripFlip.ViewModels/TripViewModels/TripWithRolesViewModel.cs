using System.Collections.Generic;
using TripFlip.ViewModels.TripRoleViewModels;

namespace TripFlip.ViewModels.TripViewModels
{
    public class TripWithRolesViewModel
    {
        public TripWithRoutesViewModel Trip { get; set; }

        public ICollection<TripRoleViewModel> TripRoles { get; set; }
    }
}
