using System;
using System.IO;
using System.Data;
using System.Diagnostics;

/// <summary>
/// 错误日志写入类
/// </summary>
public class ErrLog
{
    /// <summary>
    /// 写入一个日志
    /// </summary>
    /// <param name="LogInformation">写入的日志信息</param>
    static public void WriteErrLog(string LogInformation)
    {            
        LogInformation = DateTime.Now.ToString() + ":    " + LogInformation;
        if (!EventLog.SourceExists("DataErr"))
        {
            EventLog.CreateEventSource("DataErr", "DataErrErrLog");
        }

        EventLog.WriteEntry("DataErr", LogInformation);
    }


    /// <summary>
    /// 写入一个日志
    /// </summary>
    /// <param name="err">错误信息</param>
    /// <param name="MyMessage">自定义错误消息名称</param>
    static public void WriteErrLog(Exception err, string MyMessage)
    {
        if (!EventLog.Exists("DataErr"))
        {
            EventLog.CreateEventSource("DataErr", "DataErrErrLog");
        }
        EventLog log = new EventLog("DataErrErrLog");
        string Information = DateTime.Now.ToString() + ":    " + err.Message;
        EventLog.WriteEntry("DataErr", Information);
        Information = DateTime.Now.ToString() + ":    " + MyMessage;
        EventLog.WriteEntry("DataErr", Information);
    }
}