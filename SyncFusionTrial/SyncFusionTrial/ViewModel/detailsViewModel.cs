using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ArcheryScoringApp.Model
{
    class detailsViewModel
    {

        private ObservableCollection<Model.DetailsModel> details;

        public ObservableCollection<Model.DetailsModel> Details
        {
            get { return details; }
            set { this.details = value; }
        }
        // private Model.DetailsModel details;

        public detailsViewModel()
        {
            GenerateDetails();
        }

    internal void GenerateDetails()
        {
            details = new ObservableCollection<Model.DetailsModel>();
            DateTime tdy = DateTime.Today;
            String aDate = tdy.ToString("d");
            details.Add(new Model.DetailsModel() { Name = "Caitlin Thomas-Riley", BowType = "Recurve", Division = "JWR", Club = "Randwick", Date = aDate, ArchNZNo = "3044"  });
        }

    }
}
