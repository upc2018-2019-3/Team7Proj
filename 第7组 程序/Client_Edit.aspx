<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Client_Edit.aspx.cs" Inherits="Client_Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<link href="images/aiv367css.css" rel="stylesheet" type="text/css">
	<SCRIPT language="javascript1.2">
    <!--
    function showsubmenu(sid){
	    whichEl = eval('submenu' + sid);
	    if (whichEl.style.display == 'none'){
		    eval("submenu" + sid + ".style.display='';");
	    }
	    else{
		    eval("submenu" + sid + ".style.display='none';");
	    }
    }
    //-->
	</SCRIPT>
	<script language="javascript" src="images/calendar.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    			<table width="100%" border="0" cellspacing="1" bgcolor="#666666">
				<tr>
					<td bgcolor="#e6e6e6"><table width="100%" border="1" cellspacing="0">
							<tr>
								<td valign="bottom" background="images/add-bg.gif" class="black_12">
									<table width="80%" border="0" cellspacing="1">
										<tr>
											<td width="2"><img src="images/dot3.gif" width="7" height="14" align="absMiddle">
											</td>
											<td class="black_12">
                                                普通教师信息编辑</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td height="118" valign="top">
						<table width="100%" border="1" align="center" cellpadding="0" cellspacing="0">
							<tr>
								<td valign="top">
									<TABLE width="100%" cellPadding="0" cellSpacing="0" class="black_12">
										<TBODY>
											<TR>
												<TD class="menu_title" id="menuTitle1" onMouseOver="this.className='menu_title1'" onClick="showsubmenu(1)"
													onMouseOut="this.className='menu_title'" background="images/capbtn.gif" height="25">&nbsp;&nbsp;<img src="images/arrow3.gif" width="10" height="11" align="absMiddle">
                                                    普通教师信息</TD>
											</TR>
											<TR>
												<TD id="submenu1"><DIV class="sec_menu" style="WIDTH: 100%">
														<TABLE width="80%" cellPadding="0" cellSpacing="5" class="black_12">
															<TBODY>
                                                                <tr>
                                                                    <td align="right" width="15%">
                                                                        普通教师编号</td>
                                                                    <td width="35%">
                                                                        <asp:TextBox ID="txtClientID" runat="server" CssClass="box" MaxLength="50" Width="90%"></asp:TextBox></td>
                                                                    <td align="right" width="15%">
                                                                        </td>
                                                                    <td height="20" width="35%">
                                                                        </td>
                                                                </tr>
																<TR>
																	<TD width="15%" align="right">
                                                                        真实姓名</TD>
																	<TD width="35%">
                                                                        <asp:TextBox ID="txtClientName" runat="server" CssClass="box" MaxLength="50" Width="90%"></asp:TextBox></TD>
																	<TD width="15%" align="right">
                                                                        登录密码</TD>
																	<TD width="35%" height="20">
                                                                        <asp:TextBox ID="txtPassword" runat="server" CssClass="box" MaxLength="10" Width="90%" TextMode="Password"></asp:TextBox></TD>
																</TR>
                                                                <tr>
                                                                    <td align="right" width="15%">
                                                                        性别</td>
                                                                    <td width="35%">
                                                                        <asp:DropDownList ID="lstSex" runat="server">
                                                                            <asp:ListItem>男</asp:ListItem>
                                                                            <asp:ListItem>女</asp:ListItem>
                                                                        </asp:DropDownList></td>
                                                                    <td align="right" width="15%">
                                                                        </td>
                                                                    <td height="20" width="35%">
                                                                        </td>
                                                                </tr>
                                                              
                                                                <tr>
                                                                  
                                                                    <td align="right" width="15%">
                                                                        专业</td>
                                                                    <td height="20" width="35%">
                                                                        <asp:DropDownList ID="lstSpecID" runat="server">
                                                                        </asp:DropDownList></td>
                                                                </tr>
															</TBODY>
														</TABLE>
													</DIV>
													<DIV style="WIDTH: 100%">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtClientID"
                                                            Display="None" ErrorMessage="普通教师编号不能为空"></asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtClientName"
                                                            Display="None" ErrorMessage="真实姓名不能为空"></asp:RequiredFieldValidator>&nbsp;
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                                            ShowSummary="False" />
                                                        &nbsp;</DIV>
												</TD>
											</TR>
										</TBODY>
									</TABLE>
									<table width="100%" border="0" cellspacing="1" cellpadding="0">
										<tr>
											<td><div align="center">
													<br>
													&nbsp;
													<asp:Button id="btnOK" runat="server" CssClass="box" Text="提 交" onclick="btnOK_Click"></asp:Button>&nbsp;&nbsp;&nbsp; <input name="Submit" type="reset" class="box" value="重 置">
												</div>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
    </div>
    </form>
</body>
</html>
