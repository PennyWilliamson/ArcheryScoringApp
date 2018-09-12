using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ArcheryScoringApp
{
	public class UIStats : ContentPage
	{
        private string sightMarking;
        private string pb { get; set; }
        private string lastBest { get; set; }
        private string lastScore { get; set; }
        
		public UIStats ()
		{
            
            getStats(); // gets the values for the stats variables

            StackLayout layout = new StackLayout();
            var header = new Label { Text = " Competition Statistics ", TextColor = Color.FromHex("#010101"), FontSize = 30 };

            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(200) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(200) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            var lastMarkings = new Label { Text = "Last Sight Markings: ", TextColor = Color.FromHex("#010101"), FontSize = 30 };
            var lsm = new Label { Text = sightMarking, TextColor = Color.FromHex("#010101"), FontSize = 30 };

            var perBest = new Label { Text = "P. B.:  ", TextColor = Color.FromHex("#010101"), FontSize = 30 };
            var perB = new Label { Text = pb, TextColor = Color.FromHex("#010101"), FontSize = 30 };

            var lBest = new Label { Text = "Last Best: ", TextColor = Color.FromHex("#010101"), FontSize = 30 };
            var lb = new Label { Text = lastBest, TextColor = Color.FromHex("#010101"), FontSize = 30 };

            var lScore = new Label { Text = "Last Score: ", TextColor = Color.FromHex("#010101"), FontSize = 30 };
            var ls = new Label { Text = lastScore, TextColor = Color.FromHex("#010101"), FontSize = 30 };

            Button backButton = CreateButton("Back");
            backButton.Clicked += BackClicked;

            Button contButton = CreateButton("Continue");
            contButton.Clicked += ContClicked;

            grid.Children.Add(lastMarkings, 0, 0);
            grid.Children.Add(lsm, 1, 0);
            grid.Children.Add(perBest, 0, 1);
            grid.Children.Add(perB, 1, 1);
            grid.Children.Add(lBest, 0, 2);
            grid.Children.Add(lb, 1, 2);
            grid.Children.Add(lScore, 0, 3);
            grid.Children.Add(ls, 1, 3);
            grid.Children.Add(backButton, 0, 4);
            grid.Children.Add(contButton, 2, 4);

            layout.Children.Add(header);
            layout.Children.Add(grid);
            Content = layout;
        }

        static Button CreateButton(string label)
        {
            Button button = new Button
            {
                Text = label,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };
            return button;
        }

        private async void ContClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UIComp720());

        }

        private async void BackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void getStats()
        {
            ViewModel.StatisticsViewModel viewModel = new ViewModel.StatisticsViewModel();

            sightMarking = viewModel.GetSightMarkings(ArchMain.bowType);
           /* List<Data.ScoringSheet> list = new List<Data.ScoringSheet>();
            int id = 0;*/

            /*   list = App.Database.getPB();
              foreach(Data.ScoringSheet sheet in list)
               {
                   var i = sheet.ID;
                   var ft = sheet.FinalTotal;
                   pb = ft.ToString();
                   id = i;
               }*/

            pb = viewModel.GetPB();

            /*  list = App.Database.GetLastBest(id);
              foreach (Data.ScoringSheet sheet in list)
              {
                  var ft = sheet.FinalTotal;
                  lastBest = ft.ToString();
              }*/

            lastBest = viewModel.GetLastBst();


            /*  list = App.Database.GetLastScore();
              foreach (Data.ScoringSheet sheet in list)
              {
                  var ft = sheet.FinalTotal;
                  lastScore = ft.ToString();
              }*/
            lastScore = viewModel.GetLast();
                

    }
	}
}