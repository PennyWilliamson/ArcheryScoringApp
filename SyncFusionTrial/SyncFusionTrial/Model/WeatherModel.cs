using System;
using System.Collections.Generic;
using System.Text;

namespace ArcheryScoringApp.Model
{
    class WeatherModel
    {
        public string endRef { get; set; }
        public string temp { get; set; }
        public string speed { get; set; }
        public string dir { get; set; }
        public string hum { get; set; }
        public string other { get; set; }

        public WeatherModel(string anEndRef, string aTemp, string aSpeed, string aDir, string aHum, string anOther)
        {
            endRef = anEndRef;
            temp = aTemp;
            speed = aSpeed;
            dir = aDir;
            hum = aHum;
            other = anOther;
        }

        public void WeatherToCollection(WeatherModel endWeather)
        {
            WeatherHold.ToCollection(endWeather);
        }
    }

    static class WeatherHold
    {
        static Dictionary<string, WeatherModel> weatherHold = new Dictionary<string, WeatherModel>();

        static public void ToCollection(WeatherModel weather)
        {
            string key = weather.endRef;
            if (!weatherHold.ContainsKey(key))
            {
                weatherHold.Add(key, weather);
            }
            else
            {
                weatherHold.Remove(key);
                weatherHold.Add(key, weather);
            }
        }

        static public Boolean WeatherExist(string aRef)
        {
            bool exists = false;
            if(weatherHold.ContainsKey(aRef))
            {
                exists = true;
            }
            return exists;
        }

        static public WeatherModel GetWeather(string aRef)
        {
            WeatherModel prev = null;

            if(weatherHold.ContainsKey(aRef))
            {
                prev = weatherHold[aRef];
            }

            return prev;
        }

        static public void WeatherSaved()
        {
            foreach (var weather in weatherHold.Values)
            {
                App.database.AddWeather(weather.endRef, weather.temp, weather.speed, weather.dir, weather.hum, weather.other);
            }
        }
    }
}
