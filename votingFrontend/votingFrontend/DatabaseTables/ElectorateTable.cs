using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace votingFrontend.DatabaseTables
{
    public class ElectorateTable
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }

        public int ServerId { get; set; }

        public string Name { get; set; }

        public string Detail { get; set; }

        public string Image { get; set; }
    }
}
