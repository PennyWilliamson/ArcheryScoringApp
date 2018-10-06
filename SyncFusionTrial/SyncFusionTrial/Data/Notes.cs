using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

using System.Text;

namespace ArcheryScoringApp.Data
{
    /// <summary>
    /// Class for Notes table.
    /// Code modified from NuGet documentation.
    /// Twincoders. (2018, August 13). SQLite-Net Extensions. Retrieved August 29, 2018, from Bitbucket: https://bitbucket.org/twincoders/sqlite-net-extensions
    /// </summary>
    [Table("Notes") ]
    class Notes
    {
        [PrimaryKey, ForeignKey(typeof(End))]
        public string EndNum { get; set; }
        public string EndNotes { get; set; }

        [OneToOne]
        public End end { get; set; }
    }
}
