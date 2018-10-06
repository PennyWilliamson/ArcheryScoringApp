using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArcheryScoringApp.ViewModel
{
    /// <summary>
    /// View model class for the Statistics page.
    /// </summary>
    class StatisticsViewModel
    {
        //List for holding method returns.
        List<Data.ScoringSheet> list = new List<Data.ScoringSheet>();
        int id = 0;//for holding returns sheet ID

        /// <summary>
        /// Constructor
        /// </summary>
        public StatisticsViewModel() { }

        /// <summary>
        /// Calls the database method for getting sight markings for selected bow, 
        /// and returns them to UI.
        /// </summary>
        /// <param name="bow"></param>
        /// <returns></returns>
        public string GetSightMarkings(string bow)
        {
            List<Data.Bow> bowList = new List<Data.Bow>();
            bowList = App.Database.GetSightMarkings(bow);
            string markings = " ";
            foreach (Data.Bow bw in bowList)
            {
                var b = bw.SightMarkings;
                markings = b.ToString();
            }
            return markings;
        }

        /// <summary>
        /// Calls the database method for personal best for selected
        /// bow and distance.
        /// Returns to UI.
        /// </summary>
        /// <returns></returns>   
        public string GetPB()
        {
            string pb = " ";
            list = App.Database.getPB();
            foreach (Data.ScoringSheet sheet in list)
            {
                var i = sheet.ID;
                var ft = sheet.FinalTotal;
                pb = ft.ToString();
                id = i;
            }
            return pb;
        }

        /// <summary>
        /// Calls databse for last best for selected bow and distance
        /// combination.
        /// Then returns this to UI.
        /// </summary>
        /// <returns></returns>
        public string GetLastBst()
        {
            int i = 0;
            string lastBest = " ";
            var list = App.Database.GetLastBest(id);
            foreach (Data.ScoringSheet sheet in list)
            {
                var ft = sheet.FinalTotal;
                lastBest = ft.ToString();
                i = sheet.ID;
            }

            return lastBest;
        }


        /// <summary>
        /// Calls database method for last score and returns
        /// to UI.
        /// </summary>
        /// <returns></returns>
        public string GetLast()
        {
            string lastScore = " ";
            var list = App.Database.GetLastScore();
            foreach (Data.ScoringSheet sheet in list)
            {
                var ft = sheet.FinalTotal;
                lastScore = ft.ToString();
            }

            return lastScore;
        }
    }

}
