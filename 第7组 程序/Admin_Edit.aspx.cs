using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Admin_Edit : WebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ValidateUser("管理员");
        if (!this.IsPostBack)
        {
            this.txtAdminID.Text = Request.QueryString["AdminID"] + "";
            this.initForm();
        }
    }

    #region 事件产生的函数
    protected void btnOK_Click(object sender, EventArgs e)
    {
        AdminMgr sMgr = new AdminMgr();
        Admin admin = new Admin();
        if (this.txtAdminID.ReadOnly == false)
        {
            if (sMgr.ExistsAdmin(this.txtAdminID.Text))
            {
                this.SendMessage("该登录名称已经存在");
                return;
            }
            admin.Password = "12345";
        }
        else
        {
            admin = sMgr.GetAdmin(this.txtAdminID.Text);
        }

        admin.AdminID = this.txtAdminID.Text.Trim();
        admin.AdminName = this.txtAdminName.Text.Trim();
        if (this.txtPassword.Text != "")
        {
            admin.Password = this.txtPassword.Text;
        }
      
        

        sMgr.UpdateAdmin(admin);
        this.SendMessage("信息编辑成功");
        if (!this.txtAdminID.ReadOnly)
        {
            this.ClearTextData(this);
        }
    }
    #endregion

    #region 窗体内部函数
    /// <summary>
    /// 初始化窗体信息
    /// </summary>
    private void initForm()
    {
        if (this.txtAdminID.Text != "")
        {
            this.txtAdminID.ReadOnly = true;
            AdminMgr pMgr = new AdminMgr();
            Admin admin = pMgr.GetAdmin(this.txtAdminID.Text);
            if (admin.AdminID != "")
            {
                this.txtAdminName.Text = admin.AdminName;
              
            }
            else
            {
                this.btnOK.Enabled = false;
                this.SendMessage("没有找到该人员信息");
            }
        }
    }
    #endregion

}
