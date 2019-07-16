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
using System.Data.Odbc;
using System.Data.SqlClient;

public partial class Client_Rewrite_List : WebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ValidateUser("教师");
        if (!this.IsPostBack)
        {
            this.initForm();
        }
    }

    #region 窗体内部函数
    /// <summary>
    /// 初始化人员列表信息
    /// </summary>
    private void initForm()
    {
        RewriteMgr mgr = new RewriteMgr();
        DataTable dtNumber = mgr.GetRewriteList("",this.eUserID);
        this.dgList.DataSource = dtNumber;
        this.lblNumber.Text = "共" + dtNumber.Rows.Count.ToString() + "条记录";
        this.dgList.DataBind();
    }
    #endregion

    #region 事件产生的函数
    protected void dgList_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        RewriteMgr sMgr = new RewriteMgr();
        sMgr.DelRewrite(e.Item.Cells[0].Text);
        this.initForm();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.initForm();
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("Person_Rewrite_Edit.aspx");
    }
    #endregion

    protected void dgList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        this.dgList.CurrentPageIndex = e.NewPageIndex;
        this.initForm();
    }
}
