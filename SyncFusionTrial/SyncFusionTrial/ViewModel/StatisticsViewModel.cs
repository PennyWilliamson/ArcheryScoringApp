using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArcheryScoringApp.ViewModel
{
    class StatisticsViewModel
    {
        List<Data.ScoringSheet> list = new List<Data.ScoringSheet>();
        int id = 0;

        public StatisticsViewModel() { }//constructor

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

    public string GetLast()
        {
            int i = 0;
            string lastScore = " ";
            var list= App.Database.GetLastScore();
            foreach (Data.ScoringSheet sheet in list)
            {
                var ft = sheet.FinalTotal;
                lastScore = ft.ToString();
            }


            return lastScore;
        }
    }

}
