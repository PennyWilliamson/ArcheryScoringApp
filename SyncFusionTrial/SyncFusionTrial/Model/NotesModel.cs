
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcheryScoringApp.Model
{
    class NotesModel
    {
        public string endRef { get; set; }
        public string note { get; set; }

        public NotesModel(string anEndRef, string aNote)
        {
            this.endRef = anEndRef;
            this.note = aNote;
        }

        public void SaveToCollection(NotesModel endNotes)
        {
           // NotesModel endNotes = new NotesModel(endRef, note);
            NotesHold.ToCollection(endNotes);
        }
    }

    static class NotesHold
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
    }
}
