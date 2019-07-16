using System;
using System.Data;
using System.Configuration;

/// <summary>
/// 管理员类
/// </summary>
public class Admin
{
    private string m_AdminID;
    private string m_AdminName;
    private string m_Tel;
    private string m_Mail;
    private string m_QQ;
    private string m_Password;

	public Admin()
	{
        this.m_AdminID = "";
        this.m_AdminName = "";
        this.m_Tel = "";
        this.m_Mail = "";
        this.m_QQ = "";
        this.m_Password = "";
	}

    /// <summary>
    /// 登录名称
    /// </summary>
    public string AdminID
    {
        set
        {
            this.m_AdminID = value;
        }
        get
        {
            return this.m_AdminID;
        }
    }

    /// <summary>
    /// 管理员名称
    /// </summary>
    public string AdminName
    {
        set
        {
            this.m_AdminName = value;
        }
        get
        {
            return this.m_AdminName;
        }
    }

    /// <summary>
    /// 联系电话
    /// </summary>
    public string Tel
    {
        set
        {
            this.m_Tel = value;
        }
        get
        {
            return this.m_Tel;
        }
    }

    /// <summary>
    /// 电子邮件
    /// </summary>
    public string Mail
    {
        set
        {
            this.m_Mail = value;
        }
        get
        {
            return this.m_Mail;
        }
    }

    /// <summary>
    /// 联系QQ
    /// </summary>
    public string QQ
    {
        set
        {
            this.m_QQ = value;
        }
        get
        {
            return this.m_QQ;
        }
    }

    /// <summary>
    /// 管理员密码
    /// </summary>
    public string Password
    {
        set
        {
            this.m_Password = value;
        }
        get
        {
            return this.m_Password;
        }
    }
}
