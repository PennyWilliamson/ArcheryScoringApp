using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CsvHelper;
using Plugin.Messaging;

/// <summary>
/// Four class in file, as they all deal with backup.
/// Class only as required for database calls, otherwise this
/// would be one file, one class with four methods.
/// </summary>
namespace ArcheryScoringApp.Data
{
    /// <summary>
    /// Class for handling e-mailing of csv backup files.
    /// </summary>
    class DatabaseBackup
    {
        /// <summary>
        /// Method for e-mailing files.
        /// Based on the code from the NuGet Plugin.Messaging documentation
        /// cjlotz, j. (n/d). Xamarin.Plugins/Messaging. Retrieved September 17, 2018, from GitHub cjlotz: https://github.com/cjlotz/Xamarin.Plugins/blob/master/Messaging/Details.md
        /// </summary>
        public void EmailBow()
        {
            try //catches errors
            {
                //sets the file paths for the attachments
                string filePathPrac = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PracBackup.csv");
                string filePathComp = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Comp720Backup.csv");
                string filePathBow = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BowBackup.csv");

                var emailMessenger = CrossMessaging.Current.EmailMessenger;

                //checks that device can send e-mail
                //Creates e-mail message, opens e-mail and sends e-mail
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
                string e = ex.ToString();//error for debug
                string err = "Sorry e-mail could not be opened";//error message for pop up
                ArchMain.ErrorMess(err);
            }
        }

    }

    /// <summary>
    /// Class for use in getting PracBackup query from database
    /// and writing output to a csv file.
    /// Code for csv query modified from NuGet csvHelper documentation
    /// Close, J. (n/d). CSVHelper. Retrieved September 15, 2018, from CSVHelper GitHub: https://joshclose.github.io/CsvHelper/
    /// </summary>
    class PracBackup
    {
        //Public variables for use in database Query<PracBackup>
        public int FinalTotal { get; set; }//Final total from ScoringsSheet
        public string Date { get; set; }//date from details
        public string Dist { get; set; }//distance from details
        public string BowType { get; set; }//bow type from details
        public int ID { get; set; } //shows which end belongs to which sheet
        public int EndTotal { get; set; }//from end
        public string Score1 { get; set; }//from end
        public string Score2 { get; set; }//from end
        public string Score3 { get; set; }//from end
        public string Score4 { get; set; }//from end
        public string Score5 { get; set; }//from end
        public string Score6 { get; set; }//from end
        public string EndNotes { get; set; }//notes
        public double Temp { get; set; }//temperature from weather conditions
        public double WindSpeed { get; set; }//from weather conditions
        public string WindDir { get; set; }//from weather conditions
        public double Humidity { get; set; }//from weather conditions
        public string Other { get; set; }//from weather conditions

        /// <summary>
        /// Constructor, parameterless as needed by databse query
        /// </summary>
        public PracBackup() { }

        //List for holding query output
        static List<PracBackup> pracBack = new List<PracBackup>();

        /// <summary>
        /// Method that calls databse query, writes the returned output to csv,
        /// then writes the csv file to device.
        /// </summary>
        public void PracToCSV()
        {
            //path for file
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PracBackup.csv");
            TextWriter textWriter = File.CreateText(fileName);//creates file in that location, overriting any existing file.
            pracBack = App.Database.PracBackUp();//database call
            var pracRecords = pracBack; //sets return to var for NuGet
            var csv = new CsvWriter(textWriter);//new csv writer
            csv.WriteRecords(pracRecords);//writes the records
            csv.Flush();//empties the writer
            textWriter.Flush();//empties text writer, allowing write to happen
            textWriter.Close();//closes textwriter and CsvWriter.

        }
    }

    /// <summary>
    /// Class for use in getting CompBackup query from database
    /// and writing output to a csv file.
    /// Does not have any note or weather conditions output.
    /// Code for csv query modified from NuGet csvHelper documentation
    /// Close, J. (n/d). CSVHelper. Retrieved September 15, 2018, from CSVHelper GitHub: https://joshclose.github.io/CsvHelper/
    /// </summary>
    class CompBackup
    {
        //Public variables for use in database Query<PracBackup>
        public int FinalTotal { get; set; }//From scoring sheet
        public string Date { get; set; }//From details
        public string Dist { get; set; }//From details
        public string BowType { get; set; }//from details
        public int ID { get; set; } //shows which end belongs to which sheet, from end
        public int EndTotal { get; set; }//from end
        public string Score1 { get; set; }//from end
        public string Score2 { get; set; }//from end
        public string Score3 { get; set; }//from end
        public string Score4 { get; set; }//from end
        public string Score5 { get; set; }//from end
        public string Score6 { get; set; }//from end

        //List for hold database call return.
        static List<CompBackup> compBack = new List<CompBackup>();

        /// <summary>
        /// Parameterless constructor, required by for database Query
        /// </summary>
        public CompBackup() { }

        /// <summary>
        /// Calls database and writes return to a csv file on the device.
        /// For line comment detail see PracBackup.
        /// </summary>
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

    /// <summary>
    /// For the Bow backup. Uses the paramertless constructer
    /// for database table class Bow for the list type. 
    /// Code for writing csv modified from NuGet csvHelper documentation
    /// Close, J. (n/d). CSVHelper. Retrieved September 15, 2018, from CSVHelper GitHub: https://joshclose.github.io/CsvHelper/
    /// </summary>
    class BowBackup
    {
        //List to hold return from databse call.
        static List<Bow> bowBack = new List<Bow>();

        /// <summary>
        /// Calls database query method, and writes return to 
        /// a csv file stored on device.
        /// For line comment level detail, see PracBackup.
        /// </summary>
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
}

