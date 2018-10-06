using System;
using System.Collections.Generic;
using System.Text;

namespace ArcheryScoringApp.Model
{
    /// <summary>
    /// Class for Weather. Creates non-database object, and handles
    /// business logic.
    /// </summary>
    class WeatherModel
    {
        public string endRef { get; set; }//reference to end it is associated with.
        //Below can be null. All strings so characters like oC, % and km/h can be used. 
        public string temp { get; set; }//temperture
        public string speed { get; set; }//wind speed
        public string dir { get; set; }//wind direction
        public string hum { get; set; }//humidity
        public string other { get; set; }//other, general short note field.

        /// <summary>
        /// Constructor.
        /// Creates weather object.
        /// </summary>
        /// <param name="anEndRef"></param>
        /// <param name="aTemp"></param>
        /// <param name="aSpeed"></param>
        /// <param name="aDir"></param>
        /// <param name="aHum"></param>
        /// <param name="anOther"></param>
        public WeatherModel(string anEndRef, string aTemp, string aSpeed, string aDir, string aHum, string anOther)
        {
            endRef = anEndRef;
            temp = aTemp;
            speed = aSpeed;
            dir = aDir;
            hum = aHum;
            other = anOther;
        }

        /// <summary>
        /// Method for sending object to dataset.
        /// </summary>
        /// <param name="endWeather"></param>
        public void WeatherToCollection(WeatherModel endWeather)
        {
            WeatherHold.ToCollection(endWeather);
        }
    }

   /* /// <summary>
    /// Helper class.
    /// Holds code for dataset practice scoring.
    /// Static so it doesnot depend on end on weather object.
    /// </summary>
    static class WeatherHold1
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
    }*/
}
