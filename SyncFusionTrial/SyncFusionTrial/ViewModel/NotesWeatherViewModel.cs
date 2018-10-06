using System;
using System.Collections.Generic;
using System.Text;

namespace ArcheryScoringApp.ViewModel
{ 
    /// <summary>
    /// Viewmodel class for the notes and weather page.
    /// </summary>
    class NotesWeatherViewModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public NotesWeatherViewModel() {} 

        /// <summary>
        /// Sends notes and endNum (notes object)to the dataset.
        /// </summary>
        /// <param name="endNum"></param>
        /// <param name="note"></param>
        public void NotesSaved(string endNum, string note)
        {
            Model.NotesModel endNote = new Model.NotesModel(endNum, note);
            endNote.SaveToCollection(endNote);
        }

        /// <summary>
        /// Sends weather conditions object to the dataset.
        /// </summary>
        /// <param name="endRef"></param>
        /// <param name="temp"></param>
        /// <param name="speed"></param>
        /// <param name="dir"></param>
        /// <param name="hum"></param>
        /// <param name="other"></param>
        public void WeatherSaved(string endRef, string temp, string speed, string dir, string hum, string other)
        {
            Model.WeatherModel endConditions = new Model.WeatherModel(endRef, temp, speed, dir, hum, other);
            endConditions.WeatherToCollection(endConditions);
        }

        /// <summary>
        /// Checks if dataset contains notes for and end.
        /// Returns to UI.
        /// </summary>
        /// <param name="aRef"></param>
        /// <returns></returns>
        public bool DoNotesExist(string aRef)
        {
            bool nt = false;
            nt = Model.NotesHold.NotesExist(aRef);
            return nt;
        }

        /// <summary>
        /// Checks if dataset contains weather conditions object for an end.
        /// Returns to UI.
        /// </summary>
        /// <param name="aRef"></param>
        /// <returns></returns>
        public bool DoWeatherExist(string aRef)
        {
            bool wt = false;
            wt = Model.WeatherHold.WeatherExist(aRef);
            return wt;
        }

        /// <summary>
        /// Returns saved notes for an end from dataset
        /// to UI.
        /// </summary>
        /// <param name="aRef"></param>
        /// <returns></returns>
        public string PrevNotes(string aRef)
        {
            string prev = Model.NotesHold.GetNote(aRef);
            return prev;
        }

        /// <summary>
        /// Returns weather condtions for an end from dataset to UI.
        /// </summary>
        /// <param name="aRef"></param>
        /// <returns></returns>
        public Model.WeatherModel PrevWeather(string aRef)
        {
            Model.WeatherModel prev = Model.WeatherHold.GetWeather(aRef);
            return prev;
        }
    }
}
