using Syncfusion.Data;
using Syncfusion.SfDataGrid.XForms;
using Syncfusion.XForms.PopupLayout;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ArcheryScoringApp
{
    /// <summary>
    /// The content page for the Practice scoring page of the application.
    /// Uses SyncFusion commponets, and code for components are modelled 
    /// on SynFusion documentation and Miroscoft Xamarin.Forms documentation.
    /// Syncfusion. (2001 - 2018). Xamarin.Forms. Retrieved August 2018, from Syncfusion Documentation: https://help.syncfusion.com/xamarin/introduction/overview#how-to-best-read-this-user-guide
    /// David Britch, C. D. (2017, July 12). Xamarin.Forms User Interface Views. Retrieved August 2018, from Microsoft: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/index
    /// </summary>
    public class UIPractice : ContentPage
    {
        internal static int PracID { get; set; } ///internal so it can be accessed from other classes
        internal static int dtlIDPrac { get; set; } ///internal so it can be accessed from other classes
        internal static int HoldPracID { get; set; }///holds PracID while previous fired. 
        private string marking;//holds new sight marking for edit sight marking
        private string search; //holds search text from textchanged
        //components for display.
        private static Model.PracticeViewModel viewModel;
        private static SfDataGrid dataGrid;
        private SfPopupLayout popupDetails;
        private SfPopupLayout prevPop;
        private SfPopupLayout notValid;
        private SfPopupLayout selectEndRow;
        private static string date;//holds todays date.

        /// <summary>
        /// Constructor.
        /// Sets components and layout for Practice page.
        /// </summary>
        public UIPractice()
        {
            //creates datgrid for scoring
            dataGrid = CreateDataGrid();

            viewModel = new Model.PracticeViewModel();//viewModel for method calls

            notValid = new SfPopupLayout();//sets pop-ups used in methods
            selectEndRow = new SfPopupLayout();

            //layout for vertical
            StackLayout layout = new StackLayout()
            {
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Start,
                Padding = new Thickness(50)
            };

            //allows components to be diplayed in a grid
            var grid = new Grid { RowSpacing = 50 };

            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(35) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(35) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(35) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(60) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });

            prevPop = new SfPopupLayout();//pop-up for use in method.

            Button previousButton = createButton("Search"); //displays previous practice score sheet, searched on score
            previousButton.Clicked += PrevButtonClicked;//method call

            Button mainButton = createButton("Main"); //back to main screen
            mainButton.Clicked += MainButtonClicked;//method call

            Button ntswthrButton = createButton("Notes Weather"); //notes and weather button
            ntswthrButton.Clicked += NotesAndWeatherClicked;//method call

            Button editSightButton = createButton("Edit Sight"); //for editing sight markings
            editSightButton.Clicked += EditSightClicked;//method call

            Button saveButton = createButton("Save"); //saves current sheet and associated notes and weather entries
            saveButton.Clicked += SaveClicked;//method call

            Button detailsButton = createButton("Details");
            detailsButton.Clicked += DetailsButtonClicked;//method call

            Label searchLabel = new Label { Text = "Search by score", TextColor = Color.FromHex("#010101"), FontSize = 10 };
            var searchScore = new Entry { Text = " ", FontSize = 10 };
            searchScore.TextChanged += SearchChanged;//method call

            var newSightMarking = new Entry { Text = " ", FontSize = 10 };
            newSightMarking.TextChanged += SightChanged;//method call

            popupDetails = new SfPopupLayout();//pop-up object for method.

            //Sets grid for display
            grid.Children.Add(dataGrid, 0, 3);
            Grid.SetColumnSpan(dataGrid, 3);//allows grid to be 3 columns wide.
            grid.Children.Add(searchLabel, 0, 0);
            grid.Children.Add(searchScore, 1, 0);
            grid.Children.Add(previousButton, 2, 0);
            grid.Children.Add(ntswthrButton, 1, 2);
            grid.Children.Add(mainButton, 0, 2);
            grid.Children.Add(newSightMarking, 0, 1);
            grid.Children.Add(editSightButton, 1, 1);
            grid.Children.Add(detailsButton, 2, 1);
            grid.Children.Add(saveButton, 2, 2);

            layout.Children.Add(grid);//adds grid to layout

            Content = layout;//sets content to layout
        }

        /// <summary>
        /// Displays pop-up for invalid score.
        /// Called from PracticeModel.
        /// </summary>
        /// <param name="score"></param>
        public static void NotValid(String score)
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

        /// <summary>
        /// Handles navigation to root page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MainButtonClicked(object sender, EventArgs e)
        {
            PracID = -1;//resets it
            Model.PracEndsHold.ResetHold();
            await Navigation.PopToRootAsync();
        }

        /// <summary>
        /// Handles navigation to notes and weather page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void NotesAndWeatherClicked(object sender, EventArgs e)
        {
            string endRef = null;
            Model.PracticeModel curEnd = (Model.PracticeModel)dataGrid.SelectedItem; //uses selected item to set curEnd
            if (curEnd != null)//checks curEnd has been set
            {
                endRef = curEnd.ER;//sets endRef to that is curEnd object
            }

            if (endRef != null) //navigates to new page.
            {
                await Navigation.PushAsync(new UINotesAndWeather(endRef) { Title = "Notes and Weather" });
            }
            else
            {
                SelectEndRow();//error message
            }
        }

        /// <summary>
        /// Pop-up for when a row is not selected and notes weather button pushed.
        /// Pop-up modal.
        /// </summary>
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

        /// <summary>
        /// Gives details pop-up when details button pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailsButtonClicked(object sender, EventArgs e)
        {
            popupDetails.PopupView.ContentTemplate = new DataTemplate(() =>
            {
                popupDetails.Padding = 10;
                popupDetails.PopupView.HeaderTitle = "Details";
                popupDetails.PopupView.BackgroundColor = Color.White;
                popupDetails.HorizontalOptions = LayoutOptions.FillAndExpand;
                prevPop.PopupView.WidthRequest = 360;
                popupDetails.PopupView.ShowFooter = false;
                return CreateDetailsGrid();
            });
            popupDetails.StaysOpen = true;
            popupDetails.PopupView.ShowCloseButton = true;
            popupDetails.IsOpen = true;
            popupDetails.Show();
        }

        /// <summary>
        /// Handles text changing in sight marking entry box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SightChanged(object sender, TextChangedEventArgs e)
        {
            marking = e.NewTextValue;
        }

        /// <summary>
        /// Method for when Editsight clicked.
        /// Calls database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditSightClicked(object sender, EventArgs e)
        {
            double nsm;//for holding parsed value
            double.TryParse(marking, out nsm);
            App.Database.EditSight(nsm);//database call
        }

        /// <summary>
        /// handles texted changed in search entry box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchChanged(object sender, TextChangedEventArgs e)
        {
            search = e.NewTextValue;
        }

        /// <summary>
        /// Handle pop-up of previous scoring sheet.
        /// Pop-up modal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrevButtonClicked(object sender, EventArgs e)
        {
            HoldPracID = PracID;//maintains a copy of sheet ID
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

        /// <summary>
        /// Mthod calls saving ends, notes and weather conditions when button pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveClicked(object sender, EventArgs e)
        {

            Model.PracEndsHold.Save();
            Model.NotesHold.NotesSaved();
            Model.WeatherHold.WeatherSaved();
        }

        /// <summary>
        /// creates new buttons with text of lbl param.
        /// </summary>
        /// <param name="lbl"></param>
        /// <returns></returns>
        private static Button createButton(string lbl)
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

        /// <summary>
        /// Creates dataGrid for scoring sheet.
        /// </summary>
        /// <returns></returns>
        private static SfDataGrid CreateDataGrid()
        {
            SfDataGrid dataGrid = new SfDataGrid();

            viewModel = new Model.PracticeViewModel();
            dataGrid.ItemsSource = viewModel.EndCollection;
            dataGrid.ColumnSizer = ColumnSizer.Auto; //set to auto so it scrolls horizontally

            dataGrid.AllowEditing = true;
            dataGrid.EditTapAction = TapAction.OnTap; //Enter edit mode in single tap instead of default double tap.
            dataGrid.EditorSelectionBehavior = EditorSelectionBehavior.SelectAll;
            dataGrid.NotificationSubscriptionMode = NotificationSubscriptionMode.PropertyChange;//for updates. Makes no difference.
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


        /// <summary>
        /// Creates dataGrid for displaying previous sheet.
        /// Seperate dataGrid as it contains extra columns.
        /// </summary>
        /// <param name="ends"></param>
        /// <returns></returns>
        private static SfDataGrid CreateDataGridPrev(List<Data.End> ends)
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

            //sets datagrid.
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

        /// <summary>
        /// Creates grid for details pop-up.
        /// </summary>
        /// <returns></returns>
        private static Grid CreateDetailsGrid()
        {
            Grid gridDetails = new Grid() { RowSpacing = 0, ColumnSpacing = 0, BackgroundColor = Color.White };

            gridDetails.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            gridDetails.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            gridDetails.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            gridDetails.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            gridDetails.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            gridDetails.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10) });//Acts as a spacer
            gridDetails.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });


            //Date class variable.
            //BowType and Dist ArchMain class variables.
            gridDetails.Children.Add(new Label { Text = "Name: Caitlin Thomas-Riley", TextColor = Color.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, 0, 0);
            gridDetails.Children.Add(new Label { Text = "Bow Type: " + ArchMain.bowType, TextColor = Color.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, 2, 0);
            gridDetails.Children.Add(new Label { Text = "Division: JWR", TextColor = Color.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, 0, 1);
            gridDetails.Children.Add(new Label { Text = "Club: Randwick", TextColor = Color.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, 2, 1);
            gridDetails.Children.Add(new Label { Text = "Date: " + date, TextColor = Color.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, 0, 2);
            gridDetails.Children.Add(new Label { Text = "Archery NZ No.: 3044", TextColor = Color.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, 2, 2);
            gridDetails.Children.Add(new Label { Text = "Distance: " + ArchMain.dist + "m", TextColor = Color.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, 0, 3);

            return gridDetails;
        }


        /// <summary>
        /// For Scoring sheet and Details to be set up in database and ID's returned.
        /// </summary>
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