using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Horoscope
{
    public sealed class PredictionGenerator
    {
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
            "feel as if he is out of the loop",
            "behave with politeness",
            "pay attention to his behaviour",
            "be the fish swimming upstream",
            "feel free to go his own way",
            "get pressured by his loved ones",
            "be quite sure how to approach a certain subject",
            "feel in his heart that his new lover is quite passionate about him",
            "feel confident about himself",
            "practice what you preach",
            "feel like reacting in a way that actually turns out to be inappropriate",
            "wait for a while and allow the flow of events to reveal an element that ultimately will make you laugh instead of weep",
            "be always ready for action",
            "get some personal motivation",
            "get him pumped to go for his personal dreams",
            "encourage himself to take care of the little things",
            "get his full attention to to-do list",
            "help his friends and family",
            "not be afraid to let himself shine",
            "let other people know exactly who he is and how he operate",
            "not risk long-term security for short-term enjoyment",
            "have a handle on how to take care of himself to the best of his abilities"
        };

        private readonly List<string> _modalVerbs = new List<string>()
        {
            "will",
            "has to",
            "should",
            "wants to",
            "needs to",
        };

        private readonly List<string> _conditions = new List<string>()
        {
            "while the affairs head downstream",
            "regardless of what others can say about him",
            "even though he is not ready for it",
            "when he feels like he is not ready for it",
            "while he isn’t recognized for it",
            "when he acts contrary to the people around him act",
            "even if coming events shock him",
            "if he deem that it is worth his time",
            "even if his mood is not good",
            "even though the people around him are not in perfect harmony",
            "under the influence of astral configuration",
            "so, it is imperative that you both take a short break away from responsibilities and do something daring and fun",
            "so why not ask himself up front how you want to see himself?",
            "by always questioning himself",
            "even if this idea leaves him excited and upbeat",
            "it might involve purchasing gifts and treats for others as well as himself",
            "because the universe loves you and wants to prevent you from reckless acts"
        };

        public HoroscopePrediction GeneratePrediction(string sign)
        {
            var rng = new Random();
            var prediction = _eventsTimes[rng.Next(_eventsTimes.Count)] + " ";
            prediction += sign + " ";
            prediction += _modalVerbs[rng.Next(_modalVerbs.Count)] + " ";
            prediction += _events[rng.Next(_events.Count)] + " ";
            prediction += _conditions[rng.Next(_conditions.Count)] + ".";

            return new HoroscopePrediction(sign, prediction);
        }

        public List<HoroscopePrediction> GeneratePredictions(List<string> signs)
        {
            if (signs == null)
                throw new ArgumentNullException("Invalid argument " + nameof(signs));

            var predictions = new List<HoroscopePrediction>();

            foreach (var sign in signs)
            {
                if (sign == null)
                    throw new ArgumentNullException("Invalid argument " + nameof(sign));

                var rng = new Random();
                var prediction = _eventsTimes[rng.Next(_eventsTimes.Count)] + " ";
                prediction += sign + " ";
                prediction += _modalVerbs[rng.Next(_modalVerbs.Count)] + " ";
                prediction += _events[rng.Next(_events.Count)] + " ";
                prediction += _conditions[rng.Next(_conditions.Count)] + ".";
                predictions.Add(new HoroscopePrediction(sign, prediction));
            }

            return predictions;
        }
    }
}
