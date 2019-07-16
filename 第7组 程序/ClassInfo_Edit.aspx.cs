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

public partial class ClassInfo_Edit : WebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ValidateUser("管理员");
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
        ClassInfoMgr mgr = new ClassInfoMgr();
        this.dgList.DataSource = mgr.GetClassInfoList();
        this.dgList.DataBind();
    }
    #endregion

    #region 事件产生的函数
    protected void dgList_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        ClassInfoMgr mgr = new ClassInfoMgr();
        mgr.DelClassInfo(e.Item.Cells[0].Text);
        this.initForm();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.initForm();
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        if (this.txtClassInfoName.Text.Trim() != "")
        {
            ClassInfoMgr mgr = new ClassInfoMgr();
            ClassInfo classInfo = new ClassInfo();
            classInfo.ClassInfoName = this.txtClassInfoName.Text.Trim();
            this.txtClassInfoName.Text = "";
            mgr.UpdateClassInfo(classInfo);
            this.initForm();
        }
        else
        {
            this.SendMessage("学生班级名称不能为空");
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
        TextBox txt = (TextBox)e.Item.Cells[1].Controls[0];
        if (txt.Text.Trim() == "")
        {
            this.SendMessage("请填写更改后的名称");
        }
        else
        {
            ClassInfoMgr mgr = new ClassInfoMgr();
            ClassInfo classInfo = new ClassInfo();
            classInfo.ClassInfoID = int.Parse(e.Item.Cells[0].Text);
            classInfo.ClassInfoName = txt.Text.Trim();
            mgr.UpdateClassInfo(classInfo);
            this.dgList.EditItemIndex = -1;
            this.initForm();
        }
    }
}
