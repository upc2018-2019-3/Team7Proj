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
/// 一级指标类
/// </summary>
public class Guide
{
    private int m_GuideID;
    private string m_GuideCode;
    private string m_GuideName;
    private int m_Scale;
    private int m_Scale1;
    private int m_Scale2;
    private int m_Scale3;

	public Guide()
	{
        this.m_GuideID = 0;
        this.m_GuideCode = "";
        this.m_GuideName = "";
        this.m_Scale = 0;
        this.m_Scale1 = 60;
        this.m_Scale2 = 30;
        this.m_Scale3 = 10;
	}

    /// <summary>
    /// 编号
    /// </summary>
    public int GuideID
    {
        set
        {
            this.m_GuideID = value;
        }
        get
        {
            return this.m_GuideID;
        }
    }

    /// <summary>
    /// 类型编号
    /// </summary>
    public string GuideCode
    {
        set
        {
            this.m_GuideCode = value;
        }
        get
        {
            return this.m_GuideCode;
        }
    }

    /// <summary>
    /// 类型名称
    /// </summary>
    public string GuideName
    {
        set
        {
            this.m_GuideName = value;
        }
        get
        {
            return this.m_GuideName;
        }
    }

    /// <summary>
    /// 所占比例
    /// </summary>
    public int Scale
    {
        set
        {
            this.m_Scale = value;
        }
        get
        {
            return this.m_Scale;
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
}