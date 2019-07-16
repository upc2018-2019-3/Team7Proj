using System;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Specialized;

/// <summary>
/// 继承System.Web.UI.Page类，对该类进行改写，重新定义一些方法
/// </summary>
public partial class WebPage : System.Web.UI.Page
{
	public WebPage()
	{
        this.Load += new EventHandler(WebPage_Load);
	}

    void WebPage_Load(object sender, EventArgs e)
    {
       
    }

	#region 页面方法

	#region 函数 WriteJavaScript | 往页面写一段Javascript脚本
	/// <summary>
	/// 写入JAVASCRIPT
	/// </summary>
	/// <param name="key">要写入的名称</param>
	/// <param name="content">内容</param>
	public void WriteJavaScript(string key,string content)
	{
		string str = "";
		str = "<Script language=Javascript>\n";
		str+= "<!--\n";
		str+= content + "\n";
		str+= "//-->\n";
		str+= "</Script>\n";
        /**********************************
         * 该方法在2.0中被否决
         * this.RegisterClientScriptBlock(key, str);
         * *******************************/

        ClientScriptManager csm = this.ClientScript;
        csm.RegisterClientScriptBlock(this.GetType(), key, str);
        

        
	}
	#endregion

	#region 函数 SendMessage | 发送一条提示信息
	/// <summary>
	/// 发送一条提示信息
	/// </summary>
	public void SendMessage(string strMessage)
	{
		strMessage = "alert('" + strMessage.Replace("'","\'") + "')\n";
		this.WriteJavaScript("alter",strMessage);
	}
	#endregion

	#region 函数 ClearTextData | 清空所有文本框，列表框，复选框内容
	/// <summary>
	/// 清空所有文本框，列表框，复选框内容
	/// </summary>
	public void ClearTextData(Control control)
	{
		foreach(System.Web.UI.Control c in control.Controls)
		{
			if(c is System.Web.UI.WebControls.TextBox)
			{
				((System.Web.UI.WebControls.TextBox)c).Text = "";
				continue;
			}
			if(c is System.Web.UI.WebControls.ListControl)
			{
				((System.Web.UI.WebControls.ListControl)c).SelectedIndex = -1;
				continue;
			}
			if(c is System.Web.UI.WebControls.CheckBox)
			{
				((System.Web.UI.WebControls.CheckBox)c).Checked = false;
				continue;
			}
			if(c.HasControls())
			{
				ClearTextData(c);
			}
		}
	}
	#endregion

	#region 函数 GetRand | 返回一个随机数字
	/// <summary>
	/// 返回一个随机数字
	/// </summary>
	/// <param name="intMinNumber">要生成的随机数字下限</param>
	/// <param name="intMaxNumber">要生成的随机数字上限</param>
	/// <returns>返回的随机数字字符串</returns>
	public string GetRand(long intMinNumber,long intMaxNumber)
	{
		Random r = new Random();
		return r.Next((int)intMinNumber,(int)intMaxNumber).ToString();
	}
	#endregion

	#region 函数 CheckParams | 检测是否包含=，'等SQL注入语句
	/// <summary>
	/// 检测是否包含=，'等SQL注入语句
	/// </summary>
	/// <param name="args">参数</param>
	/// <returns>包含返回false不包含返回true</returns>
	public bool CheckParams(params object[] args)
	{
		//string[] Lawlesses={"=","'","net user","xp_cmdshell","exec master.dbo.xp_cmdshell","net localgroup administrators","select","insert","from","update","drop table",":"};
		string[] Lawlesses={"=","'","%",":"};
		if(Lawlesses==null||Lawlesses.Length<=0)return true;
		//构造正则表达式,例:Lawlesses是=号和'号,则正则表达式为 .*[=|'].*  (正则表达式相关内容请见MSDN)
		//另外,由于我是想做通用而且容易修改的函数,所以多了一步由字符数组到正则表达式,实际使用中,直接写正则表达式亦可;
		string str_Regex=".*[";
		for(int i=0;i< Lawlesses.Length-1;i++)
			str_Regex+=Lawlesses[i]+"|";
		str_Regex+=Lawlesses[Lawlesses.Length-1]+"].*";
		
		foreach(object arg in args)
		{
			if(arg is string)//如果是字符串,直接检查
			{
				if(Regex.Matches(arg.ToString(),str_Regex).Count>0)
					return false;
			}
			else if(arg is ICollection)//如果是一个集合,则检查集合内元素是否字符串,是字符串,就进行检查
			{
				foreach(object obj in (ICollection)arg)
				{
					if(obj is string)
					{
						if(Regex.Matches(obj.ToString(),str_Regex).Count>0)
							return false;
					}
				}
			}
		}
		return true;
	}
	#endregion


