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
using System.IO;

public partial class Client_Rewrite_Edit : WebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ValidateUser("教师");
        if (!this.IsPostBack)
        {
            this.hidRewriteID.Value = Request.QueryString["RewriteID"] + "";
            this.initForm();
        }
    }

    #region 事件产生的函数
    protected void btnOK_Click(object sender, EventArgs e)
    {
        RewriteMgr sMgr = new RewriteMgr();
        Rewrite rewrite = new Rewrite();
        if (this.hidRewriteID.Value != "")
        {
            rewrite = sMgr.GetRewrite(this.hidRewriteID.Value);
        }

        rewrite.ReContent = this.txtReContent.Text;
        
        sMgr.UpdateRewrite(rewrite);
        this.SendMessage("信息编辑成功");
        if (this.hidRewriteID.Value == "")
        {
            this.ClearTextData(this);
        }
        else
        {
            this.initForm();
        }
    }
    #endregion

    #region 窗体内部函数
    /// <summary>
    /// 初始化窗体信息
    /// </summary>
    private void initForm()
    {
        if (this.hidRewriteID.Value != "")
        {
            RewriteMgr rMgr = new RewriteMgr();
            Rewrite rewrite = rMgr.GetRewrite(this.hidRewriteID.Value);
            if (rewrite.RewriteID != 0)
            {
                this.txtTitle.Text = rewrite.Title;
                this.txtContent.Text = rewrite.Content;
                this.txtReContent.Text = rewrite.ReContent;
            }
            else
            {
                this.btnOK.Enabled = false;
                this.SendMessage("没有找到该信息");
            }
        }
    }
    #endregion

}
