using System;
using System.Collections.Generic;
using System.Text;

namespace ArcheryScoringApp.Model
{
    /// <summary>
    /// Static helper class for holding a competition scoring sheets
    /// 10s and Xs total. Used in case of a count back for a tie.
    /// </summary>
    static class TensAndXs
    {
        internal static int tens { get; set; }//holds number of 10s for a scoring sheet.
        internal static int xs { get; set; }//holds number of Xs for a scoring sheet.

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
