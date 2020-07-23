using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Horoscope
{
    public enum PredictionStatus
    {
        NEGATIVE,
        POSITIVE
    }

    public class MockPredictionRepository : IPredictionRepository
    {
        private Dictionary<string, PredictionStatus> _predictions;
        private string[] _directUnionPhrases;
        private string[] _opositeUnionPhrases;

        public MockPredictionRepository()
        {
            _directUnionPhrases = new string[] { " and ", ", also ", ", furthermore ", ", moreover ",
                ", what's more ", ", in additoin to this " };

            _opositeUnionPhrases = new string[] { ", but " ,", nevertheless ", ", nonetheless ",
                ", even so ", ", however ", ", despite that " };

            _predictions = new Dictionary<string, PredictionStatus>()
            {
                { "You should not start playing games without " +
                "learning their rules, because the consequences can be very sad 1", PredictionStatus.NEGATIVE },
                { "You should not start playing games without " +
                "learning their rules, because the consequences can be very sad 2", PredictionStatus.NEGATIVE },
                { "You should not start playing games without " +
                "learning their rules, because the consequences can be very sad 3", PredictionStatus.NEGATIVE },
                { "You will be happy, " +
                "just believe 1", PredictionStatus.POSITIVE },
                { "You will be happy, " +
                "just believe 2", PredictionStatus.POSITIVE },
                { "You will be happy, " +
                "just believe 3", PredictionStatus.POSITIVE }
            };
        }

        public string GetPrediction()
        {
            Random random = new Random();
            string fullPrediction;

            int firstIndex = random.Next(_predictions.Count);
            KeyValuePair<string, PredictionStatus> first = _predictions.ElementAt(firstIndex);

            int secondIndex = random.Next(_predictions.Count);
            while (secondIndex == firstIndex)
            {
                secondIndex = random.Next(_predictions.Count);
            }

            KeyValuePair<string, PredictionStatus> second = _predictions.ElementAt(secondIndex);

            if (first.Value == second.Value)
            {
                fullPrediction = first.Key + _directUnionPhrases[random.Next(_directUnionPhrases.Length)] + second.Key + ".";
            }
            else
            {
                fullPrediction = first.Key + _opositeUnionPhrases[random.Next(_directUnionPhrases.Length)] + second.Key + ".";
            }

            return fullPrediction;
        }
    }
}
