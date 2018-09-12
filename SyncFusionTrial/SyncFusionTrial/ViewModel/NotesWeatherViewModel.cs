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

        public void WeatherSaved(string endRef, double temp, double speed, string dir, double hum, string other)
        {
            Model.WeatherModel endConditions = new Model.WeatherModel(endRef, temp, speed, dir, hum, other);
            endConditions.WeatherToCollection(endConditions);
        }
    }
}
