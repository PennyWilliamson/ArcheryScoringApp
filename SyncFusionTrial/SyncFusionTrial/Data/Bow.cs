using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcheryScoringApp.Data
{
    [Table("Bow")]
    class Bow
    {
        [PrimaryKey]
        public string BowType { get; set; }
        public double SightMarkings { get; set; }

        [OneToMany]
        public List<Details> details { get; set; }
    }
}
