using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;


namespace ArcheryScoringApp.Model
{
    /// <summary>
    /// Class for handling the practice model object
    /// used by the dataGrid.
    /// Uses INotifyProperyChanged.
    /// </summary>
    class PracticeModel : INotifyPropertyChanged
    {
        private string endNum; //needed for datamodel so end will display (end: 1 etc)
        private string arrow1;//score for first arrow in end.
        private string arrow2;//score for second arrow in end.
        private string arrow3;//score for third arrow in end.
        private string arrow4;//score for fourth arrow in end.
        private string arrow5;//score for fifth arrow in end.
        private string arrow6;//score for sixth arrow in end.
        private int endTotal;//Total of all arrows shot in an end.
        private int runningTotal;//displayed on the dataGrid. Part of Scoring sheet in database.
        private string weather;//for previous sheet
        private string notes;//for previous sheet

        private int eT; //for holding new end total value
      //  static internal int rT; //for holding new running total value
        private string eR; //end reference

        public event PropertyChangedEventHandler PropertyChanged;//needed for INotifyPropertyChanged.

        /// <summary>
        /// Get, set.
        /// </summary>
        public string ER
        {
            get { return eR; }
            set { eR = value; }
        }
        /*
        public string GeteR()
        {
            return eR;
        }

        public void SeteR(string anEr)
        {
            eR = anEr;
        }*/


        /// <summary>
        /// Get, set
        /// </summary>
        public string EndNum
        {
            get { return endNum; }
            set
            { this.endNum = value; }
        }

        /// <summary>
        /// Get, set.
        /// Calls calc, and hold (dataset) methods.
        /// Uses OnPropertyChanged.
        /// </summary>
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

        /// <summary>
        /// Get, set.
        /// Calls calc, and hold (dataset) methods.
        /// Uses OnPropertyChanged.
        /// </summary>
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

        /// <summary>
        /// Get, set.
        /// Calls calc, and hold (dataset) methods.
        /// Uses OnPropertyChanged.
        /// </summary>
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

        /// <summary>
        /// Get, set.
        /// Calls calc, and hold (dataset) methods.
        /// Uses OnPropertyChanged.
        /// </summary>
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

        /// <summary>
        /// Get, set.
        /// Calls calc, and hold (dataset) methods.
        /// Uses OnPropertyChanged.
        /// </summary>
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

        /// <summary>
        /// Get, set.
        /// Calls calc, and hold (dataset) methods.
        /// Uses OnPropertyChanged.
        /// </summary>
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

        /// <summary>
        /// Get, set.
        /// </summary>
        public int EndTotal
        {
            get { return endTotal; }
            set
            {
                this.endTotal = eT;
            }
        }

        /// <summary>
        /// Get, set.
        /// </summary>
        public int RunningTotal
        {
            get { return runningTotal; }
            set
            {
                this.runningTotal = value;
            }
        }

        /// <summary>
        /// Get, set.
        /// Used for Previous scoring sheets.
        /// </summary>
        public string Weather
        {
            get { return weather; }
            set { weather = value; }
        }

        /// <summary>
        /// Get, set.
        /// Used for Previous scoring sheets.
        /// </summary>
        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }

        /// <summary>
        /// Method to handle score calculations.
        /// Calls the invalid score pop-up.
        /// </summary>
        /// <param name="score"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        private int Calc(string score, string r)
        {
            int current = 0;//current end total
            int curScr = 0;// new score entered
            int prvScr = 0;// previous score
            if (score == "X" || score == "x")
            {
                curScr = 10;//An X is scored as a 10.
            }
            else
            {

                bool valid = int.TryParse(score, out curScr);//gives value of 0 if unable to parse, which handles M's

                if (UIPractice.PracID != -1)//stops it firing on set-up
                {
                    if (valid == false)
                    {
                        if (score != "M")//M and m are valid scores, scored as a 0.
                        {
                            if (score != "m")
                            {
                                UIPractice.NotValid(score);
                                score = "0";
                            }
                        }
                    }
                    else
                    {
                        if (curScr > 10 || curScr < 0)//catches scores over 10 and negative scores.
                        {
                            curScr = 0;
                            score = "0";
                            UIPractice.NotValid(score);
                        }
                    }
                }

            }

            if (r == "X" || r == "x") //handles edits
            {
                prvScr = 10;
            }
            else
            {
                int.TryParse(r, out prvScr);
                if (prvScr > 10 || prvScr < 0)
                {
                    prvScr = 0;
                }
            }

            current = this.endTotal + curScr - prvScr; // adds new score and subtracts old score for accuracy
            runningTotal = CalcRT.RunningTotal(curScr, prvScr);
            return current;
        }

        /// <summary>
        /// Constructor for practice model object.
        /// </summary>
        /// <param name="endNum"></param>
        /// <param name="arrow1"></param>
        /// <param name="arrow2"></param>
        /// <param name="arrow3"></param>
        /// <param name="arrow4"></param>
        /// <param name="arrow5"></param>
        /// <param name="arrow6"></param>
        /// <param name="endTotal"></param>
        /// <param name="runningTotal"></param>
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
            eR = EndRef.SetRefPrac("Prac");//endRef set as part of constructor. Means each end object has a seperate, unique reference.
        }

        /// <summary>
        /// calls the HoldEnds method in static helper class,
        /// PracEndsHold. Passes current end to method.
        /// </summary>
        private void ScoresToHold()
        {
            if (UIPractice.PracID != -1)
            {
                PracEndsHold.HoldEnds(eR, endTotal, arrow1, arrow2, arrow3, arrow4, arrow5, arrow6);
            }
        }

        /// <summary>
        /// Gets the previous weather conditions for end.
        /// Used by PracticeViewModel for previous ends.
        /// </summary>
        /// <param name="endRef"></param>
        public void PrevWeather(string endRef)
        {
            string temp = "0";
            string speed = "0";
            string dir = ""; //Wind Direction
            string humid = "0";
            string other = "";
            List<Data.WeatherConditions> cond = App.Database.GetPreviousWeather(endRef);
            foreach (Data.WeatherConditions w in cond)
            {
                temp = w.Temp;
                speed = w.WindSpeed;
                dir = w.WindDir;
                humid = w.Humidity;
                other = w.Other;
            }
            string prevW = temp + "oC, " + speed + "km/h, " + dir + ", " + humid + "%, " + other;
            weather = prevW;
        }

        /// <summary>
        /// Gets the previous weather conditions for end.
        /// Used by PracticeViewModel for previous ends.
        /// </summary>
        /// <param name="endRef"></param>
        public void PrevNotes(string endRef)
        {
            string endNote = "";
            List<Data.Notes> note = App.Database.GetPreviousNote(endRef);
            foreach (Data.Notes n in note)
            {
                endNote = n.EndNotes;
            }
            notes = endNote;
        }

        /// <summary>
        /// OnPropertyChange method.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }



   
}