	#endregion

	#region 页面属性
	/// <summary>
	/// 人员身份标记
	/// </summary>
	public string eUserID
	{
		set
		{
			Session["eUserID"] = value;
		}
		get
		{
			if (Session["eUserID"] != null)
			{
				return Session["eUserID"].ToString();
			}
			else
			{
				return "";
			}
		}
	}

	/// <summary>
	/// 人员真实姓名
	/// </summary>
	public string eUserName
	{
		set
		{
			Session["eUserName"] = value;
		}
		get
		{
			if (Session["eUserName"] != null)
			{
				return Session["eUserName"].ToString();
			}
			else
			{
				return "";
			}
		}
	}

    /// <summary>
    /// 人员类别
    /// </summary>
    public string eUserType
    {
        set
        {
            Session["eUserType"] = value;
        }
        get
        {
            if (Session["eUserType"] != null)
            {
                return Session["eUserType"].ToString();
            }
            else
            {
                return "";
            }
        }
    }
	#endregion

	#region 函数ValidateUser | 验证人员登陆
	/// <summary>
	/// 验证是否为登录人员
	/// </summary>
	/// <returns>是则返回true 否则返回false</returns>
	public bool ValidateUser()
	{
		if (Session["eUserID"] != null && Session["eUserID"].ToString() != "")
		{
			return true;
		}
		else
		{
            string strContent = "alert('人员身份标记丢失，请重新登录');window.open('default.aspx','_top','');";
            this.WriteJavaScript("vali", strContent);
			return false;
		}
	}

    public bool ValidateUser(string UserType)
    {
        if (UserType.IndexOf(this.eUserType) != -1)
        {
            return true;
        }
        else
        {
            string strContent = "alert('您没有使用的权限，请重新登录');window.open('default.aspx','_top','');";
            this.WriteJavaScript("vali", strContent);
            return false;
        }
    }
	#endregion

    public bool ValidateUserType(string UserType)
    {
        if (UserType.IndexOf(this.eUserType) != -1)
        {
            return true;
        }
        else
        {            
            return false;
        }
    }

    #region 函数UploadFile | 上传一个文件
    /// <summary>
	/// 上传文件
	/// </summary>
	/// <param name="file">要上传文件的控件</param>
	/// <param name="strFolder">要上传的目录</param>
	/// <returns>返回文件名称</returns>
	public string UploadFile(System.Web.UI.HtmlControls.HtmlInputFile file, string strFolder)
	{
		if(file.PostedFile.ContentLength != 0)
		{
			string strFileName = file.Value;
			strFileName = strFileName.Substring(strFileName.LastIndexOf(@"\") + 1,strFileName.Length - strFileName.LastIndexOf(@"\") - 1);
			if(strFileName.LastIndexOf(".") != -1)
			{
				strFileName = strFileName.Substring(strFileName.LastIndexOf(@"."),strFileName.Length - strFileName.LastIndexOf(@"."));
				strFileName = GetRand(10000000,99999999) + strFileName;
				while(System.IO.File.Exists(this.Request.PhysicalApplicationPath + strFolder + strFileName))
				{
					strFileName = GetRand(10000000,99999999) + strFileName;
				}
			}
			else
			{
				strFileName = GetRand(10000000,99999999);
				while(System.IO.File.Exists(this.Request.PhysicalApplicationPath + strFolder + strFileName))
				{
					strFileName = GetRand(10000000,99999999);
				}
			}
			file.PostedFile.SaveAs(this.Request.PhysicalApplicationPath + strFolder + strFileName);
			return strFileName;
		}
		else
		{
			return "";
		}
	}
	#endregion
}
