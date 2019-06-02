using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log_Processing_Component
{
    public class Processor
    {
        Dictionary<string, Employee> employees = new Dictionary<string, Employee>();
        List<string> employeeNames = new List<string>();
        string[] splitBy = new string[] { " ", "\t" };
        string workHourResultString;
        string logResultString;
        string fullResultString;
        string path;

        public Processor(string path)
        {
            this.path = path;
        }

        public void Process()
        {
            using (StreamReader reader = new StreamReader(path))
            {
                // read file wiht code copied from MSDN
                while (reader.Peek() > -1)
                {
                    ProcessLog(ProcessLine(reader.ReadLine()));
                }
                List<Log> testLogs = new List<Log>();
                workHourResultString = "";
                logResultString = "";
                fullResultString = "";
                foreach (KeyValuePair<string, Employee> pair in employees)
                {
                    pair.Value.Process();
                    workHourResultString += pair.Value.GetTimeResultString();
                    logResultString += pair.Value.GetLogString();
                    fullResultString += pair.Value.GetTimeResultString() + pair.Value.GetLogString();

                }
            }
        }


        public Log ProcessLine(string line)
        {
            if (line.Length == 58)
            {
                var content = line.Split(splitBy, StringSplitOptions.RemoveEmptyEntries);
                var nameLength = content.Length - 7;
                int logNumber = int.Parse(content[0]);
                DateTime datetime = DateTime.Parse(content[content.Length - 2] + ' ' + content[content.Length - 1]);
                string name = String.Join(" ", content, 3, nameLength);
                Log log = new Log()
                {
                    LogNumber = logNumber,
                    Name = name,
                    LogDateTime = datetime
                };
                return log;
            }
            return null;
        }

        public void ProcessLog(Log log)
        {
            if (log != null)
            {
                if (!HasEmployee(log.Name))
                {
                    AddEmployee(log.Name);
                }
                AddLog(log);
            }

        }

        #region Manipulation

        public void AddEmployee(string name)
        {
            employees[name] = new Employee(name);
            employeeNames.Add(name);
        }

        public void AddLog(Log log)
        {
            employees[log.Name].AddLog(log);
        }

        #endregion

        #region Gets

        public Employee GetEmployee(string name)
        {
            return employees[name];
        }

        public bool HasEmployee(string name)
        {
            return employeeNames.Contains(name);
        }

        public string GetTimeResultString()
        {
            return workHourResultString;
        }

        public string GetFullResultString()
        {
            return fullResultString;
        }

        public string GetLogResultString()
        {
            return logResultString;
        }

        #endregion
    }
}
