using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ArcheryScoringApp.Model
{
    /// <summary>
    /// Viewmodel class for Comp720 screen.
    /// Used by the dataGrids for dataset binding.
    /// Code modified from Syncfusion. (2001 - 2018). Xamarin.Forms. Retrieved August 2018, from Syncfusion Documentation: https://help.syncfusion.com/xamarin/introduction/overview#how-to-best-read-this-user-guide
    /// </summary>
    class Comp720ViewModel
    {
        private ObservableCollection<Model.Comp720Model> end; // a collection for ends
        //end collection for two way databinding.
        public ObservableCollection<Model.Comp720Model> EndCollection
        {
            get { return end; }
            set
            {
                this.end = value;
            }
        }

        /// <summary>
        /// Constructor for scoring sheet dataGrid
        /// </summary>
        public Comp720ViewModel()
        {
            end = new ObservableCollection<Model.Comp720Model>();
            this.GenerateEnds();
        }

        /// <summary>
        /// Constructor for previous dataGrid pop-up.
        /// </summary>
        /// <param name="ends"></param>
        public Comp720ViewModel(List<Data.End> ends)
        {
            end = new ObservableCollection<Comp720Model>();
            this.GeneratePrevEnds(ends);
        }

        /// <summary>
        /// Generates ends for scoring sheet dataGrid dataset
        /// </summary>
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

        /// <summary>
        /// Generates ends for previous dataGrid dataset
        /// </summary>
        /// <param name="prevEnd"></param>
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
