using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace ArcheryScoringApp.Model
{
    /// <summary>
    /// Class for setting details object.
    /// This is where you would be handling the logic
    /// for getting user details from preferences in a multi-user system.
    /// </summary>
    class DetailsModel
    {
        private string name;//user name. 
        private string bowType;//from selected bow in ArchMain UI.
        private string division;//division user shoots in
        private string club;//club user belongs to.
        private string date;//current date
        private string archNZNo;//users Archery NZ number.
  
        /// <summary>
        /// Constructor for object.
        /// </summary>
        /// <param name="aName"></param>
        /// <param name="aBowType"></param>
        /// <param name="aDiv"></param>
        /// <param name="aClub"></param>
        /// <param name="aDate"></param>
        /// <param name="aArchNZNo"></param>
        public DetailsModel(string aName, string aBowType, string aDiv, string aClub, string aDate, string aArchNZNo)
        {
            name = "Name: " + aName;
            bowType = "Bow Type: " + aBowType;
            division = "Division: " + aDiv;
            club = "Club: " + aClub;
            date = "Date: " + aDate;
            archNZNo = "Archery NZ No:  " + aArchNZNo;
        }

        /// <summary>
        /// second parameterless constructor
        /// </summary>
        public DetailsModel() { }

        /// <summary>
        /// get, set
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// get, set
        /// </summary>
        public string BowType
        {
            get { return this.bowType; }
            set { this.bowType = value; }
        }

        /// <summary>
        /// get, set
        /// </summary>
        public string Division
        {
            get { return this.division; }
            set { this.division = value; }
        }

        /// <summary>
        /// get, set
        /// </summary>
        public string Club
        {
            get { return this.club; }
            set
            { club = value; }
        }

        /// <summary>
        /// get, set
        /// </summary>
        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        /// <summary>
        /// get, set
        /// </summary>
        public string ArchNZNo
        {
            get { return this.archNZNo; }
            set { archNZNo = value; }
        }

        /// <summary>
        /// Get, set
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
         public int SetDetails(string date)
        {
            int detailsID = App.Database.InsertDetails(date);           
            return detailsID;
        }

        /// <summary>
        /// static so it can be called without a constructor being set.
        /// Gets previous details from database for display in header
        /// of previous scoring sheet pop-ups.
        /// </summary>
        /// <param name="ends"></param>
        /// <returns></returns>
        static public string GetPrevDetails(List<Data.End> ends)
        {
            List<Data.End> end = ends;
            List<Data.Details> details = new List<Data.Details>();
            int id = 0;
            if (end.Count > 0)//catches wrong score entered, i.e. no ends.
            {
                for (int i = 0; i == 0; i++)
                {
                    id = end[i].ID;
                }
            }
            string date = " ";
            string dist = " ";
            details = App.Database.GetPreviousDetails(id);
            foreach(Data.Details d in details)
            {
                date = d.Date;
                dist = d.Dist;
            }
            string dateDist = ". " + date + ". " + dist + "m.";
            return dateDist;
        }
    }
}
