using System;
using System.Collections.Generic;
using System.Text;

namespace ArcheryScoringApp.Model
{
    /// <summary>
    /// Static class for holding the dataset
    /// for the practice scoring sheet, independant of any onbject.
    /// </summary>
    static class NotesHold
    {
        //dataset for holding notes for a practice scoring sheet.
        static Dictionary<string, NotesModel> notesHold = new Dictionary<string, NotesModel>();

        /// <summary>
        /// Adds the notes to the dataset with
        /// the endRef as the key.
        /// Checks to see if the notes all ready exist using 
        /// the key. If so, removes, then replaces the notes.
        /// </summary>
        /// <param name="note"></param>
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

        /// <summary>
        /// Checks to see if dataset contains
        /// endRef as a key.
        /// </summary>
        /// <param name="aRef"></param>
        /// <returns></returns>
        static public Boolean NotesExist(string aRef)
        {
            string key = aRef;
            bool exist = false;
            if (notesHold.ContainsKey(key))
            {
                exist = true;
            }

            return exist;
        }

        /// <summary>
        /// Method for getting a note value
        /// associated with a endRef key.
        /// </summary>
        /// <param name="aRef"></param>
        /// <returns></returns>
        static public string GetNote(string aRef)
        {
            string prev = " ";
            if (notesHold.ContainsKey(aRef))
            {
                NotesModel n = notesHold[aRef];//sets object to the value of key aRef.
                prev = n.note;//sets previous to the note value for the object.
            }

            return prev;
        }

        /// <summary>
        /// Method for database call to add
        /// contents of dataset to the database.
        /// </summary>
        static public void NotesSaved()
        {
            foreach (var notes in notesHold.Values)
            {
                App.database.AddNotes(notes.endRef, notes.note);
            }
        }
    }
}
