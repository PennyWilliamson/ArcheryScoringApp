using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ArcheryScoringApp.Data;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ArcheryScoringApp
{
    public partial class App : Application
    {
        internal static ASCDatabase database;//database path

        public App()
        {
            //Register Syncfusion license
            //Trial licence key, now expired
            // Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTQ2MjhAMzEzNjJlMzIyZTMwRUthNlNmN1FUY3VkZklwTHhJSFJJT2c2US80cVNIMHJlTEJUNm5iNFdUWT0 ="); trial license key
            //Community licence key 
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjE4MTJAMzEzNjJlMzIyZTMwbGdmTjRRem95WWlmR3c4TW9EcS9kU09LZmdsODEvNlBCbkgwMDdRU3BwYz0 =; MjE4MTNAMzEzNjJlMzIyZTMwWHM0N1laT2l4OWR6c0ZyVG5TWGxYVTY5cmVJamhRa3pkMThrSzllTlR0WT0 =; MjE4MTRAMzEzNjJlMzIyZTMwa0FON2R6dzlVQ3phSXZ2SUE4VWw5bkM5VDJiRFpPV3lpbjdQZ0NRM3NYST0 =; MjE4MTVAMzEzNjJlMzIyZTMwTkozdit3Ym1EVHo3TGhhclI3THpxOExKWlRBcndMMjhoRy9SUmRGUWJMYz0 =; MjE4MTZAMzEzNjJlMzIyZTMwVFRqNVdkeWUwYjRGZUNMVHJ5K1lTc25paUtrTHpJWXVPRSt6dDhwN2tpMD0 =; MjE4MTdAMzEzNjJlMzIyZTMwalhsRGdDMlpYMTU2T2VpQ2RBUElibzNxQ1Ywd3UzZnFZTkNaM1FueXd0UT0 =; MjE4MThAMzEzNjJlMzIyZTMwV3RsY3llT01ZNWV1VzZZaFVrSjdhWE9jb1IzaEQyL0FObS9TLzlGd0I0dz0 =; MjE4MTlAMzEzNjJlMzIyZTMwTURBWFVlOHB1bDdtL1p1K2NMR2xXT3BqK0tSSVZIT3NFS0ppY2pleXRvZz0 =; MjE4MjBAMzEzNjJlMzIyZTMwZktzdUFGZGdXZVhPK3FRRXZyM3VBQTFxVjFydjlOUE5JSGc0ZUZtL25yaz0 =");
            InitializeComponent();
            UIComp720.ID = -1; //to trigger the database in 720 OnAppearing
            UIPractice.PracID = -1; //to trigger the databse in Practice Appearing

            //Sets application page navigatiion, and start page
            MainPage = new NavigationPage(new ArchMain() { Title = "Archery Scoring App"});
        }

        /// <summary>
        /// Method for getting database path, or triggering creation of
        /// database if need be.
        /// Code modified from Microsoft documentation 
        /// David Britch, C. D. (2018, June 21). Xamarin.Forms Local Databases. Retrieved July 18, 2018, from Microsoft: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/databases
        /// </summary>
        internal static ASCDatabase Database
        {
            get
            {
                if (database == null)
                {
                    //creates and writes database to path if need be.
                    database = new ASCDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ASC2Database.db3"));

                }
                return database;
            }
        }

        // public int ResumeAtTodoId { get; set; }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
