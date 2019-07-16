using System;
using System.IO;
using System.Data;
using System.Diagnostics;

/// <summary>
/// ������־д����
/// </summary>
public class ErrLog
{
    /// <summary>
    /// д��һ����־
    /// </summary>
    /// <param name="LogInformation">д�����־��Ϣ</param>
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
    /// д��һ����־
    /// </summary>
    /// <param name="err">������Ϣ</param>
    /// <param name="MyMessage">�Զ��������Ϣ����</param>
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