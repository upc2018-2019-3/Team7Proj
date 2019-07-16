﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Client_AddNumber_Edit.aspx.cs" Inherits="Client_AddNumber_Edit" %>

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
                                                加分信息编辑</td>
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
                                                    加分信息</TD>
											</TR>
											<TR>
												<TD id="submenu1"><DIV class="sec_menu" style="WIDTH: 100%">
														<TABLE width="80%" cellPadding="0" cellSpacing="5" class="black_12">
															<TBODY>
                                                               <tr>
                                                                    <td align="right" width="15%">
                                                                        学员名称</td>
                                                                    <td width="35%">
                                                                        <asp:Label ID="lblPersonName" runat="server"></asp:Label></td>
                                                                    <td align="right" width="15%">
                                                                        </td>
                                                                    <td height="20" width="35%">
                                                                        <asp:HiddenField ID="hidAddNumberID" runat="server" />
                                                                        </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" width="15%">
                                                                        所属类别</td>
                                                                    <td width="35%">
                                                                        <asp:DropDownList ID="lstGuideID" runat="server" Enabled="False">
                                                                        </asp:DropDownList></td>
                                                                    <td align="right" width="15%">
                                                                        </td>
                                                                    <td height="20" width="35%">
                                                                        </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" width="15%">
                                                                        奖助学金名称</td>
                                                                    <td colspan="3" height="20">
                                                                        <asp:TextBox ID="txtTitle" runat="server" CssClass="box" MaxLength="50" Width="90%" Enabled="False"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" width="15%">
                                                                        内容</td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtContent" runat="server" Rows="5" TextMode="MultiLine" Width="80%" Enabled="False"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" width="15%">
                                                                        追加评语</td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtAddInt" runat="server"></asp:TextBox>
                                                                        <asp:CheckBox ID="chkIsAudi" runat="server" Text="通过审核" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" width="15%">
                                                                    </td>
                                                                    <td colspan="3">
                                                                    </td>
                                                                </tr>
															</TBODY>
														</TABLE>
													</DIV>
													<DIV style="WIDTH: 100%">
                                                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTitle"
                                                            Display="None" ErrorMessage="标题不能为空"></asp:RequiredFieldValidator>&nbsp;
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
													<asp:Button id="btnOK" runat="server" CssClass="box" Text="提 交" onclick="btnOK_Click"></asp:Button>&nbsp;&nbsp;&nbsp; &nbsp;
                                                <asp:Button ID="btnReturn" runat="server" CssClass="box" OnClick="btnReturn_Click"
                                                    Text="返 回" /></div>
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
