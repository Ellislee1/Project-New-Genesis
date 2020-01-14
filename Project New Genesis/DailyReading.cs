using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_New_Genesis
{
    class DailyReading
    {
        public int ID;
        public String Date;
        public List<String> Reading1;
        public List<String> Reading2;
        public List<String> Reading3;

        public DailyReading(int id, String date, List<String> reading1, List<String> reading2, List<String> reading3)
        {
            ID = id;
            Date = date;
            Reading1 = reading1;
            Reading2 = reading2;
            Reading3 = reading3;
        }
    }
}
