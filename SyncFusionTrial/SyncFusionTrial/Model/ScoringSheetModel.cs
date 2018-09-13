using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArcheryScoringApp.Model
{
    class ScoringSheetModel
    {
        public int id { get; set; }//not in constructor as set by database
        public int finalTotal { get; set; }//not in constructor as it is set when saving ends
        public int detailsID {get; set; }
        public string type { get; set; }
        public ScoringSheetModel(int aDetailsID, string aType)
        {
            detailsID = aDetailsID;
            type = aType;
        }

        public ScoringSheetModel() { }//second constructor without values

        public int SetScoringSheet(int aDetailsID, string aType)
        {
           // ScoringSheetModel scoringSheet = new ScoringSheetModel(aDetailsID, aType);
            id = App.Database.InsertScoringSheet(aDetailsID, aType);
            return id;
        }
    }
}
