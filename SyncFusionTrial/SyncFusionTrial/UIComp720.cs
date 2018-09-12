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
    public class UIComp720 : ContentPage
    {
        static internal int ID { get; set; } //internal so it can be accessed from other classes
        static internal int dtlID { get; set; }
        string marking;
        SfDataGrid dataGrid;
        // SfDataPager sfPager = new SfDataPager();
        //   SfListView details;
        SfPopupLayout popupLayout;
        SfPopupLayout prevPop;
        SfPopupLayout notValid;
        static string date;
        string search;
        int HoldID;

        public UIComp720()
        {

            dataGrid = CreateDataGrid();

            notValid = new SfPopupLayout();

            StackLayout layout = new StackLayout()
            {
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Start,
                Padding = new Thickness(50)
            };
            var header = new Label { Text = "720 Competition Scoring ", TextColor = Color.FromHex("#010101"), FontSize = 30 };
            var grid = new Grid { RowSpacing = 50 };

            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            // grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            //  grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(140) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(140) });

            Button detailsButton = createButton("Details");
            popupLayout = new SfPopupLayout();
            detailsButton.Clicked += ClickToShowPopup_Clicked;

            Button backButton = createButton("Back");
            backButton.Clicked += BackButtonClicked;

            Button mainButton = createButton("Main");
            mainButton.Clicked += MainButtonClicked;


            Button previousButton = createButton("Previous");
            prevPop = new SfPopupLayout();
            previousButton.Clicked += PrevButtonClicked;

            Button editSightButton = createButton("Edit Sight Marking");


            Label searchLabel = new Label { Text = "Search by score", TextColor = Color.FromHex("#010101"), FontSize = 20 };
            var searchScore = new Entry { Text = " " };
            searchScore.TextChanged += SearchChanged;

            var newSightMarking = new Entry { ReturnType = ReturnType.Default };
            newSightMarking.TextChanged += SightChanged;
            editSightButton.Clicked += EditSightClicked;

           

            layout.Children.Add(header);
            // Grid gridDetails = CreateGrid();

            //   Grid scoringGrid = CreateDataGrid(); 

            // grid.Children.Add(gridDetails, 1, 3 );
            // Grid.SetColumnSpan(gridDetails, 2);
            grid.Children.Add(dataGrid, 1, 3);
            grid.Children.Add(searchLabel, 0, 0);
            grid.Children.Add(searchScore, 0, 1);
          grid.Children.Add(previousButton, 0, 2);
          grid.Children.Add(backButton, 1, 0);
          grid.Children.Add(mainButton, 1, 1);
            grid.Children.Add(newSightMarking, 2, 0);
           grid.Children.Add(editSightButton, 2, 1);
            grid.Children.Add(detailsButton, 1, 2);
            layout.Children.Add(grid);


            Content = layout;
        }

        static public void NotValid(String score)
        {
            SfPopupLayout notValid = new SfPopupLayout();
            Label content = new Label { Text = "Score '" + score + "' is not a valid score and will be recorded as a '0'. Please change your score.", TextColor = Color.FromHex("#010101"), BackgroundColor = Color.White, FontSize = 30 };
            notValid.PopupView.ContentTemplate = new DataTemplate(() =>
            {
                notValid.PopupView.HeaderTitle = "Invalid Score";
                notValid.PopupView.ShowFooter = false;
                return content;
            });
            notValid.StaysOpen = true;
            notValid.PopupView.ShowCloseButton = true;
            notValid.IsOpen = true;
            notValid.Show();
        }

        private void ClickToShowPopup_Clicked(object sender, EventArgs e)
        {
            popupLayout.PopupView.ContentTemplate = new DataTemplate(() =>
            {
                popupLayout.PopupView.HeaderTitle = "Details";
                popupLayout.PopupView.BackgroundColor = Color.White;
                popupLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
                popupLayout.PopupView.ShowFooter = false;
                return CreateGrid();
            });
            popupLayout.StaysOpen = true;
            popupLayout.PopupView.ShowCloseButton = true;
            popupLayout.IsOpen = true;
            popupLayout.Show();
        }

        static Button createButton(string lbl)
        {
            Button button = new Button
            {
                Text = lbl,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };
            return button;
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
            HoldID = ID;
            ID = -1;//stops code firing in PracticeModel.
            List<Data.End> ends = new List<Data.End>();
            int srch;//int variable for parsing string to int
            int.TryParse(search, out srch);
            ends = App.Database.GetPreviousEnds(srch, "720Competition");
            prevPop.PopupView.ContentTemplate = new DataTemplate(() =>
            {
                prevPop.PopupView.HeaderTitle = "Previous 720 Competition sheet";
                prevPop.BackgroundColor = Color.White;
                prevPop.HorizontalOptions = LayoutOptions.FillAndExpand;
                prevPop.PopupView.ShowFooter = false;
                return CreateDataGridPrev(ends);
            });
            prevPop.StaysOpen = true;
            prevPop.PopupView.ShowCloseButton = true;
            prevPop.IsOpen = true;
            prevPop.Show();
            ID = HoldID;
        }

        private async void BackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void MainButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

        static SfDataGrid CreateDataGrid()
        {
            SfDataGrid dataGrid = new SfDataGrid();
            Model.Comp720ViewModel viewModel = new Model.Comp720ViewModel();
            dataGrid.ItemsSource = viewModel.EndCollection;
            dataGrid.ColumnSizer = ColumnSizer.Auto; //so it scrolls horizontally

           

            dataGrid.AllowEditing = true;
            dataGrid.EditTapAction = TapAction.OnTap; //Enter edit mode in single tap instead of default double tap.
            dataGrid.EditorSelectionBehavior = EditorSelectionBehavior.SelectAll;
            dataGrid.NotificationSubscriptionMode = NotificationSubscriptionMode.PropertyChange;
          //  dataGrid.View.LiveDataUpdateMode = LiveDataUpdateMode.AllowDataShaping;

            dataGrid.QueryCellStyle += DataGrid_QueryCellStyle;

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

            dataGrid.Columns.Add(endNo);
            dataGrid.Columns.Add(arrow1);
            dataGrid.Columns.Add(arrow2);
            dataGrid.Columns.Add(arrow3);
            dataGrid.Columns.Add(threeTotal);
            dataGrid.Columns.Add(endTotal);
            dataGrid.Columns.Add(runningTotal);

            return dataGrid;
        }

        static private void DataGrid_QueryCellStyle(object sender, QueryCellStyleEventArgs e)
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

        static Grid CreateGrid()
        {

            Grid gridDetails = new Grid() { RowSpacing = 0, ColumnSpacing = 0, BackgroundColor = Color.White };

            gridDetails.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            gridDetails.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            gridDetails.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            gridDetails.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
            gridDetails.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });


            gridDetails.Children.Add(new Label { Text = "Name: Caitlin Thomas-Riley", TextColor = Color.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, 0, 0);
            gridDetails.Children.Add(new Label { Text = "Bow Type: Recurve", TextColor = Color.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, 1, 0);
            gridDetails.Children.Add(new Label { Text = "Division: JWR", TextColor = Color.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, 0, 1);
            gridDetails.Children.Add(new Label { Text = "Club: Randwick", TextColor = Color.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, 1, 1);
            gridDetails.Children.Add(new Label { Text = "Date: " + date, TextColor = Color.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, 0, 2);
            gridDetails.Children.Add(new Label { Text = "Archery NZ No.: 3044", TextColor = Color.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, 1, 2);


            return gridDetails;
        }

        static SfDataGrid CreateDataGridPrev(List<Data.End> ends)
        {
            SfDataGrid dataGrid = new SfDataGrid();
            Model.Comp720ViewModel viewModel = new Model.Comp720ViewModel(ends);
            dataGrid.ItemsSource = viewModel.EndCollection;
            dataGrid.ColumnSizer = ColumnSizer.Auto; //so it scrolls horizontally



            dataGrid.AllowEditing = true;
            dataGrid.EditTapAction = TapAction.OnTap; //Enter edit mode in single tap instead of default double tap.
            dataGrid.EditorSelectionBehavior = EditorSelectionBehavior.SelectAll;
            dataGrid.NotificationSubscriptionMode = NotificationSubscriptionMode.PropertyChange;
            //  dataGrid.View.LiveDataUpdateMode = LiveDataUpdateMode.AllowDataShaping;

            dataGrid.QueryCellStyle += DataGrid_QueryCellStyle;

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

            dataGrid.Columns.Add(endNo);
            dataGrid.Columns.Add(arrow1);
            dataGrid.Columns.Add(arrow2);
            dataGrid.Columns.Add(arrow3);
            dataGrid.Columns.Add(threeTotal);
            dataGrid.Columns.Add(endTotal);
            dataGrid.Columns.Add(runningTotal);

            return dataGrid;
        }

        //need an OnAppearing for Scoring sheet to be set up in database and ID returned.
        protected override void OnAppearing()
        {
            DateTime tdy = DateTime.Today;
            date = tdy.ToString("d");
            string type = "720Competition";
            if (ID == -1) //set to -1 in App constructor so that this does not refire on a back or refresh
            {
                Model.calcRTComp.curRT = 0; //resets running total

                Model.DetailsModel details = new Model.DetailsModel();
                dtlID = details.SetDetails(date);
                // dtlID = App.Database.InsertDetails(date);

                Model.ScoringSheetModel scoringSheet = new Model.ScoringSheetModel();
                // ID = App.Database.InsertScoringSheet(dtlID, type); // sets ID to the scoring sheet ID
                ID = scoringSheet.SetScoringSheet(dtlID, type);// sets ID to the scoring sheet ID
            }
        }
    }
    }