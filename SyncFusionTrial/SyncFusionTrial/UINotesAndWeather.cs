using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ArcheryScoringApp
{
    /// <summary>
    /// The content page for the main page of the application.
    /// Code for components are modelled on Miroscoft Xamarin.Forms documentation.
    /// David Britch, C. D. (2017, July 12). Xamarin.Forms User Interface Views. Retrieved August 2018, from Microsoft: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/index
    /// </summary>
	public class UINotesAndWeather : ContentPage
	{
        private string noteEntry; //variable for note editor input and output.
        private string otherEntry; //variable for other entry box input and output.
        private string windDirEntry; //variable for wind direction entry box input and output.
        private string windspeedEntry; //variable for wind speed entry box input and output.
        private string tempEntry; //variable for temperature entry box input and output.
        private string humidEntry; //variable for humidity entry box input and output.
        private string endRef; //holds passed in end reference for database.

        ViewModel.NotesWeatherViewModel viewModel; // for setting object for method calls.

        /// <summary>
        /// Constructor.
        /// Uses passed in aRef to set endRef. Needed for database and dataset
        /// so that the note and weather cna be associated with an end.
        /// Sets the content and layout of the page.
        /// </summary>
        /// <param name="aRef"></param>
        public UINotesAndWeather(string aRef)
        {
            //sets object for method calls.
            viewModel = new ViewModel.NotesWeatherViewModel();
            
            //sets endRef to passed in value.
            endRef = aRef;

            //checking if endRef is already in dataset.
            GetPrevNotesAndWeather();

            //Allows page to scroll.
            var scroll = new ScrollView();

            //vertical layout
            StackLayout layout = new StackLayout()
            {
                Padding = new Thickness(50)
            }; 
           
            //Allows elements to be placed in a grid layout.
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

            var temp = new Label { Text = "Temperature", TextColor = Color.Black, FontSize = 20 };
            var tempInput = new Entry { Text = tempEntry, MaxLength = 10, FontSize = 20 };//entry text set to class variable.
            tempInput.TextChanged += TempChanged;//method call when text changes.

            var hum = new Label { Text = "Humidity", TextColor = Color.Black, FontSize = 20 };
            var humInput = new Entry { Text = humidEntry, MaxLength = 10, FontSize = 20 };//entry text set to class variable.
            humInput.TextChanged += HumidChanged;//method call when text changes

            var wSpeed = new Label { Text = "Windspeed", TextColor = Color.Black, FontSize = 20 };
            var wSpeedInput = new Entry { Text = windspeedEntry, MaxLength = 10, FontSize = 20 };//entry text set to class variable.
            wSpeedInput.TextChanged += WindSpeedChanged;//method call when text changes.

            var wDir = new Label { Text = "Wind direction", TextColor = Color.Black, FontSize = 20 };
            var wDirInput = new Entry { Text = windDirEntry, MaxLength = 10, FontSize = 20 };//entry text set to class variable.
            wDirInput.TextChanged += WindDirChanged;//method call when text changes.

            var other = new Label { Text = "Other", TextColor = Color.Black, FontSize = 20 };
            var otherInput = new Entry { Text = otherEntry, MaxLength = 150, FontSize = 20 };//entry text set to class variable.
            otherInput.TextChanged += OtherChanged;//method call when text changes.

            var notes = new Label { Text = "Notes", TextColor = Color.Black, FontSize = 20 };
            //Editor text set to class variable.
            //Resizes with new line.
            var notesInput = new Editor
            {
                Text = noteEntry,
                MaxLength = 250,
                AutoSize = EditorAutoSizeOption.TextChanges,
                FontSize = 20
            };
           notesInput.TextChanged += NoteChanged;//method call for text changed.

            Button backButton = CreateButton("Back");
            backButton.Pressed += BackButtonPressed;//method call for button pressed.

            Button saveButton = CreateButton("Save");
            saveButton.Clicked += SaveClicked;//method call for button pressed.

            //Sets grid layout.
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
            Grid.SetColumnSpan(notesInput, 2);//allows two columns width for notes.
            grid.Children.Add(saveButton, 1, 8);


            layout.Children.Add(grid);//grid added to stack layout.
            scroll.Content = layout;//stackloyout added to scroll.

            Content = scroll;//sets pages content.
            }

        /// <summary>
        /// Creates a new button with text set to label.
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
        /// Handles notes editor box text changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NoteChanged(object sender, TextChangedEventArgs e)
        {
            noteEntry = e.NewTextValue;
        }

        /// <summary>
        /// Handles text change for temperature entry box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TempChanged(object sender, TextChangedEventArgs e)
        {
            tempEntry = e.NewTextValue;
        }

        /// <summary>
        /// Handles text change for windpseed entry box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindSpeedChanged(object sender, TextChangedEventArgs e)
        {
            windspeedEntry = e.NewTextValue;
        }

        /// <summary>
        /// Handles text change for wind direction entry box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindDirChanged(object sender, TextChangedEventArgs e)
        {
            windDirEntry = e.NewTextValue;
        }

        /// <summary>
        /// Handles text change for humidity entry box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HumidChanged(object sender, TextChangedEventArgs e)
        {
            humidEntry = e.NewTextValue;
        }

        /// <summary>
        /// Handles text changed for other entry box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OtherChanged(object sender, TextChangedEventArgs e)
        {
            otherEntry = e.NewTextValue;
        }

        /// <summary>
        /// Checks to see if notes or weather already exist in dataset for end.
        /// If they exist, calls method and sets class variables to return for display.
        /// </summary>
        private void GetPrevNotesAndWeather()
        {
            bool notesExist = viewModel.DoNotesExist(endRef);
            bool weatherExist = viewModel.DoWeatherExist(endRef);

            if (notesExist == true)
            {
               noteEntry = viewModel.PrevNotes(endRef);
            }

            if(weatherExist == true)
            { //uses values from object return to set class values.
                Model.WeatherModel prev = viewModel.PrevWeather(endRef);
                otherEntry = prev.other;
                windDirEntry = prev.dir;
                windspeedEntry = prev.speed;
                tempEntry = prev.temp;
                humidEntry = prev.hum;
            }
        }

        /// <summary>
        /// Handles save button clicked code.
        /// async due to page navigation call.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SaveClicked(object sender, EventArgs e)
        {
            if(noteEntry != null)
            {
                viewModel.NotesSaved(endRef, noteEntry);
            }
            
            //windspeed not included as if windspeed = 0, then windDir will be null. 
            //Assumption made that practice will not happen in 0 degree conditions
            if(tempEntry != null || windDirEntry != null || humidEntry != null || otherEntry != null)
            {
                // App.Database.AddWeather(endRef, temp, windspeed, windDir, humid, other);
                viewModel.WeatherSaved(endRef, tempEntry, windspeedEntry, windDirEntry, humidEntry, otherEntry);
            }

            await Navigation.PopAsync(); //goes back to previous practice screen on stack, preserves existing data on practice screen.
        }

        /// <summary>
        /// Handles back navigation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BackButtonPressed(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}