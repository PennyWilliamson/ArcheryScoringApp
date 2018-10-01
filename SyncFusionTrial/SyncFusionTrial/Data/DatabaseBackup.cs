using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CsvHelper;
using Plugin.Messaging;

namespace ArcheryScoringApp.Data
{
    class DatabaseBackup
    {


        public void EmailBow()
        {
            //based on code from the NuGet documentation https://github.com/cjlotz/Xamarin.Plugins/blob/master/Messaging/Details.md 
            try
            {

                string filePathPrac = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PracBackup.csv");
                string filePathComp = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Comp720Backup.csv");
                string filePathBow = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BowBackup.csv");
                string path = filePathBow;


                var emailMessenger = CrossMessaging.Current.EmailMessenger;
                if (emailMessenger.CanSendEmail)
                {

                    var email = new EmailMessageBuilder()
                    .To("")
                    .Subject("Archery Scoring App Database backup")
                    .Body("Files for backing up database")
                    .WithAttachment(filePathBow, "BowBackup.csv")
                    .WithAttachment(filePathComp, "Comp720Backup.csv")
                    .WithAttachment(filePathPrac, "PracBackup.csv")
                    .Build();
                    emailMessenger.SendEmail(email);
                }
            }
            catch (Exception ex)
            {
                string e = ex.ToString();
                string err = "Sorry e-mail could not be opened";
                ArchMain.ErrorMess(err);
            }
        }

    }

    class PracBackup
    {
        public int FinalTotal { get; set; }
        public string Date { get; set; }
        public string Dist { get; set; }
        public string BowType { get; set; }
        public int ID { get; set; } //shows which end belongs to which sheet
        public int EndTotal { get; set; }
        public string Score1 { get; set; }
        public string Score2 { get; set; }
        public string Score3 { get; set; }
        public string Score4 { get; set; }
        public string Score5 { get; set; }
        public string Score6 { get; set; }
        public string EndNotes { get; set; }
        public double Temp { get; set; }
        public double WindSpeed { get; set; }
        public string WindDir { get; set; }
        public double Humidity { get; set; }
        public string Other { get; set; }


        public PracBackup() { }
        static List<PracBackup> pracBack = new List<PracBackup>();

        public void PracToCSV()
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PracBackup.csv");
            TextWriter textWriter = File.CreateText(fileName);
            pracBack = App.Database.PracBackUp();
            var pracRecords = pracBack;
            var csv = new CsvWriter(textWriter);
            csv.WriteRecords(pracRecords);
            csv.Flush();
            textWriter.Flush();
            textWriter.Close();


        }
    }

    class CompBackup
    {
        public int FinalTotal { get; set; }
        public string Date { get; set; }
        public string Dist { get; set; }
        public string BowType { get; set; }
        public int ID { get; set; } //shows which end belongs to which sheet
        public int EndTotal { get; set; }
        public string Score1 { get; set; }
        public string Score2 { get; set; }
        public string Score3 { get; set; }
        public string Score4 { get; set; }
        public string Score5 { get; set; }
        public string Score6 { get; set; }

        static List<CompBackup> compBack = new List<CompBackup>();

        public CompBackup() { }

        public void CompToCSV()
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Comp720Backup.csv");
            TextWriter textWriter = File.CreateText(fileName);
            compBack = App.Database.CompBackup();
            var compRecords = compBack;
            var csv = new CsvWriter(textWriter);
            csv.WriteRecords(compRecords);
            csv.Flush();
            textWriter.Flush();
            textWriter.Close();


        }
    }

    class BowBackup
    {
        static List<Bow> bowBack = new List<Bow>();


        public void BowToCSV()
        {

            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BowBackup.csv");
            TextWriter textWriter = File.CreateText(fileName);
            bowBack = App.Database.Exportbows();
            var bowRecords = bowBack;//works to here
            var csv = new CsvWriter(textWriter);//gives null error
            csv.WriteRecords(bowRecords);
            csv.Flush();
            textWriter.Flush();
            textWriter.Close();

            string text = File.ReadAllText(fileName);
            //   ArchMain.ErrorMess(text);

        }
    }

    //try for android
    class ForDroid
    {

        static List<Bow> bowBack = new List<Bow>();

        public void EmailBow()
        {
            //based on code from https://github.com/cjlotz/Xamarin.Plugins/blob/master/Messaging/Details.md
            try
            {
                string fileBow = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BowBackup.csv");


                var emailMessenger = CrossMessaging.Current.EmailMessenger;
                if (emailMessenger.CanSendEmail)
                {

                    var email = new EmailMessageBuilder()
          .To("")
          .Subject("Archery Scoring App Database backup")
          .Body("Files for backing up database")
       .WithAttachment(fileBow, "BowBackup.csv")
         //     .WithAttachment(filePathComp, "Comp720Backup.csv")
         //    .WithAttachment(filePathPrac, "PracBackup.csv")
         .Build();
                    emailMessenger.SendEmail(email);
                }
            }
            catch (Exception ex)
            {
                string err = ex.ToString();
                ArchMain.ErrorMess(err);
            }
        }

        public void BowToCSV()
        {

            string fileBow = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BowBackup.csv ");
            TextWriter textWriter = File.CreateText(fileBow);
            bowBack = App.Database.Exportbows();
            var bowRecords = bowBack;//works to here
            var csv = new CsvWriter(textWriter);//gives null error
            csv.WriteRecords(bowRecords);
            csv.Flush();
            textWriter.Flush();
            textWriter.Close();

            string text = File.ReadAllText(fileBow);
            string t = text;
            //   ArchMain.ErrorMess(text);

        }


    }

}
