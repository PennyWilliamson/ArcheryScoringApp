using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcheryScoringApp.Data
{
    [Table("End")]
    class End
    {
        [PrimaryKey]
        public string EndNum { get; set; }
        [ForeignKey(typeof(ScoringSheet))]
        public int ID { get; set; }
        public int EndTotal { get; set; }
        public string Score1 {get; set;}
        public string Score2 { get; set; }
        public string Score3 { get; set; }
        public string Score4 { get; set; }
        public string Score5 { get; set; }
        public string Score6 { get; set; }

        [OneToOne]
        public Notes Notes { get; set; }

        [OneToOne]
        public WeatherConditions Weather { get; set; }

        [ManyToOne]//held as ManyToOne due to null issues.
        public ScoringSheet scoringSheet { get; set; }

    }
}
