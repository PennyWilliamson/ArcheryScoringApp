
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcheryScoringApp.Model
{
    /// <summary>
    /// Class for notes model.
    /// Creates object and calls method for adding it 
    /// to the static dataset.
    /// </summary>
    class NotesModel
    {
        public string endRef { get; set; }//end the note is associated with.
        public string note { get; set; }//the note.

        /// <summary>
        /// Constructor for object.
        /// </summary>
        /// <param name="anEndRef"></param>
        /// <param name="aNote"></param>
        public NotesModel(string anEndRef, string aNote)
        {
            this.endRef = anEndRef;
            this.note = aNote;
        }

        /// <summary>
        /// Method to add the notes object to the dataset.
        /// </summary>
        /// <param name="endNotes"></param>
        public void SaveToCollection(NotesModel endNotes)
        {
            NotesHold.ToCollection(endNotes);
        }
    }

  /*  static class NotesHold
    {
        static Dictionary<string, NotesModel> notesHold = new Dictionary<string, NotesModel>();

        static public void ToCollection(NotesModel note)
        {
            string key = note.endRef;
            if (!notesHold.ContainsKey(key))
            {
                notesHold.Add(key, note);
            }
            else
            {
                notesHold.Remove(key);
                notesHold.Add(key, note);
            }
        }

        static public Boolean NotesExist(string aRef)
        {
            string key = aRef;
            bool exist = false;
            if(notesHold.ContainsKey(key))
            {
                exist = true;
            }

            return exist;
        }

        static public string GetNote(string aRef)
        {
            string prev = " ";
            if(notesHold.ContainsKey(aRef))
            {
                NotesModel n = notesHold[aRef];
                prev = n.note;
            }

            return prev;
        }

        static public void NotesSaved()
        {
            foreach(var notes in notesHold.Values)
            {
                App.database.AddNotes(notes.endRef, notes.note);
            }
        }
    }*/
}
