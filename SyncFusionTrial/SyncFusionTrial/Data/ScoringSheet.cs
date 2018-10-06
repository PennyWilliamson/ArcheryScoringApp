using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcheryScoringApp.Data
{
    /// <summary>
    /// Class for Scoring sheet table.
    /// Code modified from NuGet documentation.
    /// Twincoders. (2018, August 13). SQLite-Net Extensions. Retrieved August 29, 2018, from Bitbucket: https://bitbucket.org/twincoders/sqlite-net-extensions
    /// </summary>
    [Table("ScoringSheet")]
    class ScoringSheet
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [Indexed]//as searched for stats and previous
        public int FinalTotal { get; set; }
        [ForeignKey(typeof(Details))]
        public int DetailsID { get; set; }
        [Indexed]//as searched for stats and previous
        public string Type { get; set; }

        [OneToOne]
        public Details details { get; set; }

    }
}
