using System;
using System.Collections.Generic;
using System.Text;
using SQLiteNetExtensions;
using SQLite;
using System.Linq;
using SQLiteNetExtensions.Extensions;
using System.Threading.Tasks;
using System.Data;

namespace ArcheryScoringApp.Data
{
    /// <summary>
    /// Class for the database creation and database tools.
    /// code modified from NuGet documentation for SQLite.Extensions
    /// and NuGet documentation for dependant SQLite-net-PCL NuGet.
    /// as well as Microsoft documentation
    /// Twincoders. (2018, August 13). SQLite-Net Extensions. Retrieved August 29, 2018, from Bitbucket: https://bitbucket.org/twincoders/sqlite-net-extensions
    /// praeclarum. (n/d). GettingStarted. Retrieved August 2018, from GitHub praeclarum/sqlite-net/wiki: https://github.com/praeclarum/sqlite-net/wiki/GettingStarted
    /// David Britch, C. D. (2018, June 21). Xamarin.Forms Local Databases. Retrieved August 28, 2018, from Microsoft: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/database
    /// </summary>
    class ASCDatabase
    {
        //database connection.
        internal readonly SQLiteConnection dbConn;

        /// <summary>
        /// Constructor for database class.
        /// Takes in database file path as param.
        /// </summary>
        /// <param name="dbFilePath"></param>
        public ASCDatabase(string dbFilePath)
        {
            dbConn = new SQLiteConnection(dbFilePath);//connection string code
            //creates tables if they do not already exist.
            dbConn.CreateTable<ScoringSheet>();
            dbConn.CreateTable<End>();
            dbConn.CreateTable<Details>();
            dbConn.CreateTable<Bow>();
            dbConn.CreateTable<Notes>();
            dbConn.CreateTable<WeatherConditions>();
        }

        /// <summary>
        /// Method for inserting the scoring sheet into database.
        /// As it takes in details ID, details need to be inserted first.
        /// Returns sheet ID.
        /// </summary>
        /// <param name="dtlsID"></param>
        /// <param name="typ"></param>
        /// <returns></returns>
        public int InsertScoringSheet(int dtlsID, string typ)
        {
            var sheet = new ScoringSheet()
            {
                Type = typ,
                DetailsID = dtlsID
            };

            dbConn.Insert(sheet);
            var detail = dbConn.Get<Details>(dtlsID);
            sheet.details = detail; //for ref integ
            dbConn.UpdateWithChildren(sheet);
            var scrShtID = sheet.ID;
            return scrShtID;
        }

        /// <summary>
        /// Mehtod for inserting details into database.
        /// Takes in param of date.
        /// Returns details ID.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public int InsertDetails(string date)
        {
            var details = new Details()
            {
                FirstName = "Caitlin",
                LastName = "Thomas-Riley",
                BowType = UIArchMain.bowType,
                Division = "JWR",
                Club = "Randwick",
                Date = date,
                ArchNZNum = 3044,
                Dist = UIArchMain.dist
            };

            dbConn.Insert(details);

            var bow = dbConn.Get<Bow>(UIArchMain.bowType);
            details.bow = bow;
            dbConn.UpdateWithChildren(details);
            var dtlID = details.DetailsID;
            return dtlID;
        }

        /// <summary>
        /// Method for inserting end into database.
        /// Takes in end object as param.
        /// </summary>
        /// <param name="anEnd"></param>
        public void InsertEnds(Model.EndModel anEnd)
        {
            var end = new End()
            {
                EndNum = anEnd.endNum,
                ID = anEnd.id,
                EndTotal = anEnd.endTotal,
                Score1 = anEnd.score1,
                Score2 = anEnd.score2,
                Score3 = anEnd.score3,
                Score4 = anEnd.score4,
                Score5 = anEnd.score5,
                Score6 = anEnd.score6

            };

            dbConn.InsertOrReplace(end);//handles the end already being there, ie, editing scores.
            var sheet = dbConn.Get<ScoringSheet>(anEnd.id);//for ref integ
            end.scoringSheet = sheet;
            dbConn.UpdateWithChildren(end);
        }

        /// <summary>
        /// Method for updating final total for end.
        /// Takes in all fields as update updates all feild in tuple.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fnlTtl"></param>
        /// <param name="dtlsID"></param>
        /// <param name="typ"></param>
        public void UpdateFinalScore(int id, int fnlTtl, int dtlsID, string typ)
        {
            var sheet = new ScoringSheet()
            {
                ID = id,
                FinalTotal = fnlTtl,
                DetailsID = dtlsID,
                Type = typ

            };
            dbConn.Update(sheet);
        }

