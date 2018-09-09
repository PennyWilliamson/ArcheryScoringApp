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
        public int FinalTotal { get; set; }
        [ForeignKey(typeof(Details))]
        public int DetailsID { get; set; }
        public string Type { get; set; }

        [OneToOne]
        public Details details { get; set; }

        [OneToMany]
        public List<End> Ends { get; set; }
    }
}
