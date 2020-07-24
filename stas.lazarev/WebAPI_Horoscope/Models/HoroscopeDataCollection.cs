using System;

namespace WebAPI_Horoscope.Models
{
    /// <summary>
    /// A class that is responsible for storing Horoscope components
    /// </summary>
    public class HoroscopeDataCollection
    {
        public string[] ZodiacSign { get; set; }
        public string[] PredictionTime { get; set; }
        public string[] PredictionOne { get; set; }
        public string[] PredictionTwo { get; set; }
    }
}
