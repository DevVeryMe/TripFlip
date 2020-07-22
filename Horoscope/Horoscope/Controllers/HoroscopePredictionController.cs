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
        private static readonly Dictionary<string, string> Horoscopes = new Dictionary<string, string>()
        {
            { "aquarius", "You may feel smothered by certain individuals and loved ones who hang on you like barnacles on a rock," +
                          " Aquarius. You're a big part of their emotional support system and they rely on you for strength. Today," +
                          " however, you could get frustrated by this extra weight. You'll more than likely want to rip those barnacles" +
                          " off in order to give you more breathing room. Freedom from others' emotional baggage is the key for today." },

            { "pisces", "You may end up scaring people with your dramatic, emotional outbursts, Pisces. Your behavior is likely to be" +
                        "erratic. The smallest things could set you off on a crazy tirade. The root of these issues probably has to do" +
                        " with a need to be loved, and you feel like others don't give you the attention you think you deserve. Try not" +
                        " to let your happiness depend so much on others' actions. The most important love is self-love." },

            { "aries", "You might surprise a few people with your words and actions, which could seem out of character for you, " +
                       "Aries. Don't be surprised if you don't think or act like your normal self. There is a streak of the oddball" +
                       " inside you that wants to have its day in the spotlight. Let that part of you shine through by wearing your" +
                       " plaid pants with a polka dot shirt and furry orange hat. Wear bright, expressive colors that show off your" +
                       " willingness to stand out in a crowd." },

            { "taurus", "You could feel like a giant wrecking ball that's anxious to break down existing structures. Perhaps you're" +
                        " upset about a new development in your town, the country's political system, or the power structure within" +
                        " your home. Regardless of the exact reason, you're certain that change needs to happen in order to make" +
                        " progress. Often one must tear down an existing structure before a better one can be built." },

            { "gemini", "Keep pen and paper close by, Gemini, and have the number of the patent office on hand. You're apt to come up" +
                        " with some original ideas that could be revolutionary. You're likely be more intrigued by gadgets and electronic" +
                        " devices than usual. You have the power to make cunning observations about how things work. You may end up" +
                        " with a brilliant idea on how to improve the efficiency of an appliance in your house." },

            { "cancer", "You may have endless ideas streaming through your head but little patience to see any of them through to" +
                        " the end, Cancer. Perhaps you're so busy jumping from one thought to the next that you don't take any of them" +
                        " far enough to know whether or not they're worth pursuing. Write thoughts down as they come to you, and go over" +
                        " them later when your mind is in a quieter state." },

            { "leo", "It's OK to change your opinion, Leo. You may pride yourself on being the solid one who always has an answer" +
                     " or knows exactly where to go. You may look upon others as flaky, indecisive, or fickle. It's important that you" +
                     " not shut down your thinking after making a decision about something. Keep your mind open to changes that occur" +
                     " around you and maybe you'll have a change of heart." },
            { "virgo", "You may feel a strong need to express your individuality, Virgo. You don't want to blend into the herd, unseen" +
                       " as you walk down the street. You want to be the sheep with the blue wool, the person skipping instead of" +
                       " walking. Find a place or group that lets you be yourself and not worry about what other people think about your" +
                       " style. Your originality is inspiring. Don't squelch it." },

            { "libra", "Going with the flow may not necessarily appeal to you today, Libra. This is one time when you may want to be" +
                       " the fish swimming upstream while the school heads downstream. Feel free to go your own way regardless of what" +
                       " others have to say about it. You may get pressured by your loved ones to act a certain way or go somewhere" +
                       " special. Don't do these things just to please them. Do what pleases you." },

            { "scorpio", "There are a few emotional surprises coming your way, Scorpio. You might act a certain way under normal" +
                         " circumstances, but today you're apt to take a completely different route. Perhaps you don't know why you" +
                         " suddenly feel comfortable about a situation that you'd normally be quite skeptical about. Nevertheless, this" +
                         " feeling is possible. Don't bother questioning your emotions. Simply roll with whatever circumstances come" +
                         " your way. You'll open many doors for yourself." },

            { "sagittarius", "You're apt to need emotional freedom, Sagittarius. Perhaps you sense that you're being coerced or" +
                             " manipulated into feeling a certain way and feel trapped in the situation. If you can distance yourself" +
                             ", you're likely to see that you're indeed getting caught up in someone else's drama. Right now it's" +
                             " imperative that you find an outlet where you can express yourself freely without feeling like you" +
                             " have to act a particular way." },

            { "capricorn", "Take inventory of your habits and behaviors, Capricorn. Look to certain patterns and note the ones that do" +
                           " and don't work for you. You'll more than likely feel an urge to break free from repetition and create new" +
                           " paths that allow for other opportunities to come along. You may not even be consciously aware that in many" +
                           " ways you're beating a dead horse. Don't continue to waste your energy on paths that go nowhere." }
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
            var key = Horoscopes.Keys.ElementAt(rng.Next(Horoscopes.Count));

            return new HoroscopePrediction(key, Horoscopes[key]);
        }

        [HttpGet]
        [Route("/prediction")]
        public HoroscopePrediction GetPrediction(string sign)
        {
            if(string.IsNullOrWhiteSpace(sign))
                throw new ArgumentException("Invalid argument " + nameof(sign));

            var lowerCaseSign = sign.ToLower();

            return Horoscopes.ContainsKey(lowerCaseSign) ? 
                new HoroscopePrediction(lowerCaseSign, Horoscopes[lowerCaseSign]) : 
                new HoroscopePrediction(sign, "Invalid sign \"" + sign + "\"");
        }
    }
}
