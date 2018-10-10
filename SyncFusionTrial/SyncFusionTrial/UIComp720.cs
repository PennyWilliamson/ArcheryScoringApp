using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Syncfusion.SfDataGrid.XForms;
using System.Collections.ObjectModel;
using Syncfusion.Data;
using Syncfusion.XForms.PopupLayout;

namespace ArcheryScoringApp
{
    /// <summary>
    /// The content page for the 720 Competition scoring page of the application.
    /// Uses SyncFusion commponets, and code for components are modelled 
    /// on SynFusion documentation and Miroscoft Xamarin.Forms documentation.
    /// Syncfusion. (2001 - 2018). Xamarin.Forms. Retrieved August 2018, from Syncfusion Documentation: https://help.syncfusion.com/xamarin/introduction/overview#how-to-best-read-this-user-guide
    /// David Britch, C. D. (2017, July 12). Xamarin.Forms User Interface Views. Retrieved August 2018, from Microsoft: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/index
    /// </summary>
    public class UIComp720 : ContentPage
    {
        internal static int ID { get; set; } // Scoring sheet ID from database, internal so it can be accessed from other classes.
        internal static int dtlID { get; set; } //Details ID from database, internal so it can be accessed from other classes.
        private string marking; //holds new sight marking for update.
        private static SfDataGrid dataGrid;//datagrid element, static as method static.
        private SfPopupLayout popupDetails;//pop up for details
        private SfPopupLayout prevPop;//pop up for previous results
        private SfPopupLayout notValid;//popup for invalid scores
        private static string date;//holds todays date
        private string search;//value for previous search
        private int HoldID;//holds ID when being reset to prevent code in other classes firing.

        /// <summary>
        /// Constructor.
        /// Sets components and layout for page.
        /// </summary>
        public UIComp720()
        {

            dataGrid = CreateDataGrid();

            notValid = new SfPopupLayout();
            prevPop = new SfPopupLayout();

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
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(70) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });

            Button detailsButton = createButton("Details");
            popupDetails = new SfPopupLayout();
            detailsButton.Clicked += DetailsButtonClicked;

            Button backButton = createButton("Back");
            backButton.Clicked += BackButtonClicked;

            Button mainButton = createButton("Main");
            mainButton.Clicked += MainButtonClicked;


            Button previousButton = createButton("Search");
            prevPop = new SfPopupLayout();
            previousButton.Clicked += PrevButtonClicked;

            Button editSightButton = createButton("Edit Sight");


            Label searchLabel = new Label { Text = "Search by score", TextColor = Color.FromHex("#010101"), FontSize = 10 };
            var searchScore = new Entry { Text = " ", FontSize = 10 };
            searchScore.TextChanged += SearchChanged;

            var newSightMarking = new Entry { Text = " ", FontSize = 10 };
            newSightMarking.TextChanged += SightChanged;
            editSightButton.Clicked += EditSightClicked;


            grid.Children.Add(dataGrid, 0, 3);
            Grid.SetColumnSpan(dataGrid, 3);
            grid.Children.Add(searchLabel, 0, 0);
            grid.Children.Add(searchScore, 1, 0);
            grid.Children.Add(previousButton, 2, 0);
            grid.Children.Add(backButton, 0, 2);
            grid.Children.Add(mainButton, 2, 2);
            grid.Children.Add(newSightMarking, 0, 1);
            grid.Children.Add(editSightButton, 1, 1);
            grid.Children.Add(detailsButton, 1, 2);
            layout.Children.Add(grid);


