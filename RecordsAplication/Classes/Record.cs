using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordsAplication.Classes
{
    public class Record
    {
        public int IdState { get; set; }
        public static IEnumerable<Record> AllRecords()
        {
            IEnumerable<Record> allRecords = new List<Record>();
            return allRecords;
        }
    }
}
