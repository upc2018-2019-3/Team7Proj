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
public class AddNumber
{
    private int m_AddNumberID;
    private string m_Title;
    private string m_Content;
    private string m_IsAudi;
    private Person m_Person;
    private Client m_Client;
    private Guide2 m_Guide2;    
    private Guide m_Guide;
    private int m_AddInt = 0;

	public AddNumber()
	{
        this.m_AddNumberID = 0;
        this.m_Title = "";
        this.m_Content = "";
        this.m_IsAudi = "";
        this.m_Person = new Person();
        this.m_Client = new Client();
        this.m_Guide2 = new Guide2();
        this.m_Guide = new Guide();
        this.m_AddInt = 0;
	}

    /// <summary>
    /// 编号
    /// </summary>
    public int AddNumberID
    {
        set
        {
            this.m_AddNumberID = value;
        }
        get
        {
            return this.m_AddNumberID;
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
    /// 是否通过
    /// </summary>
    public string IsAudi
    {
        set
        {
            this.m_IsAudi = value;
        }
        get
        {
            return this.m_IsAudi;
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
    /// 小类
    /// </summary>
    public Guide2 Guide2
    {
        set
        {
            this.m_Guide2 = value;
        }
        get
        {
            return this.m_Guide2;
        }
    }

    /// <summary>
    /// 大类
    /// </summary>
    public Guide Guide
    {
        set
        {
            this.m_Guide = value;
        }
        get
        {
            return this.m_Guide;
        }
    }

    /// <summary>
    /// 加的分数
    /// </summary>
    public int AddInt
    {
        set
        {
            this.m_AddInt = value;
        }
        get
        {
            return this.m_AddInt;
        }
    }
}