using TripFlip.Services.Interfaces;

namespace TripFlip.Services
{
    public class GreetingService : IGreetingService
    {
        public string GetGreeting()
        {
            return "Hello, World!";
        }
    }
}
