using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ArcheryScoringApp.Model
{
    class DetailsModel :INotifyPropertyChanged
    {
        private string name;
        private string bowType;
        private string division;
    private string club; 
private string date; 
        private string archNZNo;
       // private DateTime tdy = DateTime.Today;

     /*   public DetailsModel(string aName, string aBowType, string aDiv, string aClub, string aDate, string aArchNZNo )
        {
            name = "Name: " + aName;
            bowType = "Bow Type: " + aBowType;
            division = "Division: " + aDiv;
            club = "Club: " + aClub;
            date = "Date: " + aDate;
            archNZNo = "Archery NZ No:  " + aArchNZNo; 
        } */


          public string Name
          {
              get {
                  return this.name; }
              set
              {
                  this.name = value;
                OnPropertyChanged("Name");
            }
          }


          public string BowType
          {
              get { return this.bowType; }
              set
              {
                  this.bowType = value;
                OnPropertyChanged("BowType");
            }
          }

          public string Division
          {
              get { return this.division; }
              set
              {
                  this.division = value;
                OnPropertyChanged("Club");
            }
          }

          public string Club
          {
              get {

                  return this.club; }
              set
              {
                  club = value;
                OnPropertyChanged("Club");
            }
          }

          public string Date
          {
              get {
                 
                  return date; }
              set
              {

                date = value;
                OnPropertyChanged("Date");
            }
          }

          public string ArchNZNo
          {
              get {
                  return this.archNZNo; }
              set { archNZNo = value;
                OnPropertyChanged("ArchNZNo");
            }
          }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
