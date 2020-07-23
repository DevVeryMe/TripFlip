using System;
using System.IO;

using WebAPI_Horoscope.Models;
using Newtonsoft.Json;

namespace WebAPI_Horoscope
{
    /// <summary>
    /// A service that is responsible for reading Horoscope-related static data from a JSON file
    /// </summary>
    public class HoroscopeDataService
    {
        public HoroscopeDataCollection Data { get; set; }

        public HoroscopeDataService()
        {
            string filepath = "Resources\\HoroscopeData.json";

            // read data from JSON file
            if (File.Exists(filepath))
            {
                using (StreamReader sr = new StreamReader(filepath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    Data = (HoroscopeDataCollection)serializer.Deserialize(sr, typeof(HoroscopeDataCollection));
                }
            }
        }
    }
}
