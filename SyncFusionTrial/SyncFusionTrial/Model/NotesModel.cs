
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

  
}
