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
    class PracticeViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Model.PracticeModel> end; // a collection for ends
        public ObservableCollection<Model.PracticeModel> EndCollection
        {
            get { return end; }
            set { end = value;
                OnPropertyChanged("EndCollection");
            }            
        }

        public PracticeViewModel()
        {
            end = new ObservableCollection<PracticeModel>();
            this.GenerateEnds();
        }

        public PracticeViewModel(List<Data.End> prev)
        {
            end = new ObservableCollection<PracticeModel>();
            this.GeneratePrevEnds(prev);
        }

        private void GenerateEnds() 
        {
            end.Add(new Model.PracticeModel("End: 1", " ", " ", " ", " ", " ", " ", 0));
            end.Add(new Model.PracticeModel("End: 2", " ", " ", " ", " ", " ", " ", 0));
            end.Add(new Model.PracticeModel("End: 3", " ", " ", " ", " ", " ", " ", 0));
            end.Add(new Model.PracticeModel("End: 4", " ", " ", " ", " ", " ", " ", 0));
            end.Add(new Model.PracticeModel("End: 5", " ", " ", " ", " ", " ", " ", 0));
            end.Add(new Model.PracticeModel("End: 6", " ", " ", " ", " ", " ", " ", 0));
            end.Add(new Model.PracticeModel("End: 7", " ", " ", " ", " ", " ", " ", 0));
            end.Add(new Model.PracticeModel("End: 8", " ", " ", " ", " ", " ", " ", 0));
            end.Add(new Model.PracticeModel("End: 9", " ", " ", " ", " ", " ", " ", 0));
            end.Add(new Model.PracticeModel("End: 10", " ", " ", " ", " ", " ", " ", 0));
            end.Add(new Model.PracticeModel("End: 11", " ", " ", " ", " ", " ", " ", 0));
            end.Add(new Model.PracticeModel("End: 12", " ", " ", " ", " ", " ", " ", 0));
        }

        public void GeneratePrevEnds(List<Data.End> prevEnd)
        {
            int i = 1; //for end numbers in display
                  
            foreach (Data.End pEnd in prevEnd)
            {
                PracticeModel ed = new PracticeModel("End: " + i, pEnd.Score1, pEnd.Score2, pEnd.Score3, pEnd.Score4, pEnd.Score5, pEnd.Score6, pEnd.EndTotal);
                string endRef = pEnd.EndNum;//gets the endRef for database call
                ed.PrevWeather(endRef);
                ed.PrevWeather(endRef);
                //OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, ed));
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
