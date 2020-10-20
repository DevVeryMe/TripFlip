using System.Threading.Tasks;
using TripFlip.Services.Interfaces.Models;

namespace TripFlip.Services.Interfaces
{
    public interface IMailService
    {
        Task SendAsync(EmailMessage emailMessage);
    }
}
