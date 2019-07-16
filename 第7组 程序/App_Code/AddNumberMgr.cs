using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;

/// <summary>
/// 留言管理类
/// </summary>
public class AddNumberMgr
{
	public AddNumberMgr()
	{
        
	}

    #region　函数UpdateAddNumber | 更新AddNumber信息
    /// <summary>
    /// 更新AddNumber类信息
    /// </summary>
    /// <param name="addNumber">留言管理类参数</param>
    /// <returns>更新成功返回true 否则返回false</returns>
    public bool UpdateAddNumber(AddNumber addNumber)
    {
        string strSQL = "";
        SqlDataAdapter sa = null;
        SqlCommandBuilder builder = null;
        DataRow row = null;
        DataTable dt = null;
        bool returnValue = false;
        if (addNumber != null)
        {
            if (addNumber.AddNumberID == 0)
            {
                strSQL = "SELECT Top 0 * FROM Sys_AddNumber";
            }
            else
            {
                strSQL = "SELECT Top 1 * FROM Sys_AddNumber WHERE AddNumberID = " + addNumber.AddNumberID.ToString();
            }

            dt = CMMgr.GetDataTable(strSQL);

            if (dt.Rows.Count > 0)
            {
                row = dt.Rows[0];
            }
            else
            {
                row = dt.NewRow();
            }

            row["Title"] = addNumber.Title;
            row["Content"] = addNumber.Content;
            row["IsAudi"] = addNumber.IsAudi;
            row["PersonID"] = addNumber.Person.PersonID;
            row["ClientID"] = addNumber.Client.ClientID;
            row["Guide2ID"] = addNumber.Guide2.Guide2ID;           
            row["GuideID"] = addNumber.Guide.GuideID;
            row["AddInt"] = addNumber.AddInt;

            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(row);
            }

            SqlConnection conn = CMMgr.GetConnection();
            using (sa = new SqlDataAdapter("SELECT Top 0 * FROM Sys_AddNumber", conn))
            {
                try
                {
                    builder = new SqlCommandBuilder(sa);
                    sa.Update(dt);
                    returnValue = true;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }
        return returnValue;
    }
    #endregion

    #region　函数GetAddNumber | 获取AddNumber信息
    /// <summary>
    /// 获取一条AddNumber类信息
    /// </summary>
    /// <param name="AddNumberID">登录编号</param>
    /// <returns>一条AddNumber类记录</returns>
    public AddNumber GetAddNumber(string AddNumberID)
    {
        AddNumber addNumber = new AddNumber();
        Guide2Mgr g2Mgr = new Guide2Mgr();
        GuideMgr gMgr = new GuideMgr();
        PersonMgr pMgr = new PersonMgr();
        ClientMgr cMgr = new ClientMgr();

        string strSQL = "SELECT Top 1 * FROM Sys_AddNumber WHERE AddNumberID = " + AddNumberID;
        DataTable dt = CMMgr.GetDataTable(strSQL);
        if (dt.Rows.Count > 0)
        {
            DataRow row = dt.Rows[0];
            addNumber.AddNumberID = int.Parse(row["AddNumberID"].ToString());
            addNumber.Title = row["Title"].ToString();
            addNumber.Content = row["Content"].ToString();
            addNumber.IsAudi = row["IsAudi"].ToString();
            addNumber.Person = pMgr.GetPerson(row["PersonID"].ToString());
            addNumber.Client = cMgr.GetClient(row["ClientID"].ToString());
            addNumber.Guide2 = g2Mgr.GetGuide2(row["Guide2ID"].ToString());            
            addNumber.Guide = gMgr.GetGuide(row["GuideID"].ToString());
            addNumber.AddInt = int.Parse(row["AddInt"].ToString());

            return addNumber;
        }
        else
        {
            return addNumber;
        }
    }
    #endregion

    #region　函数DelAddNumber | 删除AddNumber信息
    /// <summary>
    /// 删除AddNumber类信息
    /// </summary>
    /// <param name="AddNumberID">登录编号参数</param>
    public void DelAddNumber(string AddNumberID)
    {
        string strSQL = "DELETE FROM Sys_AddNumber WHERE AddNumberID = " + AddNumberID;
        CMMgr.ExecuteNonQuery(strSQL);
    }

    public void DelAddNumber(int AddNumberID)
    {
        this.DelAddNumber(AddNumberID.ToString());
    }
    #endregion

    #region 函数 GetAddNumberList | 获取留言信息列表
    /// <summary>
    /// 函数 GetAddNumberList | 获取留言信息列表
    /// </summary>
    /// <returns>留言列表</returns>
    public DataTable GetAddNumberList(string PersonID,string ClientID,string GuideID,string Guide2ID)
    {
        string strSQL = @"SELECT 
                            *
                            ,(SELECT PersonName FROM Sys_Person WHERE PersonID = Sys_AddNumber.PersonID) AS PersonName 
                            ,(SELECT ClientName FROM Sys_Client WHERE ClientID = Sys_AddNumber.ClientID) AS ClientName
                            ,(SELECT Guide2Name FROM Sys_Guide2 WHERE Guide2ID = Sys_AddNumber.Guide2ID) AS Guide2Name 
                            ,(SELECT GuideName FROM Sys_Guide WHERE GuideID = Sys_AddNumber.GuideID) AS GuideName 
                          FROM 
                            Sys_AddNumber 
                          WHERE 1=1 ";
        if (PersonID != "")
        {
            strSQL += "AND PersonID = '" + PersonID + "' ";
        }
        if (ClientID != "")
        {
            strSQL += "AND ClientID = '" + ClientID + "' ";
        }
        if (GuideID != "")
        {
            strSQL += "AND GuideID = '" + GuideID + "' ";
        }
        if (Guide2ID != "")
        {
            strSQL += "AND Guide2ID = '" + Guide2ID + "' ";
        }

        strSQL += "Order By AddNumberID Desc";
        return CMMgr.GetDataTable(strSQL);
    }

    #endregion

}
