using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : WebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region 事件产生的函数
    protected void btnLogin_Click(object sender, ImageClickEventArgs e)
    {
        switch (this.lstLoginType.SelectedValue)
        {
            case "管理员":
                AdminMgr aMgr = new AdminMgr();
                Admin admin = aMgr.GetAdmin(this.txtLoginName.Text);
                if (admin.AdminID != "")
                {
                    if (this.txtPassword.Text == admin.Password)
                    {
                        this.eUserID = admin.AdminID;
                        this.eUserName = admin.AdminName;
                        this.eUserType = "管理员";
                        Response.Redirect("MainFrame.aspx");
                    }
                    else
                    {
                        this.SendMessage("密码不正确");
                    }
                }
                else
                {
                    this.SendMessage("没有找到该教师");
                }
                break;
            case "教师":
                ClientMgr pMgr = new ClientMgr();
                Client client = pMgr.GetClient(this.txtLoginName.Text);
                if (client.ClientID != "")
                {
                    if (this.txtPassword.Text == client.Password)
                    {
                        this.eUserID = client.ClientID;
                        this.eUserName = client.ClientName;
                        this.eUserType = "教师";
                        Response.Redirect("MainFrame.aspx");
                    }
                    else
                    {
                        this.SendMessage("密码不正确");
                    }
                }
                else
                {
                    this.SendMessage("没有找到该教师");
                }
                break;
            case "学生":
                PersonMgr cMgr = new PersonMgr();
                Person person = cMgr.GetPerson(this.txtLoginName.Text);
                if (person.PersonID != "")
                {
                    if (this.txtPassword.Text == person.Password)
                    {
                        this.eUserID = person.PersonID;
                        this.eUserName = person.PersonName;
                        this.eUserType = "学生";
                        Response.Redirect("MainFrame.aspx");
                    }
                    else
                    {
                        this.SendMessage("密码不正确");
                    }
                }
                else
                {
                    this.SendMessage("没有找到该学生");
                }
                break;
        }
    }
    #endregion
}
