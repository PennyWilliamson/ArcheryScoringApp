﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArcheryScoringApp.Model
{
    class EndModel
    {
        public string endNum { get; set; }
        public int id { get; set; } 
        public int endTotal { get; set; }
        public string score1 { get; set; }
        public string score2 { get; set; }
        public string score3 { get; set; }
        public string score4 { get; set; }
        public string score5 { get; set; }
        public string score6 { get; set; }

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

        public EndModel() { }//second constructor for calling GetPrev

        public static List<Data.End> GetPrev(int finalScore, string type)
            {
            List<Data.End> ends = App.Database.GetPreviousEnds(finalScore, type);           
            return ends;
        }
    }
}
