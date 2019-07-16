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

public partial class Client_AddNumber_Edit : WebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ValidateUser("教师");
        if (!this.IsPostBack)
        {
            this.hidAddNumberID.Value = Request.QueryString["AddNumberID"] + "";
            this.initGuide();
            this.initForm();
        }
    }

    #region 事件产生的函数
    protected void btnOK_Click(object sender, EventArgs e)
    {
        AddNumberMgr sMgr = new AddNumberMgr();
        AddNumber addNumber = sMgr.GetAddNumber(this.hidAddNumberID.Value);
        int AddInt = 0;

        if (this.chkIsAudi.Checked)
        {
            if (this.txtAddInt.Text.Trim() == "")
            {
                this.SendMessage("通过审核必须填写增加的分数");
            }
            else if (!int.TryParse(this.txtAddInt.Text.Trim(), out AddInt))
            {
                this.SendMessage("增加的分数必须为一个整数");
            }
            else
            {
                addNumber.IsAudi = "是";
                addNumber.AddInt = AddInt;
                sMgr.UpdateAddNumber(addNumber);

                AssesMgr mgr = new AssesMgr();
                Asses asses = new Asses();
                asses.Guide.GuideID = addNumber.Guide.GuideID;
                switch (asses.Guide.GuideID)
                {
                    case 1:
                        asses.Guide2.Guide2ID = 4;
                        break;
                    case 2:
                        asses.Guide2.Guide2ID = 9;
                        break;
                    case 3:
                        asses.Guide2.Guide2ID = 14;
                        break;
                }
                asses.Memo = "";
                asses.Person.PersonID = addNumber.Person.PersonID;
                asses.Scale1 = addNumber.AddInt;
                mgr.UpdateAsses(asses);
                this.btnOK.Enabled = false;
            }
        }
        else
        {
            addNumber.IsAudi = "否";
            addNumber.AddInt = 0;
            sMgr.UpdateAddNumber(addNumber);
        }
                
        this.SendMessage("信息编辑成功");
    }
    #endregion

    #region 窗体内部函数

    private void initGuide()
    {
        GuideMgr mgr = new GuideMgr();
        this.lstGuideID.DataSource = mgr.GetGuideList();
        this.lstGuideID.DataTextField = "GuideName";
        this.lstGuideID.DataValueField = "GuideID";
        this.lstGuideID.DataBind();
    }

    /// <summary>
    /// 初始化窗体信息
    /// </summary>
    private void initForm()
    {
        if (this.hidAddNumberID.Value != "")
        {
            this.lstGuideID.Enabled = false;

            AddNumberMgr aMgr = new AddNumberMgr();
            AddNumber addNumber = aMgr.GetAddNumber(this.hidAddNumberID.Value);
            if (addNumber.AddNumberID != 0)
            {
                this.lblPersonName.Text = addNumber.Person.PersonName;
                this.lstGuideID.ClearSelection();
                if (this.lstGuideID.Items.FindByValue(addNumber.Guide.GuideID.ToString()) != null)
                {
                    this.lstGuideID.Items.FindByValue(addNumber.Guide.GuideID.ToString()).Selected = true;
                }
                this.txtTitle.Text = addNumber.Title;
                this.txtContent.Text = addNumber.Content;

                if (addNumber.IsAudi == "是")
                {
                    this.chkIsAudi.Checked = true;
                    this.txtAddInt.Text = addNumber.AddInt.ToString();
                    this.btnOK.Enabled = false;
                }

            }
            else
            {
                this.btnOK.Enabled = false;
                this.SendMessage("没有找到该信息");
            }
        }
    }
    #endregion

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Client_AddNumber_List.aspx");
    }
}
