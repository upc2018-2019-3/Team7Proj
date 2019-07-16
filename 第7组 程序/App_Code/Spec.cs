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
/// 专业类
/// </summary>
public class Spec
{
    private int m_SpecID;
    private string m_SpecName;

	public Spec()
	{
        this.m_SpecID = 0;
        this.m_SpecName = "";
	}

    /// <summary>
    /// 编号
    /// </summary>
    public int SpecID
    {
        set
        {
            this.m_SpecID = value;
        }
        get
        {
            return this.m_SpecID;
        }
    }

    /// <summary>
    /// 类型名称
    /// </summary>
    public string SpecName
    {
        set
        {
            this.m_SpecName = value;
        }
        get
        {
            return this.m_SpecName;
        }
    }
}