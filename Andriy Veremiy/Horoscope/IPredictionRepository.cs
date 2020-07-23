using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Horoscope
{
    public interface IPredictionRepository
    {
        HoroscopePrediction GetPredictionBySign(string sign);

        IEnumerable<HoroscopePrediction> GetAllPredictions();
    }
}
