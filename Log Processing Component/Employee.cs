using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log_Processing_Component
{
    public class Employee
    {
        string name;
        TimeSpan totalWorkTime = new TimeSpan(0);
        List<Log> allLogs = new List<Log>();
        List<Log> pairedLogs = new List<Log>();
        List<Log> oddLogs = new List<Log>();

        public Employee(string name)
        {
            this.name = name;
        }

        public void Process()
        {
            //the data should already be sorted, but better double check just in case;
            SortLog();
            // process the log by pair, and catch if there are odd datas.
            for (int i = 0; i < allLogs.Count; i += 2)
            {
                //if it reaches the last index, which means nothing is there to pair with it
                if (i == allLogs.Count - 1)
                {
                    oddLogs.Add(allLogs[i]);
                }
                // if the two are the same day
                else if (allLogs[i].LogDateTime.Date == allLogs[i + 1].LogDateTime.Date)
                {
                    AddToWorkHour(allLogs[i + 1].LogDateTime - allLogs[i].LogDateTime);
                    pairedLogs.Add(allLogs[i]);
                    pairedLogs.Add(allLogs[i + 1]);
                }
                // if the two are not the same day
                else
                {
                    oddLogs.Add(allLogs[i]);
                    i--;
                }
            }
        }

        #region Basic Manipulation

        public void AddToWorkHour(TimeSpan time)
        {
            totalWorkTime = totalWorkTime.Add(time);
        }

        public void AddLog(Log log)
        {
            allLogs.Add(log);
        }

        public void SortLog()
        {
            allLogs.Sort();
        }

        #endregion

        #region Gets

        public string GetName()
        {
            return name;
        }

        public TimeSpan GetTotalWorkTime()
        {
            return totalWorkTime;
        }

        public List<Log> GetAllLogs()
        {
            return allLogs;
        }

        public List<Log> GetPairedLogds()
        {
            return pairedLogs;
        }

        public List<Log> GetOddLogs()
        {
            return oddLogs;
        }

        #endregion

        #region Get Results

        public string GetLogString()
        {
            string result = $"  Employee {name}'s Logs:\n    All Paired Logs :\n";
            foreach(Log log in pairedLogs)
            {
                result += $"      {log}\n";
            }
            result += "    All Odd Logs :\n";
            foreach(Log log in oddLogs)
            {
                result += $"      {log}\n";
            }
            result += "\n\n";
            return result;
        }

        public string GetTimeResultString()
        {
            return $"  Employee {name} worked {totalWorkTime.TotalHours : 00.00} Hours\n";
        }

        #endregion
    }
}
