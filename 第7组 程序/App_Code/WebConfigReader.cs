using System;

/// <summary>
/// Web.Config 读取类
/// </summary>
public class WebConfigReader
{
	/// <summary>
	/// 函数 GetTitle 返回一个WEB页面的标题信息
	/// </summary>
	/// <returns>WEB页面的标题配置信息</returns>
	static public string GetTitle()
	{
        return System.Configuration.ConfigurationManager.AppSettings["Title"].ToString();
	}

	/// <summary>
	/// 函数 GetDataBaseString 返回一个数据库连接字符串信息
	/// </summary>
	/// <returns>返回一个数据库连接字符串信息</returns>
	static public string GetDatabaseString()
	{
        return System.Configuration.ConfigurationManager.AppSettings["DatabaseString"].ToString();
	}

	/// <summary>
	/// 函数 GetAppSetting 返回一个配置字符串的内容
	/// </summary>
	/// <param name="AppName">要读取的配置字符串的名称</param>
	/// <returns>返回一个配置字符串的内容</returns>
	static public string GetAppSetting(string AppName)
	{
        return System.Configuration.ConfigurationManager.AppSettings[AppName].ToString();
	}
}