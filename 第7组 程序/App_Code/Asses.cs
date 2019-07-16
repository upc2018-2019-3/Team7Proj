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
/// 评定指标类
/// </summary>
public class Asses
{
    private int m_AssesID;
    private Person m_Person;
    private Guide m_Guide;
    private Guide2 m_Guide2;
    private int m_Scale1;
    private int m_Scale2;
    private int m_Scale3;
    private string m_Scale1Name;
    private string m_Scale2Name;
    private string m_Scale3Name;
    private int m_CountNumber;
    private string m_Memo;

	public Asses()
	{
        this.m_AssesID = 0;
        this.m_Person = new Person();
        this.m_Guide = new Guide();
        this.m_Guide2 = new Guide2();
        this.m_Scale1 = 0;
        this.m_Scale2 = 0;
        this.m_Scale3 = 0;
        this.m_Scale1Name = "";
        this.m_Scale2Name = "";
        this.m_Scale3Name = "";
        this.m_CountNumber = 0;
        this.m_Memo = "";
	}

    /// <summary>
    /// 编号
    /// </summary>
    public int AssesID
    {
        set
        {
            this.m_AssesID = value;
        }
        get
        {
            return this.m_AssesID;
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
    /// 一级指标
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
    /// 二级指标
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
    /// 智
    /// </summary>
    public int Scale1
    {
        set
        {
            this.m_Scale1 = value;
        }
        get
        {
            return this.m_Scale1;
        }
    }

    /// <summary>
    /// 德
    /// </summary>
    public int Scale2
    {
        set
        {
            this.m_Scale2 = value;
        }
        get
        {
            return this.m_Scale2;
        }
    }

    /// <summary>
    /// 体
    /// </summary>
    public int Scale3
    {
        set
        {
            this.m_Scale3 = value;
        }
        get
        {
            return this.m_Scale3;
        }
    }

    /// <summary>
    /// 说明
    /// </summary>
    public string Scale1Name
    {
        set
        {
            this.m_Scale1Name = value;
        }
        get
        {
            return this.m_Scale1Name;
        }
    }

    /// <summary>
    /// 说明
    /// </summary>
    public string Scale2Name
    {
        set
        {
            this.m_Scale2Name = value;
        }
        get
        {
            return this.m_Scale2Name;
        }
    }

    /// <summary>
    /// 说明
    /// </summary>
    public string Scale3Name
    {
        set
        {
            this.m_Scale3Name = value;
        }
        get
        {
            return this.m_Scale3Name;
        }
    }

    /// <summary>
    /// 总分
    /// </summary>
    public int CountNumber
    {
        set
        {
            this.m_CountNumber = value;
        }
        get
        {
            return this.m_CountNumber;
        }
    }

    /// <summary>
    /// 分数说明
    /// </summary>
    public string Memo
    {
        set
        {
            this.m_Memo = value;
        }
        get
        {
            return this.m_Memo;
        }
    }
}