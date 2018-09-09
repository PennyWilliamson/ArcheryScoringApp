using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

using System.Text;

namespace ArcheryScoringApp.Data
{
    [Table("Notes") ]
    class Notes
    {
        [PrimaryKey, ForeignKey(typeof(End))]
        public string EndNum { get; set; }
        public string EndNotes { get; set; }
    }
}
