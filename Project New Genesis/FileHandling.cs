using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// NOTE:
/// delimiter for file = ;
/// delimiter for classes = ,
/// delimiter for readings = .
/// Example [01-Jan,"Gen-1. Gen-2","Psa-1. Psa-2","Mat-1. Mat-2";]
/// </summary>
namespace Project_New_Genesis
{
    class FileHandling
    {
        private readonly String PATH = "Readings Plans\\";

        public List<DailyReading> GetFile(string fileName)
        {
            using (TextFieldParser parser = new TextFieldParser(PATH + fileName))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters("/");
                int i = 0;
                List<DailyReading> readings = new List<DailyReading>();
                while (!parser.EndOfData)
                {
                    //Processing row
                    string[] fields = parser.ReadFields();
                    foreach (string field in fields)
                    {
                        string itTemp = field.Replace(" ", "");
                        string[] temp = itTemp.Split(',');
                        int id = i;
                        string date = temp[0];
                        List<String> readings1 = GetReadings(temp[1]);
                        List<String> readings2 = GetReadings(temp[2]);
                        List<String> readings3 = GetReadings(temp[3]);

                        DailyReading newReading = new DailyReading(id, date, readings1, readings2, readings3);

                        readings.Add(newReading);

                        i++;
                    }
                }
                return readings;
            }
        }

        private List<String> GetReadings(String obj)
        {
            List<String> temp = new List<String>();
            if (obj.Contains(';'))
            {
                string[] split = obj.Split(';');
                string[] Prefix = split[0].Split('-');
                int upper = int.Parse(split[1]);
                int lower = int.Parse(Prefix[1]);
                for (int i = lower; i <= upper; i++)
                {
                    temp.Add(Prefix[0] + "-" + i);
                }
            } else
            {
                temp.Add(obj);
            }
            return temp.ToList();
        }
    }
}
