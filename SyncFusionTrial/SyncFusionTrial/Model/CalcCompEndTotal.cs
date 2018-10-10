using System;
using System.Collections.Generic;
using System.Text;

namespace ArcheryScoringApp.Model
{
    /// <summary>
    /// Static helper class for calculating the end total for competition.
    /// Seperate from Competition object as a comptition end is scored
    /// in two sets of three.
    /// Also used for for holding a competition scoring sheets
    /// 10s and Xs total for display. Used in case of a count back for a tie.
    /// </summary>
    static class CalcCompEndTotal
    {
        static int endTotal { get; set; } // holds endTotal as an end is two sets of three
        static string er { get; set; } // holds reference to allow two sets of three to be scored as one end

        internal static int tens { get; set; }//holds number of 10s for a scoring sheet.
        internal static int xs { get; set; }//holds number of Xs for a scoring sheet.

        /// <summary>
        /// Method for calculating end total for competition.
        /// </summary>
        /// <param name="score"></param>
        /// <param name="prvScr"></param>
        /// <param name="aER"></param>
        /// <returns></returns>
        static public int CalcEnd(int score, int prvScr, string aER)
        {
            if (er == aER)
            {
                endTotal = endTotal + score - prvScr; // minus prvScr to allow scores to be changed in case of user error

                return endTotal;
            }
            else
            {
                er = aER; // new end
                endTotal = 0; // clears total for new end
                endTotal = endTotal + score - prvScr; // minus prvScr to allow scores to be changed in case of user error

                return endTotal;
            }
        }

        
        /// <summary>
        /// Get method. Returns value of xs.
        /// </summary>
        /// <returns></returns>
        static public string GetXs()
        {
            string a = xs.ToString() + ". ";//". " so it fails try parse and is set to 0, so score is not affected.
            return a;
        }

        /// <summary>
        /// Get method. Returns value of tens. 
        /// </summary>
        /// <returns></returns>
        static public string GetTens()
        {
            string b = tens.ToString() + ". ";//"." so it fails try parse and is set to 0, so score not affected
            return b;
        }

        /// <summary>
        /// Adds one to the value of tens.
        /// </summary>
        /// <returns></returns>
        static public int Tens()
        {
            tens++;
            return tens;
        }

        /// <summary>
        /// Adds one to the value of xs.
        /// </summary>
        /// <returns></returns>
        static public int Xs()
        {
            xs++;
            return xs;
        }

        /// <summary>
        /// Subtracts 1 off the value of tens.
        /// Used for when scores are edited.
        /// </summary>
        /// <returns></returns>
        static public int RemoveTens()
        {
            tens = tens - 1;
            return tens;
        }

        /// <summary>
        /// Subtracts 1 off the value of xs.
        /// Used for when scores are edited.
        /// </summary>
        /// <returns></returns>
        static public int RemoveXs()
        {
            xs = xs - 1;
            return xs;
        }
    }
}
    
