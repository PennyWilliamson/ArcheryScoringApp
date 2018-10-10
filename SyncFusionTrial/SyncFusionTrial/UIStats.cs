using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ArcheryScoringApp
{
    /// <summary>
    /// The content page for the Statistics page of the application.
    /// Code for components are modelled on Miroscoft Xamarin.Forms documentation.
    /// David Britch, C. D. (2017, July 12). Xamarin.Forms User Interface Views. Retrieved August 2018, from Microsoft: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/index
    /// </summary>
	public class UIStats : ContentPage
	{
        private string sightMarking; //previous sight marking
        private string pb { get; set; }//personal best
        private string lastBest { get; set; }//score between personal best and now
        private string lastScore { get; set; }//last score scored with bow.
        
        /// <summary>
        /// Constructor for Statistics page.
        /// Sets components and layout.
        /// </summary>
		public UIStats ()
		{
            
            getStats(); // gets the values for the stats variables
            var scroll = new ScrollView(); //allows page to scroll.

            //Vertical layout
            StackLayout layout = new StackLayout()
            {
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Start,
                Padding = new Thickness(20)
            };

            //allows elements to be placed in a grid array, allowing more than one element to a line.
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(200) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20) });

            var lastMarkings = new Label { Text = "Last Sight Markings: ", TextColor = Color.FromHex("#010101"), FontSize = 20 };
            var lsm = new Label { Text = sightMarking, TextColor = Color.FromHex("#010101"), FontSize = 20 };//text set to class variable.

            var perBest = new Label { Text = "P. B.:  ", TextColor = Color.FromHex("#010101"), FontSize = 20 };
            var perB = new Label { Text = pb, TextColor = Color.FromHex("#010101"), FontSize = 20 };//text set to class variable.

            var lBest = new Label { Text = "Last Best: ", TextColor = Color.FromHex("#010101"), FontSize = 20 };
            var lb = new Label { Text = lastBest, TextColor = Color.FromHex("#010101"), FontSize = 20 };//text set to class variable.

            var lScore = new Label { Text = "Last Score: ", TextColor = Color.FromHex("#010101"), FontSize = 20 };
            var ls = new Label { Text = lastScore, TextColor = Color.FromHex("#010101"), FontSize = 20 };//text set to class variable.

            Button backButton = CreateButton("Back");//creates button Back
            backButton.Clicked += BackClicked;//method call for when button is clicked.

            Button contButton = CreateButton("Continue");
            contButton.Clicked += ContClicked;

            //sets page layout
            grid.Children.Add(lastMarkings, 0, 0);
            grid.Children.Add(lsm, 1, 0);
            grid.Children.Add(perBest, 0, 1);
            grid.Children.Add(perB, 1, 1);
            grid.Children.Add(lBest, 0, 2);
            grid.Children.Add(lb, 1, 2);
            grid.Children.Add(lScore, 0, 3);
            grid.Children.Add(ls, 1, 3);
            grid.Children.Add(backButton, 0, 4);
            grid.Children.Add(contButton, 1, 4);

            layout.Children.Add(grid);//stacklayout containg a grid.
            scroll.Content = layout;//scroll layout containing stack layout.
            Content = scroll;//sets page content.
        }

        /// <summary>
        /// Creates a new button with text from
        /// passed in label variable.
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        static Button CreateButton(string label)
        {
            Button button = new Button
            {
                Text = label,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.LightBlue
            };
            return button;
        }

        /// <summary>
        /// Naviagation, sets nxt page in stack as the
        /// 720 Competition screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ContClicked(object sender, EventArgs e)
        {
            //Title sets the title in the navigation bar.
            await Navigation.PushAsync(new UIComp720() { Title = "720 Competition Scoring" });

        }

        /// <summary>
        /// Navigation, pops current page off stack to
        /// display previous page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        /// <summary>
        /// Method for getting the values for
        /// page variables.
        /// </summary>
        private void getStats()
        {
            //View model object for method calls.
            ViewModel.StatisticsViewModel viewModel = new ViewModel.StatisticsViewModel();

            sightMarking= viewModel.GetSightMarkings(UIArchMain.bowType);


            pb = viewModel.GetPB();

            lastScore = viewModel.GetLast();


            if (pb != lastScore)
            {
                lastBest = viewModel.GetLastBst();
            }
            else { lastBest = lastScore; }//sets last best to last score, if personal best was last score.
        }
	}
}