using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ArcheryScoringApp.Model
{
    /// <summary>
    /// Model for the dataGrids to use for
    /// the 720 Competition screens.
    /// Displayed in sets of 3, so
    /// each end consists of two of these objects.
    /// EndRef class ensures they both have the same endRef,
    /// and CompEnd stiches the two ends together.
    /// </summary>
    class Comp720Model : INotifyPropertyChanged
    {
        private string endNum;//needed for datamodel so end will display (end: 1 etc) and if it equals " " its the second set of 3
        private string arrow1;//First score in set of three
        private string arrow2;//Second sccore in set of three
        private string arrow3;//Third score in set of three
        private int threeTotal;//Total for set of three
        private int endTotal;//endTotal.
        private int runningTotal;//running total.

        private int eT; //for holding new end total value
        private int rT; //for holding new running total value
        private int tT; //for holding three total
        public string eR; //end reference

        /// <summary>
        /// Get, set.
        /// </summary>
        public string ER
        {
            get { return eR; }
            set { eR = value; }
        }

        /// <summary>
        /// Get, set.
        /// </summary>
        public string EndNum
        {
            get { return endNum; }
            set
            {
                this.endNum = value;
            }
        }

        /// <summary>
        /// Get, set.
        /// Set also handles adding total method calls for adding the
        /// score to totals and stiching together the two sets of three.
        /// Set also handles x's and 10's after each 6 rows.
        /// </summary>
        public string Arrow1
        {
            get { return arrow1; }
            set
            {
                if (endNum == "Xs: " || endNum == "10s: ")
                {
                    GetTensAndXs(endNum);
                }
                else
                {
                    string r = arrow1;
                    this.arrow1 = value;
                    tT = Calc(arrow1, r);
                    SaveCompEnd();
                    threeTotal = tT;
                }
                OnPropertyChanged("Arrow1");
            }
        }

        /// <summary>
        /// Get, set.
        /// Set also handles adding total method calls for adding the
        /// score to totals and stiching together the two sets of three.
        /// </summary>
        public string Arrow2
        {
            get { return arrow2; }
            set
            {
                string r = arrow2;
                this.arrow2 = value;
                tT = Calc(arrow2, r);
                threeTotal = tT;
                SaveCompEnd();
                OnPropertyChanged("Arrow2");
            }
        }

        /// <summary>
        /// Get, set.
        /// Set also handles adding total method calls for adding the
        /// score to totals and stiching together the two sets of three.
        /// </summary>
        public string Arrow3
        {
            get { return this.arrow3; }
            set
            {
                string r = arrow3;
                this.arrow3 = value;
                tT = Calc(arrow3, r);
                threeTotal = tT;
                SaveCompEnd();
                OnPropertyChanged("Arrow3");
            }
        }


        /// <summary>
        /// Get, set.
        /// </summary>
        public int ThreeTotal
        {
            get { return threeTotal; }
            set
            {
                this.threeTotal = tT;
                OnPropertyChanged("ThreeTotal");
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
                this.endTotal = value;
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
        /// Parses the totals and calls the calculation methods.
        /// </summary>
        /// <param name="score"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public int Calc(string score, string r)
        {
            int current = 0; //current score for three total
            int curScr = 0;// new arrow score
            int prvScr = 0;// previous arrow score
            if (score == "X" || score == "x" || score == "10")
            {
                curScr = 10;
                if (score == "X" || score == "x")
                {
                    CalcCompEndTotal.Xs();//increases count of Xs by one
                }
                else
                {
                    CalcCompEndTotal.Tens();//increases count of 10s by one
                }
            }
            else
            {
                bool valid = int.TryParse(score, out curScr);
                if (UIComp720.ID != -1)//stops it firing on set-up
                {
                    if (valid == false)
                    {
                        if (score != "M")
                        {
                            if (score != "m")
                            {
                                UIComp720.NotValid(score);
                                score = "0";
                            }
                        }
                    }
                    else
                    {
                        if (curScr > 10 || curScr < 0)
                        {
                            curScr = 0;
                            score = "0";
                            UIComp720.NotValid(score);
                        }
                    }
                }
            }

            if (r == "X" || r == "x" || r == "10")
            {
                prvScr = 10;
                if (r == "X" || r == "x")
                {
                    CalcCompEndTotal.RemoveXs();//decreases count of Xs by one
                }
                else
                {
                    CalcCompEndTotal.RemoveTens();//decreases count of 10s by one
                }
            }
            else
            {
                int.TryParse(r, out prvScr); //returns 0 if not parsable, so M's are captured.
                if (prvScr > 10 || prvScr < 0)
                {
                    prvScr = 0;
                }

            }
            current = this.threeTotal + curScr - prvScr; // minus prvScr to allow scores to be changed in case of user error
            runningTotal = CalcRT.RunningTotal(curScr, prvScr);
            endTotal = CalcCompEndTotal.CalcEnd(curScr, prvScr, eR);
            return current;
        }

        /// <summary>
        /// Gets the number of tens and x's for display on dataGrid.
        /// </summary>
        /// <param name="endNum"></param>
        public void GetTensAndXs(string endNum)
        {
            if (endNum == "Xs: ")
            {
                arrow1 = CalcCompEndTotal.GetXs();

            }
            if (endNum == "10s: ")
            {
                arrow1 = CalcCompEndTotal.GetTens();

            }
        }

        /// <summary>
        /// Constructor for object.
        /// </summary>
        /// <param name="endNum"></param>
        /// <param name="arrow1"></param>
        /// <param name="arrow2"></param>
        /// <param name="arrow3"></param>
        /// <param name="endTotal"></param>
        public Comp720Model(string endNum, string arrow1, string arrow2, string arrow3, int endTotal)
        {
            this.EndNum = endNum;
            this.Arrow1 = arrow1;
            this.Arrow2 = arrow2;
            this.Arrow3 = arrow3;
            this.EndTotal = endTotal;
            eR = EndRef.SetRefComp("720Comp");
        }

        /// <summary>
        /// Calls method to stich two sets of three into an end.
        /// </summary>
        private void SaveCompEnd()
        {
            if (eR != null) //stops CompHoldEnds firing on screen appearing.
            {
                if (endNum.Contains("End") || endNum.Equals(" ")) // stops X's and 10's rows saving to database.
                {
                    CompEnd.CompHoldEnd(endNum, eR, arrow1, arrow2, arrow3, endTotal);
                }
            }
        }

        //Needed for INotifyPropertyChanged.
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// OnPropertyChanged method for INotifyPropertyChanged.
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

  /*  static class calcRTComp
    {
        static internal int curRT { get; set; }

        static public int runningTotal(int eT, int prvScr)
        {
            curRT = curRT + eT - prvScr; // minus prvScr to allow scores to be changed in case of user error
            return curRT;
        }
    }*/
    /*
    static class calcEndTotal
    {
        static int endTotal { get; set; } // holds endTotal as an end is two sets of three
        static string er { get; set; } // holds reference to allow two sets of three to be scored as one end

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
    } */

   /* static class EndRef
    {
        static int counter = 0;
        // static DateTime date = DateTime.Today;
        static Random aNum = new Random();
        static int endCount = 1; //keeps track of end numbers
        static string eR { get; set; }

        static public string SetRef()
        {
            int ranNum = aNum.Next(1, 1000000);

            if (counter == 0) //allows two sets of three to have the same end reference
            {
                eR = "720" + ranNum + endCount.ToString(); //ranNum used as a way to make this a unique identifier, 720 identifies it as a 720 comp end
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
    } */

  /*  static class TensAndXs
    {
        internal static int tens { get; set; }
        internal static int xs { get; set; }

        static public string GetXs()
        {
            string a = xs.ToString() + ". ";//". " so it fails try parse and is set to 0, so score is not affected.
            return a;
        }

        static public string GetTens()
        {
            string b = tens.ToString() + ". ";//"." so it fails try parse and is set to 0, so score not affected
            return b;
        }

        static public int Tens()
        {
            tens++;
            return tens;
        }

        static public int Xs()
        {
            xs++;
            return xs;
        }

        static public int RemoveTens()
        {
            tens = tens - 1;
            return tens;
        }

        static public int RemoveXs()
        {
            xs = xs - 1;
            return xs;
        }
    }*/

   /* static class CompEnd
    {
        static string endRef;
        static string arrw1;
        static string arrw2;
        static string arrw3;
        static string arrw4;
        static string arrw5;
        static string arrw6;
        static int endTtl;

        static public void CompHoldEnd(string endNum, string anEndRef, string arrow1, string arrow2, string arrow3, int endTotal)
        {
            if (anEndRef != null)
            {
                if (endNum == " ")
                {
                    arrw4 = arrow1;
                    arrw5 = arrow2;
                    arrw6 = arrow3;
                    endTtl = endTotal;
                    SaveEnd();

                }
                else
                {
                    endRef = anEndRef;
                    arrw1 = arrow1;
                    arrw2 = arrow2;
                    arrw3 = arrow3;
                }
            }
        }

        static private void SaveEnd()
        {
            if (UIComp720.ID != -1)//for when previous is displayed.
            {
                EndModel end = new EndModel(endRef, UIComp720.ID, endTtl, arrw1, arrw2, arrw3, arrw4, arrw5, arrw6);
                App.Database.InsertEnds(end);
                App.Database.UpdateFinalScore(UIComp720.ID, CalcRT.curRT, UIComp720.dtlID, "720Competition");
            }
        }

    }*/
}
