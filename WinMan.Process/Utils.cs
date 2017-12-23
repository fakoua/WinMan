﻿using System.Collections.Generic;
using WinMan.Process.Models;

namespace WinMan.Process
{
    public class Utils
    {
        public static List <ProcessModel> GetProcesses()
        {
            var rtnVal = new List<ProcessModel>();
            var procs = System.Diagnostics.Process.GetProcesses();

            foreach (var proc in procs)
            {
                try
                {
                    var p = new ProcessModel()
                    {
                        Id = proc.Id,
                        MainWindowTitle = proc.MainWindowTitle,
                        ProcessName = proc.ProcessName,
                        SessionId = proc.SessionId,
                        StartTime = proc.StartTime,
                        TotlaProcessorTime = proc.TotalProcessorTime,
                        UserProcessorTime = proc.UserProcessorTime,
                        WorkingSet64 = proc.WorkingSet64
                    };
                    rtnVal.Add(p);
                }
                catch (System.Exception)
                {
                   //TODO: Should Check the security
                }
               
            }

            return rtnVal;
        }
    }
}