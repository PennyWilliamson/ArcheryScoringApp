using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcheryScoringApp.Data
{
    /// <summary>
    /// Class for Details table.
    /// Code modified from NuGet documentation.
    /// Twincoders. (2018, August 13). SQLite-Net Extensions. Retrieved August 29, 2018, from Bitbucket: https://bitbucket.org/twincoders/sqlite-net-extensions
    /// </summary>
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
        [Indexed]//as searched for stats
        public string Dist {get; set;}

        [ManyToOne]
        public Bow bow { get; set; }
    }
}
