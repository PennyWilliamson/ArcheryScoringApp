using System;
using System.Collections.Generic;
using System.Text;

namespace ArcheryScoringApp.Model
{
    /// <summary>
    /// Helper class.
    /// Static class for holding the ends for a practice sheet
    /// independant of an object.
    /// </summary>
    static class PracEndsHold
    {
        //dataset for holding ends.
        static Dictionary<string, EndModel> hold = new Dictionary<string, EndModel>();

        /// <summary>
        /// Checks if endRef already a key in dataset, and is so removes end, then readds it.
        /// Otherwise, adds the end, with endRef as key.
        /// </summary>
        /// <param name="anEndRef"></param>
        /// <param name="anEndTotal"></param>
        /// <param name="aScore1"></param>
        /// <param name="aScore2"></param>
        /// <param name="aScore3"></param>
        /// <param name="aScore4"></param>
        /// <param name="aScore5"></param>
        /// <param name="aScore6"></param>
        static public void HoldEnds(string anEndRef, int anEndTotal, string aScore1, string aScore2, string aScore3, string aScore4, string aScore5, string aScore6)
        {

            EndModel end = new EndModel(anEndRef, UIPractice.PracID, anEndTotal, aScore1, aScore2, aScore3, aScore4, aScore5, aScore6);


            if (!hold.ContainsKey(anEndRef))
            {
                hold.Add(anEndRef, end);
            }
            else
            {
                hold.Remove(anEndRef);
                hold.Add(anEndRef, end);
            }
        }

        /// <summary>
        /// Saves dataset to the database.
        /// Triggered on save button press.
        /// </summary>
        static public void Save()
        {
            foreach (var end in hold.Values)
            {
                App.Database.InsertEnds(end);
                App.Database.UpdateFinalScore(UIPractice.PracID, CalcRT.curRT, UIPractice.dtlIDPrac, "Practice");//adds final total to scoring sheet
            }
        }

        /// <summary>
        /// Resets dataset to new set.
        /// </summary>
        static public void ResetHold()
        {
            hold = new Dictionary<string, EndModel>();
        }
    }
}
