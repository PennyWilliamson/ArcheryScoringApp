using System;
using System.Collections.Generic;
using System.Text;

namespace ArcheryScoringApp.Model
{
    /// <summary>
    /// Static helper class for combining the two sets of
    /// three into one end for Competition scoring.
    /// </summary>
    class CompEnd
    {
        static string endRef;//the unique ID for the end.
        static string arrw1;//score for first arrow in end.
        static string arrw2;//score for second arrow in end.
        static string arrw3;//score for third arrow in end.
        static string arrw4;//score for fourth arrow in end.
        static string arrw5;//score for fifth arrow in end.
        static string arrw6;//score for sixth arrow in end.
        static int endTtl;//end total.

        /// <summary>
        /// Method for combining two sets of three into an end.
        /// </summary>
        /// <param name="endNum"></param>
        /// <param name="anEndRef"></param>
        /// <param name="arrow1"></param>
        /// <param name="arrow2"></param>
        /// <param name="arrow3"></param>
        /// <param name="endTotal"></param>
        static public void CompHoldEnd(string endNum, string anEndRef, string arrow1, string arrow2, string arrow3, int endTotal)
        {
            if (anEndRef != null)//checks its a valid end.
            {
                if (endNum == " ")//looks for which set of three.
                {
                    arrw4 = arrow1;
                    arrw5 = arrow2;
                    arrw6 = arrow3;
                    endTtl = endTotal;
                    SaveEnd();

                }
                else
                {
                    endRef = anEndRef;
                    arrw1 = arrow1;
                    arrw2 = arrow2;
                    arrw3 = arrow3;
                    SaveEnd();
                }
            }
        }

        /// <summary>
        /// Calls the database method for adding an end to the database.
        /// </summary>
        static private void SaveEnd()
        {
            if (UIComp720.ID != -1)//for when previous is displayed, so it does not get readded to database.
            {
                EndModel end = new EndModel(endRef, UIComp720.ID, endTtl, arrw1, arrw2, arrw3, arrw4, arrw5, arrw6);
                App.Database.InsertEnds(end);
                App.Database.UpdateFinalScore(UIComp720.ID, CalcRT.curRT, UIComp720.dtlID, "720Competition");
            }
        }

    }
}
