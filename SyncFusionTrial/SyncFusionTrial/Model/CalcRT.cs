using System;
using System.Collections.Generic;
using System.Text;

namespace ArcheryScoringApp.Model
{
    /// <summary>
    /// Helper class for calculating running total for Pratice and competition.
    /// Holds the static variable curRT.
    /// Static as it is independant of any object.
    /// </summary>
    static class CalcRT
    {
        //Holds runing total variable. Reset to zero on Comp and Prac button press from Main screen.
        internal static int curRT { get; set; } //static as this is independant of end objects.

        /// <summary>
        /// Adds new score to running total and removes any previous scores.
        /// End total (eT) and previous score (pvrScr) are params.
        /// </summary>
        /// <param name="eT"></param>
        /// <param name="pvrScr"></param>
        /// <returns></returns>
        static public int runningTotal(int eT, int pvrScr)
        {
            curRT = curRT + eT - pvrScr; //adds new score and subtracts old score for accuracy.
            return curRT;
        }
    }
}
