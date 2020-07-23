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
        private readonly IPredictionRepository _predictionRepository;

        public HoroscopePredictionController(IPredictionRepository predictionRepository)
        {
            _predictionRepository = predictionRepository;
        }

        [HttpGet]
        public IEnumerable<HoroscopePrediction> GetAllPredictions()
        {
            return _predictionRepository.GetAllPredictions();
        }

        [Route("{sign}")]
        [HttpGet]
        public HoroscopePrediction GetPrediction(string sign)
        {
            try
            {
                return _predictionRepository.GetPredictionBySign(sign);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return new HoroscopePrediction();
        }
    }
}
