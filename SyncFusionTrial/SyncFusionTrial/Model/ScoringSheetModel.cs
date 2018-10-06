using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArcheryScoringApp.Model
{
    /// <summary>
    /// Class for creating non database scorinf sheet model, and adding to database.
    /// </summary>
    class ScoringSheetModel
    {
        public int id { get; set; }//not in constructor as set by database
        public int finalTotal { get; set; }//not in constructor as it is set when saving ends
        public int detailsID {get; set; }//ID of associated details database entry.
        public string type { get; set; }//type of competition or practice

        /// <summary>
        /// Constructor for scoring sheet object.
        /// </summary>
        /// <param name="aDetailsID"></param>
        /// <param name="aType"></param>
        public ScoringSheetModel(int aDetailsID, string aType)
        {
            detailsID = aDetailsID;
            type = aType;
        }

        /// <summary>
        /// Second constructor without param.
        /// </summary>
        public ScoringSheetModel() { }//second constructor without values

        /// <summary>
        /// Calls database method to add scoring sheet.
        /// </summary>
        /// <param name="aDetailsID"></param>
        /// <param name="aType"></param>
        /// <returns></returns>
        public int SetScoringSheet(int aDetailsID, string aType)
        {
            id = App.Database.InsertScoringSheet(aDetailsID, aType);
            return id;
        }
    }
}
