using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcheryScoringApp.Data
{
    /// <summary>
    /// Class for Bow table.
    /// Code modified from NuGet documentation.
    /// Twincoders. (2018, August 13). SQLite-Net Extensions. Retrieved August 29, 2018, from Bitbucket: https://bitbucket.org/twincoders/sqlite-net-extensions
    /// </summary>
    [Table("Bow")]
    class Bow
    {
        [PrimaryKey]
        public string BowType { get; set; }
        public double SightMarkings { get; set; }

        
    }
}
