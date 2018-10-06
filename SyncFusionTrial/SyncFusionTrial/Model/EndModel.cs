using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArcheryScoringApp.Model
{
    /// <summary>
    /// Class for a generic end model.
    /// Used for getting the previous ends for a scoring sheet.
    /// </summary>
    class EndModel
    {
        public string endNum { get; set; }//unique end ID
        public int id { get; set; } //scoring sheet ID
        public int endTotal { get; set; }//total of scores for an end.
        public string score1 { get; set; }//score of first arrow
        public string score2 { get; set; }//score of second arrow.
        public string score3 { get; set; }//score of third arrow.
        public string score4 { get; set; }//score of fourth arrow.
        public string score5 { get; set; }//score of fifth arrow.
        public string score6 { get; set; }//score of sixth arrow.

        /// <summary>
        /// Constructor for object.
        /// </summary>
        /// <param name="anEndNum"></param>
        /// <param name="anID"></param>
        /// <param name="anEndTotal"></param>
        /// <param name="aScore1"></param>
        /// <param name="aScore2"></param>
        /// <param name="aScore3"></param>
        /// <param name="aScore4"></param>
        /// <param name="aScore5"></param>
        /// <param name="aScore6"></param>
        public EndModel(string anEndNum,  int anID, int anEndTotal, string aScore1, string aScore2, string aScore3, string aScore4, string aScore5, string aScore6)
        {
            endNum = anEndNum;
            id = anID;
            endTotal = anEndTotal;
            score1 = aScore1;
            score2 = aScore2;
            score3 = aScore3;
            score4 = aScore4;
            score5 = aScore5;
            score6 = aScore6;
        }

        /// <summary>
        /// Second constructor for calling GetPrev
        /// </summary>
        public EndModel() { }

        /// <summary>
        /// Class for calling database for GetPreviousEnds.
        /// </summary>
        /// <param name="finalScore"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<Data.End> GetPrev(int finalScore, string type)
            {
            List<Data.End> ends = App.Database.GetPreviousEnds(finalScore, type);           
            return ends;
        }
    }
}
