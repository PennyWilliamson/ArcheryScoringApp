using System;
using System.Collections.Generic;
using System.Text;

namespace ArcheryScoringApp.Model
{
    /// <summary>
    /// Helper class for assigning end references (endRef)
    /// for practice and competition ends.
    /// </summary>
    static class EndRef
    {
        static int counter = 0;//keeps track of end numbers
        static Random aNum = new Random();//a random number object.
        static int endCount = 1; //allows ends be numbered 1 to 12 as last 1 or 2 digits.
        static string eR { get; set; }//holds end Ref for return.

        /// <summary>
        /// Sets End ref for comp.
        /// Handles the fact that an end is two sets of three with an if loop.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        static public string SetRefComp(string type)
        {
            int ranNum = aNum.Next(1, 1000000);//sets random number range.

            if (counter == 0) //allows two sets of three to have the same end reference
            {
                eR = type + ranNum + endCount.ToString(); //ranNum used as a way to make this a unique identifier, 720 identifies it as a 720 comp end
                counter = 1;
                endCount = endCount + 1;
                return eR;
            }
            else
            {
                counter = 0;
                return eR;
            }
        }

        /// <summary>
        /// Sets end Ref for prac.
        /// Seperate method as practice ends are not displayed as two sets of three.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        static public string SetRefPrac(string type)
        {
            int ranNum = aNum.Next(1, 1000000);//sets random number with range as in brackets.

            eR = "Prac" + ranNum + endCount.ToString();//Prac identifies it as practice, ranNum helps make it unique.
            endCount = endCount + 1;

            return eR;
        }
    }
}
