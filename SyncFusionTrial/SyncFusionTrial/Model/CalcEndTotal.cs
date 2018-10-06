using System;
using System.Collections.Generic;
using System.Text;

namespace ArcheryScoringApp.Model
{
    /// <summary>
    /// Static helper class for calculating the end total for competition.
    /// Seperate from Competition object as a comptition end is scored
    /// in two sets of three.
    /// </summary>
    static class CalcEndTotal
    {
        static int endTotal { get; set; } // holds endTotal as an end is two sets of three
        static string er { get; set; } // holds reference to allow two sets of three to be scored as one end

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
        }
    }
