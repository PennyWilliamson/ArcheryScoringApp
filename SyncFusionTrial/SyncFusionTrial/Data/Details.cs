using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcheryScoringApp.Data
{
    [Table("Details")]
    class Details
    {
        [PrimaryKey, AutoIncrement]
        public int DetailsID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [ForeignKey(typeof(Bow))]
        public string BowType { get; set; }
        public string Division { get; set; }
        public string Club { get; set; }
        public string Date { get; set; }
        public int ArchNZNum { get; set; }
        public string Dist {get; set;}

    }
}
