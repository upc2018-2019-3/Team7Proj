using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// 学生类
/// </summary>
public class Person
{
    private string m_PersonID;    
    private string m_PersonName;
    private string m_Password;
    private string m_Sex;
    private string m_Birthday;
    private ClassInfo m_ClassInfo;
    private Spec m_Spec;
    private string m_Tel;
    private string m_Mail;

	public Person()
	{
        this.m_PersonID = "";
        this.m_PersonName = "";
        this.m_Password = "";
        this.m_Sex = "";
        this.m_Birthday = "";
        this.m_ClassInfo = new ClassInfo();
        this.m_Spec = new Spec();
        this.m_Tel = "";
        this.m_Mail = "";
	}

    /// <summary>
    /// 登录编号
    /// </summary>
    public string PersonID
    {
        set
        {
            this.m_PersonID = value;
        }
        get
        {
            return this.m_PersonID;
        }
    }

    /// <summary>
    /// 登录密码
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
    /// 真实姓名
    /// </summary>
    public string PersonName
    {
        set
        {
            this.m_PersonName = value;
        }
        get
        {
            return this.m_PersonName;
        }
    }

    /// <summary>
    /// 出生日期
    /// </summary>
    public string Birthday
    {
        set
        {
            this.m_Birthday = value;
        }
        get
        {
            return this.m_Birthday;
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
    /// 班级
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
    /// 专业
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
    /// 电话
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
}
