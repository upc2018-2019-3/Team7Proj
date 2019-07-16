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

public partial class AssesList_List : WebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ValidateUser("管理员,教师,学生");
        if (!this.IsPostBack)
        {
            this.hidGuideID.Value = Request.QueryString["GuideID"] + "";
            this.hidPersonID.Value = Request.QueryString["PersonID"] + "";
            this.initForm();
        }
    }

    #region 窗体内部函数

    /// <summary>
    /// 初始化人员列表信息
    /// </summary>
    private void initForm()
    {
        AssesMgr sMgr = new AssesMgr();
        DataTable dtNumber = sMgr.GetAssesNumberList(this.hidPersonID.Value
                                               ,int.Parse(this.hidGuideID.Value));
        this.dgList.DataSource = dtNumber;
        this.lblNumber.Text = "共" + dtNumber.Rows.Count.ToString() + "条记录";
        this.dgList.DataBind();
    }
    #endregion

    #region 事件产生的函数
    protected void dgList_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        AssesMgr sMgr = new AssesMgr();
        sMgr.DelAsses(e.Item.Cells[0].Text);
        this.initForm();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("Asses_List.aspx");
    }
    #endregion

    protected void dgList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        this.dgList.CurrentPageIndex = e.NewPageIndex;
        this.initForm();
    }
    protected void dgList_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            //AssesMgr mgr = new AssesMgr();
            //AddNumberMgr aMgr = new AddNumberMgr();
            //Asses asses = mgr.GetAsses(e.Item.Cells[0].Text);

            //DataTable dt = aMgr.GetAddNumberList(asses.Person.PersonID
            //                                     , ""
            //                                     , asses.Guide.GuideID.ToString()
            //                                     , asses.Guide2.Guide2ID.ToString());
            //foreach (DataRow row in dt.Rows)
            //{
            //    if (row["IsAudi"].ToString() == "是")
            //    {
            //        e.Item.Cells[8].Text = (int.Parse(e.Item.Cells[8].Text) + int.Parse(row["AddInt"].ToString())).ToString();
            //    }
            //}

        }
    }
    protected void dgList_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "btnDel":
                CMMgr.ExecuteNonQuery("DELETE FROM Sys_Asses WHERE AssesID = " + e.CommandArgument.ToString());
                this.initForm();
                break;
        }
    }
}
