using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Syncfusion.XForms.ComboBox;
using Syncfusion.XForms.PopupLayout;

namespace ArcheryScoringApp
{
    /// <summary>
    /// The content page for the main page of the application.
    /// Uses SyncFusion commponets, and code for components are modelled 
    /// on SynFusion documentation and Miroscoft Xamarin.Forms documentation.
    /// Syncfusion. (2001 - 2018). Xamarin.Forms. Retrieved August 2018, from Syncfusion Documentation: https://help.syncfusion.com/xamarin/introduction/overview#how-to-best-read-this-user-guide
    /// David Britch, C. D. (2017, July 12). Xamarin.Forms User Interface Views. Retrieved August 2018, from Microsoft: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/index
    /// </summary>
	public class UIArchMain : ContentPage
	{
        internal static string bowType;///holds bow type selected from combo box, static internal as it is accessed within namespace.
        internal static string dist;///holds distance selected from combo box, static internal as it is accessed within namespace.
        internal static string compType;///holds competition type selected from combo box, static internal as it is accessed within namespace.
        private SfPopupLayout noBowOrDist;///pop up, declared in constructor for use in methods.

        /// <summary>
        /// Constructor.
        /// Sets the elements on the page and the layout.
        /// </summary>
        public UIArchMain ()
		{
            //resets values
            UIComp720.ID = -1;
            UIPractice.PracID = -1;
            Model.PracEndsHold.ResetHold();

            noBowOrDist = new SfPopupLayout();
            var scroll = new ScrollView();//allows page to scroll.

             StackLayout layout = new StackLayout()//vertical layout
             {
                 VerticalOptions = LayoutOptions.Start,
                  HorizontalOptions = LayoutOptions.Start,
                  Padding = new Thickness(50)
              };
            
            var grid = new Grid { RowSpacing = 50};//allows more than one element across the page

            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20) });


            var label1 = new Label { Text = "Bow Type: ", TextColor = Color.FromHex("#010101"), FontSize = 20 };
            List<String> bowTypeList = new List<String>(); //List for combo box.
            bowTypeList.Add("Recurve");
            bowTypeList.Add("Compound");
            bowTypeList.Add("Barebow");
            bowTypeList.Add("Crossbow");
            bowTypeList.Add("Longbow");
            

            SfComboBox comboBoxBow = new SfComboBox();
            comboBoxBow.HeightRequest = 40;
            comboBoxBow.MaximumDropDownHeight = 200;
            comboBoxBow.IsEditableMode = false;
            comboBoxBow.DataSource = bowTypeList;
            comboBoxBow.SelectionChanged += (object sender, SelectionChangedEventArgs e) =>
            {
                var a = comboBoxBow.SelectedIndex;
                bowType = comboBoxBow.SelectedValue.ToString(); //sets class variable bowType to selected value.
                App.Database.AddBow(bowType); //adds bow to database
            }; 

            var label2 = new Label { Text = "Distance: ", TextColor = Color.FromHex("#010101"), FontSize = 20 };
            List<String> distList = new List<String>();//List for distance combo box
            distList.Add("90");
            distList.Add("70");
            distList.Add("60");
            distList.Add("50");
            distList.Add("40");
            distList.Add("30");
            distList.Add("20");
            distList.Add("15");

            SfComboBox comboBoxDist = new SfComboBox();
            comboBoxDist.HeightRequest = 40;
            comboBoxDist.MaximumDropDownHeight = 200;
            comboBoxDist.IsEditableMode = false;
            comboBoxDist.DataSource = distList;
            comboBoxDist.SelectionChanged += (object sender, SelectionChangedEventArgs e) =>
            {
                var a = comboBoxDist.SelectedIndex;
                dist = comboBoxDist.SelectedValue.ToString();//sets class variable distance.
            };//no database call as distance is part of details

            var label3 = new Label { Text = "Competition: ", TextColor = Color.FromHex("#010101"), FontSize = 20 };
            List<String> compList = new List<String>();//sets list for competition type
            compList.Add("720");

            SfComboBox comboBoxComp = new SfComboBox();
            comboBoxComp.HeightRequest = 40;
            comboBoxComp.MaximumDropDownHeight = 200;
            comboBoxComp.IsEditableMode = false;
            comboBoxComp.DataSource = compList;
            comboBoxComp.SelectionChanged += (object sender, SelectionChangedEventArgs e) =>
            {
                var a = comboBoxComp.SelectedIndex;
                compType = comboBoxComp.SelectedValue.ToString();//sets selected value to class variable compType
            };//no database call as compType part of scoring sheet.

            //Button for competition
            Button compButton = new Button
            {
                Text = "Competition",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.LightBlue
            };
            compButton.Clicked += CompButtonClicked; //method call for button press

            //Button for practice
            Button pracButton = new Button
            {
                Text = "Practice",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.LightBlue
            };
            pracButton.Clicked += PracButtonClicked;//method call for button press

            //Button for backup
            Button backupButton = new Button
            {
                Text = "Backup",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.LightBlue
            };
            backupButton.Clicked += BackUpButtonClicked;//method call for button press

            //elements added to grid for display
            grid.Children.Add(label1, 0, 0);
            grid.Children.Add(comboBoxBow, 1, 0);
            grid.Children.Add(label2, 0, 1);
            grid.Children.Add(comboBoxDist, 1, 1);
            grid.Children.Add(label3, 0, 2);
            grid.Children.Add(comboBoxComp, 1, 2);
            grid.Children.Add(compButton, 0, 3);
            grid.Children.Add(pracButton, 1, 3);
            grid.Children.Add(backupButton, 0, 4);

            layout.Children.Add(grid);//grid in layout
            scroll.Content = layout;//layout in scroll
            Content = scroll;//allows scrolling
        }

        private async void CompButtonClicked(object sender, EventArgs e)
        {
            //checks if bow and distance have been selected, needed for database calls in competition page
            if (bowType != null && dist != null)
            {
                //values reset
                UIComp720.ID = -1;
                Model.CalcCompEndTotal.tens = 0;
                Model.CalcCompEndTotal.xs = 0;
                Model.CalcRT.curRT = 0;
                await Navigation.PushAsync(new UIStats() { Title = "Competition Statistics" });//calls new page
            }
            else
            {
                ValuesNull();//calls popup method.
            }
        }

        /// <summary>
        /// Method for when practice button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void PracButtonClicked(object sender, EventArgs e)
        {
            if (bowType != null && dist != null)
            {
                UIPractice.PracID = -1;
                Model.CalcRT.curRT = 0;
                await Navigation.PushAsync(new UIPractice() { Title = "Practice Scoring"});//calls new practice page
            }
            else
            {
                ValuesNull();
            }
        }

        /// <summary>
        /// Method for popup when distance or bowtype have not been selected.
        /// Popup is modal.
        /// </summary>
        private void ValuesNull()
        {
            Label content = new Label { Text = "Please select a Bow Type and Distance from the drop down menus and try again.", TextColor = Color.FromHex("#010101"), BackgroundColor = Color.White, FontSize = 30 };
            noBowOrDist.PopupView.ContentTemplate = new DataTemplate(() =>
                {
                    noBowOrDist.Padding = 10;
                    noBowOrDist.PopupView.HeaderTitle = "Choose from dropdowns";
                    noBowOrDist.PopupView.ShowFooter = false;
                    return content;
                });
            noBowOrDist.StaysOpen = true;
            noBowOrDist.PopupView.ShowCloseButton = true;
            noBowOrDist.IsOpen = true;
            noBowOrDist.Show();
        }

        /// <summary>
        /// Method for when backup button is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackUpButtonClicked(object sender, EventArgs e)
        {
            //all following classes in DatabaseBackup file.
             Data.BowBackup bowBack = new Data.BowBackup();//class for writing bow backup csv, needed for database query output type.
             Data.CompBackup compBack = new Data.CompBackup();//class for writing competition backup csv, needed for database query output type.
            Data.PracBackup pracBack = new Data.PracBackup();//class for writing practice backup csv, needed for database query output type.
            Data.DatabaseBackup email = new Data.DatabaseBackup();//class for sending e-mail.
             bowBack.BowToCSV();
             compBack.CompToCSV();
             pracBack.PracToCSV();
             email.EmailBackup();

        }
        
        /// <summary>
        /// The error message for e-mail and file writing.
        /// Called from the DatabaseBackup class.
        /// Popup is modal.
        /// </summary>
        /// <param name="e"></param>
        static internal void ErrorMess(string e)
        {
            SfPopupLayout noBowOrDist = new SfPopupLayout();//sets popup as method is static
            Label err = new Label { Text = e };// e passed from database backup classes
            noBowOrDist.PopupView.ContentTemplate = new DataTemplate(() =>
            {
                noBowOrDist.Padding = 10;
                noBowOrDist.PopupView.HeaderTitle = "Choose from dropdowns";
                noBowOrDist.PopupView.ShowFooter = false;
                return err;
            });
            noBowOrDist.StaysOpen = true;
            noBowOrDist.PopupView.ShowCloseButton = true;
            noBowOrDist.IsOpen = true;
            noBowOrDist.Show();
        }
    }
}