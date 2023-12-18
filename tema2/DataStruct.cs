using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DataStruct
    {
        public string key { get; set; }
        public string value { get; set; }

        public DataStruct(string strValue1, string strValue2)
        {
            key = strValue1;
            value = strValue2;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object? obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                DataStruct d = (DataStruct)obj;
                return (key == d.key) && (value == d.value);
            }
        }
    }

    public class Colection
    {
        public object resultList { get; set; } // stare in care ne reducem sau deplasam
        public object listNumber { get; set; } //randul
        public object character { get; set; } // coloana din matr

        public Colection(object firstList, object with, object bValue)
        {
            listNumber = firstList;
            character = with;
            resultList = bValue;
        }
    }
}
