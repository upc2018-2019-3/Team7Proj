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

public partial class Guide_Edit : WebPage
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
        GuideMgr mgr = new GuideMgr();
        this.dgList.DataSource = mgr.GetGuideList();
        this.dgList.DataBind();
    }
    #endregion

    #region 事件产生的函数
    protected void dgList_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        GuideMgr mgr = new GuideMgr();
        mgr.DelGuide(e.Item.Cells[0].Text);
        this.initForm();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.initForm();
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        int intScale = 0;
        if (this.txtGuideName.Text.Trim() == "")
        {
            this.SendMessage("指标名称不能为空");
        }
        else if (this.txtGuideCode.Text.Trim() == "")
        {
            this.SendMessage("指标编码不能为空");
        }
       
        else
        {
            GuideMgr mgr = new GuideMgr();
            Guide guide = new Guide();
            guide.GuideName = this.txtGuideName.Text.Trim();
            guide.GuideCode = this.txtGuideCode.Text.Trim();
           
            this.txtGuideName.Text = "";
            this.txtGuideCode.Text = "";
          
            mgr.UpdateGuide(guide);
            this.initForm();
        }
    }
    #endregion

    protected void dgList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        this.dgList.CurrentPageIndex = e.NewPageIndex;
        this.initForm();
    }

    protected void dgList_EditCommand(object source, DataGridCommandEventArgs e)
    {
        this.dgList.EditItemIndex = e.Item.ItemIndex;
        this.initForm();
    }
    protected void dgList_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        this.dgList.EditItemIndex = -1;
        this.initForm();
    }

    protected void dgList_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        TextBox txtGuideName = (TextBox)e.Item.Cells[2].Controls[0];
     
        int intScale = 0;

        if (txtGuideName.Text.Trim() == "")
        {
            this.SendMessage("请填写更改后的名称");
        }
     
        else
        {
            GuideMgr mgr = new GuideMgr();
            Guide guide = mgr.GetGuide(e.Item.Cells[0].Text);
            guide.GuideName = txtGuideName.Text.Trim();
         
            guide.Scale = intScale;
            mgr.UpdateGuide(guide);
            this.dgList.EditItemIndex = -1;
            this.initForm();
        }
    }
}
