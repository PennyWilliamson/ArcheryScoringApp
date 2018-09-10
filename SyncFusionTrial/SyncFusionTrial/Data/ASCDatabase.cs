using System;
using System.Collections.Generic;
using System.Text;
using SQLiteNetExtensions;
using SQLite;
using System.Linq;

namespace ArcheryScoringApp.Data
{
    class ASCDatabase
    {
        public readonly SQLiteConnection db;
        

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
                ArchNZNum = 3044

        };
            db.Insert(details);
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
                BowType = bow
            };
            try
            {
                db.Insert(newBow);
            }
            catch (Exception ex)
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

        public List<ScoringSheet> getPB()
        { 
            string type = "720Competition";
            var b = db.Query<ScoringSheet>("SELECT ID, FinalTotal from ScoringSheet WHERE Type = ? ORDER BY FinalTotal DESC LIMIT 1", type);
            // "Select MAX(FinalTotal) From ScoringSheet where Type = 720Competition" always returns first in list
            
            return b;
        }

        public List<ScoringSheet> GetLastScore()
        {
            var b = db.Query<ScoringSheet>("SELECT FinalTotal from ScoringSheet ORDER BY ID DESC LIMIT 1");
            return b;
        }

        public List<ScoringSheet> GetLastBest(int id)
        { 
            var b = db.Query<ScoringSheet>("SELECT FinalTotal from ScoringSheet WHERE ID > ? ORDER BY FinalTotal DESC LIMIT 1", id);
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
        }

        public List<End> GetPreviousEnds(int FinalScore, string Type)
        {
            var a = db.Query<End>("select EndNum, Score1, Score2, Score3, Score4, Score5, Score6, EndTotal from `End` AS E, ScoringSheet AS S where S.FinalTotal = ? AND S.Type = ? AND S.ID = E.ID ", FinalScore, Type);

            return a;
        }
    }
}

