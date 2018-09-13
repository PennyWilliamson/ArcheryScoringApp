using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcheryScoringApp.Data
{
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
