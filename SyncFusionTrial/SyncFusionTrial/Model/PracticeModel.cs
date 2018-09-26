using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;


namespace ArcheryScoringApp.Model
{
    class PracticeModel : INotifyPropertyChanged
    {
        private string endNum; //needed for datamodel so end will display (end: 1 etc)
        private string arrow1;
        private string arrow2;
        private string arrow3;
        private string arrow4;
        private string arrow5;
        private string arrow6;
        private int endTotal;
        private int runningTotal;
        private string weather;//for previous sheet
        private string notes;//for previous sheet

        private int eT; //for holding new end total value
        static internal int rT; //for holding new running total value
        private string eR; //end reference

        public event PropertyChangedEventHandler PropertyChanged;

        public string ER
        {
            get { return eR; }
            set
            {
                eR = EndRefPrac.SetRef();
                var a = eR;
            }
        }

        public string GeteR()
        {
            return eR;
        }

        public void SeteR(string anEr)
        {
            eR = anEr;
        }

        public string EndNum
        {
            get { return endNum; }
            set
            { this.endNum = value; }
        }

        public string Arrow1
        {
            get { return arrow1; }
            set
            {
                string r = arrow1;//value of arrow before updating
                this.arrow1 = value;
                eT = Calc(arrow1, r);
                endTotal = eT;
                ScoresToHold();
                OnPropertyChanged("Arrow1");
            }
        }

        public string Arrow2
        {
            get { return arrow2; }
            set
            {
                string r = arrow2;
                this.arrow2 = value;
                eT = Calc(arrow2, r);
                endTotal = eT;
                ScoresToHold();
                OnPropertyChanged("Arrow2");
            }
        }

        public string Arrow3
        {
            get { return this.arrow3; }
            set
            {
                string r = arrow3;
                this.arrow3 = value;
                eT = Calc(arrow3, r);
                endTotal = eT;
                ScoresToHold();
                OnPropertyChanged("Arrow3");
            }
        }

        public string Arrow4
        {
            get { return arrow4; }
            set
            {
                string r = arrow4;
                this.arrow4 = value;
                eT = Calc(arrow4, r);
                endTotal = eT;
                ScoresToHold();
                OnPropertyChanged("Arrow4");
            }
        }

        public string Arrow5
        {
            get { return arrow5; }
            set
            {
                string r = arrow5;
                this.arrow5 = value;
                eT = Calc(arrow5, r);
                endTotal = eT;
                ScoresToHold();
                OnPropertyChanged("Arrow5");
            }
        }

        public string Arrow6
        {
            get { return arrow6; }
            set
            {
                string r = arrow6;
                this.arrow6 = value;
                eT = Calc(arrow6, r);
                endTotal = eT;
                ScoresToHold();
                OnPropertyChanged("Arrow6");
                
            } 
        }


        public int EndTotal
        {
            get { return endTotal; }
            set
            {
                this.endTotal = eT;
                OnPropertyChanged("EndTotal");
            }
        }

        public int RunningTotal
        {
            get { return runningTotal; }
            set
            {
                this.runningTotal = value;
            }
        }

        public string Weather
        {
            get { return weather; }
            set { weather = value; }
        }

        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }

        public int Calc(string score, string r)
        {
            int current = 0;//current end total
            int curScr = 0;// new score entered
            int prvScr = 0;// previous score
            if (score == "X")
            {
                curScr = 10;
            }
            else
            {

                bool valid = int.TryParse(score, out curScr);//gives value of 0 if unable to parse, which handles M's
                int a = UIPractice.PracID;
                int aa = a;
                if (UIPractice.PracID != -1)//stops it firing on set-up
                {
                    if (valid == false)
                    {
                        if (score != "M")
                        {                          
                            UIPractice.NotValid(score);
                            score = "0";
                        }
                    }
                    else
                    {
                        if (curScr > 10 || curScr < 0)
                        {
                            curScr = 0;
                            score = "0";
                            UIPractice.NotValid(score);
                        }
                    }
                }

            }

            if (r == "X") //handles edits
            {
                prvScr = 10;
            }
            else
            {
                int.TryParse(r, out prvScr);
                if(prvScr > 10 || prvScr < 0)
                {
                    prvScr = 0;
                }
            }

            current = this.endTotal + curScr - prvScr; // adds new score and subtracts old score for accuracy
            runningTotal = calcRT.runningTotal(curScr, prvScr);
            return current;
        }

        public PracticeModel(string endNum, string arrow1, string arrow2, string arrow3, string arrow4, string arrow5, string arrow6, int endTotal, int runningTotal) 
        {
            this.EndNum = endNum;
            this.Arrow1 = arrow1;
            this.Arrow2 = arrow2;
            this.Arrow4 = arrow4;
            this.Arrow3 = arrow3;
            this.Arrow5 = arrow5;
            this.Arrow6 = arrow6;
            this.EndTotal = endTotal;
            this.runningTotal = runningTotal;
            eR = EndRefPrac.SetRef();
        }

        private void ScoresToHold()
        {
            if (UIPractice.PracID != -1)
            {
                int a = UIPractice.PracID;
                int aa = a;
                PracEndsHold.HoldEnds(eR, endTotal, arrow1, arrow2, arrow3, arrow4, arrow5, arrow6);
            }
        }


        public void PrevWeather(string endRef)
        {
            double temp = 0;
            double speed = 0;
            string dir = ""; //Wind Direction
            double humid = 0;
            string other = "";
            List<Data.WeatherConditions> cond = App.Database.GetPreviousWeather(endRef);
            foreach(Data.WeatherConditions w in cond)
            {
                temp = w.Temp;
                speed = w.WindSpeed;
                dir = w.WindDir;
                humid = w.Humidity;
                other = w.Other;
            }
            string prevW = temp + "oC, "+ speed + "km/h, "+ dir + ", "+ humid + "%, " + other;
            weather = prevW;
        } 

        public void PrevNotes(string endRef)
        {
            string endNote = "";
            List<Data.Notes> note = App.Database.GetPreviousNote(endRef);
            foreach(Data.Notes n in note)
            {
                endNote = n.EndNotes;
            }
            notes = endNote;
        }
        

       protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        } 

    }



    static class calcRT
    {
        internal static int curRT { get; set; } //static as this is independant of end objects.

        static public int runningTotal(int eT, int pvrScr)
        {
            curRT = curRT + eT - pvrScr; //adds new score and subtracts old score for accuracy.
            return curRT;
        }
    }

    static class EndRefPrac
    {
        static Random aNum = new Random();
        static int endCount = 1;
        static string eR { get; set; }

        static public string SetRef()
        { 
            int ranNum = aNum.Next(1, 1000);
            eR = "Prac" + ranNum + endCount.ToString();//Prac identifies it as practice, ranNum helps make it unique.
            endCount = endCount + 1;

            return eR;
        }
    }

    static class PracEndsHold
    {
        static Dictionary<string, EndModel> hold = new Dictionary<string, EndModel>();

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

        static public void Save()
        {
            foreach (var end in hold.Values)
            {
                App.Database.InsertEnds(end);
                App.Database.UpdateFinalScore(UIPractice.PracID, PracticeModel.rT, UIPractice.dtlIDPrac, "Practice");//adds final total to scoring sheet
            }
        }

        static public void ResetHold()
        {
            hold = new Dictionary<string, EndModel>();
        }
    }
}
