using System;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Specialized;

/// <summary>
/// �̳�System.Web.UI.Page�࣬�Ը�����и�д�����¶���һЩ����
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

	#region ҳ�淽��

	#region ���� WriteJavaScript | ��ҳ��дһ��Javascript�ű�
	/// <summary>
	/// д��JAVASCRIPT
	/// </summary>
	/// <param name="key">Ҫд�������</param>
	/// <param name="content">����</param>
	public void WriteJavaScript(string key,string content)
	{
		string str = "";
		str = "<Script language=Javascript>\n";
		str+= "<!--\n";
		str+= content + "\n";
		str+= "//-->\n";
		str+= "</Script>\n";
        /**********************************
         * �÷�����2.0�б����
         * this.RegisterClientScriptBlock(key, str);
         * *******************************/

        ClientScriptManager csm = this.ClientScript;
        csm.RegisterClientScriptBlock(this.GetType(), key, str);
        

        
	}
	#endregion

	#region ���� SendMessage | ����һ����ʾ��Ϣ
	/// <summary>
	/// ����һ����ʾ��Ϣ
	/// </summary>
	public void SendMessage(string strMessage)
	{
		strMessage = "alert('" + strMessage.Replace("'","\'") + "')\n";
		this.WriteJavaScript("alter",strMessage);
	}
	#endregion

	#region ���� ClearTextData | ��������ı����б�򣬸�ѡ������
	/// <summary>
	/// ��������ı����б�򣬸�ѡ������
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

	#region ���� GetRand | ����һ���������
	/// <summary>
	/// ����һ���������
	/// </summary>
	/// <param name="intMinNumber">Ҫ���ɵ������������</param>
	/// <param name="intMaxNumber">Ҫ���ɵ������������</param>
	/// <returns>���ص���������ַ���</returns>
	public string GetRand(long intMinNumber,long intMaxNumber)
	{
		Random r = new Random();
		return r.Next((int)intMinNumber,(int)intMaxNumber).ToString();
	}
	#endregion

	#region ���� CheckParams | ����Ƿ����=��'��SQLע�����
	/// <summary>
	/// ����Ƿ����=��'��SQLע�����
	/// </summary>
	/// <param name="args">����</param>
	/// <returns>��������false����������true</returns>
	public bool CheckParams(params object[] args)
	{
		//string[] Lawlesses={"=","'","net user","xp_cmdshell","exec master.dbo.xp_cmdshell","net localgroup administrators","select","insert","from","update","drop table",":"};
		string[] Lawlesses={"=","'","%",":"};
		if(Lawlesses==null||Lawlesses.Length<=0)return true;
		//����������ʽ,��:Lawlesses��=�ź�'��,��������ʽΪ .*[=|'].*  (������ʽ����������MSDN)
		//����,������������ͨ�ö��������޸ĵĺ���,���Զ���һ�����ַ����鵽������ʽ,ʵ��ʹ����,ֱ��д������ʽ���;
		string str_Regex=".*[";
		for(int i=0;i< Lawlesses.Length-1;i++)
			str_Regex+=Lawlesses[i]+"|";
		str_Regex+=Lawlesses[Lawlesses.Length-1]+"].*";
		
		foreach(object arg in args)
		{
			if(arg is string)//������ַ���,ֱ�Ӽ��
			{
				if(Regex.Matches(arg.ToString(),str_Regex).Count>0)
					return false;
			}
			else if(arg is ICollection)//�����һ������,���鼯����Ԫ���Ƿ��ַ���,���ַ���,�ͽ��м��
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

	#region ҳ������
	/// <summary>
	/// ��Ա��ݱ��
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
	/// ��Ա��ʵ����
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
    /// ��Ա���
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

	#region ����ValidateUser | ��֤��Ա��½
	/// <summary>
	/// ��֤�Ƿ�Ϊ��¼��Ա
	/// </summary>
	/// <returns>���򷵻�true ���򷵻�false</returns>
	public bool ValidateUser()
	{
		if (Session["eUserID"] != null && Session["eUserID"].ToString() != "")
		{
			return true;
		}
		else
		{
            string strContent = "alert('��Ա��ݱ�Ƕ�ʧ�������µ�¼');window.open('default.aspx','_top','');";
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
            string strContent = "alert('��û��ʹ�õ�Ȩ�ޣ������µ�¼');window.open('default.aspx','_top','');";
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

    #region ����UploadFile | �ϴ�һ���ļ�
    /// <summary>
	/// �ϴ��ļ�
	/// </summary>
	/// <param name="file">Ҫ�ϴ��ļ��Ŀؼ�</param>
	/// <param name="strFolder">Ҫ�ϴ���Ŀ¼</param>
	/// <returns>�����ļ�����</returns>
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
