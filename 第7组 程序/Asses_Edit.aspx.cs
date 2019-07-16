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

public partial class Asses_Edit : WebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ValidateUser("管理员,教师");
        if (!this.IsPostBack)
        {
            this.hidAssesID.Value = Request.QueryString["AssesID"] + "";
            this.initPerson();
            this.initGuide();
            this.initGuide2();
            this.initForm();
        }
    }

    #region 事件产生的函数
    protected void btnOK_Click(object sender, EventArgs e)
    {
        AssesMgr sMgr = new AssesMgr();
        Asses asses = new Asses();
        GuideMgr gMgr = new GuideMgr();

        if (this.hidAssesID.Value != "")
        {
            asses = sMgr.GetAsses(this.hidAssesID.Value);
        }

        asses.Person.PersonID = this.lstPersonID.SelectedValue;
        asses.Guide.GuideID = int.Parse(this.lstGuideID.SelectedValue);
        asses.Guide2.Guide2ID = int.Parse(this.lstGuide2ID.SelectedValue);
        asses.Scale1 = int.Parse(this.txtScale1.Text.Trim());
        asses.Scale1Name = this.txtScale1Name.Text.Trim();

        asses.Memo = this.txtMemo.Text;
        sMgr.UpdateAsses(asses);
        this.SendMessage("信息编辑成功");
        if (this.hidAssesID.Value == "")
        {
            this.ClearTextData(this);
        }
    }
    #endregion

    #region 窗体内部函数
    private void initPerson()
    {
        PersonMgr mgr = new PersonMgr();
        this.lstPersonID.DataSource = mgr.GetPersonList("", "");
        this.lstPersonID.DataTextField = "PersonName";
        this.lstPersonID.DataValueField = "PersonID";
        this.lstPersonID.DataBind();
    }

    private void initGuide()
    {
        GuideMgr mgr = new GuideMgr();
        this.lstGuideID.DataSource = mgr.GetGuideList();
        this.lstGuideID.DataTextField = "GuideName";
        this.lstGuideID.DataValueField = "GuideID";
        this.lstGuideID.DataBind();
    }

    private void initGuide2()
    {
        Guide2Mgr mgr = new Guide2Mgr();
        this.lstGuide2ID.DataSource = mgr.GetGuide2List(this.lstGuideID.SelectedValue);
        this.lstGuide2ID.DataTextField = "Guide2Name";
        this.lstGuide2ID.DataValueField = "Guide2ID";
        this.lstGuide2ID.DataBind();
    }

    /// <summary>
    /// 初始化窗体信息
    /// </summary>
    private void initForm()
    {
        if (this.hidAssesID.Value != "")
        {
            this.lstPersonID.Enabled = false;
            AssesMgr aMgr = new AssesMgr();
            Asses asses = aMgr.GetAsses(this.hidAssesID.Value);
            if (asses.AssesID != 0)
            {
                this.lstPersonID.ClearSelection();
                if (this.lstPersonID.Items.FindByValue(asses.Person.PersonID) != null)
                {
                    this.lstPersonID.Items.FindByValue(asses.Person.PersonID).Selected = true;
                }
                this.lstGuideID.ClearSelection();
                if (this.lstGuideID.Items.FindByValue(asses.Guide.GuideID.ToString()) != null)
                {
                    this.lstGuideID.Items.FindByValue(asses.Guide.GuideID.ToString()).Selected = true;
                }
                this.initGuide2();
                this.lstGuide2ID.ClearSelection();
                if (this.lstGuide2ID.Items.FindByValue(asses.Guide2.Guide2ID.ToString()) != null)
                {
                    this.lstGuide2ID.Items.FindByValue(asses.Guide2.Guide2ID.ToString()).Selected = true;
                }
                this.txtScale1.Text = asses.Scale1.ToString();
                this.txtScale1Name.Text = asses.Scale1Name;
                this.txtMemo.Text = asses.Memo;
            }
            else
            {
                this.btnOK.Enabled = false;
                this.SendMessage("没有找到该信息");
            }
        }
    }
    #endregion

    protected void lstGuideID_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.initGuide2();
    }
}