            Content = layout;
        }

        /// <summary>
        /// Method for popup when score is not a valid score
        /// public as it is called from Comp720Model.
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
        /// Displays details when details button clicked.
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
        /// method for creating a new button with
        /// text suplied by lbl param.
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
                BackgroundColor = Color.LightBlue
            };
            return button;
        }

        /// <summary>
        /// Handles when text in sight entry box changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SightChanged(object sender, TextChangedEventArgs e)
        {
            marking = e.NewTextValue;
        }

        /// <summary>
        /// Handles when button for editing sight is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditSightClicked(object sender, EventArgs e)
        {
            double nsm;//for hloding double after text is parsed.
            double.TryParse(marking, out nsm);
            App.Database.EditSight(nsm);//database insert call.
        }

        /// <summary>
        /// Handles text being changed in search entry field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchChanged(object sender, TextChangedEventArgs e)
        {
            search = e.NewTextValue;
        }

        /// <summary>
        /// Handles prev scoring sheet pop-up.
        /// Pop-up is modal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrevButtonClicked(object sender, EventArgs e)
        {
            HoldID = ID;
            ID = -1;//stops code firing in Comp720Model.
            List<Data.End> ends = new List<Data.End>(); // for holding return from method call.
            int srch;//int variable for parsing string to int
            int.TryParse(search, out srch);
            ends = Model.EndModel.GetPrev(srch, "720Competition");
            string distDate = Model.DetailsModel.GetPrevDetails(ends); //gets the distance and date for header.
            prevPop.PopupView.ContentTemplate = new DataTemplate(() =>
            {
                prevPop.Padding = 10;
                prevPop.PopupView.HeaderTitle = "720 Comp" + distDate;
                prevPop.BackgroundColor = Color.White;
                prevPop.HorizontalOptions = LayoutOptions.FillAndExpand;
                prevPop.VerticalOptions = LayoutOptions.FillAndExpand;
                prevPop.PopupView.ShowFooter = false;
                prevPop.PopupView.WidthRequest = 360;
                return CreateDataGridPrev(ends);
            });
            prevPop.StaysOpen = true;
            prevPop.PopupView.ShowCloseButton = true;
            prevPop.IsOpen = true;
            prevPop.Show();
            ID = HoldID;
        }

        /// <summary>
        /// Navigation method for back.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        /// <summary>
        /// Naviagtion method for root page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MainButtonClicked(object sender, EventArgs e)
        {
            ID = -1; //resets it.
            await Navigation.PopToRootAsync();
        }

        /// <summary>
        /// Creates the datgrid for scoring.
        /// </summary>
        /// <returns></returns>
        private static SfDataGrid CreateDataGrid()
        {
            SfDataGrid dataGrid = new SfDataGrid();//object for grid.
            Model.Comp720ViewModel viewModel = new Model.Comp720ViewModel();//object for method calls
            dataGrid.ItemsSource = viewModel.EndCollection;//item source for data binding.
            dataGrid.ColumnSizer = ColumnSizer.Auto; //so it scrolls horizontally

            dataGrid.AllowEditing = true;
            dataGrid.EditTapAction = TapAction.OnTap; //Enter edit mode in single tap instead of default double tap.
            dataGrid.EditorSelectionBehavior = EditorSelectionBehavior.SelectAll;
           // dataGrid.NotificationSubscriptionMode = NotificationSubscriptionMode.PropertyChange;
            dataGrid.SelectionMode = SelectionMode.Single;

            dataGrid.QueryCellStyle += DataGrid_QueryCellStyle;//method for setting the datagrid style of some fields black.

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

            GridTextColumn threeTotal = new GridTextColumn();
            threeTotal.MappingName = "ThreeTotal";
            threeTotal.HeaderText = "3s";

            GridTextColumn endTotal = new GridTextColumn();
            endTotal.MappingName = "EndTotal";
            endTotal.HeaderText = "End Total";

            //for running total column
            GridTextColumn runningTotal = new GridTextColumn();
            runningTotal.MappingName = "RunningTotal";
            runningTotal.HeaderText = "Running Total";

            //sets datagrid.
            dataGrid.Columns.Add(endNo);
            dataGrid.Columns.Add(arrow1);
            dataGrid.Columns.Add(arrow2);
            dataGrid.Columns.Add(arrow3);
            dataGrid.Columns.Add(threeTotal);
            dataGrid.Columns.Add(endTotal);
            dataGrid.Columns.Add(runningTotal);

            return dataGrid;
        }

        /// <summary>
        /// Sets style for scoring and previous datagrids.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void DataGrid_QueryCellStyle(object sender, QueryCellStyleEventArgs e)
        {
            if (e.ColumnIndex == 5 && e.RowIndex % 2 == 1) //black for every other end total
            {
                e.Style.BackgroundColor = Color.Black;
                e.Style.ForegroundColor = Color.Black;

            }

            if (e.ColumnIndex == 6 && e.RowIndex % 2 == 1)  //black for every other runnig total
            {
                e.Style.BackgroundColor = Color.Black;
                e.Style.ForegroundColor = Color.Black;

            }

            e.Handled = true;
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

            //Bowtype and distance set to ArchMain class variables.
            gridDetails.Children.Add(new Label { Text = "Name: Caitlin Thomas-Riley", TextColor = Color.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, 0, 0);
            gridDetails.Children.Add(new Label { Text = "Bow Type: " + UIArchMain.bowType, TextColor = Color.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, 2, 0);
            gridDetails.Children.Add(new Label { Text = "Division: JWR", TextColor = Color.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, 0, 1);
            gridDetails.Children.Add(new Label { Text = "Club: Randwick", TextColor = Color.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, 2, 1);
            gridDetails.Children.Add(new Label { Text = "Date: " + date, TextColor = Color.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, 0, 2);
            gridDetails.Children.Add(new Label { Text = "Archery NZ No.: 3044", TextColor = Color.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, 2, 2);
            gridDetails.Children.Add(new Label { Text = "Distance: " + UIArchMain.dist + "m", TextColor = Color.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, 0, 3);

            return gridDetails;
        }

        /// <summary>
        /// Creates dataGrid for previous pop-up.
        /// Seperate datagrid due to issues with having the same dataGrid displaying twice.
        /// </summary>
        /// <param name="ends"></param>
        /// <returns></returns>
        private static SfDataGrid CreateDataGridPrev(List<Data.End> ends)
        {
            SfDataGrid dataGrid = new SfDataGrid();
            Model.Comp720ViewModel viewModel = new Model.Comp720ViewModel(ends);
            dataGrid.ItemsSource = viewModel.EndCollection;
            dataGrid.ColumnSizer = ColumnSizer.Auto; //so it scrolls horizontally

            dataGrid.AllowEditing = true;
            dataGrid.EditTapAction = TapAction.OnTap; //Enter edit mode in single tap instead of default double tap.
            dataGrid.EditorSelectionBehavior = EditorSelectionBehavior.SelectAll;
            dataGrid.NotificationSubscriptionMode = NotificationSubscriptionMode.PropertyChange;

            dataGrid.QueryCellStyle += DataGrid_QueryCellStyle;//method call for style.

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

            GridTextColumn threeTotal = new GridTextColumn();
            threeTotal.MappingName = "ThreeTotal";
            threeTotal.HeaderText = "3s";

            GridTextColumn endTotal = new GridTextColumn();
            endTotal.MappingName = "EndTotal";
            endTotal.HeaderText = "End Total";

            //for running total column
            GridTextColumn runningTotal = new GridTextColumn();
            runningTotal.MappingName = "RunningTotal";
            runningTotal.HeaderText = "Running Total";

            //sets datagrid.
            dataGrid.Columns.Add(endNo);
            dataGrid.Columns.Add(arrow1);
            dataGrid.Columns.Add(arrow2);
            dataGrid.Columns.Add(arrow3);
            dataGrid.Columns.Add(threeTotal);
            dataGrid.Columns.Add(endTotal);
            dataGrid.Columns.Add(runningTotal);

            return dataGrid;
        }


        /// <summary>
        /// For Scoring sheet and Details to be set up in database and ID's returned.
        /// </summary>
        protected override void OnAppearing()
        {
            DateTime tdy = DateTime.Today;
            date = tdy.ToString("d");
            string type = "720Competition";
            if (ID == -1) //set to -1 in App constructor so that this does not refire on a back or refresh
            {
                Model.CalcRT.curRT = 0; //resets running total

                Model.DetailsModel details = new Model.DetailsModel();
                dtlID = details.SetDetails(date);


                Model.ScoringSheetModel scoringSheet = new Model.ScoringSheetModel();
                ID = scoringSheet.SetScoringSheet(dtlID, type);// sets ID to the scoring sheet ID
            }
        }
    }
}