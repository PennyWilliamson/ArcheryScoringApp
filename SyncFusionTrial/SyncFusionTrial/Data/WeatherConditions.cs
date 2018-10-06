using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcheryScoringApp.Data
{
    /// <summary>
    /// Class for Weather Conditions table.
    /// Code modified from NuGet documentation.
    /// Twincoders. (2018, August 13). SQLite-Net Extensions. Retrieved August 29, 2018, from Bitbucket: https://bitbucket.org/twincoders/sqlite-net-extensions
    /// </summary>
    [Table("WeatherConditions")]
    class WeatherConditions
    {
        [PrimaryKey, ForeignKey(typeof(End))]
        public string EndNum { get; set; }
        public string Temp { get; set; }
        public string WindSpeed { get; set; }
        public string WindDir { get; set; } //Wind Direction
        public string Humidity { get; set; }
        public string Other { get; set; }

        [OneToOne]
        public End end { get; set; }
    }
}
