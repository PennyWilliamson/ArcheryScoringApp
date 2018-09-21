using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace ArcheryScoringApp.Model
{
    class DetailsModel
    {
        private string name;
        private string bowType;
        private string division;
        private string club;
        private string date;
        private string archNZNo;
  

        public DetailsModel(string aName, string aBowType, string aDiv, string aClub, string aDate, string aArchNZNo)
        {
            name = "Name: " + aName;
            bowType = "Bow Type: " + aBowType;
            division = "Division: " + aDiv;
            club = "Club: " + aClub;
            date = "Date: " + aDate;
            archNZNo = "Archery NZ No:  " + aArchNZNo;
        }

        public DetailsModel() { }//second parameterless constructor

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }


        public string BowType
        {
            get { return this.bowType; }
            set { this.bowType = value; }
        }

        public string Division
        {
            get { return this.division; }
            set { this.division = value; }
        }

        public string Club
        {
            get { return this.club; }
            set
            { club = value; }
        }

        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        public string ArchNZNo
        {
            get { return this.archNZNo; }
            set { archNZNo = value; }
        }

         public int SetDetails(string date)
        {
            int detailsID = App.Database.InsertDetails(date);           
            return detailsID;
        }

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
