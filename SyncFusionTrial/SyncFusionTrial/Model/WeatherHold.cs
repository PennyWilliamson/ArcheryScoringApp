using System;
using System.Collections.Generic;
using System.Text;

namespace ArcheryScoringApp.Model
{
    /// <summary>
    /// Helper class.
    /// Holds code for dataset practice scoring.
    /// Static so it does not depend on end on weather object.
    /// </summary>
    class WeatherHold
    {
        static Dictionary<string, WeatherModel> weatherHold = new Dictionary<string, WeatherModel>();

        /// <summary>
        /// Adds param object to dataset.
        /// Removes and replaces with new object if it already exists.
        /// </summary>
        /// <param name="weather"></param>
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

        /// <summary>
        /// Checks to see if dataset contains an object associated with an end.
        /// </summary>
        /// <param name="aRef"></param>
        /// <returns></returns>
        static public Boolean WeatherExist(string aRef)
        {
            bool exists = false;
            if (weatherHold.ContainsKey(aRef))
            {
                exists = true;
            }
            return exists;
        }

        /// <summary>
        /// Returns the weather object associated with an end.
        /// </summary>
        /// <param name="aRef"></param>
        /// <returns></returns>
        static public WeatherModel GetWeather(string aRef)
        {
            WeatherModel prev = null;

            if (weatherHold.ContainsKey(aRef))
            {
                prev = weatherHold[aRef];
            }

            return prev;
        }

        /// <summary>
        /// Saves the ends in the dataset to the database.
        /// </summary>
        static public void WeatherSaved()
        {
            foreach (var weather in weatherHold.Values)
            {
                App.database.AddWeather(weather.endRef, weather.temp, weather.speed, weather.dir, weather.hum, weather.other);
            }
        }
    }
}
