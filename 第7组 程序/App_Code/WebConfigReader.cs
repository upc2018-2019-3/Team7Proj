using System;

/// <summary>
/// Web.Config ��ȡ��
/// </summary>
public class WebConfigReader
{
	/// <summary>
	/// ���� GetTitle ����һ��WEBҳ��ı�����Ϣ
	/// </summary>
	/// <returns>WEBҳ��ı���������Ϣ</returns>
	static public string GetTitle()
	{
        return System.Configuration.ConfigurationManager.AppSettings["Title"].ToString();
	}

	/// <summary>
	/// ���� GetDataBaseString ����һ�����ݿ������ַ�����Ϣ
	/// </summary>
	/// <returns>����һ�����ݿ������ַ�����Ϣ</returns>
	static public string GetDatabaseString()
	{
        return System.Configuration.ConfigurationManager.AppSettings["DatabaseString"].ToString();
	}

	/// <summary>
	/// ���� GetAppSetting ����һ�������ַ���������
	/// </summary>
	/// <param name="AppName">Ҫ��ȡ�������ַ���������</param>
	/// <returns>����һ�������ַ���������</returns>
	static public string GetAppSetting(string AppName)
	{
        return System.Configuration.ConfigurationManager.AppSettings[AppName].ToString();
	}
}