using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log_Processing_Component
{
    public class Log : IComparable<Log>
    {
        public int LogNumber { get; set; }
        public string Name { get; set; }
        public DateTime LogDateTime { get; set; }

        public int CompareTo(Log other)
        {
            return LogDateTime.CompareTo(other.LogDateTime);
        }

        public override string ToString()
        {
            return $"{LogNumber : 000000000}, {LogDateTime}";
        }
    }
}
