namespace Horoscope
{
    public sealed class HoroscopePrediction
    {
        public string Sign { get; }

        public string Prediction { get; }

        public HoroscopePrediction(string sign, string prediction)
        {
            Sign = sign;
            Prediction = prediction;
        }
    }
}
