using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoroskopController : ControllerBase
    {
        public Random random = new Random();

        public Dictionary<int, string> whatHappens = new Dictionary<int, string>
        {
            {1, "should find time for physical activity." },
            {2, "should go out into the world to gain inspiration." },
            {3, "should leave the stress in the past." },
            {4, "should listen reverently to your body." },
            {5, "will wait for a quiet day." },
            {6, "must know that shopping should be treated with caution." },
            {7, "should throw all envy into the background." },
            {8, "need to bypass large shopping centers and prefer parks." },
            {9, "need to start thinking about changes in life." },
            {10, "will do housework." },
            {11, "should be careful with your health." },
            {12, "should remember that any good times are the result of hard work in the past." }
        };

        public Dictionary<int, string> who = new Dictionary<int, string>
        {
            {1, "Aries" }, {2, "Taurus" }, {3, "Gemini" }, {4, "Cancer" }, {5, "Leo" }, {6, "Virgo" },
            {7, "Libra" }, {8, "Scorpio" }, {9, "Sagittarius" }, {10, "Capricorn" }, {11, "Aquarius" }, {12, "Pisces" }
        };

        public Dictionary<int, string> when = new Dictionary<int, string>
        {
            {1, "Today" }, {2, "Tomorrow" }, {3, "At the day after tomorrow" }, {4, "Next week" }, {5, "Next month" }
        };

        [HttpGet]
        public ActionResult<string> Get()
        {
            try
            {
                var whoKey = random.Next(1, 13);
                var whenKey = random.Next(1, 6);
                var whatKey = random.Next(1, 13);
                var prediction = $"{when[whenKey]} {who[whoKey]} {whatHappens[whatKey]}";
                return Ok(prediction);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{sign}")]
        public ActionResult<string> Get(string sign)
        {
            try
            {
                var whenKey = random.Next(1, 6);
                var whatKey = random.Next(1, 13);
                var whoKey = who.FirstOrDefault(x => x.Value.Equals(sign)).Key;
                var prediction = $"{when[whenKey]} {who[whoKey]} {whatHappens[whatKey]}";
                return Ok(prediction);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("There is no such sign");
            }
        }
    }
}
