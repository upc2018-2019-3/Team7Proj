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
/// 班级类
/// </summary>
public class ClassInfo
{
    private int m_ClassInfoID;
    private string m_ClassInfoName;

	public ClassInfo()
	{
        this.m_ClassInfoID = 0;
        this.m_ClassInfoName = "";
	}

    /// <summary>
    /// 编号
    /// </summary>
    public int ClassInfoID
    {
        set
        {
            this.m_ClassInfoID = value;
        }
        get
        {
            return this.m_ClassInfoID;
        }
    }

    /// <summary>
    /// 类型名称
    /// </summary>
    public string ClassInfoName
    {
        set
        {
            this.m_ClassInfoName = value;
        }
        get
        {
            return this.m_ClassInfoName;
        }
    }
}