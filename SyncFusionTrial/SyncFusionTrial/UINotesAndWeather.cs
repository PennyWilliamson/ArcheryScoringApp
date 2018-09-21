using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ArcheryScoringApp
{
	public class UINotesAndWeather : ContentPage
	{
        string note;
        string other;
        string windDir;
        double windspeed;
        double temp;
        double humid;
        string endRef;

        public UINotesAndWeather(string aRef)
        {
            endRef = aRef;
            var scroll = new ScrollView();
            StackLayout layout = new StackLayout()
            {
              //  VerticalOptions = LayoutOptions.Start,
              //  HorizontalOptions = LayoutOptions.Start,
                Padding = new Thickness(50)
            }; 
           

            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20) });


            var weatherHead = new Label { Text = "Weather", TextColor = Color.Black, FontSize = 20 };

            var temp = new Label { Text = "Temperture", TextColor = Color.Black, FontSize = 20 };
            var tempInput = new Entry { MaxLength = 5 };
            tempInput.TextChanged += TempChanged;

            var hum = new Label { Text = "Humidity", TextColor = Color.Black, FontSize = 20 };
            var humInput = new Entry { MaxLength = 7 };
            humInput.TextChanged += HumidChanged;

            var wSpeed = new Label { Text = "Windspeed", TextColor = Color.Black, FontSize = 20 };
            var wSpeedInput = new Entry { MaxLength = 10 };
            wSpeedInput.TextChanged += WindSpeedChanged;

            var wDir = new Label { Text = "Wind direction", TextColor = Color.Black, FontSize = 20 };
            var wDirInput = new Entry { MaxLength = 8 };
            wDirInput.TextChanged += WindDirChanged;

            var other = new Label { Text = "Other", TextColor = Color.Black, FontSize = 20 };
            var otherInput = new Entry { MaxLength = 30 };
            otherInput.TextChanged += OtherChanged;

            var notes = new Label { Text = "Notes", TextColor = Color.Black, FontSize = 20 };
            var notesInput = new Editor
            {
                MaxLength = 250,
                AutoSize = EditorAutoSizeOption.TextChanges
            };
           notesInput.TextChanged += NoteChanged;

            Button backButton = CreateButton("Back");
            backButton.Pressed += BackButtonPressed;

            Button saveButton = CreateButton("Save");
            saveButton.Clicked += SaveClicked;

            grid.Children.Add(weatherHead, 0, 0);
            grid.Children.Add(temp, 0, 1);
            grid.Children.Add(tempInput, 1, 1);
            grid.Children.Add(hum, 0, 2);
            grid.Children.Add(humInput, 1, 2);
            grid.Children.Add(wSpeed, 0, 3);
            grid.Children.Add(wSpeedInput, 1, 3);
            grid.Children.Add(wDir, 0, 4);
            grid.Children.Add(wDirInput, 1, 4);
            grid.Children.Add(other, 0, 5);
            grid.Children.Add(otherInput, 1, 5);
            grid.Children.Add(backButton, 0, 8);
            grid.Children.Add(notes, 0, 6);
            grid.Children.Add(notesInput, 0, 7);
            Grid.SetColumnSpan(notesInput, 2);
            grid.Children.Add(saveButton, 1, 8);


            layout.Children.Add(grid);
            scroll.Content = layout;

            Content = scroll;
            }

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



        
        public void NoteChanged(object sender, TextChangedEventArgs e)
        {
            note = e.NewTextValue;
        }

        public void TempChanged(object sender, TextChangedEventArgs e)
        {
            var a = e.NewTextValue;
            double.TryParse(a, out temp);
        }

        public void WindSpeedChanged(object sender, TextChangedEventArgs e)
        {
            var a = e.NewTextValue;
            double.TryParse(a, out windspeed);
        }

        public void WindDirChanged(object sender, TextChangedEventArgs e)
        {
            windDir = e.NewTextValue;
        }

        public void HumidChanged(object sender, TextChangedEventArgs e)
        {
            var a = e.NewTextValue;
            double.TryParse(a, out humid);
        }

        public void OtherChanged(object sender, TextChangedEventArgs e)
        {
            other = e.NewTextValue;
        }

        public async void SaveClicked(object sender, EventArgs e)
        {
            ViewModel.NotesWeatherViewModel viewModel = new ViewModel.NotesWeatherViewModel();
            if(note != null)
            {
                //App.Database.AddNotes(endRef, note);
                viewModel.NotesSaved(endRef, note);
            }
            
            //windspeed not included as if windspeed = 0, then windDir will be null. 
            //Assumption made that practice will not happen in 0 degree conditions
            if(temp != 0 || windDir != null || humid != 0 || other != null)
            {
                // App.Database.AddWeather(endRef, temp, windspeed, windDir, humid, other);
                viewModel.WeatherSaved(endRef, temp, windspeed, windDir, humid, other);
            }

            await Navigation.PopAsync();
        }

        private async void BackButtonPressed(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}