        /// <summary>
        /// Method for adding bow to database.
        /// Uses a try catch finally due to lack of InsertOrIgnore in NuGet used.
        /// </summary>
        /// <param name="bow"></param>
        public void AddBow(string bow)
        {
            var newBow = new Bow()
            {
                BowType = bow,
                SightMarkings = 0
            };
            try
            {
                dbConn.Insert(newBow);
            }
            catch (Exception ex)//as there is no Insert Or Ignore in TwinCoders Nuget
            { }
            finally { }
        }

        /// <summary>
        /// Adds sight marking for bow seected on main page.
        /// </summary>
        /// <param name="markings"></param>
        public void EditSight(double markings)
        {
            var bow = new Bow()
            {//as update replaces tuple
                BowType = UIArchMain.bowType,
                SightMarkings = markings
            };

            dbConn.Update(bow);
        }

        /// <summary>
        /// Queries database for sight markings
        /// for current selected bow.
        /// </summary>
        /// <param name="bow"></param>
        /// <returns></returns>
        public List<Bow> GetSightMarkings(string bow)
        {
            var b = dbConn.Query<Bow>("SELECT SightMarkings FROM Bow WHERE BowType = ?", bow);
            return b;
        }

        /// <summary>
        /// Queries database for personal best score for combination of competition type, bow and distance.
        /// Returns list of scoring sheet type containing ID and Final total.
        /// </summary>
        /// <returns></returns>
        public List<ScoringSheet> getPB()
        {
            string type = "720Competition";//hard set as only 720 competition is in, will need passed as a variable
            string distance = UIArchMain.dist;
            string bow = UIArchMain.bowType;
            var b = dbConn.Query<ScoringSheet>("SELECT ID, FinalTotal FROM ScoringSheet AS ss JOIN Details AS d ON ss.DetailsID = d.DetailsID WHERE ss.Type = ? AND d.BowType = ? AND d.Dist = 70 Order BY FinalTotal DESC LIMIT 1", type, bow, distance);

            return b;
        }

        /// <summary>
        /// Queries database for last score score for combination of competition type, bow and distance.
        /// Returns list of scoring sheet type containing ID and Final total.
        /// </summary>
        /// <returns></returns>
        public List<ScoringSheet> GetLastScore()
        {
            int i = 0;
            string type = "720Competition";//hard set as only 720 competition is in, will need passed as a variable
            string distance = UIArchMain.dist;
            string bow = UIArchMain.bowType;
            var b = dbConn.Query<ScoringSheet>("SELECT DISTINCT ID, FinalTotal FROM ScoringSheet AS ss JOIN Details AS d ON ss.DetailsID = d.DetailsID WHERE FinalTotal > ? AND ss.Type = ? AND d.BowType = ? AND d.Dist = ? ORDER BY ID DESC LIMIT 1", i, type, bow, distance);
            return b;
        }

        /// <summary>
        /// Queries database for personal best score for combination of competition type, bow and distance.
        /// Returns list of scoring sheet type containing ID and Final total.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ScoringSheet> GetLastBest(int id)
        {
            int i = 0;
            string type = "720Competition";//hard set as only 720 competition is in, will need passed as a variable
            string distance = UIArchMain.dist;
            string bow = UIArchMain.bowType;
            var b = dbConn.Query<ScoringSheet>("SELECT DISTINCT ID, FinalTotal FROM ScoringSheet AS ss JOIN Details as d ON ss.DetailsID = d.DetailsID WHERE FinalTotal > ? AND ss.Type = ? AND ss.ID > ? AND d.BowType = ? AND d.Dist = ? ORDER BY FinalTotal DESC LIMIT 1", i, type, id, bow, distance);
            return b;
        }

        /// <summary>
        /// Method for adding notes and associated end reference to database.
        /// </summary>
        /// <param name="endRef"></param>
        /// <param name="note"></param>
        public void AddNotes(string endRef, string note)
        {
            //checks if end is in database
            var a = dbConn.Query<End>("SELECT * FROM 'End' where EndNum = ?", endRef);
            if(a.Count == 0)
            {
                var dummyEnd = new End()
                {
                    EndNum = endRef,
                    ID = UIPractice.PracID,
                    EndTotal = 0
                };
                dbConn.InsertOrReplace(dummyEnd);//handles end not being there
            }

                var notes = new Notes()
                {
                    EndNum = endRef,
                    EndNotes = note
                };

                dbConn.InsertOrReplace(notes);

                var end = dbConn.Get<End>(endRef);

                notes.end = end;

                dbConn.UpdateWithChildren(notes);

        }

