using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Syncfusion.XForms.ComboBox;
using Syncfusion.XForms.PopupLayout;

namespace ArcheryScoringApp
{
	public class ArchMain : ContentPage
	{
        static internal string bowType;
        static internal string dist;
        static internal string compType;
        SfPopupLayout noBowOrDist;

        public ArchMain ()
		{
            //resetting on return to main.
            UIComp720.ID = -1;
            UIPractice.PracID = -1;
            Model.PracEndsHold.ResetHold();

            noBowOrDist = new SfPopupLayout();

             StackLayout layout = new StackLayout()
             {
                 VerticalOptions = LayoutOptions.Start,
                  HorizontalOptions = LayoutOptions.Start,
                  Padding = new Thickness(50)
              };
            var header = new Label { Text = "Archery Scoring Application ", TextColor = Color.FromHex("#010101"), FontSize = 30 };
            var grid = new Grid { RowSpacing = 50};

            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });


            var label1 = new Label { Text = "Bow Type: ", TextColor = Color.FromHex("#010101"), FontSize = 20 };
            List<String> bowTypeList = new List<String>();
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
                bowType = comboBoxBow.SelectedValue.ToString();
                App.Database.AddBow(bowType); //adds bow to database
            }; 

            var label2 = new Label { Text = "Distance: ", TextColor = Color.FromHex("#010101"), FontSize = 20 };
            List<String> distList = new List<String>();
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
                dist = comboBoxDist.SelectedValue.ToString();
            };

            var label3 = new Label { Text = "Competition: ", TextColor = Color.FromHex("#010101"), FontSize = 20 };
            List<String> compList = new List<String>();
            compList.Add("720");

            SfComboBox comboBoxComp = new SfComboBox();
            comboBoxComp.HeightRequest = 40;
            comboBoxComp.MaximumDropDownHeight = 200;
            comboBoxComp.IsEditableMode = false;
            comboBoxComp.DataSource = compList;
            comboBoxComp.SelectionChanged += (object sender, SelectionChangedEventArgs e) =>
            {
                var a = comboBoxComp.SelectedIndex;
                compType = comboBoxComp.SelectedValue.ToString();
            };

            Button compButton = new Button
            {
                Text = "Competition",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };
            compButton.Clicked += CompButtonClicked;

            Button pracButton = new Button
            {
                Text = "Practice",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };
            pracButton.Clicked += PracButtonClicked;

            Button backupButton = new Button
            {
                Text = "Backup",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };

            grid.Children.Add(compButton, 0, 0);
            grid.Children.Add(pracButton, 1, 0);
            grid.Children.Add(label1, 0, 1);
            grid.Children.Add(comboBoxBow, 1, 1);
            grid.Children.Add(label2, 0, 2);
            grid.Children.Add(comboBoxDist, 1, 2);
            grid.Children.Add(label3, 0, 3);
            grid.Children.Add(comboBoxComp, 1, 3);
            grid.Children.Add(backupButton, 0, 4);
            layout.Children.Add(header);
            layout.Children.Add(grid);
            Content = layout;
        }

        private async void CompButtonClicked(object sender, EventArgs e)
        {
            if (bowType != null && dist != null)
            {
                await Navigation.PushAsync(new UIStats());
            }
            else
            {
                ValuesNull();
            }
        }

        private async void PracButtonClicked(object sender, EventArgs e)
        {
            if (bowType != null && dist != null)
            {
                await Navigation.PushAsync(new UIPractice());
            }
            else
            {
                ValuesNull();
            }
        }

        private void ValuesNull()
        {
            Label content = new Label { Text = "Please select a Bow Type and Distance from the drop down menus and try again.", TextColor = Color.FromHex("#010101"), BackgroundColor = Color.White, FontSize = 30 };
            noBowOrDist.PopupView.ContentTemplate = new DataTemplate(() =>
                {
                    noBowOrDist.PopupView.HeaderTitle = "Choose from dropdowns";
                    noBowOrDist.PopupView.ShowFooter = false;
                    return content;
                });
            noBowOrDist.StaysOpen = true;
            noBowOrDist.PopupView.ShowCloseButton = true;
            noBowOrDist.IsOpen = true;
            noBowOrDist.Show();
        }
    }
}