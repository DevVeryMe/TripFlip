using System.Threading.Tasks;
using TripFlip.Services.DTO;

namespace TripFlip.Services.Interfaces
{
    public interface IRouteService
    {
        Task CreateAsync(RouteDto routeDto);
    }
}
