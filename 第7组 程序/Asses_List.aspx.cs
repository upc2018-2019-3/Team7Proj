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

public partial class Asses_List : WebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ValidateUser("管理员,教师");
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
        PersonMgr mgr = new PersonMgr();
        DataTable dtNumber = mgr.GetPersonList(this.txtAssesID.Text, this.txtAssesName.Text);
        this.dgList.DataSource = dtNumber;
        this.lblNumber.Text = "共" + dtNumber.Rows.Count.ToString() + "条记录";
        this.dgList.DataBind();
    }
    #endregion

    #region 事件产生的函数
    protected void dgList_DeleteCommand(object source, DataGridCommandEventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.initForm();
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("Asses_Edit.aspx");
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
            HyperLink hyGuide1 = (HyperLink)e.Item.FindControl("hyGuide1");
            HyperLink hyGuide2 = (HyperLink)e.Item.FindControl("hyGuide2");
            HyperLink hyGuide3 = (HyperLink)e.Item.FindControl("hyGuide3");
            GuideMgr gMgr = new GuideMgr();
            Guide guide1 = gMgr.GetGuide("1");
            Guide guide2 = gMgr.GetGuide("2");
            Guide guide3 = gMgr.GetGuide("3");

            hyGuide1.Text = "0";
            hyGuide2.Text = "0";
            hyGuide3.Text = "0";
            PersonMgr mgr = new PersonMgr();
            Person person = mgr.GetPerson(e.Item.Cells[0].Text);
            AssesMgr aMgr = new AssesMgr();
            DataTable dt = aMgr.GetAssesNumberList(person.PersonID, 1);
            foreach (DataRow row in dt.Rows)
            {
                hyGuide1.Text = (int.Parse(hyGuide1.Text) + int.Parse(row["Scale1"].ToString())).ToString();
            }
            if (hyGuide1.Text != "0")
            {
                if (int.Parse(hyGuide1.Text) > 100)
                {
                    hyGuide1.Text = "100";
                }
                hyGuide1.Text = (int.Parse(hyGuide1.Text) * guide1.Scale / 100).ToString();
            }

            dt = aMgr.GetAssesNumberList(person.PersonID, 2);
            foreach (DataRow row in dt.Rows)
            {
                hyGuide2.Text = (int.Parse(hyGuide2.Text) + int.Parse(row["Scale1"].ToString())).ToString();
            }
            if (hyGuide2.Text != "0")
            {
                if (int.Parse(hyGuide2.Text) > 100)
                {
                    hyGuide2.Text = "100";
                }
                hyGuide2.Text = (int.Parse(hyGuide2.Text) * guide2.Scale / 100).ToString();
            }

            dt = aMgr.GetAssesNumberList(person.PersonID, 3);
            foreach (DataRow row in dt.Rows)
            {
                hyGuide3.Text = (int.Parse(hyGuide3.Text) + int.Parse(row["Scale1"].ToString())).ToString();
            }
            if (hyGuide3.Text != "0")
            {
                if (int.Parse(hyGuide3.Text) > 100)
                {
                    hyGuide3.Text = "100";
                }
                hyGuide3.Text = (int.Parse(hyGuide3.Text) * guide3.Scale / 100).ToString();
            }

            e.Item.Cells[e.Item.Cells.Count - 1].Text = (int.Parse(hyGuide1.Text) + int.Parse(hyGuide2.Text) + int.Parse(hyGuide3.Text)).ToString();
        }
    }
}
