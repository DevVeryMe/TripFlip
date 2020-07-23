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
            "Aquarius",
            "Pisces",
            "Aries",
            "Taurus",
            "Gemini",
            "Cancer",
            "Leo",
            "Virgo",
            "Libra",
            "Scorpio",
            "Sagittarius",
            "Capricorn",
        };

        [HttpGet]
        [Route("/random-prediction")]
        public HoroscopePrediction GetRandomPrediction()
        {
            var rnd = new Random();
            var sign = _signs[rnd.Next(_signs.Count)];
            var predictionGenerator = new PredictionGenerator();

            return predictionGenerator.GeneratePrediction(sign);
        }

        [HttpGet]
        [Route("/prediction")]
        public HoroscopePrediction GetPrediction(string sign)
        {
            if (string.IsNullOrWhiteSpace(sign))
                throw new ArgumentNullException("Invalid argument " + nameof(sign));

            if (!_signs.Exists(x => string.Equals(x, sign, StringComparison.CurrentCultureIgnoreCase)))
                throw new ArgumentException("Invalid argument " + nameof(sign));

            var predictionGenerator = new PredictionGenerator();
            return predictionGenerator.GeneratePrediction(sign);
        }
    }
}
