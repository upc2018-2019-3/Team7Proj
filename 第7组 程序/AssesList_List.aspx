<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssesList_List.aspx.cs" Inherits="AssesList_List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
	<link href="images/aiv367css.css" rel="stylesheet" type="text/css">
</head>
<body>
    <form id="form1" runat="server">
    <div>
    			<table width="100%" border="0" cellspacing="1" bgcolor="#666666">
				<tr>
					<td bgcolor="#e6e6e6"><table width="100%" border="1" cellspacing="0">
							<tr>
								<td valign="bottom" class="black_12">
									<table width="80%" border="0" cellspacing="1">
										<tr>
											<td width="2"><img src="images/dot3.gif" width="7" height="14" align="absMiddle">
											</td>
											<td class="black_12">
                                                评定信息编辑</td>
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
					<td height="236" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#b7b7ce" class="black_12">
							<tr>
								<td height="30" bgcolor="#ffffff"><table width="100%" border="0" cellspacing="0" cellpadding="0">
										<tr>
											<td class="black_12" style="width: 165px">&nbsp;&nbsp;<img src="images/news.png" width="25" height="23" align="absMiddle">
                                                评定列表
                                                <asp:Label ID="lblNumber" runat="server" ForeColor="Red"></asp:Label></td>
											<td align="right">
                                                <asp:HiddenField ID="hidGuideID" runat="server" />
                                                <asp:HiddenField ID="hidPersonID" runat="server" />
												<asp:Button id="btnSearch" runat="server" CssClass="box" Text="返 回" DESIGNTIMEDRAGDROP="31" onclick="btnSearch_Click"></asp:Button></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td height="2" bgcolor="#666699"></td>
							</tr>
							<tr>
								<td bgcolor="#ffffff"><table width="100%" border="0" cellspacing="2">
										<tr>
											<td height="1" background="images/line2.gif"></td>
										</tr>
										<tr>
											<td height="1" background="images/line2.gif"></td>
										</tr>
										<tr>
											<td valign="top"><table width="100%" border="1" cellspacing="2" cellpadding="0">
													<tr>
														<td valign="top">
															<asp:DataGrid id="dgList" runat="server" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
																BackColor="White" CellPadding="4" Width="100%" AutoGenerateColumns="False" Font-Size="Small"
																PageSize="15" AllowPaging="True" OnDeleteCommand="dgList_DeleteCommand" OnPageIndexChanged="dgList_PageIndexChanged" OnItemDataBound="dgList_ItemDataBound" OnItemCommand="dgList_ItemCommand">
																<FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
																<SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
																<ItemStyle ForeColor="#330099" BackColor="White"></ItemStyle>
																<HeaderStyle ForeColor="Black" BackColor="#EBEBEB"></HeaderStyle>
																<Columns>
																	<asp:BoundColumn DataField="PersonID" HeaderText="学号">
																		<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                            Font-Underline="False" HorizontalAlign="Center" />
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="PersonName" HeaderText="姓名">
																		<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                            Font-Underline="False" HorizontalAlign="Center" />
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="Sex" HeaderText="性别">
																		<HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Center"></ItemStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="ClassInfoName" HeaderText="班级">
																		<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Center"></ItemStyle>
																	</asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="SpecName" HeaderText="专业">
                                                                        <HeaderStyle Width="10%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                            Font-Underline="False" HorizontalAlign="Center" />
                                                                    </asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="GuideName" HeaderText="大类">
                                                                        <HeaderStyle Width="10%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                            Font-Underline="False" HorizontalAlign="Center" />
                                                                    </asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="Guide2Name" HeaderText="小类">
                                                                        <HeaderStyle Width="10%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                            Font-Underline="False" HorizontalAlign="Center" />
                                                                    </asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="Scale1" HeaderText="分数">
                                                                        <HeaderStyle Width="10%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                            Font-Underline="False" HorizontalAlign="Center" />
                                                                    </asp:BoundColumn>
                                                                    <asp:TemplateColumn HeaderText="删除">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton runat="server" ID="btnDel" Text="删除" CommandName="btnDel" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"AssesID") %>'></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="10%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                            Font-Underline="False" HorizontalAlign="Center" />
                                                                    </asp:TemplateColumn>
																</Columns>
																<PagerStyle HorizontalAlign="Right" ForeColor="#330099" BackColor="#AEE7F7" Mode="NumericPages"></PagerStyle>
															</asp:DataGrid></td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td height="1" background="images/line2.gif"></td>
										</tr>
										<tr>
											<td height="1" background="images/line2.gif"></td>
										</tr>
										<tr>
											<td height="1" background="images/line2.gif"></td>
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
