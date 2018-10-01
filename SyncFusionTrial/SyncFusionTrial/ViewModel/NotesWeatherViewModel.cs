using System;
using System.Collections.Generic;
using System.Text;

namespace ArcheryScoringApp.ViewModel
{ 
    class NotesWeatherViewModel
    {
        public NotesWeatherViewModel() {} //constructor

        public void NotesSaved(string endNum, string note)
        {
            Model.NotesModel endNote = new Model.NotesModel(endNum, note);
            endNote.SaveToCollection(endNote);
        }

        public void WeatherSaved(string endRef, string temp, string speed, string dir, string hum, string other)
        {
            Model.WeatherModel endConditions = new Model.WeatherModel(endRef, temp, speed, dir, hum, other);
            endConditions.WeatherToCollection(endConditions);
        }

        public bool DoNotesExist(string aRef)
        {
            bool nt = false;
            nt = Model.NotesHold.NotesExist(aRef);
            return nt;
        }

        public bool DoWeatherExist(string aRef)
        {
            bool wt = false;
            wt = Model.WeatherHold.WeatherExist(aRef);
            return wt;
        }

        public string PrevNotes(string aRef)
        {
            string prev = Model.NotesHold.GetNote(aRef);
            return prev;
        }

        public Model.WeatherModel PrevWeather(string aRef)
        {
            Model.WeatherModel prev = Model.WeatherHold.GetWeather(aRef);
            return prev;
        }
    }
}
