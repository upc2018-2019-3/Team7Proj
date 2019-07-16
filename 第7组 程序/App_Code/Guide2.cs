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
/// 二级指标类
/// </summary>
public class Guide2
{
    private int m_Guide2ID;
    private Guide m_Guide;
    private string m_Guide2Name;

	public Guide2()
	{
        this.m_Guide2ID = 0;
        this.m_Guide = new Guide();
        this.m_Guide2Name = "";
	}

    /// <summary>
    /// 编号
    /// </summary>
    public int Guide2ID
    {
        set
        {
            this.m_Guide2ID = value;
        }
        get
        {
            return this.m_Guide2ID;
        }
    }

    /// <summary>
    /// 上级
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
    /// 类型名称
    /// </summary>
    public string Guide2Name
    {
        set
        {
            this.m_Guide2Name = value;
        }
        get
        {
            return this.m_Guide2Name;
        }
    }

}