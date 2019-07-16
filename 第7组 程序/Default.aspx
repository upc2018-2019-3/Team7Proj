<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>奖学金评定系统</title>
	<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
	<link href="images/aiv367css.css" rel="stylesheet" type="text/css">
	<style type="text/css">
	    BODY { BACKGROUND-IMAGE: url(images/bg.png) }
	    .style1 { FONT-SIZE: 30px }
	</style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Layer1" style="BORDER-TOP-WIDTH:1px; BORDER-LEFT-WIDTH:1px; Z-INDEX:1; BORDER-LEFT-COLOR:#000000; LEFT:586px; BACKGROUND-IMAGE:url(images/login_3.gif); BORDER-BOTTOM-WIDTH:1px; BORDER-BOTTOM-COLOR:#000000; WIDTH:201px; BORDER-TOP-COLOR:#000000; BACKGROUND-REPEAT:no-repeat; POSITION:absolute; TOP:346px; HEIGHT:119px; BORDER-RIGHT-WIDTH:1px; BORDER-RIGHT-COLOR:#000000; layer-background-image:url(images/login_3.gif)">
			<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td style="PADDING-TOP:10px" align="center">
						<table width="80%" border="0" align="center" cellpadding="1" cellspacing="3" class="black_12">
							<tr>
								<td><div align="center">用户名:</div>
								</td>
								<td>
									<asp:TextBox Columns="15" CssClass="box-2" id="txtLoginName" runat="server" TextMode="SingleLine"></asp:TextBox></td>
							</tr>
							<tr>
								<td><div align="right">密 码:</div>
								</td>
								<td>
									<asp:TextBox Columns="15" CssClass="box-2" id="txtPassword" runat="server" TextMode="Password"></asp:TextBox></td>
							</tr>
							<tr valign="middle">
								<td height="20" colspan="2"><div align="center">&nbsp;<asp:DropDownList ID="lstLoginType" runat="server">
                                        <asp:ListItem Value="管理员">管理员</asp:ListItem>
                                        <asp:ListItem Value="教师">教师</asp:ListItem>
                                        <asp:ListItem Value="学生">学生</asp:ListItem>
                                    </asp:DropDownList>
										<asp:ImageButton id="btnLogin" runat="server" ImageUrl="images/btn_login.jpg" OnClick="btnLogin_Click"></asp:ImageButton></div>
								</td>
							</tr>
						</table>
                  </td>
				</tr>
			</table>
	  </div>
		<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="请输入教师名称" ControlToValidate="txtLoginName"
			Display="None"></asp:RequiredFieldValidator>
		<asp:ValidationSummary id="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>
		<div id="Layer2" class="style1" style="Z-INDEX:2; LEFT:40px; WIDTH:452px; POSITION:absolute; TOP:132px; HEIGHT:49px"><strong><FONT color="#66ccff"></FONT></strong></div>
    </form>
</body>
</html>
