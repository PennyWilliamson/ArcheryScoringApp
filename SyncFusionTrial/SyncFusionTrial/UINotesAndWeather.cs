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

        public UINotesAndWeather()
        {
            StackLayout layout = new StackLayout();
            var header = new Label { Text = " Notes and Statistics ", TextColor = Color.FromHex("#010101"), FontSize = 30 };

            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(200) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(200) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            var weatherHead = new Label { Text = "Weather", TextColor = Color.Black, FontSize = 30 };

            var temp = new Label { Text = "Temperture", TextColor = Color.Black, FontSize = 30 };
            var tempInput = new Entry { MaxLength = 5 };
            tempInput.TextChanged += TempChanged;

            var hum = new Label { Text = "Humidity", TextColor = Color.Black, FontSize = 30 };
            var humInput = new Entry { MaxLength = 7 };
            humInput.TextChanged += HumidChanged;

            var wSpeed = new Label { Text = "Windspeed", TextColor = Color.Black, FontSize = 30 };
            var wSpeedInput = new Entry { MaxLength = 10 };
            wSpeedInput.TextChanged += WindSpeedChanged;

            var wDir = new Label { Text = "Wind direction", TextColor = Color.Black, FontSize = 30 };
            var wDirInput = new Entry { MaxLength = 8 };
            wDirInput.TextChanged += WindDirChanged;

            var other = new Label { Text = "Other", TextColor = Color.Black, FontSize = 30 };
            var otherInput = new Entry { MaxLength = 30 };
            otherInput.TextChanged += OtherChanged;

            var notes = new Label { Text = "Notes", TextColor = Color.Black, FontSize = 30 };
            var notesInput = new Editor
            {
                MaxLength = 250,
                AutoSize = EditorAutoSizeOption.TextChanges
            };
           notesInput.TextChanged += NoteChanged;

            Button backButton = CreateButton("Back");
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
            grid.Children.Add(backButton, 0, 6);
            grid.Children.Add(notes, 3, 0);
            grid.Children.Add(notesInput, 2, 1);
            Grid.SetColumnSpan(notesInput, 2);
            grid.Children.Add(saveButton, 3, 6);

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

        public void SaveClicked(object sender, EventArgs e)
        {
            if(note != null)
            {
                App.Database.AddNotes(note);
            }
            
            //windspeed not included as if windspeed = 0, then windDir will be null. 
            //Assumption made that practice will not happen in 0 degree conditions
            if(temp != 0 || windDir != null || humid != 0 || other != null)
            {
                App.Database.AddWeather(temp, windspeed, windDir, humid, other);
            }
        }
    }
}