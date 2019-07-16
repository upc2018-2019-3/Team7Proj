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

public partial class Client_Edit : WebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ValidateUser("管理员");
        if (!this.IsPostBack)
        {
            this.txtClientID.Text = Request.QueryString["ClientID"] + "";
            this.initList();
            this.initForm();
        }
    }

    #region 事件产生的函数
    protected void btnOK_Click(object sender, EventArgs e)
    {
        ClientMgr sMgr = new ClientMgr();
        Client client = new Client();
        if (this.txtClientID.ReadOnly == false)
        {
            if (sMgr.ExistsClient(this.txtClientID.Text))
            {
                this.SendMessage("该登录名称已经存在");
                return;
            }
            client.Password = "12345";
        }
        else
        {
            client = sMgr.GetClient(this.txtClientID.Text);
        }

        client.ClientID = this.txtClientID.Text.Trim();
        client.ClientName = this.txtClientName.Text.Trim();
        client.Spec.SpecID = int.Parse(this.lstSpecID.SelectedValue);
        client.Sex = this.lstSex.SelectedValue;
        if (this.txtPassword.Text != "")
        {
            client.Password = this.txtPassword.Text;
        }
     
        

        sMgr.UpdateClient(client);
        this.SendMessage("信息编辑成功");
        if (!this.txtClientID.ReadOnly)
        {
            this.ClearTextData(this);
        }
    }
    #endregion

    #region 窗体内部函数
    private void initList()
    {
        SpecMgr sMgr = new SpecMgr();
        this.lstSpecID.DataSource = sMgr.GetSpecList();
        this.lstSpecID.DataTextField = "SpecName";
        this.lstSpecID.DataValueField = "SpecID";
        this.lstSpecID.DataBind();
    }

    /// <summary>
    /// 初始化窗体信息
    /// </summary>
    private void initForm()
    {
        if (this.txtClientID.Text != "")
        {
            this.txtClientID.ReadOnly = true;
            ClientMgr pMgr = new ClientMgr();
            Client client = pMgr.GetClient(this.txtClientID.Text);
            if (client.ClientID != "")
            {
                this.txtClientName.Text = client.ClientName;
                this.lstSex.ClearSelection();
                if (this.lstSex.Items.FindByValue(client.Sex) != null)
                {
                    this.lstSex.Items.FindByValue(client.Sex).Selected = true;
                }
                this.lstSpecID.ClearSelection();
                if (this.lstSpecID.Items.FindByValue(client.Spec.SpecID.ToString()) != null)
                {
                    this.lstSpecID.Items.FindByValue(client.Spec.SpecID.ToString()).Selected = true;
                }

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
