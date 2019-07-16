using System;
using System.Data;
using System.Configuration;

/// <summary>
/// 教师类
/// </summary>
public class Client
{
    private string m_ClientID;
    private string m_ClientName;
    private string m_Password;
    private string m_Sex;
    private ClassInfo m_ClassInfo;
    private Spec m_Spec;
    private string m_Tel;
    private string m_Mail;
    private string m_QQ;
    

	public Client()
	{
        this.m_ClientID = "";
        this.m_ClientName = "";
        this.m_Password = "";
        this.m_Sex = "男";
        this.m_ClassInfo = new ClassInfo();
        this.m_Spec = new Spec();
        this.m_Tel = "";
        this.m_Mail = "";
        this.m_QQ = "";
        
	}

    /// <summary>
    /// 登录名称
    /// </summary>
    public string ClientID
    {
        set
        {
            this.m_ClientID = value;
        }
        get
        {
            return this.m_ClientID;
        }
    }

    /// <summary>
    /// 教师名称
    /// </summary>
    public string ClientName
    {
        set
        {
            this.m_ClientName = value;
        }
        get
        {
            return this.m_ClientName;
        }
    }


    /// <summary>
    /// 教师密码
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


    /// <summary>
    /// 性别
    /// </summary>
    public string Sex
    {
        set
        {
            this.m_Sex = value;
        }
        get
        {
            return this.m_Sex;
        }
    }


    /// <summary>
    /// 所在班级
    /// </summary>
    public ClassInfo ClassInfo
    {
        set
        {
            this.m_ClassInfo = value;
        }
        get
        {
            return this.m_ClassInfo;
        }
    }

    /// <summary>
    /// 所在专业
    /// </summary>
    public Spec Spec
    {
        set
        {
            this.m_Spec = value;
        }
        get
        {
            return this.m_Spec;
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
}
