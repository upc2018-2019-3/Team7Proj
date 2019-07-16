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
/// 留言类
/// </summary>
public class Rewrite
{
    private int m_RewriteID;
    private string m_Title;
    private string m_Content;
    private DateTime m_InputDate;
    private Person m_Person;
    private Client m_Client;    
    private string m_ReContent;

	public Rewrite()
	{
        this.m_RewriteID = 0;
        this.m_Title = "";
        this.m_Content = "";
        this.m_InputDate = DateTime.Now;
        this.m_Person = new Person();
        this.m_Client = new Client();
        this.m_ReContent = "";
	}

    /// <summary>
    /// 编号
    /// </summary>
    public int RewriteID
    {
        set
        {
            this.m_RewriteID = value;
        }
        get
        {
            return this.m_RewriteID;
        }
    }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title
    {
        set
        {
            this.m_Title = value;
        }
        get
        {
            return this.m_Title;
        }
    }

    /// <summary>
    /// 留言内容
    /// </summary>
    public string Content
    {
        set
        {
            this.m_Content = value;
        }
        get
        {
            return this.m_Content;
        }
    }

    /// <summary>
    /// 留言时间
    /// </summary>
    public DateTime InputDate
    {
        set
        {
            this.m_InputDate = value;
        }
        get
        {
            return this.m_InputDate;
        }
    }

    /// <summary>
    /// 学生
    /// </summary>
    public Person Person
    {
        set
        {
            this.m_Person = value;
        }
        get
        {
            return this.m_Person;
        }
    }


    /// <summary>
    /// 教师
    /// </summary>
    public Client Client
    {
        set
        {
            this.m_Client = value;
        }
        get
        {
            return this.m_Client;
        }
    }

    /// <summary>
    /// 回复内容
    /// </summary>
    public string ReContent
    {
        set
        {
            this.m_ReContent = value;
        }
        get
        {
            return this.m_ReContent;
        }
    }
}