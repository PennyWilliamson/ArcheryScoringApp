using System;
using System.Collections.Generic;
using System.Text;
using SQLiteNetExtensions;
using SQLite;
using System.Linq;
using SQLiteNetExtensions.Extensions;
using System.Threading.Tasks;


namespace ArcheryScoringApp.Data
{
    class ASCDatabase
    {
        internal readonly SQLiteConnection db;//internal so it can be closed on sleep.
        
        
        public ASCDatabase(string dbFilePath)
        {
            db = new SQLiteConnection(dbFilePath);//connection string code
            db.CreateTable<ScoringSheet>();
            db.CreateTable<End>();
            db.CreateTable<Details>();
            db.CreateTable<Bow>();
            db.CreateTable<Notes>();
            db.CreateTable<WeatherConditions>();
        }

        
        public int InsertScoringSheet(int dtlsID, string typ)
        {
            var sheet = new ScoringSheet()
            {
                Type = typ,
                DetailsID = dtlsID
            };

            db.Insert(sheet);
            var detail = db.Get<Details>(dtlsID);
            sheet.details = detail; //for ref integ
            db.UpdateWithChildren(sheet);
            var scrShtID = sheet.ID;
            return scrShtID;
        }
    
        public int InsertDetails(string date)
        {
            var details = new Details()
            {
                FirstName = "Caitlin",
                LastName = "Thomas-Riley",
                BowType = "Recurve",
                Division = "JWR",
                Club = "Randwick",
                Date = date,
                ArchNZNum = 3044,
                Dist = ArchMain.dist
        };
            db.Insert(details);
            var bow = db.Get<Bow>(ArchMain.bowType);
            details.bow = bow;
            db.UpdateWithChildren(details);
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

                db.InsertOrReplace(end);//handles the end already being there, ie, editing scores.
                var sheet = db.Get<ScoringSheet>(anEnd.id);//for ref integ
            end.scoringSheet = sheet;
            db.UpdateWithChildren(end);
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
            db.Update(sheet);
                
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
               db.Insert(newBow);
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

           db.Update(bow);
        }

        public List<Bow> GetSightMarkings(string bow)
        {
            var b = db.Query<Bow>("SELECT SightMarkings FROM Bow WHERE BowType = ?", bow);           
            return b;
        }

        public List<ScoringSheet> getPB()
        { 
            string type = "720Competition";//hard set as only 720 competition is in, will need passed as a variable
            string distance = ArchMain.dist;
            string bow = ArchMain.bowType;
            var b = db.Query<ScoringSheet>("SELECT DISTINCT ID, FinalTotal FROM ScoringSheet AS ss, DETAILS AS d WHERE ss.Type = ? AND d.BowType = ? AND d.Dist = ? ORDER BY FinalTotal DESC LIMIT 1", type, bow, distance);
            // "Select MAX(FinalTotal) From ScoringSheet where Type = 720Competition" always returns first in list
            
            return b;
        }

        public List<ScoringSheet> GetLastScore()
        {
            string type = "720Competition";//hard set as only 720 competition is in, will need passed as a variable
            string distance = ArchMain.dist;
            string bow = ArchMain.bowType;
            var b = db.Query<ScoringSheet>("SELECT DISTINCT ID, FinalTotal FROM ScoringSheet AS ss, Details AS d WHERE ss.Type = ? AND d.BowType = ? AND d.Dist = ? ORDER BY ID DESC LIMIT 1", type, bow, distance);
            return b;
        }

        public List<ScoringSheet> GetLastBest(int id)
        {
            string type = "720Competition";//hard set as only 720 competition is in, will need passed as a variable
            string distance = ArchMain.dist;
            string bow = ArchMain.bowType;
            var b = db.Query<ScoringSheet>("SELECT DISTINCT FinalTotal FROM ScoringSheet AS ss, Details as d WHERE ss.Type = ? AND ss.ID > ? AND d.BowType = ? AND d.Dist = ? ORDER BY FinalTotal DESC LIMIT 1", type, id, bow, distance);
            return b;

        }

        public void AddNotes(string endRef, string note)
        {
            var notes = new Notes()
            {
                EndNum = endRef, //sets the endNum to current end's eR
                EndNotes = note
            };

            db.InsertOrReplace(notes);
            var end = db.Get<End>(endRef);
            end.Notes = notes;
            db.UpdateWithChildren(end);
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
            db.InsertOrReplace(weather);
            var end = db.Get<End>(endRef);
            end.Weather = weather;
            db.UpdateWithChildren(end);
        }

        public List<End> GetPreviousEnds(int FinalScore, string Type)
        {
            var a = db.Query<End>("select EndNum, Score1, Score2, Score3, Score4, Score5, Score6, EndTotal from `End` AS E, ScoringSheet AS S where S.FinalTotal = ? AND S.Type = ? AND S.ID = E.ID ", FinalScore, Type);
            return a;
        }
    }
}

