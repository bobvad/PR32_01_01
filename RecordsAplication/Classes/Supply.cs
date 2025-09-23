using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordsAplication.Classes
{
    public class Supply
    {
        public int IdRecord { get; set; }
        public static IEnumerable<Supply> AllSupply()
        {
            List<Supply> allSupply = new List<Supply>();
            return allSupply;
        }
    }
}
