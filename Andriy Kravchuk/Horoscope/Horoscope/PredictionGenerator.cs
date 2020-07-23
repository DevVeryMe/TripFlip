using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Horoscope
{
    public sealed class PredictionGenerator
    {
        private readonly List<string> _personList = new List<string>()
        {
            "you"
        };

        private readonly List<string> _eventsTimes = new List<string>()
        {
            "Next day",
            "This week",
            "Next weekend",
            "Tonight",
            "Today",
            "Soon",
        };

        private readonly List<string> _events = new List<string>()
        {
            "will feel as if he is out of the loop",
            //"should be ready for action",
            "have to behave with politeness",
            "should pay attention to",
            "wants to be the fish swimming upstream",
            "needs to feel free to go his own way",
            "will get pressured by his loved ones",
            "will be quite sure how to approach a certain subject",
            "wants to feel in his heart that his new lover is quite passionate about him"
        };

        private readonly List<string> _conditions = new List<string>()
        {
            "while the affairs head downstream",
            "regardless of what others can say about him",
            "even though he is not ready for it",
            "when he isn’t recognized for it"
        };

        public HoroscopePrediction GeneratePrediction(string sign)
        {
            var rng = new Random();
            var prediction = _eventsTimes[rng.Next(_eventsTimes.Count)] + " ";
            prediction += sign + " ";
            prediction += _events[rng.Next(_events.Count)] + " ";
            prediction += _conditions[rng.Next(_conditions.Count)] + ".";

            return new HoroscopePrediction(sign, prediction);
        }
    }
}
