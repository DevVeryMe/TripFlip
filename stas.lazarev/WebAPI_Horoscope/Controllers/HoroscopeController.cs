using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WebAPI_Horoscope.Models;

namespace WebAPI_Horoscope.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoroscopeController : ControllerBase
    {
        HoroscopeDataCollection horoscopeData;

        public HoroscopeController(HoroscopeDataService dataService)
        {
            horoscopeData = dataService.Data;
        }

        /// <summary>
        /// Returns horoscope string on all of the zodiac signs
        /// </summary>
        string GetFullHoroscope()
        {
            var h = horoscopeData;
            string[] conjunctions = { "однако", "но", "тем не менее", "а также", "и не забудьте", "и при этом" };

            string result = $"Гороскоп на {DateTime.Now.ToString("dd.MM.yyyy")}:\n";

            Random rnd = new Random();
            foreach (var sign in horoscopeData.ZodiacSign)
            {
                result +=
                    $"{sign} {h.PredictionTime[rnd.Next(0, h.PredictionTime.Length - 1)]} " +
                    $"{h.PredictionOne[rnd.Next(0, h.PredictionOne.Length - 1)]}, " +
                    $"{conjunctions[rnd.Next(0, conjunctions.Length - 1)]} " +
                    $"{h.PredictionTwo[rnd.Next(0, h.PredictionTwo.Length - 1)]}.\n";
            }

            return result;
        }

        /// <summary>
        /// Returns horoscope string on user-given zodiac sign
        /// </summary>
        string GetHoroscopeOnParam(string userZodiacSign)
        {
            // check the validity of a user-given zodiac sign
            string result = CheckZodiacSign(userZodiacSign);
            if (result == null)
                return $"Error: {userZodiacSign} is not a zodiac sign. At least not the one that I know :)";

            // generate horoscope string
            var h = horoscopeData;
            string[] conjunctions = { "однако", "но", "тем не менее", "а также", "и не забудьте", "и при этом" };
            Random rnd = new Random();
            string res = $"Специальный гороскоп для {result}:\n" +
                $"{result}, {h.PredictionTime[rnd.Next(0, h.PredictionTime.Length - 1)]} " +
                $"{h.PredictionOne[rnd.Next(0, h.PredictionOne.Length - 1)]}, " +
                $"{conjunctions[rnd.Next(0, conjunctions.Length - 1)]} " +
                $"{h.PredictionTwo[rnd.Next(0, h.PredictionTwo.Length - 1)]}.";

            return res;
        }

        /// <summary>
        /// Checks the validity of a user-given zodiac sign. Returns null if it's invalid
        /// </summary>
        string CheckZodiacSign(string zodiacSign)
        {
            string[] zodiacSigns = { "овен", "телец", "близнецы", "рак", "лев", "дева", "весы", "скорпион",
                "змееносец", "стрелец", "козерог", "водолей", "рыбы"};

            var selected_sign = zodiacSigns.Where(item => item == zodiacSign);

            if (selected_sign.Count() == 0)
                return null;

            return selected_sign.First();
        }

        [HttpGet]
        public string Get()
        {
            return GetFullHoroscope();
        }

        [HttpGet("{id}")]
        public string Get(string id)
        {
            return GetHoroscopeOnParam(id.ToLower());
        }
    }
}
