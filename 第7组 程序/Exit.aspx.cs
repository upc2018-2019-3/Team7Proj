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

public partial class Exit : WebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.eUserID = "";
        this.eUserName = "";
        this.eUserType = "";
        this.WriteJavaScript("key", "window.open('Default.aspx','_top')");
    }
}
