using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Horoscope.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HoroscopePredictionController : ControllerBase
    {
        private static readonly string[] HoroscopeSigns = new[]
        {
            "ARIES", "TAURUS", "GEMINI", "CANCER", "LEO", "VIRGO", 
            "LIBRA", "SCORPIO", "SAGITTARUS", "CAPROCORN", "AQUARIUS", "PISCES"
        };

        private readonly IPredictionRepository _predictionRepository;

        public HoroscopePredictionController(IPredictionRepository predictionRepository)
        {
            _predictionRepository = predictionRepository;
        }

        [HttpGet]
        public IEnumerable<HoroscopePrediction> GetAllPredictions()
        {
            List<HoroscopePrediction> predictions = new List<HoroscopePrediction>();
            foreach (var sing in HoroscopeSigns)
            {
                predictions.Add(new HoroscopePrediction
                {
                    HoroscopeSingn = sing,
                    Prediction = _predictionRepository.GetPrediction(),
                    Date = DateTime.Now.ToShortDateString()
                });
            }
            return predictions;
        }

        [Route("{signId}")]
        [HttpGet]
        public HoroscopePrediction GetPrediction(int signId)
        {
            int id = (signId >= 0 && signId < 12) ? signId : 0;
            return new HoroscopePrediction
            {
                HoroscopeSingn = HoroscopeSigns[id],
                Prediction = _predictionRepository.GetPrediction(),
                Date = DateTime.Now.ToShortDateString()
            };
        }
    }
}
