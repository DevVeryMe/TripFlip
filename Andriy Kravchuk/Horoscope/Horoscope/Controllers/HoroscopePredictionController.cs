using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Horoscope.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HoroscopePredictionController : ControllerBase
    {
        private static readonly List<string> _signs = new List<string>()
        {
            "aquarius",
            "pisces",
            "aries",
            "taurus",
            "gemini",
            "cancer",
            "leo",
            "virgo",
            "libra",
            "scorpio",
            "sagittarius",
            "capricorn",
        };

        private static readonly List<string> _personList = new List<string>()
        {
            "you"
        };

        private static readonly List<string> _eventsTimes = new List<string>()
        {
            "next day",
            "this week",
            "next weekend",
            "tonight",
            "today",
            "soon",
        };

        private static readonly List<string> _events = new List<string>()
        {
            "will feel as if you are out of the loop",
            "should be ready for action",
            "have to behave with politeness",
            "should pay attention to",
            "want to be the fish swimming upstream",
            "need to feel free to go your own way",
            "will get pressured by your loved ones",
            "will be quite sure how to approach a certain subject",
            "want to feel in your heart that your new lover is quite passionate about you"
        };

        private static readonly List<string> _conditions = new List<string>()
        {
            "while the affairs head downstream",
            "regardless of what others can say about you",
            "even though you are not ready for it",
            "when you aren’t recognized for it"
        };

        private readonly ILogger<HoroscopePredictionController> _logger;

        public HoroscopePredictionController(ILogger<HoroscopePredictionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("/random-prediction")]
        public HoroscopePrediction GetRandomPrediction()
        {
            var rng = new Random();
            var sign = _signs[rng.Next(_signs.Count)];
            var str = _eventsTimes[rng.Next(_eventsTimes.Count)] + " ";
            str += _personList[rng.Next(_personList.Count)] + " ";
            str += _events[rng.Next(_events.Count)] + " ";
            str += _conditions[rng.Next(_conditions.Count)] + ".";

            return new HoroscopePrediction(sign, str);
        }

        [HttpGet]
        [Route("/prediction")]
        public HoroscopePrediction GetPrediction(string sign)
        {
            return null;
        }
    }
}
