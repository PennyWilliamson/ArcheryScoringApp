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
    class ASCDatabase
    {
        internal readonly SQLiteConnection dbConn;//internal so it can be closed on sleep.
        
        
        public ASCDatabase(string dbFilePath)
        {
            dbConn = new SQLiteConnection(dbFilePath);//connection string code
            dbConn.CreateTable<ScoringSheet>();
            dbConn.CreateTable<End>();
            dbConn.CreateTable<Details>();
            dbConn.CreateTable<Bow>();
            dbConn.CreateTable<Notes>();
            dbConn.CreateTable<WeatherConditions>();
        }

        
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
    
        public int InsertDetails(string date)
        {
            var details = new Details()
            {
                FirstName = "Caitlin",
                LastName = "Thomas-Riley",
                BowType = ArchMain.bowType,
                Division = "JWR",
                Club = "Randwick",
                Date = date,
                ArchNZNum = 3044,
                Dist = ArchMain.dist
        };
            dbConn.Insert(details);
            var bow = dbConn.Get<Bow>(ArchMain.bowType);
            details.bow = bow;
            dbConn.UpdateWithChildren(details);
            var dtlID = details.DetailsID;
            return dtlID;

        }

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

        public void EditSight(double markings)
        {
            var bow = new Bow()
            {
                BowType = ArchMain.bowType,
                SightMarkings = markings
            };

           dbConn.Update(bow);
        }

        public List<Bow> GetSightMarkings(string bow)
        {
            var b = dbConn.Query<Bow>("SELECT SightMarkings FROM Bow WHERE BowType = ?", bow);           
            return b;
        }

        public List<ScoringSheet> getPB()
        { 
            string type = "720Competition";//hard set as only 720 competition is in, will need passed as a variable
            string distance = ArchMain.dist;
            string bow = ArchMain.bowType;
            var b = dbConn.Query<ScoringSheet>("SELECT DISTINCT ID, FinalTotal FROM ScoringSheet AS ss, Details AS d WHERE ss.Type = ? AND d.BowType = ? AND d.Dist = ? ORDER BY FinalTotal DESC LIMIT 1", type, bow, distance);
            
            return b;
        }

        public List<ScoringSheet> GetLastScore()
        {
            string type = "720Competition";//hard set as only 720 competition is in, will need passed as a variable
            string distance = ArchMain.dist;
            string bow = ArchMain.bowType;
            var b = dbConn.Query<ScoringSheet>("SELECT DISTINCT ID, FinalTotal FROM ScoringSheet AS ss, Details AS d WHERE ss.Type = ? AND d.BowType = ? AND d.Dist = ? ORDER BY ID DESC LIMIT 1", type, bow, distance);
            return b;
        }

        public List<ScoringSheet> GetLastBest(int id)
        {
            string type = "720Competition";//hard set as only 720 competition is in, will need passed as a variable
            string distance = ArchMain.dist;
            string bow = ArchMain.bowType;
            var b = dbConn.Query<ScoringSheet>("SELECT DISTINCT FinalTotal FROM ScoringSheet AS ss, Details as d WHERE ss.Type = ? AND ss.ID > ? AND d.BowType = ? AND d.Dist = ? ORDER BY FinalTotal DESC LIMIT 1", type, id, bow, distance);
            return b;

        }

        public void AddNotes(string endRef, string note)
        {
            var notes = new Notes()
            {
                EndNum = endRef, //sets the endNum to current end's eR
                EndNotes = note
            };

            dbConn.InsertOrReplace(notes);
            var end = dbConn.Get<End>(endRef);
            notes.end = end;
            dbConn.UpdateWithChildren(notes);
        }

        public void AddWeather(string endRef, double temp, double speed, string dir, double hum, string other)
        {
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

        public List<End> GetPreviousEnds(int FinalScore, string Type)
        {
            var a = dbConn.Query<End>("SELECT E.ID, EndNum, Score1, Score2, Score3, Score4, Score5, Score6, EndTotal from `End` AS E, ScoringSheet AS S where S.FinalTotal = ? AND S.Type = ? AND E.ID = S.ID ORDER BY S.ID ASC", FinalScore, Type);
            return a;
        }

        public List<Details> GetPreviousDetails(int id)
        {
            var a = dbConn.Query<Details>("SELECT Date, Dist FROM Details AS d, ScoringSheet AS s WHERE s.ID = ? AND d.DetailsID = s.DetailsID", id);
            return a;
        }

        public List<WeatherConditions> GetPreviousWeather(string endRef)
        {
            var a = dbConn.Query<WeatherConditions>("SELECT Temp, WindSpeed, WindDir, Humidity, Other FROM WeatherConditions WHERE EndNum = ?", endRef);
            return a;
        }

        public List<Notes> GetPreviousNote(string endRef)
        {
            var a = dbConn.Query<Notes>("SELECT EndNotes FROM Notes WHERE EndNum = ?", endRef);
            return a;
        }



        public List<Bow> Exportbows()
        {
            var a = dbConn.Query<Bow>("SELECT * FROM Bow");
            return a;
        }

        public List<CompBackup> CompBackup()
        {
            string type = "720Competition";
            var a = dbConn.Query<CompBackup>("SELECT  d.'Date', d.Dist, d.BowType, e.*, s.FinalTotal FROM Details as d, ScoringSheet AS s, 'End' AS E WHERE d.DetailsID = s.DetailsID AND s.Type = ? AND s.ID = e.ID", type);
            //works in Db browser. No notes or ends as they can be not there, allows this to be used for Comp and Prac.
            return a;
        }

        public List<PracBackup> PracBackUp()//need to change typing for set
        {
            string type = "Practice";
            var a = dbConn.Query<PracBackup>("SELECT  d.'Date', d.Dist, d.BowType, e.*, s.FinalTotal, n.EndNotes, w.* FROM  'End' AS e JOIN ScoringSheet AS s ON e.ID = s.ID JOIN Details as d ON s.DetailsID = d.DetailsID LEFT OUTER JOIN Notes n ON e.EndNum = n.EndNum LEFT OUTER JOIN WeatherConditions w ON e.EndNum = w.EndNum WHERE s.Type = ?", type);
            return a;//returns nulls as PracBackUP
        }
    }
}

