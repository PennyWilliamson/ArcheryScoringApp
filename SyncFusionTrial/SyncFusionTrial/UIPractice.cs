﻿using Syncfusion.Data;
using Syncfusion.SfDataGrid.XForms;
using Syncfusion.XForms.PopupLayout;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ArcheryScoringApp
{
    public class UIPractice : ContentPage
    {
        static internal int PracID { get; set; } //internal so it can be accessed from other classes
        static internal int dtlIDPrac { get; set; }
        static internal int HoldPracID { get; set; }//holds PracID while previous fired. 
        string marking;//holds new sight marking for edit sight marking
        string search; //holds search text from textchanged
        static Model.PracticeViewModel viewModel;
        static SfDataGrid dataGrid;
        SfPopupLayout popupLayout;
        SfPopupLayout prevPop;
        SfPopupLayout notValid;
        SfPopupLayout selectEndRow;
        static string date;


        public UIPractice()
        {

            dataGrid = CreateDataGrid();

            viewModel = new Model.PracticeViewModel();

            notValid = new SfPopupLayout();
            selectEndRow = new SfPopupLayout();

            StackLayout layout = new StackLayout()
            {
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Start,
                Padding = new Thickness(50)
            };


            var grid = new Grid { RowSpacing = 50 };

            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(35) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(35) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(35) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(60) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });

            prevPop = new SfPopupLayout();

            Button previousButton = createButton("Search"); //displays previous practice score sheet, searched on score
            previousButton.Clicked += PrevButtonClicked;

            Button mainButton = createButton("Main"); //back to main screen
            mainButton.Clicked += MainButtonClicked;

            Button ntswthrButton = createButton("Notes Weather"); //notes and weather button
            ntswthrButton.Clicked += NotesAndWeatherClicked;

            Button editSightButton = createButton("Edit Sight"); //for editing sight markings
            editSightButton.Clicked += EditSightClicked;

            Button saveButton = createButton("Save"); //saves current sheet and associated notes and weather entries
            saveButton.Clicked += SaveClicked;

            Button detailsButton = createButton("Details");



            Label searchLabel = new Label { Text = "Search by score", TextColor = Color.FromHex("#010101"), FontSize = 10 };
            var searchScore = new Entry { Text = " ", FontSize = 10 };
            searchScore.TextChanged += SearchChanged;

            var newSightMarking = new Entry { Text = " ", FontSize = 10 };
            newSightMarking.TextChanged += SightChanged;


            popupLayout = new SfPopupLayout();

            detailsButton.Clicked += DetailsButtonClicked;


            grid.Children.Add(dataGrid, 0, 3);
            Grid.SetColumnSpan(dataGrid, 3);
            grid.Children.Add(searchLabel, 0, 0);
            grid.Children.Add(searchScore, 1, 0);
            grid.Children.Add(previousButton, 2, 0);
            grid.Children.Add(ntswthrButton, 1, 2);
            grid.Children.Add(mainButton, 0, 2);
            grid.Children.Add(newSightMarking, 0, 1);
            grid.Children.Add(editSightButton, 1, 1);
            grid.Children.Add(detailsButton, 2, 1);
            grid.Children.Add(saveButton, 2, 2);
            layout.Children.Add(grid);

            Content = layout;


        }

        static public void NotValid(String score)
        {
            SfPopupLayout notValid = new SfPopupLayout();
            Label content = new Label { Text = "Entered score is not a valid score and will be recorded as a '0'. Please change your score.", TextColor = Color.FromHex("#010101"), BackgroundColor = Color.White, FontSize = 30 };
            notValid.PopupView.ContentTemplate = new DataTemplate(() =>
            {
                notValid.Padding = 10;
                notValid.PopupView.HeaderTitle = "Invalid Score";
                notValid.PopupView.ShowFooter = false;
                return content;
            });
            notValid.StaysOpen = true;
            notValid.PopupView.ShowCloseButton = true;
            notValid.IsOpen = true;
            notValid.Show();
        }

        private async void MainButtonClicked(object sender, EventArgs e)
        {
            PracID = -1;//resets it
            Model.PracEndsHold.ResetHold();
            await Navigation.PopToRootAsync();
        }

        private async void NotesAndWeatherClicked(object sender, EventArgs e)
        {
            string endRef = null;
            Model.PracticeModel curEnd = (Model.PracticeModel)dataGrid.SelectedItem;
            if (curEnd != null)
            {
                endRef = curEnd.ER;
            }

            if (endRef != null)
            {
                await Navigation.PushAsync(new UINotesAndWeather(endRef) { Title = "Notes and Weather" });
            }
            else
            {
                SelectEndRow();
            }

        }

        private void SelectEndRow()
        {
            Label content = new Label { Text = "Please Select a Row and try again.", TextColor = Color.Black, BackgroundColor = Color.White, FontSize = 30 };
            selectEndRow.PopupView.ContentTemplate = new DataTemplate(() =>
            {
                selectEndRow.Padding = 10;
                selectEndRow.PopupView.HeaderTitle = "Try Again";
                selectEndRow.PopupView.BackgroundColor = Color.White;
                selectEndRow.HorizontalOptions = LayoutOptions.FillAndExpand;
                selectEndRow.PopupView.ShowFooter = false;
                return content;
            });
            selectEndRow.StaysOpen = true;
            selectEndRow.PopupView.ShowCloseButton = true;
            selectEndRow.IsOpen = true;
            selectEndRow.Show();
        }

        private void DetailsButtonClicked(object sender, EventArgs e)
        {
            popupLayout.PopupView.ContentTemplate = new DataTemplate(() =>
            {
                popupLayout.Padding = 10;
                popupLayout.PopupView.HeaderTitle = "Details";
                popupLayout.PopupView.BackgroundColor = Color.White;
                popupLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
                prevPop.PopupView.WidthRequest = 360;
                popupLayout.PopupView.ShowFooter = false;
                return CreateGrid();
            });
            popupLayout.StaysOpen = true;
            popupLayout.PopupView.ShowCloseButton = true;
            popupLayout.IsOpen = true;
            popupLayout.Show();
        }

        public void SightChanged(object sender, TextChangedEventArgs e)
        {
            marking = e.NewTextValue;
        }

        private void EditSightClicked(object sender, EventArgs e)
        {
            double nsm;
            double.TryParse(marking, out nsm);
            App.Database.EditSight(nsm);
        }

        public void SearchChanged(object sender, TextChangedEventArgs e)
        {
            search = e.NewTextValue;
        }

        private void PrevButtonClicked(object sender, EventArgs e)
        {
            HoldPracID = PracID;
            PracID = -1;//stops code firing in PracticeModel.
            List<Data.End> ends = new List<Data.End>();
            int srch;//int variable for parsing string to int

            int.TryParse(search, out srch);
            ends = Model.EndModel.GetPrev(srch, "Practice");
            string distDate = Model.DetailsModel.GetPrevDetails(ends);
            prevPop.PopupView.ContentTemplate = new DataTemplate(() =>
            {
                prevPop.Padding = 10;
                prevPop.PopupView.HeaderTitle = "Prac" + distDate;
                prevPop.BackgroundColor = Color.White;
                prevPop.HorizontalOptions = LayoutOptions.FillAndExpand;
                prevPop.VerticalOptions = LayoutOptions.FillAndExpand;
                prevPop.PopupView.WidthRequest = 360;
                prevPop.PopupView.ShowFooter = false;
                return CreateDataGridPrev(ends);
            });
            prevPop.StaysOpen = true;
            prevPop.PopupView.ShowCloseButton = true;
            prevPop.IsOpen = true;
            prevPop.Show();
            PracID = HoldPracID;

        }

        private void SaveClicked(object sender, EventArgs e)
        {

            Model.PracEndsHold.Save();
            Model.NotesHold.NotesSaved();
            Model.WeatherHold.WeatherSaved();
        }

        static Button createButton(string lbl)
        {
            Button button = new Button
            {
                Text = lbl,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.LightBlue,
                FontSize = 10
            };
            return button;
        }

        static SfDataGrid CreateDataGrid()
        {
            SfDataGrid dataGrid = new SfDataGrid();

            viewModel = new Model.PracticeViewModel();
            dataGrid.ItemsSource = viewModel.EndCollection;
            dataGrid.ColumnSizer = ColumnSizer.Auto; //set to auto so it scrolls horizontally

            dataGrid.AllowEditing = true;
            dataGrid.EditTapAction = TapAction.OnTap; //Enter edit mode in single tap instead of default double tap.
            dataGrid.EditorSelectionBehavior = EditorSelectionBehavior.SelectAll;
            dataGrid.NotificationSubscriptionMode = NotificationSubscriptionMode.PropertyChange;
            dataGrid.SelectionMode = SelectionMode.Single;


            //so end number column cannot be edited
            GridTextColumn endNo = new GridTextColumn();
            endNo.MappingName = "EndNum";
            endNo.AllowEditing = false;
            dataGrid.AutoGenerateColumns = false;

            GridTextColumn arrow1 = new GridTextColumn();
            arrow1.MappingName = "Arrow1";
            arrow1.HeaderText = "Arrow 1";

            GridTextColumn arrow2 = new GridTextColumn();
            arrow2.MappingName = "Arrow2";
            arrow2.HeaderText = "Arrow 2";

            GridTextColumn arrow3 = new GridTextColumn();
            arrow3.MappingName = "Arrow3";
            arrow3.HeaderText = "Arrow 3";

            GridTextColumn arrow4 = new GridTextColumn();
            arrow4.MappingName = "Arrow4";
            arrow4.HeaderText = "Arrow 4";

            GridTextColumn arrow5 = new GridTextColumn();
            arrow5.MappingName = "Arrow5";
            arrow5.HeaderText = "Arrow 5";

            GridTextColumn arrow6 = new GridTextColumn();
            arrow6.MappingName = "Arrow6";
            arrow6.HeaderText = "Arrow 6";

            GridTextColumn endTotal = new GridTextColumn();
            endTotal.MappingName = "EndTotal";
            endTotal.HeaderText = "End Total";


            //for running total column
            GridTextColumn runningTotal = new GridTextColumn();
            runningTotal.MappingName = "RunningTotal";
            runningTotal.HeaderText = "Running Total";

            dataGrid.Columns.Add(endNo);
            dataGrid.Columns.Add(arrow1);
            dataGrid.Columns.Add(arrow2);
            dataGrid.Columns.Add(arrow3);
            dataGrid.Columns.Add(arrow4);
            dataGrid.Columns.Add(arrow5);
            dataGrid.Columns.Add(arrow6);
            dataGrid.Columns.Add(endTotal);
            dataGrid.Columns.Add(runningTotal);

            return dataGrid;
        }



        static SfDataGrid CreateDataGridPrev(List<Data.End> ends)
        {
            SfDataGrid dataGrid = new SfDataGrid();

            viewModel = new Model.PracticeViewModel(ends);
            dataGrid.ItemsSource = viewModel.EndCollection;
            dataGrid.ColumnSizer = ColumnSizer.Auto; //set to auto so it scrolls horizontally

            dataGrid.AllowEditing = true;
            dataGrid.EditTapAction = TapAction.OnTap; //Enter edit mode in single tap instead of default double tap.
            dataGrid.EditorSelectionBehavior = EditorSelectionBehavior.SelectAll;
            dataGrid.NotificationSubscriptionMode = NotificationSubscriptionMode.PropertyChange;
            dataGrid.NotificationSubscriptionMode = NotificationSubscriptionMode.CollectionChange;


            //so end number column cannot be edited
            GridTextColumn endNo = new GridTextColumn();
            endNo.MappingName = "EndNum";
            endNo.AllowEditing = false;
            dataGrid.AutoGenerateColumns = false;

            GridTextColumn arrow1 = new GridTextColumn();
            arrow1.MappingName = "Arrow1";
            arrow1.HeaderText = "Arrow 1";

            GridTextColumn arrow2 = new GridTextColumn();
            arrow2.MappingName = "Arrow2";
            arrow2.HeaderText = "Arrow 2";

            GridTextColumn arrow3 = new GridTextColumn();
            arrow3.MappingName = "Arrow3";
            arrow3.HeaderText = "Arrow 3";

            GridTextColumn arrow4 = new GridTextColumn();
            arrow4.MappingName = "Arrow4";
            arrow4.HeaderText = "Arrow 4";

            GridTextColumn arrow5 = new GridTextColumn();
            arrow5.MappingName = "Arrow5";
            arrow5.HeaderText = "Arrow 5";

            GridTextColumn arrow6 = new GridTextColumn();
            arrow6.MappingName = "Arrow6";
            arrow6.HeaderText = "Arrow 6";

            GridTextColumn endTotal = new GridTextColumn();
            endTotal.MappingName = "EndTotal";
            endTotal.HeaderText = "End Total";


            //for running total column
            GridTextColumn runningTotal = new GridTextColumn();
            runningTotal.MappingName = "RunningTotal";
            runningTotal.HeaderText = "Running Total";

            //for Weather column
            GridTextColumn weather = new GridTextColumn();
            weather.MappingName = "Weather";
            weather.HeaderText = "Weather Conditions";

            //for Notes column
            GridTextColumn notes = new GridTextColumn();
            notes.MappingName = "Notes";
            notes.HeaderText = "End Notes";

            dataGrid.Columns.Add(endNo);
            dataGrid.Columns.Add(arrow1);
            dataGrid.Columns.Add(arrow2);
            dataGrid.Columns.Add(arrow3);
            dataGrid.Columns.Add(arrow4);
            dataGrid.Columns.Add(arrow5);
            dataGrid.Columns.Add(arrow6);
            dataGrid.Columns.Add(endTotal);
            dataGrid.Columns.Add(runningTotal);
            dataGrid.Columns.Add(weather);
            dataGrid.Columns.Add(notes);

            return dataGrid;
        }


        static Grid CreateGrid()
        {
            Grid gridDetails = new Grid() { RowSpacing = 0, ColumnSpacing = 0, BackgroundColor = Color.White };

            gridDetails.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            gridDetails.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            gridDetails.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            gridDetails.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            gridDetails.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            gridDetails.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10) });//Acts as a spacer
            gridDetails.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });



            gridDetails.Children.Add(new Label { Text = "Name: Caitlin Thomas-Riley", TextColor = Color.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, 0, 0);
            gridDetails.Children.Add(new Label { Text = "Bow Type: " + ArchMain.bowType, TextColor = Color.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, 2, 0);
            gridDetails.Children.Add(new Label { Text = "Division: JWR", TextColor = Color.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, 0, 1);
            gridDetails.Children.Add(new Label { Text = "Club: Randwick", TextColor = Color.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, 2, 1);
            gridDetails.Children.Add(new Label { Text = "Date: " + date, TextColor = Color.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, 0, 2);
            gridDetails.Children.Add(new Label { Text = "Archery NZ No.: 3044", TextColor = Color.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, 2, 2);
            gridDetails.Children.Add(new Label { Text = "Distance: " + ArchMain.dist + "m", TextColor = Color.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, 0, 3);




            return gridDetails;
        }





        //need an OnAppearing for Scoring sheet to be set up in database and ID returned.
        protected override void OnAppearing()
        {
            DateTime tdy = DateTime.Today;
            date = tdy.ToString("d");
            string type = "Practice";
            if (PracID == -1) //set to -1 in App constructor so that this does not refire a new save
            {
                Model.PracticeModel.rT = 0; //resets running total
                Model.DetailsModel details = new Model.DetailsModel();
                dtlIDPrac = details.SetDetails(date);


                Model.ScoringSheetModel scoringSheet = new Model.ScoringSheetModel();

                PracID = scoringSheet.SetScoringSheet(dtlIDPrac, type);// sets ID to the scoring sheet ID
                int p = PracID; //seems to stop PracID being -1.
            }
        }
    }
}