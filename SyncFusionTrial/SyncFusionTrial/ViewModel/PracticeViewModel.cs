using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ArcheryScoringApp.Model
{
    /// <summary>
    /// Viewmodel class for Practice screen.
    /// Used by dataGrids for dataset.
    /// Code modified from Syncfusion. (2001 - 2018). Xamarin.Forms. Retrieved August 2018, from Syncfusion Documentation: https://help.syncfusion.com/xamarin/introduction/overview#how-to-best-read-this-user-guide
    /// </summary>
    class PracticeViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Model.PracticeModel> end; // a collection for ends
        //collection for dataGrid binding collection.
        public ObservableCollection<Model.PracticeModel> EndCollection
        {
            get { return end; }
            set { end = value;
                OnPropertyChanged("EndCollection");
            }            
        }

        /// <summary>
        /// Constructor for scoring screen dataGrid
        /// </summary>
        public PracticeViewModel()
        {
            end = new ObservableCollection<PracticeModel>();
            this.GenerateEnds();
        }

        /// <summary>
        /// Constructor for Previous dataGrid
        /// </summary>
        /// <param name="prev"></param>
        public PracticeViewModel(List<Data.End> prev)
        {
            end = new ObservableCollection<PracticeModel>();
            this.GeneratePrevEnds(prev);
        }

        /// <summary>
        /// Generates ends for scoring screen dataGrid dataset.
        /// </summary>
        private void GenerateEnds() 
        {
            end.Add(new Model.PracticeModel("End: 1", " ", " ", " ", " ", " ", " ", 0, 0));
            end.Add(new Model.PracticeModel("End: 2", " ", " ", " ", " ", " ", " ", 0, 0));
            end.Add(new Model.PracticeModel("End: 3", " ", " ", " ", " ", " ", " ", 0, 0));
            end.Add(new Model.PracticeModel("End: 4", " ", " ", " ", " ", " ", " ", 0, 0));
            end.Add(new Model.PracticeModel("End: 5", " ", " ", " ", " ", " ", " ", 0, 0));
            end.Add(new Model.PracticeModel("End: 6", " ", " ", " ", " ", " ", " ", 0, 0));
            end.Add(new Model.PracticeModel("End: 7", " ", " ", " ", " ", " ", " ", 0, 0));
            end.Add(new Model.PracticeModel("End: 8", " ", " ", " ", " ", " ", " ", 0, 0));
            end.Add(new Model.PracticeModel("End: 9", " ", " ", " ", " ", " ", " ", 0, 0));
            end.Add(new Model.PracticeModel("End: 10", " ", " ", " ", " ", " ", " ", 0, 0));
            end.Add(new Model.PracticeModel("End: 11", " ", " ", " ", " ", " ", " ", 0, 0));
            end.Add(new Model.PracticeModel("End: 12", " ", " ", " ", " ", " ", " ", 0, 0));
        }

        /// <summary>
        /// Generates ends for previous dataGrid dataset.
        /// </summary>
        /// <param name="prevEnd"></param>
        public void GeneratePrevEnds(List<Data.End> prevEnd)
        {
            int i = 1; //for end numbers in display
                  
            foreach (Data.End pEnd in prevEnd)
            {
                int runningTotal = 0;
                PracticeModel ed = new PracticeModel("End: " + i, pEnd.Score1, pEnd.Score2, pEnd.Score3, pEnd.Score4, pEnd.Score5, pEnd.Score6, pEnd.EndTotal, runningTotal);
                string endRef = pEnd.EndNum;//gets the endRef for database call
                ed.PrevWeather(endRef);
                ed.PrevNotes(endRef);
                end.Add(ed);

                i = i + 1; //increase end by 1
                var a = end;
            }

        }




        public event PropertyChangedEventHandler PropertyChanged;

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