        /// <summary>
        /// Method for adding weather conditions and associated endRef to the database.
        /// </summary>
        /// <param name="endRef"></param>
        /// <param name="temp"></param>
        /// <param name="speed"></param>
        /// <param name="dir"></param>
        /// <param name="hum"></param>
        /// <param name="other"></param>
        public void AddWeather(string endRef, string temp, string speed, string dir, string hum, string other)
        {
            //checks if end is in database
            var a = dbConn.Query<End>("SELECT * FROM 'End' where EndNum = ?", endRef);
            if (a.Count == 0)
            {
                var dummyEnd = new End()
                {
                    EndNum = endRef,
                    ID = UIPractice.PracID,
                    EndTotal = 0
                };
                dbConn.InsertOrReplace(dummyEnd);//handles end not being there
            }

            var weather = new WeatherConditions
            {
                EndNum = endRef,//sets the endNum to current end's eR
                Temp = temp,
                WindSpeed = speed,
                WindDir = dir,
                Humidity = hum,
                Other = other,
            };
            dbConn.InsertOrReplace(weather);

            var end = dbConn.Get<End>(endRef);
            
                weather.end = end;

            dbConn.UpdateWithChildren(weather);
        }

        /// <summary>
        /// Method for returning previous ends associated with a scoring sheet, identified by final total.
        /// </summary>
        /// <param name="FinalScore"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public List<End> GetPreviousEnds(int FinalScore, string Type)
        {
            var a = dbConn.Query<End>("SELECT E.ID, EndNum, Score1, Score2, Score3, Score4, Score5, Score6, EndTotal from `End` AS E, ScoringSheet AS S where S.FinalTotal = ? AND S.Type = ? AND E.ID = S.ID ORDER BY S.ID ASC", FinalScore, Type);
            return a;
        }

        /// <summary>
        /// Method for returning details associated with a scoring sheet.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Details> GetPreviousDetails(int id)
        {
            var a = dbConn.Query<Details>("SELECT Date, Dist FROM Details AS d, ScoringSheet AS s WHERE s.ID = ? AND d.DetailsID = s.DetailsID", id);
            return a;
        }

        /// <summary>
        /// Method for returning weather conditions associated with an end.
        /// </summary>
        /// <param name="endRef"></param>
        /// <returns></returns>
        public List<WeatherConditions> GetPreviousWeather(string endRef)
        {
            var a = dbConn.Query<WeatherConditions>("SELECT Temp, WindSpeed, WindDir, Humidity, Other FROM WeatherConditions WHERE EndNum = ?", endRef);
            return a;
        }

        /// <summary>
        /// Method for returning notes associated with an end.
        /// </summary>
        /// <param name="endRef"></param>
        /// <returns></returns>
        public List<Notes> GetPreviousNote(string endRef)
        {
            var a = dbConn.Query<Notes>("SELECT EndNotes FROM Notes WHERE EndNum = ?", endRef);
            return a;
        }


        /// <summary>
        /// Method for backing up bows in database.
        /// </summary>
        /// <returns></returns>
        public List<Bow> Exportbows()
        {
            var a = dbConn.Query<Bow>("SELECT * FROM Bow");
            return a;
        }

        /// <summary>
        /// Method for backing up competition data in database.
        /// </summary>
        /// <returns></returns>
        public List<CompBackup> CompBackup()
        {
            string type = "720Competition";
            var a = dbConn.Query<CompBackup>("SELECT  d.'Date', d.Dist, d.BowType, e.*, s.FinalTotal FROM Details as d, ScoringSheet AS s, 'End' AS E WHERE d.DetailsID = s.DetailsID AND s.Type = ? AND s.ID = e.ID", type);
            //works in Db browser. No notes or ends as they can be not there, allows this to be used for Comp and Prac.
            return a;
        }

        /// <summary>
        /// Method for backing up practice data in database.
        /// </summary>
        /// <returns></returns>
        public List<PracBackup> PracBackUp()//need to change typing for set
        {
            string type = "Practice";
            var a = dbConn.Query<PracBackup>("SELECT  d.'Date', d.Dist, d.BowType, e.*, s.FinalTotal, n.EndNotes, w.* FROM  'End' AS e JOIN ScoringSheet AS s ON e.ID = s.ID JOIN Details as d ON s.DetailsID = d.DetailsID LEFT OUTER JOIN Notes n ON e.EndNum = n.EndNum LEFT OUTER JOIN WeatherConditions w ON e.EndNum = w.EndNum WHERE s.Type = ?", type);
            return a;//returns nulls as PracBackUP
        }
    }
}

