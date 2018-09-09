using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ArcheryScoringApp.Model
{
    class Comp720ViewModel
    {
        private ObservableCollection<Model.Comp720Model> end; // a collection for ends
        public ObservableCollection<Model.Comp720Model> EndCollection
        {
            get { return end; }
            set
            {
                this.end = value;
            }
        }

        public Comp720ViewModel()
        {
            end = new ObservableCollection<Model.Comp720Model>();
            this.GenerateEnds();
        }

        public Comp720ViewModel(List<Data.End> ends)
        {
            end = new ObservableCollection<Comp720Model>();
            this.GeneratePrevEnds(ends);
        }

        private void GenerateEnds()
        {
            end.Add(new Model.Comp720Model("End: 1", " ", " ", " ", 0));
            end.Add(new Model.Comp720Model(" ", " ", " ", " ",  0));
            end.Add(new Model.Comp720Model("End: 2", " ", " ", " ",  0));
            end.Add(new Model.Comp720Model(" ", " ", " ", " ",  0));
            end.Add(new Model.Comp720Model("End: 3", " ", " ", " ",  0));
            end.Add(new Model.Comp720Model(" ", " ", " ", " ",  0));
            end.Add(new Model.Comp720Model("End: 4", " ", " ", " ",  0));
            end.Add(new Model.Comp720Model(" ", " ", " ", " ",  0));
            end.Add(new Model.Comp720Model("End: 5", " ", " ", " ",  0));
            end.Add(new Model.Comp720Model(" ", " ", " ", " ", 0));
            end.Add(new Model.Comp720Model("End: 6", " ", " ", " ", 0));
            end.Add(new Model.Comp720Model(" ", " ", " ", " ", 0));
            end.Add(new Model.Comp720Model("Xs: ", " ", " ", " ", 0));
            end.Add(new Model.Comp720Model("10s: ", " ", " ", " ", 0));
            end.Add(new Model.Comp720Model("End: 7", " ", " ", " ", 0));
            end.Add(new Model.Comp720Model(" ", " ", " ", " ", 0));
            end.Add(new Model.Comp720Model("End: 8", " ", " ", " ", 0));
            end.Add(new Model.Comp720Model(" ", " ", " ", " ", 0));
            end.Add(new Model.Comp720Model("End: 9", " ", " ", " ", 0));
            end.Add(new Model.Comp720Model(" ", " ", " ", " ", 0));
            end.Add(new Model.Comp720Model("End: 10", " ", " ", " ", 0));
            end.Add(new Model.Comp720Model(" ", " ", " ", " ", 0));
            end.Add(new Model.Comp720Model("End: 11", " ", " ", " ", 0));
            end.Add(new Model.Comp720Model(" ", " ", " ", " ", 0));
            end.Add(new Model.Comp720Model("End: 12", " ", " ", " ", 0));
            end.Add(new Model.Comp720Model(" ", " ", " ", " ", 0));
            end.Add(new Model.Comp720Model("Xs: ", " ", " ", " ", 0));
            end.Add(new Model.Comp720Model("10s: ", " ", " ", " ", 0));
        }

        private void GeneratePrevEnds(List<Data.End> prevEnd)
        {
            int i = 1;
            foreach(Data.End pEnd in prevEnd)
            {
                end.Add(new Model.Comp720Model("End: " + i, pEnd.Score1, pEnd.Score2, pEnd.Score3, 0));
                end.Add(new Model.Comp720Model(" ", pEnd.Score4, pEnd.Score5, pEnd.Score6, pEnd.EndTotal));
                i = i + 1;
            }
        }
    }
}
