﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Forbes
{ 
public static class LogWriter
{
    private static string m_exePath = string.Empty;
   
    public static void LogWrite(string logMessage)
    {
        m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        try
        {
            using (StreamWriter w = File.AppendText(m_exePath + "\\" + "log" + DateTime.Now.ToString("yyyyMMdd") + ".txt"))
                {
                Log(logMessage, w);
            }
        }
        catch (Exception ex)
        {
        }
    }
        public static void LogWrite(Dictionary<string, object> dic)
        {
            using (StreamWriter w = File.AppendText(m_exePath + "\\" + "log" + DateTime.Now.ToString("yyyyMMdd") + ".txt"))
                foreach (var entry in dic)
                    w.WriteLine("[{0}: {1}]", entry.Key, entry.Value);
        }

    public static void Log(string logMessage, TextWriter txtWriter)
    {
        try
        {
            txtWriter.Write("\r\nLog Entry : ");
            txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());           
            txtWriter.WriteLine("  :{0}", logMessage);
            txtWriter.WriteLine("-------------------------------");
        }
        catch (Exception ex)
        {
        }
    }
}
}
