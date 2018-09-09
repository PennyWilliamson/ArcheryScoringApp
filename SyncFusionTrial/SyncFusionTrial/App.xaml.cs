using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ArcheryScoringApp.Data;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace ArcheryScoringApp
{
	public partial class App : Application
	{
        internal static ASCDatabase database;
        
		public App ()
		{
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTQ2MjhAMzEzNjJlMzIyZTMwRUthNlNmN1FUY3VkZklwTHhJSFJJT2c2US80cVNIMHJlTEJUNm5iNFdUWT0 =");
            
            InitializeComponent();
            UIComp720.ID = -1; //to trigger the database in 720 OnAppearing
            UIPractice.PracID = -1; //to trigger the databse in Practice Appearing

            MainPage = new UIComp720();
		}

        internal static ASCDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new ASCDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ASCDatabase.db3"));

                }
                return database;
            }
        }

       // public int ResumeAtTodoId { get; set; }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
