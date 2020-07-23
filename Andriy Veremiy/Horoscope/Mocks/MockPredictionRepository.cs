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
        private static readonly List<string> _horoscopeSigns = new List<string>
        {
            "ARIES", "TAURUS", "GEMINI", "CANCER", "LEO", "VIRGO",
            "LIBRA", "SCORPIO", "SAGITTARUS", "CAPROCORN", "AQUARIUS", "PISCES"
        };

        private Dictionary<string, PredictionStatus> _predictions;
        private string[] _directUnionPhrases;
        private string[] _inverseUnionPhrases;
        private string[] _timePeriods;

        public MockPredictionRepository()
        {
            InitializeValues();
        }

        private void InitializeValues()
        {
            _directUnionPhrases = new string[] { " and ", ", also ", ", furthermore ", ", moreover ",
                ", what's more ", ", in additoin to this " };

            _inverseUnionPhrases = new string[] { ", but " ,", nevertheless ", ", nonetheless ",
                ", even so ", ", however ", ", despite that " };

            _timePeriods = new string[] { "Soon", "In a short time", "In the near future",
                "Today", "This week", "This month", "This year"  };

            _predictions = new Dictionary<string, PredictionStatus>()
            {
                { "You should not start playing games without " +
                "learning their rules, because the consequences can be very sad", PredictionStatus.NEGATIVE },
                { "You may find it difficult to convince people that you are right. " +
                "Don't even try, just do what you consider right", PredictionStatus.NEGATIVE },
                { "You may be disturbed by internal tension, ready at any moment to " +
                "break free and sweep away everything in its path", PredictionStatus.NEGATIVE },
                { "Do not be too touchy, you risk " +
                "missing out on a great offer", PredictionStatus.NEGATIVE },
                { "Grudging from a coworker or friend will be a little upsetting, " +
                "but a good lesson for you", PredictionStatus.NEGATIVE },
                { "You have to solve family problems", PredictionStatus.NEGATIVE },
                { "Think seven times before getting into any conflicts, " +
                "the result will not please you", PredictionStatus.NEGATIVE },
                { "At work, minor troubles may arise as a result of " +
                "lack of confidence in your own abilities", PredictionStatus.NEGATIVE },
                { "Today you will not have as much work as it seems to you at first glance, but you can easily " +
                "do it for a long time and even almost indefinitely", PredictionStatus.NEGATIVE },
                { "Your boss seems to need everything at once. Today he will load you " +
                "so much with chores that you will get home half-dead", PredictionStatus.NEGATIVE },
                { "You will be happy, " +
                "just believe it", PredictionStatus.POSITIVE },
                { "A phenomenal number of valuable thoughts " +
                "and brilliant ideas will visit you today", PredictionStatus.POSITIVE },
                { "You will be a BOSS. God knows how you will succeed, " +
                "but you will succeed for sure", PredictionStatus.POSITIVE },
                { "In the depths of your subconscious mind, a forceful powerful " +
                "enough to fulfill the most unrealizable hopes", PredictionStatus.POSITIVE },
                { "For some reason you have decided that there are no unsolvable questions for you. " +
                "Maybe that's the way it is", PredictionStatus.POSITIVE },
                { "The more energetic you are, " +
                "the more pleasure it will bring you", PredictionStatus.POSITIVE },
                { "Your loved one will please you", PredictionStatus.POSITIVE },
                { "If before that you were ignored, then today the authorities will " +
                "will finally give you a chance to prove yourself", PredictionStatus.POSITIVE },
                { "Today you will feel a real emotional surge. It will be especially " +
                "pleasant that you will experience mostly pleasant emotions", PredictionStatus.POSITIVE },
                { "It will work well on a wave of good mood, so the day " +
                "should be considered a good one", PredictionStatus.POSITIVE }
            };
        }

        private string GeneratePredictionText()
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
            string secondKey = Char.ToLowerInvariant(second.Key[0]) + second.Key.Substring(1);

            if (first.Value == second.Value)
            {
                fullPrediction = first.Key + _directUnionPhrases[random.Next(_directUnionPhrases.Length)] + secondKey + ".";
            }
            else
            {
                fullPrediction = first.Key + _inverseUnionPhrases[random.Next(_inverseUnionPhrases.Length)] + secondKey + ".";
            }

            return fullPrediction;
        }

        private string GeneratePredictionTimePeriod()
        {
            Random random = new Random();

            return _timePeriods[random.Next(_timePeriods.Length)];
        }

        public HoroscopePrediction GetPredictionBySign(string sign)
        {
            if (_horoscopeSigns.Contains(sign.ToUpper()))
            {
                return new HoroscopePrediction
                {
                    HoroscopeSingn = sign.ToUpper(),
                    Prediction = GeneratePredictionText(),
                    TimePeriod = GeneratePredictionTimePeriod()
                };
            }
            else
            {
                throw new Exception("Not existing horoscope sign");
            }
        }

        public IEnumerable<HoroscopePrediction> GetAllPredictions()
        {
            List<HoroscopePrediction> predictions = new List<HoroscopePrediction>();
            foreach (var sign in _horoscopeSigns)
            {
                predictions.Add(new HoroscopePrediction
                {
                    HoroscopeSingn = sign,
                    Prediction = GeneratePredictionText(),
                    TimePeriod = GeneratePredictionTimePeriod()
                });
            }
            return predictions;
        }
    }
}
